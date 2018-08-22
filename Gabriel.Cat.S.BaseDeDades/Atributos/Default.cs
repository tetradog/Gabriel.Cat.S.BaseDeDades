using System;
using System.Collections.Generic;
using System.Text;

namespace Gabriel.Cat.S.BaseDeDades
{//creo que solo pueden ser valores simples...por lo que veo son valores basicos y funciones
    [AttributeUsage(AttributeTargets.Property)]
    /// <summary>
    /// Son los valores por defecto de un objeto
    /// </summary>
    public class DefaultSQL:System.Attribute
    {
        /// <summary>
        /// Forman parte del objeto por defecto
        /// </summary>
        public string DefaultValue { get; private set; }
        public bool EsUnaFuncion { get; private set; }
        public DefaultSQL( object valorPorDefecto,bool esUnaFuncion=false)
        {
            DefaultValue = valorPorDefecto.ToString();
            EsUnaFuncion = esUnaFuncion;
        }
    }
}
