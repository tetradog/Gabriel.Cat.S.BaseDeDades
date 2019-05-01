using System;
using System.Collections.Generic;
using System.Text;

namespace Gabriel.Cat.S.BaseDeDades
{
    [AttributeUsage(AttributeTargets.Property)]
    public class Check: ConstraintWithId
    {
        //puede haber más de una...como las identifico?
        public string Condicion { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="condicion"></param>
        /// <param name="idCheck">Si hay más de una hay que poner una forma de identificarla, por cierto se usa el ToString() del objeto pasado como parametro </param>
        public Check(string condicion,object idCheck=null):base("Check",idCheck)
        {
            Condicion = condicion;
        }

        public override string ToString()
        {
            return Condicion;
        }
    }
}
