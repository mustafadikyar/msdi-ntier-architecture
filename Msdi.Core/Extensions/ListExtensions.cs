using Msdi.Core.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Msdi.Core.Extensions
{
    /// <summary>
    /// Extension methods for IList
    /// </summary>
    public static class ListExtensions
    {

        /// <summary>
        /// Shuffles an IList using Cryptographically secure randomization
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static IList<T> Shuffle<T>(this IList<T> source)
        {
            if (source.Count == 0)
            {
                return Activator.CreateInstance<IList<T>>();
            }

            var destination = source.DeepCloneObject() as IList<T>;

            var length = destination?.Count() ?? 0;

            for (var currentIndex = 0; currentIndex < length; currentIndex++)
            {
                var swapIndex = RandomHelpers.Next(length);
                destination.Swap(currentIndex, swapIndex);
            }

            return destination;
        }

        /// <summary>
        /// Converts a Generic List to its CSV representation
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="list">List of Type T to convert to CSV</param>
        /// <returns></returns>
        public static string ToCsv<T>(this IList<T> list)
        {
            if (list == null || list.Count == 0)
            {
                return String.Empty;
            }

            // Get type from 0th member
            Type t = list[0].GetType();
            string newLine = Environment.NewLine;

            // ReSharper disable once ConvertToUsingDeclaration
            using (var sw = new StringWriter())
            {
                // Make a new instance of the class name we figured out to get its props
                object o = Activator.CreateInstance(t);
                //gets all properties
                PropertyInfo[] objectProperties = o.GetType().GetProperties();

                string headerToWrite = String.Empty;

                // Foreach of the properties in class above, write out properties
                // this is the header row
                foreach (PropertyInfo property in objectProperties)
                {
                    headerToWrite += property.Name + ";";
                }

                // Remove last character
                headerToWrite = headerToWrite.Remove(headerToWrite.Length - 1);
                sw.Write(headerToWrite);
                sw.Write(newLine);

                // This acts as data row
                foreach (T item in list)
                {
                    string stringToWrite = String.Empty;

                    // This acts as data column
                    foreach (PropertyInfo pi in objectProperties)
                    {
                        // This is the row+col intersection (the value)
                        string whatToWrite = Convert.ToString(item.GetType()
                                                    .GetProperty(pi.Name)
                                                    .GetValue(item, null)).Replace(',', ' ') + ';';

                        stringToWrite += whatToWrite;
                    }

                    // Remove last ';'
                    stringToWrite = stringToWrite.Remove(stringToWrite.Length - 1);
                    sw.Write(stringToWrite);
                    sw.Write(newLine);
                }

                return sw.ToString();
            }

        }

        /// <summary>
        /// TODO: Fix this
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static IEnumerable<T> DeepClone<T>(this IEnumerable<T> source)
        {

            var serialized = JsonConvert.SerializeObject(source);
            return JsonConvert.DeserializeObject<IEnumerable<T>>(serialized);

            // return new List<T>(source);

        }

        #region -- Internal Methods --

        /// <summary>
        /// Swaps contents of firstIndex with secondIndex
        /// </summary>
        /// <typeparam name="T">Type of IList</typeparam>
        /// <param name="source">Source IList</param>
        /// <param name="firstIndex">First index</param>
        /// <param name="secondIndex">Second index</param>
        internal static void Swap<T>(this IList<T> source, int firstIndex, int secondIndex)
        {
            var temp = source[firstIndex];
            source[firstIndex] = source[secondIndex];
            source[secondIndex] = temp;
        }

        #endregion

    }
}
