using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gabriel.Cat.S.BaseDeDades;
using Gabriel.Cat.S.Utilitats;

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
        public static string CreateTableSQL(this Type obj, BaseDeDatos bd)
        {
            StringBuilder strCreate = new StringBuilder("create or replace table ");
            List<System.Reflection.PropertyInfo> columnsObj = obj.GetProperties().Filtra((p) => p.CanRead && p.CanWrite && p.GetAttributes().Filtra((atr) => atr is IgnoreSQL).Count == 0);
            PropiedadTipo propiedad;
            NameSQL nameSql;
            IList<Attribute> attributes;
            bool containsPrimaryKey = false;
            strCreate.Append(obj.GetNameSQL());
            strCreate.Append('(');
            //columns
            //name typeSQL,
            for (int i = 0; i < columnsObj.Count; i++)
            {
                propiedad = new PropiedadTipo(columnsObj[i]);
                nameSql = propiedad.Atributos.Filtra((atr) => atr is NameSQL).FirstOrDefault() as NameSQL;
                if (!containsPrimaryKey && propiedad.Atributos.Filtra((atr) => atr is PrimaryKeySQL).FirstOrDefault() as PrimaryKeySQL != null)
                    containsPrimaryKey = true;

                if (nameSql != null)
                {
                    strCreate.Append(nameSql.ToString());
                }
                else strCreate.Append(propiedad.Nombre);
                strCreate.Append(' ');
                strCreate.Append(bd.GetDeclaracionType(propiedad.Tipo, propiedad.Atributos.Filtra((atr) => atr is LimiteCampoSQL).FirstOrDefault() as LimiteCampoSQL));//si necesito más atributos los paso por aqui

                strCreate.Append(',');
            }
            if (!containsPrimaryKey)
            {
                //se le tiene que poner una columna IdAutoIncrement
                strCreate.Append("Id");
                strCreate.Append(' ');
                strCreate.Append(bd.GetAutoIncrementColumn());
                strCreate.Append(',');
            }

            for (int i = 0; i < columnsObj.Count; i++)
            {
                attributes = new PropiedadTipo(columnsObj[i]).Atributos;
                for (int j = 0; j < attributes.Count; j++)
                {
                    //constrains
                    //falta hacer

                }
            }
            if (strCreate[strCreate.Length - 1] == ',')
                strCreate.Remove(strCreate.Length - 1, 1);
            strCreate.Append(')');
            return strCreate.ToString();
        }
    }
}
