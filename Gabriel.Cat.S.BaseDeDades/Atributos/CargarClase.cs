using System;
using System.Collections.Generic;
using System.Text;

namespace Gabriel.Cat.S.BaseDeDades
{
    [System.AttributeUsage(System.AttributeTargets.Class)]
    public class CargarClaseSQL:System.Attribute
    {
        public enum CargaInicial
        {
            Id,
            /// <summary>
            /// Se cargaran las propiedades que esten marcadas
            /// </summary>
            Minima,
            Toda
        }
        public CargarClaseSQL(CargaInicial carga)
        {
            this.Carga = carga;
        }
        public CargaInicial Carga { get; private set; }
    }
}
