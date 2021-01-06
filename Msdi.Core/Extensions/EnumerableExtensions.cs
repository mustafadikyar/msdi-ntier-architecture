using Msdi.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Msdi.Core.Extensions
{
    /// <summary>
    /// Extension methods for IEnumerable
    /// </summary>
    public static class EnumerableExtensions
    {

        /// <summary>
        /// Converts an IEnumerable to its CSV representation
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="list">IEnumerable of Type T to convert to CSV</param>
        /// <returns></returns>
        public static string ToCsv<T>(this IEnumerable<T> list)
        {
            return list.ToCsv();
        }

        /// <summary>
        /// Groups a IEnumerable w.r.t. an attribute
        /// </summary>
        /// <typeparam name="TSource">Type of source IEnumerable</typeparam>
        /// <typeparam name="TKey">Type of attribute of IEnumerable w.r.t. the IEnumerable must be grouped</typeparam>
        /// <param name="source">Source IEnumerable</param>
        /// <param name="keySelector"></param>
        /// <returns></returns>
        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            return source?.GroupBy(keySelector).Select(grp => grp.First());
        }

        /// <summary>
        /// Gets a random element for the IEnumerable
        /// </summary>
        /// <typeparam name="T">Type of source IEnumerable</typeparam>
        /// <param name="source">Source IEnumerable collection</param>
        /// <returns></returns>
        public static T GetRandomElement<T>(this IEnumerable<T> source)
        {
            // If the list is empty then return an empty instance of T
            if (source.IsEmpty())
            {
                return Activator.CreateInstance<T>();
            }

            // Get a random number
            int randomNumber = IntHelpers.GetRandomNumber(source.Count() - 1);

            return source.ElementAt(randomNumber);
        }

        /// <summary>
        /// Shuffles the specified source.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source)
        {
            var shuffled = source.OrderBy(src => RandomHelpers.Next(source.Count()));

            return shuffled;
        }

        /// <summary>
        /// Gets a random element for the IEnumerable after one shuffle
        /// </summary>
        /// <typeparam name="T">Type of source IEnumerable</typeparam>
        /// <param name="source">Source IEnumerable collection</param>
        /// <param name="shuffleTimes">Number of times to shuffle</param>
        /// <returns></returns>
        public static T GetRandomShuffled<T>(this IEnumerable<T> source, int shuffleTimes = 1)
        {
            shuffleTimes = (shuffleTimes < 1) ? 1 : shuffleTimes;

            for (int i = 0; i < shuffleTimes; i++)
            {
                source = source.Shuffle();
            }

            return source.GetRandomElement();
        }

        /// <summary>
        /// Returns 'true' if an IEnumerable is empty. False otherwise.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static bool IsEmpty<T>(this IEnumerable<T> list)
        {
            return (list == null || !list.Any());
        }

        /// <summary>
        /// Partitions the list into specified chunk size.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sourceList">The source list.</param>
        /// <param name="chunkSize">Size of the chunk.</param>
        /// <returns></returns>
        public static IEnumerable<IEnumerable<T>> Partition<T>(this IEnumerable<T> sourceList, int chunkSize)
        {
            return sourceList
                .Select((value, index) => new { Index = index, Value = value })
                .GroupBy(group => group.Index / chunkSize)
                .Select(group => group.Select(grp => grp.Value));
        }

        /// <summary>
        /// Partitions the specified source list.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sourceList">The source list.</param>
        /// <returns></returns>
        public static IEnumerable<IEnumerable<T>> Partition<T>(this IEnumerable<T> sourceList)
        {
            // ReSharper disable once PossibleMultipleEnumeration
            int chunkSize = (int)Math.Floor(Math.Sqrt(sourceList.Count()));

            return Partition(sourceList, chunkSize);
        }

        /// <summary>
        /// Flattens the specified source.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">The IEnumerable source.</param>
        /// <returns></returns>
        public static IEnumerable<T> Flatten<T>(this IEnumerable<IEnumerable<T>> source)
        {
            if (source.IsEmpty())
            {
                return Activator.CreateInstance<IEnumerable<T>>();
            }

            return source.SelectMany(item => item);
        }


        /// <summary>
        /// Maximum or the default.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable">The enumerable.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns></returns>
        public static T MaxOrDefault<T>(this IEnumerable<T> enumerable, T defaultValue = default)
        {
            return enumerable.DefaultIfEmpty(defaultValue).Max();
        }

    }
}
