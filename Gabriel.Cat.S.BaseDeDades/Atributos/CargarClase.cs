using System;
using System.Collections.Generic;
using System.Text;

namespace Gabriel.Cat.S.BaseDeDades
{
   
    [System.AttributeUsage(System.AttributeTargets.Class)]
    public class CargarClaseSQL:System.Attribute
    {
        public enum Carga
        {
            Id,
            /// <summary>
            /// Se cargaran las propiedades que esten marcadas, si se libera memoria ram las que estan marcadas se omitiran.
            /// </summary>
            Minima,
            Toda
        }
        public CargarClaseSQL(Carga carga)
        {
            this.Valor = carga;
        }
        public Carga Valor { get; private set; }


    }
}
