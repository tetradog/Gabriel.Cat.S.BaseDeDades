using System;
using System.Collections.Generic;
using System.Text;

namespace Gabriel.Cat.S.BaseDeDades
{
    
    /// <summary>
    /// Hace que se salte la propiedad
    /// </summary>
    [System.AttributeUsage(System.AttributeTargets.Property)]
    public class IgnoreSQL:System.Attribute
    {
 
    }
}
