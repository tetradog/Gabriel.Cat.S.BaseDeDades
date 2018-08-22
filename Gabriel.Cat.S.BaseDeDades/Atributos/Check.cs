using System;
using System.Collections.Generic;
using System.Text;

namespace Gabriel.Cat.S.BaseDeDades
{
    [AttributeUsage(AttributeTargets.Class)]
    public class Check:System.Attribute
    {

        public string Condicion { get; private set; }
        public Check(string condicion)
        {
            Condicion = condicion;
        }
        public override string ToString()
        {
            return Condicion;
        }
    }
}
