namespace Gabriel.Cat.S.BaseDeDades
{
    [System.AttributeUsage(System.AttributeTargets.Property)]
    public class LimiteCampoSQL:System.Attribute
    {
        public LimiteCampoSQL(params int[] limites)
        {
            Limites = limites;
        }
        public int[] Limites { get; private set; }
    }
}