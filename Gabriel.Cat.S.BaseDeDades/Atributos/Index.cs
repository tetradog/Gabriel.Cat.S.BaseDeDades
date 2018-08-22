using System;
using System.Collections.Generic;
using System.Text;

namespace Gabriel.Cat.S.BaseDeDades.Atributos
{
    [AttributeUsage(AttributeTargets.Property)]
    public class Index:Constraint
    {
        public Index() : base("Index") { }
    }
}
