using System;
using System.Collections.Generic;
using System.Text;

namespace Gabriel.Cat.S.BaseDeDades
{
    //si hay más es que es compuesta
    [System.AttributeUsage(System.AttributeTargets.Property,AllowMultiple =false)]
    public class PrimaryKeySQL:Constraint
    {
        public PrimaryKeySQL() : base("PrimaryKey") { }
    }
}
