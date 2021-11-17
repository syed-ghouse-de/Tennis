using Hexagon.Game.Tennis.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexagon.Game.Framework.Extension
{
    /// <summary>
    /// Extensions class
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Extension for ForEach
        /// </summary>
        /// <typeparam name="T">Type of an object</typeparam>
        /// <param name="collection">collections</param>
        /// <param name="action">action</param>
        /// <returns>Enumerable of type T</returns>
        public static IEnumerable<T> ForEach<T>(this IEnumerable<T> collection, Action<T> action)
        {
            foreach (var item in collection)
            {
                action(item);
            }

            return collection;
        }

        /// <summary>
        /// Convert player points enum to digits
        /// </summary>
        /// <param name="point">Point to be converted to digit</param>
        /// <returns>Return digit in string type</returns>
        public static string ToDigit(this PlayerPoint point)
        {
            List<string> points = new List<string>()
                { "0", "15", "30", "40", "A", "40", "G" };

            // Return point in digit
            return points[(int)point];
        }

    }
}
