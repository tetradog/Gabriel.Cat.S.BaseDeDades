using System;
using System.Collections.Generic;
using System.Text;

namespace Gabriel.Cat.S.BaseDeDades
{
    [AttributeUsage(AttributeTargets.Class|AttributeTargets.Property)]
    public class Check:Constraint
    {
        //puede haber más de una...como las identifico?
        public string IdCheck { get; private set; }
        public string Condicion { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="condicion"></param>
        /// <param name="idCheck">Si hay más de una hay que poner una forma de identificarla, por cierto se usa el ToString() del objeto pasado como parametro </param>
        public Check(string condicion,object idCheck=null):base("Check")
        {
            Condicion = condicion;
            IdCheck = idCheck!=null?idCheck.ToString():"";
        }
        public override string GetConstrainName(string tableName, string nameProperty)
        {
            StringBuilder str=new StringBuilder( base.GetConstrainName(tableName, nameProperty));
            if (IdCheck != "")
            {
                str.Append('_');
                str.Append(IdCheck);
            }
            return str.ToString();
        }
        public override string ToString()
        {
            return Condicion;
        }
    }
}
