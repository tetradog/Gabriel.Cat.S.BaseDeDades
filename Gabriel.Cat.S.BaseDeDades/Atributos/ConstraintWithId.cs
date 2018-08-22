using System.Text;

namespace Gabriel.Cat.S.BaseDeDades
{
    public abstract class ConstraintWithId:Constraint
    {
        public string Id { get; private set; }
        public ConstraintWithId(string prefix,object idIndex) : base(prefix)
        {
            Id = idIndex != null ? idIndex.ToString() : "";

        }
        public override string GetConstrainName(string tableName, string nameProperty)
        {
            StringBuilder str = new StringBuilder(base.GetConstrainName(tableName, nameProperty));
            if (Id != "")
            {
                str.Append('_');
                str.Append(Id);
            }
            return str.ToString();
        }
    }
}
