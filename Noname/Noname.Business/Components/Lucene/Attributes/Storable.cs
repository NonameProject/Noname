using Lucene.Net.Documents;
using System;

namespace Abitcareer.Business.Components.Lucene.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class Storable
        : Attribute
    {
        public Field.Index Type = Field.Index.NO;
    }
}