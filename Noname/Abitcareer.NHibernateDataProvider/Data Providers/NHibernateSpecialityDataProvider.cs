using Abitcareer.Business.Data_Providers_Contracts;
using Abitcareer.Business.Models;
using NHibernate.Criterion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abitcareer.NHibernateDataProvider.Data_Providers
{
    public class NHibernateSpecialityDataProvider : NHibernateDataProviderBase<Speciality>, ISpecialityDataProvider
    {
        public Speciality GetByName(string name)
        {
            return Execute(session =>
            {
                return session
                    .CreateCriteria(typeof(Speciality))
                    .Add(Restrictions.Eq("Name", name))
                    .UniqueResult<Speciality>();
            });
        }

        public void ClearSalaries( )
        {
            var list = GetList().AsParallel();
            foreach(var spec in list){
                for (int i = 0; i < spec.Salaries.Keys.Count; i++ )
                    spec.Salaries[spec.Salaries.Keys.ElementAt(i)] = 0;
                Update(spec);
            }
        }
    }
}
