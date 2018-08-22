using System;
using System.Collections.Generic;
using System.Text;

namespace Gabriel.Cat.S.BaseDeDades
{
    [AttributeUsage(AttributeTargets.Property,AllowMultiple =false)]
    public class UniqueSQL:Constraint
    {
        public UniqueSQL() : base("Unique") { }
    }
}
