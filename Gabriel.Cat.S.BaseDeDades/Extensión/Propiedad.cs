using Gabriel.Cat.S.Extension;
using Gabriel.Cat.S.Utilitats;
using System.Collections.Generic;
using System.Linq;

namespace Gabriel.Cat.S.BaseDeDades
{
	public static class Propiedad
    {
		public static string GetNameSQL(this Utilitats.Propiedad propiedad)
        {
            string name;
            NameSQL nameSQL = propiedad.Info.Atributos.Filtra((atr) => atr is NameSQL).FirstOrDefault() as NameSQL;
            if (nameSQL != null)
                name = nameSQL.ToString();
            else name = propiedad.Info.Nombre;
            return name;
        }
        private static bool PropiedadSerializable(this Utilitats.Propiedad propiedad)
        {
            bool esSerializable = propiedad.Info.Uso == (UsoPropiedad.Get | UsoPropiedad.Set);
            if (!esSerializable && propiedad.Info.Tipo.ImplementInterficie(typeof(IList<>)))
                esSerializable = true;


            return esSerializable;
        }
        public static bool IsPropertySQL(this Utilitats.Propiedad propiedad)
        {
            return PropiedadSerializable(propiedad) && propiedad.Info.Atributos.OfType<IgnoreSQL>().Count() == 0;
        }
        public static OrderSQL TryGetOrder(this Utilitats.Propiedad propiedad)
        {
            return propiedad.Info.Atributos.Filtra((atr) => atr is OrderSQL).FirstOrDefault() as OrderSQL;
        }
        public static string[] GetForeignKeySQL(this PropiedadTipo propiedad)
        {
            string[] primaryKeysTipoReference = propiedad.Tipo.GetPrimaryKeyColumnsSQL();
            string[] foreignKey = new string[primaryKeysTipoReference.Length];
            string nameSQL = propiedad.Tipo.GetNameSQL();
            for (int i = 0; i < primaryKeysTipoReference.Length; i++)
                foreignKey[i] = nameSQL + primaryKeysTipoReference[i];
            return foreignKey;
        }
    }
}