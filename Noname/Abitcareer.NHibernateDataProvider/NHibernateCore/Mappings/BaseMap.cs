using Abitcareer.Business.Models;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abitcareer.NHibernateDataProvider.NHibernateCore.Mappings
{
    public class BaseMap<TEntity> : ClassMap<TEntity>
        where TEntity : BaseModel
    {
        public BaseMap()
        {
            Id(x => x.Id);

            Map(x => x.Name).Not.Nullable().Length(300);

            Map(x => x.Xml);
        }
    }
}
