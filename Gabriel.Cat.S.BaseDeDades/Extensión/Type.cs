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

            List<System.Reflection.PropertyInfo> columnsObjForeignKey;
            PropiedadTipo propiedad;
            PropiedadTipo propiedadReference;
            string[] foreignKeys;
            IList<Attribute> attributes;
            bool containsPrimaryKey = false;
            Constraint constraint;
            int endName;
            strCreate.Append(obj.GetNameSQL());
            strCreate.Append('(');
            //columns
            //name typeSQL,
            for (int i = 0; i < columnsObj.Count; i++)
            {
                propiedad = new PropiedadTipo(columnsObj[i]);
                if (propiedad.Tipo.IsSimpleType())
                {
                    if (!containsPrimaryKey && propiedad.Atributos.Filtra((atr) => atr is PrimaryKeySQL).FirstOrDefault() as PrimaryKeySQL != null)
                        containsPrimaryKey = true;
                    strCreate.Append(propiedad.GetNameSQL());
                    strCreate.Append(' ');
                    strCreate.Append(bd.GetDeclaracionType(propiedad.Tipo, propiedad.Atributos.Filtra((atr) => atr is LimiteCampoSQL).FirstOrDefault() as LimiteCampoSQL));

                    strCreate.Append(',');
                }
            }
            //si tiene una foreignkey compuesta necesito hacer esto...
            for (int i = 0; i < columnsObj.Count; i++)
            {
                propiedad = new PropiedadTipo(columnsObj[i]);
                if (!propiedad.Tipo.IsSimpleType())
                {
                    endName = propiedad.GetNameSQL().Length;
                    foreignKeys= propiedad.GetForeignKeySQL();
                   
                    if (!containsPrimaryKey && propiedad.Atributos.Filtra((atr) => atr is PrimaryKeySQL).FirstOrDefault() as PrimaryKeySQL != null)
                        containsPrimaryKey = true;
                    for (int j = 0; j < foreignKeys.Length; i++)
                    {
                        strCreate.Append(foreignKeys[j]);
                        strCreate.Append(' ');
                        propiedadReference = propiedad.Tipo.GetColumnPropiedadTipo(foreignKeys[j].Substring(endName));//cojo la propiedad de la columna primaryKey del tipo a hacer referencia
                        strCreate.Append(bd.GetDeclaracionType(propiedadReference.Tipo, propiedadReference.Atributos.Filtra((atr) => atr is LimiteCampoSQL).FirstOrDefault() as LimiteCampoSQL));

                        strCreate.Append(',');
                    }
                }
            }
            if (!containsPrimaryKey)
            {
                //se le tiene que poner una columna IdAutoIncrement
                strCreate.Append("Id");
                strCreate.Append(' ');
                strCreate.Append(bd.GetAutoIncrementColumnDeclaration());
                strCreate.Append(',');
            }
            //añado las constrains de cada columna, le pongo un nombre único y calculable
            for (int i = 0; i < columnsObj.Count; i++)
            {
                attributes = new PropiedadTipo(columnsObj[i]).Atributos.Filtra((atr) => atr is Constraint && !(atr is PrimaryKeySQL));
                for (int j = 0; j < attributes.Count; j++)
                {
                    //constrains de la columna
                    constraint = attributes[j] as Constraint;
                    //falta hacer
                    strCreate.Append(constraint.GetDeclarationConstraintLine(obj.GetNameSQL(), new PropiedadTipo(columnsObj[i]).GetNameSQL()));
                    strCreate.Append(",");

                }
            }
            // la PrimaryKey que puede ser compuesta

            //ahora tengo todas las columnas de la tabla que harán de PrimaryKey
            strCreate.Append("Primary Key(");
            strCreate.Append(String.Join(",", obj.GetPrimaryKeyColumnsSQL()));
            if (strCreate[strCreate.Length - 1] == ',')
                strCreate.Remove(strCreate.Length - 1, 1);//quito la ','
            strCreate.Append("),");
            // los ForeignKey
            //las columnas que sean clases que no sean basicas
            columnsObjForeignKey = columnsObj.Filtra((column) => !(new PropiedadTipo(column).Tipo.IsSimpleType()));
            for (int i = 0; i < columnsObjForeignKey.Count; i++)
            {
                propiedad = new PropiedadTipo(columnsObjForeignKey[i]);
                strCreate.Append("Foreign Key(");
                strCreate.Append(String.Join(",", propiedad.GetForeignKeySQL()));//hacer extension para Property que coja el nombre del tipo
                if (strCreate[strCreate.Length - 1] == ',')
                    strCreate.Remove(strCreate.Length - 1, 1);//quito la ','
                strCreate.Append(") References ");
                strCreate.Append(propiedad.Tipo.GetNameSQL());
                strCreate.Append("(");
                strCreate.Append(String.Join(",", propiedad.Tipo.GetPrimaryKeyColumnsSQL()));
                if (strCreate[strCreate.Length - 1] == ',')
                    strCreate.Remove(strCreate.Length - 1, 1);//quito la ','
                strCreate.Append("),");

            }
            if (strCreate[strCreate.Length - 1] == ',')
                strCreate.Remove(strCreate.Length - 1, 1);
            strCreate.Append(");");
            return strCreate.ToString();
        }
        public static PropiedadTipo GetColumnPropiedadTipo(this Type type,string sqlNameColumn)
        {
            PropiedadTipo propiedad=type.GetPropiedadesTipos().Filtra((t) => t.GetNameSQL().Equals(sqlNameColumn)).FirstOrDefault();
            if (propiedad == null)
                throw new Exception("El tipo "+type.Name+" no contiene la columnaSQL "+sqlNameColumn);
            return propiedad;
        }
        public static string[] GetPrimaryKeyColumnsSQL(this Type type)
        {
            List<System.Reflection.PropertyInfo> columnsObjPrimaryKey = type.GetProperties().Filtra((column) => new PropiedadTipo(column).Atributos.Filtra((atr) => atr is PrimaryKeySQL).Count != 0);
            string[] columnsPrimaryKey = new string[columnsObjPrimaryKey.Count];
            //ahora tengo todas las columnas de la tabla que harán de PrimaryKey
            for (int i = 0; i < columnsObjPrimaryKey.Count; i++)
                columnsPrimaryKey[i] = new PropiedadTipo(columnsObjPrimaryKey[i]).GetNameSQL();

            if (columnsObjPrimaryKey.Count == 0)
                columnsPrimaryKey = new string[] { "Id" };

            return columnsPrimaryKey;
        }
        public static bool IsSimpleType(this Type type)
        {
            return Gabriel.Cat.S.Utilitats.Serializar.AsseblyQualifiedName.Contains(type.AssemblyQualifiedName);
        }
    }
}
