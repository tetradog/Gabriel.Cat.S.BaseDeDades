using System;
using System.Collections.Generic;
using System.Text;

namespace Gabriel.Cat.S.BaseDeDades
{
    public abstract class BaseDeDatos
    {
        public abstract string GetDeclaracionType(Type typeCSharp, LimiteCampoSQL limiteCampoSQL=null);
        public abstract string GetAutoIncrementColumnDeclaration();//poner otro nombre más descriptivo     
    }
}
