using System;
using System.Collections.Generic;
using System.Text;

namespace Gabriel.Cat.S.BaseDeDades
{
    [System.AttributeUsage(System.AttributeTargets.Class | System.AttributeTargets.Property)]
    /// <summary>
    /// Nombres de tablas y campos
    /// </summary>
    /// 
    public class Name:System.Attribute
    {

        string name;
        public Name(string name)
        {
            this.name = name;
        }
        public override string ToString()
        {
            return name;
        }
    }
}
