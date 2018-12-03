using System;
using System.Collections.Generic;
using System.Text;

namespace Gabriel.Cat.S.BaseDeDades
{
    public abstract class BaseDeDatos
    {
        public abstract string GetSQLType(Type typeCSharp, LimiteCampoSQL limiteCampoSQL=null);
        public abstract bool GetAutoIncrementColumn();
    }
}
