using Lucene.Net.Analysis.Standard;
using Lucene.Net.Index;
using Lucene.Net.QueryParsers;
using Lucene.Net.Search;
using Lucene.Net.Store;
using System;
using System.IO;
using Version = Lucene.Net.Util.Version;

namespace Abitcareer.Business.Components.Lucene
{
    public abstract class MyBaseSearcher
    {
        #region constants
        protected const int HITS_LIMIT = 1000;
        protected const string DEFAULT_DIR = "lucene_index";
        #endregion

        #region private protected
        protected string path;

        protected FSDirectory _dir;

        protected FSDirectory directory
        {
            get
            {
                if (IndexWriter.IsLocked(_dir))
                {
                    IndexWriter.Unlock(_dir);
                }
                var lockFilePath = Path.Combine(path, "write.lock");
                if (File.Exists(lockFilePath))
                {
                    File.Delete(lockFilePath);
                }
                return _dir;
            }
            set
            {
                _dir = value;
            }
        }
        #endregion

        #region constructors
        public MyBaseSearcher( )
            : this(Path.Combine("./", DEFAULT_DIR))
        {
        }

        public MyBaseSearcher(string directoryPath)
        {
            path = directoryPath;
            directory = FSDirectory.Open(new DirectoryInfo(path));
        }
        #endregion

        /// <summary>
        /// Clear all index
        /// </summary>
        /// <returns>true if sucsess, false if directory was locked</returns>
        public bool ClearIndex( )
        {
            try
            {
                var analyzer = new StandardAnalyzer(Version.LUCENE_30);
                using (var writer = new IndexWriter(directory, analyzer, true, IndexWriter.MaxFieldLength.UNLIMITED))
                {
                    writer.DeleteAll();

                    analyzer.Close();
                }
            } catch
            {
                return false;
            }
            return true;
        }

        public void Optimize( )
        {
            var analyzer = new StandardAnalyzer(Version.LUCENE_30);
            using (var writer = new IndexWriter(directory, analyzer, IndexWriter.MaxFieldLength.UNLIMITED))
            {
                analyzer.Close();
                writer.Optimize();
            }
        }

        protected Query parseQuery(string searchQuery, QueryParser parser)
        {
            Query query;
            try
            {
                query = parser.Parse(searchQuery.Trim());
            } catch (ParseException)
            {
                query = parser.Parse(QueryParser.Escape(searchQuery.Trim()));
            }
            return query;
        }
    }
}