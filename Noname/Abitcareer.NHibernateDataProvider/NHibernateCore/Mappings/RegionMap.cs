﻿using Abitcareer.Business.Models;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abitcareer.NHibernateDataProvider.NHibernateCore.Mappings
{
    public class RegionMap : BaseMap<Region>
    {
        public RegionMap()
            : base()
        {
            HasMany(x => x.Cities).Inverse();
        }
    }
}
