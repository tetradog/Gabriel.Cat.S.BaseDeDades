using System.Collections.Generic;
using System.Text;
using System.Linq;
using Gabriel.Cat.S.BaseDeDades;
using Gabriel.Cat.S.Utilitats;
using System;

namespace Gabriel.Cat.S.Extension
{
    public static class ObjectExtension
    {
        public static string GetNameSQL(this object obj)
        {
            return TypeExtension.GetNameSQL(obj.GetType());

        }

        public static List<KeyValuePair<string,object>> GetPropertiesNameValuePair(this object obj)
        {
            List<Utilitats.Propiedad> propiedades=obj.GetPropiedades();
            List<KeyValuePair<string, object>> propiedadesSQL = new List<KeyValuePair<string, object>>();
            NameSQL nameSQL;
            string name;
            //ordeno
            List<OrderSQL> ordenSQLs = new List<OrderSQL>();
            SortedList<OrderSQL,Utilitats.Propiedad> propiedadesConOrden=new SortedList<OrderSQL, Utilitats.Propiedad>();
            OrderSQL orden;
            for (int i=propiedades.Count-1;i>=0;i--)
            {
                orden = propiedades[i].Info.Atributos.Filtra((atr) => atr is OrderSQL).FirstOrDefault() as OrderSQL;
                if(orden!=null)
                {
                    ordenSQLs.Add(orden);
                    propiedadesConOrden.Add(orden,propiedades[i]);
                    propiedades.RemoveAt(i);
                }
            }
            ordenSQLs.Sort();
            for(int i= ordenSQLs.Count-1; i>=0;i--)
            {
                propiedades.Insert(0, propiedadesConOrden[ordenSQLs[i]]);

            }
            //pongo las propiedades 
            for(int i=0;i<propiedades.Count;i++)
            {
                //miro si tiene el atributo ignore
                if (PropiedadSerializable(propiedades[i])&&propiedades[i].Info.Atributos.OfType<IgnoreSQL>().Count() == 0)
                {
                    //miro si tiene el atributo name
                    nameSQL =propiedades[i].Info.Atributos.Filtra((atr)=>atr is NameSQL).FirstOrDefault() as NameSQL;
                    if (nameSQL != null)
                        name = nameSQL.ToString();
                    else name = propiedades[i].Info.Nombre;
                    propiedadesSQL.Add(new KeyValuePair<string, object>(name, propiedades[i].Objeto));
                }
            }

            return propiedadesSQL;
        }
       
        private static bool PropiedadSerializable(Propiedad propiedad)
        {
            bool esSerializable = propiedad.Info.Uso==(UsoPropiedad.Get|UsoPropiedad.Set);
            if (!esSerializable && propiedad.Info.Tipo.ImplementInterficie(typeof(IList<>)))
                esSerializable = true;

            
            return esSerializable;
        }

        public static string CreateTableSQL(this object obj,BaseDeDatos bd)
        {
            return TypeExtension.CreateTableSQL(obj.GetType(), bd);
        }
    }
}
