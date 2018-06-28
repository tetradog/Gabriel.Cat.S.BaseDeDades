using System;
using System.Collections.Generic;
using System.Text;
using Gabriel.Cat.S.Utilitats;
namespace Gabriel.Cat.S.BaseDeDades
{
    /// <summary>
    /// Define el orden de las propiedades
    /// </summary>
    [System.AttributeUsage(System.AttributeTargets.Property)]
    
    public class OrderSQL:System.Attribute,IComparable<OrderSQL>
    {
        int order;
        public OrderSQL(int order)
        {
            this.order = order;
        }
        public override string ToString()
        {
            return order + "";
        }

        int IComparable<OrderSQL>.CompareTo(OrderSQL other)
        {
            int compareTo;
            if(other!=null)
            {
                compareTo = order.CompareTo(other.order);
            }
            else
            {
                compareTo = (int)CompareTo.Inferior;
            }
            return compareTo;
        }
    }
}
