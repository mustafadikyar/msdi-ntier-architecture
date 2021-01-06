using System;
using System.Reflection;

namespace Msdi.Core.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public static class ObjectExtensions
    {

        /// <summary>
        /// Deep clone an Object
        /// TODO: Debug and verify
        /// </summary>
        /// <param name="sourceObject">Object to clone</param>
        /// <returns></returns>
        public static object DeepCloneObject(this object sourceObject)
        {

            if (sourceObject == null)
            {
                return null;
            }

            Type type = sourceObject.GetType();

            if (type.IsValueType || type == typeof(string))
            {
                return sourceObject;
            }
            else if (type.IsArray)
            {
                Type elementType = Type.GetType(type.FullName.Replace("[]", string.Empty));

                var array = sourceObject as Array;

                Array copied = Array.CreateInstance(elementType, array.Length);

                for (int i = 0; i < array.Length; i++)
                {
                    copied.SetValue(DeepCloneObject(array.GetValue(i)), i);
                }

                return Convert.ChangeType(copied, sourceObject.GetType());
            }
            else if (type.IsClass)
            {
                object toret = Activator.CreateInstance(sourceObject.GetType());

                FieldInfo[] fields = type.GetFields(BindingFlags.Public
                                                  | BindingFlags.NonPublic
                                                  | BindingFlags.Instance);

                foreach (FieldInfo field in fields)
                {
                    object fieldValue = field.GetValue(sourceObject);
                    if (fieldValue == null)
                        continue;
                    field.SetValue(toret, DeepCloneObject(fieldValue));
                }

                // return toret;
                return Convert.ChangeType(toret, sourceObject.GetType());
            }
            else
            {
                throw new ArgumentException("Unknown type");
            }

        }

        /// <summary>
        /// Deep clones an object using Force DeepCloner
        /// </summary>
        /// <param name="sourceObject">Source object to clone</param>
        /// <returns>Cloned copy of sourceObject</returns>
        public static object DeepClone(this object sourceObject)
        {
            return sourceObject.DeepClone();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj1"></param>
        /// <param name="obj2"></param>
        /// <returns></returns>
        public static bool DeepCompare(this object obj1, object obj2)
        {

            if (obj1 == null || obj2 == null)
            {
                return false;
            }

            if (!obj1.GetType().Equals(obj2.GetType()))
            {
                return false;
            }

            Type type = obj1.GetType();

            if (type.IsPrimitive || typeof(string).Equals(type))
            {
                return obj1.Equals(obj2);
            }

            if (type.IsArray)
            {
                Array first = obj1 as Array;
                Array second = obj2 as Array;

                var en = first.GetEnumerator();
                int i = 0;
                while (en.MoveNext())
                {

                    if (!DeepCompare(en.Current, second.GetValue(i)))
                        return false;
                    i++;
                }

            }
            else
            {

                foreach (PropertyInfo pi in type.GetProperties(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public))
                {
                    if (!DeepCompare(pi.GetValue(obj1), pi.GetValue(obj2)))
                    {
                        return false;
                    }
                }

                foreach (FieldInfo fi in type.GetFields(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public))
                {
                    if (!DeepCompare(fi.GetValue(obj1), fi.GetValue(obj2)))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// Gets the copy of a object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="objectToCopy">The s.</param>
        /// <returns></returns>
        public static T GetCopy<T>(this T objectToCopy)
        {
            T newObject = Activator.CreateInstance<T>();

            foreach (PropertyInfo i in newObject.GetType().GetProperties())
            {
                if (i.CanWrite)
                {
                    object value = objectToCopy.GetType().GetProperty(i.Name).GetValue(objectToCopy, null);
                    i.SetValue(newObject, value, null);
                }
            }

            return newObject;
        }

    }
}
