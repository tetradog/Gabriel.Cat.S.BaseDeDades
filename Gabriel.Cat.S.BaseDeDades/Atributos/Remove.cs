using System;
using System.Collections.Generic;
using System.Text;

namespace Gabriel.Cat.S.BaseDeDades
{

    [AttributeUsage(System.AttributeTargets.Property)]
    public class RemoveSQL:System.Attribute
    {
        public enum Option
        {
            Null,
            Default,
            Delete
        }

        public Option OptionOnDelete { get; private set; }

        public RemoveSQL(Option optionOnDelete=Option.Null)
        {
            OptionOnDelete = optionOnDelete;
        }
        public override string ToString()
        {
            return OptionOnDelete.ToString();
        }
    }
}
