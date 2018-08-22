using System;
using System.Collections.Generic;
using System.Text;

namespace Gabriel.Cat.S.BaseDeDades
{
    /// <summary>
    /// Es la clase para los objetos sin PrimaryKey definida
    /// </summary>
    public class ObjIdAuto
    {
        public string Id { get; private set; }
        public Object Object { get; private set; }
        public ObjIdAuto(string id,object obj)
        {
            Id = id;
            Object = obj;
        }
    }
}
