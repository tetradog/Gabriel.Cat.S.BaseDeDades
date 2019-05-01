using System;
using System.Collections.Generic;
using System.Text;

namespace Gabriel.Cat.S.BaseDeDades
{
    public delegate object UpdateMethod(object oldObj,object emptyNewObject);
    /// <summary>
    /// Se usa para decir que se ha actualizado el tipo y saber cual era antes.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class,AllowMultiple =false)]
    public class OldVersion : System.Attribute
    {
        public struct UpdateClass
        {
            Type newType;
            string nameOfMethodUpdate;
            public UpdateClass(Type newType, string nameOfMethodUpdate)
            {
                this.newType = newType;
                this.nameOfMethodUpdate = nameOfMethodUpdate;
            }
            public bool Valid
            {
                get { return newType != null && nameOfMethodUpdate != null; }
            }
            public Type NewType
            {
                get { return newType; }
            }
            public string NameOfMethodUpdate
            {
                get { return nameOfMethodUpdate; }
            }
            public UpdateMethod UpdateMethod
            {
                get
                {
                    UpdateClass thisClass = this;
                    return (oldObj,newObj) =>
                    {

                        return thisClass.NewType.InvokeMember(thisClass.NameOfMethodUpdate, System.Reflection.BindingFlags.InvokeMethod, null, null, new object[] { oldObj,newObj });
                    };
                }
            }
        }

        public Type Base { get; private set; }
        public UpdateMethod UpdateMethod { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="baseType"></param>
        /// <param name="newType">si no se especifica se pasarán los valores de las propiedades (con el mismo nombre y tipo) de la vieja a la nueva</param>
        /// <param name="methodUpdate">se necesita que el anterior parametro no sea null ni este para que sea valido, en caso de no serlo se usará el metodo por reflexión por defecto</param>
        public OldVersion(Type baseType, Type newType=null,string methodUpdate=null)
        {
            UpdateClass updateClass = new UpdateClass(newType, methodUpdate);
            Base = baseType;
            if (updateClass.Valid)
                UpdateMethod = updateClass.UpdateMethod;
            else
            {
                UpdateMethod = (oldObj, newObj) => {
                    SortedList<string, System.Reflection.PropertyInfo> dicOld = new SortedList<string, System.Reflection.PropertyInfo>();
                    System.Reflection.PropertyInfo[] properties=oldObj.GetType().GetProperties();
                    for (int i = 0; i < properties.Length; i++)
                        dicOld.Add(properties[i].Name, properties[i]);
                    properties = newObj.GetType().GetProperties();
                    for (int i = 0; i < properties.Length; i++)
                        if (dicOld.ContainsKey(properties[i].Name) && dicOld[properties[i].Name].PropertyType.Name == properties[i].PropertyType.Name)
                            properties[i].SetValue(newObj, dicOld[properties[i].Name].GetValue(oldObj));
                    return newObj;
                };
            }
        }
        public override string ToString()
        {
            return Base.AssemblyQualifiedName;
        }
    }
}
