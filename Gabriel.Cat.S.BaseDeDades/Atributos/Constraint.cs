using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Gabriel.Cat.S.Extension;
namespace Gabriel.Cat.S.BaseDeDades
{
    public abstract class Constraint:System.Attribute
    {
        protected string prefix;
        protected Constraint(string prefix)
        {
            this.prefix = prefix;
        }
        public string GetConstrainName(Type tipo,PropertyInfo property)
        {
            return GetConstrainName(tipo.GetNameSQL(),property.Name);
        }
        public string GetConstrainName(Type tipo, string nameProperty)
        {
            return GetConstrainName(tipo.GetNameSQL(), nameProperty);
        }
        public string GetConstrainName(object tipo, string nameProperty)
        {
            return GetConstrainName(tipo.GetNameSQL(), nameProperty);
        }
        public virtual string GetConstrainName(string tableName, string nameProperty)
        {
            StringBuilder str = new StringBuilder(prefix);
            str.Append('_');
            str.Append(tableName);
            str.Append('_');
            str.Append(nameProperty);
            return str.ToString();
        }
    }
}
