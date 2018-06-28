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
    public class NameSQL:System.Attribute
    {

        string name;
        public NameSQL(string name)
        {
            this.name = name;
        }
        public override string ToString()
        {
            return name;
        }
    }
}
