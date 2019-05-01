using System;
using System.Collections.Generic;

namespace Gabriel.Cat.S.BaseDeDades
{
    // tiene que ser unica la columna osea si no tiene el atributo Unique/PrimaryKey lanzo error
    [AttributeUsage(AttributeTargets.Property)]
    public class Index:ConstraintWithId
    {

        public Index(object idIndex=null) : base("Index",idIndex) {
        

        }

    }
}
