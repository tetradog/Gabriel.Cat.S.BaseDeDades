using System;
using System.Collections.Generic;
using System.Text;

namespace Gabriel.Cat.S.BaseDeDades
{//creo que solo pueden ser valores simples...por lo que veo son valores basicos y funciones
    /// <summary>
    /// Son los valores por defecto de un objeto
    /// </summary>
    public class DefaultSQL:System.Attribute
    {
        /// <summary>
        /// Forman parte del objeto por defecto
        /// </summary>
        public string[] ParametrosPorDefecto { get; private set; }
        public DefaultSQL(params object[] parametrosPorDefecto)
        {
            ParametrosPorDefecto = new string[parametrosPorDefecto.Length];
            for (int i = 0; i < parametrosPorDefecto.Length; i++)
                ParametrosPorDefecto[i] = parametrosPorDefecto[i].ToString();
        }
    }
}
