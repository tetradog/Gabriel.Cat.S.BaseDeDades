using System;
using System.Collections.Generic;
using System.Text;

namespace Gabriel.Cat.S.BaseDeDades
{
    [AttributeUsage(AttributeTargets.Property,AllowMultiple =false)]
    /// <summary>
    /// Son los valores por defecto de un objeto
    /// </summary>
    public class DefaultSQL:Constraint
    {
        /// <summary>
        /// Forman parte del objeto por defecto
        /// </summary>
        public string Value { get; private set; }
        public bool EsUnaFuncion { get; private set; }
        public DefaultSQL( object valorPorDefectoOFuncion,bool esUnaFuncion=false):base("Default")
        {
            Value = valorPorDefectoOFuncion.ToString();
            EsUnaFuncion = esUnaFuncion;
        }
        public override string ToString()
        {
            return Value;
        }
    }
}
