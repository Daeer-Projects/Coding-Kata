using System;
using System.Linq;

namespace KarateChopKata
{
    /// <summary>
    /// An extension for the <see langword="int"/> array.
    /// </summary>
    public static class ArrayExtension
    {
        // Attempt 1:

        ///// <summary>
        ///// An extension that locates the position of an item in the array.
        ///// Note: The array is zero based, so the result is returned as a zero based position.
        ///// </summary>
        ///// <param name="inputArray"> The array we are searching through. </param>
        ///// <param name="searchParameter"> The item in the array we are searching for. </param>
        ///// <returns>
        ///// The position in the array of the item.
        ///// </returns>
        //public static int Chop(this int[] inputArray, int searchParameter)
        //{
        //    var result = -1;

        //    if (inputArray.Length > 0 && searchParameter != 0)
        //    {
        //        // First idea is to do a for each loop.
        //        // Not fully necessary.
        //        foreach (var item in inputArray)
        //        {
        //            if (item == searchParameter)
        //            {
        //                result = Array.IndexOf(inputArray, item);
        //            }
        //        }
        //    }

        //    return result;
        //}

        // Attempt 2:

        ///// <summary>
        ///// An extension that locates the position of an item in the array.
        ///// Note: The array is zero based, so the result is returned as a zero based position.
        ///// </summary>
        ///// <param name="inputArray"> The array we are searching through. </param>
        ///// <param name="searchParameter"> The item in the array we are searching for. </param>
        ///// <returns>
        ///// The position in the array of the item.
        ///// </returns>
        //public static int Chop(this int[] inputArray, int searchParameter)
        //{
        //    var result = -1;

        //    if (inputArray.Length > 0 && searchParameter != 0)
        //    {
        //        // Can we just use the array class itself to find the parameter index?
        //        result = Array.IndexOf(inputArray, searchParameter);
        //    }

        //    return result;
        //}

        // Attempt 3:

        ///// <summary>
        ///// An extension that locates the position of an item in the array.
        ///// Note: The array is zero based, so the result is returned as a zero based position.
        ///// </summary>
        ///// <param name="inputArray"> The array we are searching through. </param>
        ///// <param name="searchParameter"> The item in the array we are searching for. </param>
        ///// <returns>
        ///// The position in the array of the item.
        ///// </returns>
        //public static int Chop(this int[] inputArray, int searchParameter)
        //{
        //    var result = -1;

        //    if (inputArray.Length > 0)
        //    {
        //        // Using linq to find the index.  Not a very elegant method.
        //        result = inputArray.Any(x => x != 0 && x.Equals(searchParameter))
        //            ? Array.IndexOf(inputArray, inputArray.First(x => x.Equals(searchParameter)))
        //            : -1;
        //    }

        //    return result;
        //}

        //// Attempt 4:

        ///// <summary>
        ///// An extension that locates the position of an item in the array.
        ///// Note: The array is zero based, so the result is returned as a zero based position.
        ///// </summary>
        ///// <param name="inputArray"> The array we are searching through. </param>
        ///// <param name="searchParameter"> The item in the array we are searching for. </param>
        ///// <returns>
        ///// The position in the array of the item.
        ///// </returns>
        //public static int Chop(this int[] inputArray, int searchParameter)
        //{
        //    // Using linq to find the index.  Not a very elegant method.
        //    var result = inputArray.Any(x => x != 0 && x.Equals(searchParameter))
        //        ? Array.IndexOf(inputArray, inputArray.First(x => x.Equals(searchParameter)))
        //        : -1;

        //    return result;
        //}

        // Attempt 5:

        /// <summary>
        /// An extension that locates the position of an item in the array.
        /// Note: The array is zero based, so the result is returned as a zero based position.
        /// </summary>
        /// <param name="inputArray"> The array we are searching through. </param>
        /// <param name="searchParameter"> The item in the array we are searching for. </param>
        /// <exception cref="T:System.OverflowException">The array is multidimensional and contains more than <see cref="F:System.Int32.MaxValue" /> elements.</exception>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="array" /> is <see langword="null" />.</exception>
        /// <returns>
        /// The position in the array of the item.
        /// </returns>
        public static int Chop(this int[] inputArray, int searchParameter)
        {
            // linq doesn't seem to be the right tool for this job.
            // Looks like the Array class is the best way to go, can I improve
            // on attempt 2?
            var result = (inputArray.Length > 0 && searchParameter != 0)
                ? Array.IndexOf(inputArray, searchParameter)
                : -1;

            // I could just return rather than declare the "result" variable.
            return result;
        }
    }
}
