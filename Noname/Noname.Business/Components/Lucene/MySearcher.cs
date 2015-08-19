using Lucene.Net.Analysis.Standard;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.QueryParsers;
using Lucene.Net.Search;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using Abitcareer.Business.Components.Lucene.Attributes;
using Version = Lucene.Net.Util.Version;

namespace Abitcareer.Business.Components.Lucene
{
    public class MySearcher<T>
        : MyBaseSearcher
        where T : new()
    {
        private string[] indexFields;
        public MySearcher( )
            : base()
        {
            fillIndeFieldsList();
        }

        public MySearcher(string directoryPath)
            : base(directoryPath)
        {
            fillIndeFieldsList();
        }

        #region public methods
        public IEnumerable<T> Search(string input, string fieldName = "")
        {
            if (string.IsNullOrEmpty(input)) input = string.Format("Id:[0 TO {0}]", 999);
            else
            {
                var terms = input.Trim().Replace("-", " ").Replace("_", "-").Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(x => {
                        string t = x.Trim();
                        return string.Format("(({0}*)^2 {0}~0.6)", t);
                    });
                input = string.Join("AND", terms);
            }
            return search(input, fieldName);
        }

        public void AddUpdateIndex(IEnumerable<T> TList)
        {
            var analyzer = new StandardAnalyzer(Version.LUCENE_30);

            using (var writer = new IndexWriter(directory, analyzer, IndexWriter.MaxFieldLength.UNLIMITED))
            {
                foreach (var TValue in TList)
                {
                    addToLuceneIndex(TValue, writer);
                }
                writer.Flush(true, true, true);
            }
            analyzer.Close();
        }

        public void AddUpdateIndex(T TValue)
        {
            AddUpdateIndex(new List<T> { TValue });
        }

        #endregion

        #region private methods

        private void fillIndeFieldsList( )
        {
            List<string> list = new List<string>();
            foreach (PropertyInfo property in typeof(T).GetProperties())
            {
                var attr = property.GetCustomAttribute<Storable>();
                if (attr == null || attr.Type == Field.Index.NO) continue;
                list.Add(property.Name);
            }
            indexFields = list.ToArray();
        }

        private void addToLuceneIndex(T TValue, IndexWriter writer)
        {
            var doc = MapTValueToDoc(TValue);
            string id = doc.GetField("Id").StringValue;
            if (!string.IsNullOrEmpty(id))
            {
                var searchQuery = new TermQuery(new Term("Id", id));
                writer.UpdateDocument(searchQuery.Term, doc);
            }
            writer.AddDocument(doc);
        }

        private Document MapTValueToDoc(T TValue)
        {
            var doc = new Document();

            var properties = typeof(T).GetProperties();
            foreach (PropertyInfo property in properties)
            {
                var attr = property.GetCustomAttribute<Storable>();
                if (attr == null) continue;                
                string fieldName = property.Name;
                object fieldValue = property.GetValue(TValue);

                doc.Add(new Field(fieldName, fieldValue.ToString(), Field.Store.YES, attr.Type));
            }
            return doc;
        }

        private T MapDocToTValue(Document doc)
        {
            T res = new T();

            var properties = typeof(T).GetProperties();
            foreach (PropertyInfo property in properties)
            {
                var attr = property.GetCustomAttribute<Storable>();
                if (attr == null) continue;                
                string fieldName = property.Name;
                var val = TypeDescriptor.GetConverter(property.PropertyType)
                            .ConvertFrom(doc.Get(fieldName));
                property.SetValue(res, val);    
            }

            return res;
        }

        private IEnumerable<T> MapDocToTValue(IEnumerable<ScoreDoc> hits,
            IndexSearcher searcher)
        {
            return hits.Select(hit =>
                MapDocToTValue(searcher.Doc(hit.Doc)))
                .ToList();
        }

        private IEnumerable<T> search(string searchQuery, string searchField = "")
        {
            using (var searcher = new IndexSearcher(directory, false))
            {
                var analyzer = new StandardAnalyzer(Version.LUCENE_30);
                QueryParser parser;

                if (!string.IsNullOrEmpty(searchField))
                {
                    parser = new QueryParser(Version.LUCENE_30, searchField, analyzer);
                }
                else
                {
                    
                    parser = new MultiFieldQueryParser
                        (Version.LUCENE_30, indexFields, analyzer);
                }
                var query = parseQuery(searchQuery, parser);
                var hits = searcher.Search(query, HITS_LIMIT).ScoreDocs;
                var results = MapDocToTValue(hits, searcher);
                analyzer.Close();
                return results;
            }
        }
        #endregion
    }
}