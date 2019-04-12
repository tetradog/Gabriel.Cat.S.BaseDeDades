namespace Gabriel.Cat.S.BaseDeDades
{
    [System.AttributeUsage(System.AttributeTargets.Property)]
    public class LimiteCampoSQL:System.Attribute
    {
        public LimiteCampoSQL(int limite)
        {
            Limite = limite;
        }
        public int Limite { get; private set; }
    }
}