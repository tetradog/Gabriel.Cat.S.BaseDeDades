using System;
using System.Linq;
using Gabriel.Cat.S.BaseDeDades;

namespace Gabriel.Cat.S.Extension
{
    public static class TypeExtension
    {
        public static string GetNameSQL(this Type tipo)
        {
            string name;
            NameSQL nameSQL = tipo.CustomAttributes.OfType<NameSQL>().FirstOrDefault() as NameSQL;
            if (nameSQL != null)
                name = nameSQL.ToString();
            else name = tipo.Name;
            return name;
        }
    }
}
