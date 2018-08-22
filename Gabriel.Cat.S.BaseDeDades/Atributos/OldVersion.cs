using System;
using System.Collections.Generic;
using System.Text;

namespace Gabriel.Cat.S.BaseDeDades
{
    /// <summary>
    /// Se usa para decir que se ha actualizado el tipo y saber cual era antes.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class OldVersion:System.Attribute
    {

        public Type Base { get; set; }
        public OldVersion(Type baseType)
        {
            Base = baseType;
        }
        public override string ToString()
        {
            return Base.AssemblyQualifiedName;
        }
    }
}
