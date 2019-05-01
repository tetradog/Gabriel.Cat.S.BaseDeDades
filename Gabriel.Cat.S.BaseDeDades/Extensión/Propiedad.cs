using Gabriel.Cat.S.Utilitats;
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
    }
}