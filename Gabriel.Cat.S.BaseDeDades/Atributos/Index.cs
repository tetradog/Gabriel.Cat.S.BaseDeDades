using System;
using System.Collections.Generic;

namespace Gabriel.Cat.S.BaseDeDades
{
    [AttributeUsage(AttributeTargets.Property)]
    public class Index:ConstraintWithId
    {

        public Index(object idIndex=null) : base("Index",idIndex) {
        

        }

    }
}
