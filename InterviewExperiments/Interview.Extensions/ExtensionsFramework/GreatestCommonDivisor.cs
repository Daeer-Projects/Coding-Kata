using System.Collections.Generic;
using System.Linq;

namespace ExtensionsFramework
{
    public class GreatestCommonDivisor
    {
        // METHOD SIGNATURE BEGINS, THIS METHOD IS REQUIRED
        public int generalizedGCD(int num, int[] arr)
        {
            var result = 0;

            // So, we need to loop through all numbers from 1 to n (5 in these tests) and see what 
            // number is the largest positive value that is divisible without any remainder.

            // Lets get the first number.
            var elementValues = new Dictionary<int, int[]>();

            // So it looks like we need to loop through the elements in the array and find out what
            // the result of dividing it by this index.
            for (int element = 0; element < arr.Length; element++)
            {
                // So lets find out the values that are divisible for this element.
                var currentArray = new List<int>();
                var arrayItemValue = arr[element];

                for (int index = 1; index < num + 1; index++)
                {

                    if (arrayItemValue % index == 0)
                    {
                        currentArray.Add(index);
                    }
                }

                elementValues.Add(arrayItemValue, currentArray.ToArray());
            }

            // So we have an array of values that is divisible without any remainder.
            // Whats the maximum for each of our elements 

            //var thing = elementValues.Select(d => d.Value.Intersect())

            //for (int dict = 0; dict < elementValues.Count; dict++)
            //{
            //    var firstValue = elementValues[dict].GetValue(0);

            //    // Loop through the rest to see if it matches.

            //    for (int otherDict = dict + 1; otherDict < elementValues.Count; otherDict++)
            //    {
            //        var value = elementValues[otherDict].GetValue(0);

            //    }

            //}

            //foreach (var elementValue in elementValues)
            //{
            //    //var positiveValue = elementValue.Value.Where(x => x != 0).Select(x => x).First();
            //    var firstValue = elementValue.Value[0];

            //    foreach (var item in elementValue.Value)
            //    {
                    
            //    }

            //}

            return result;
        }
        // METHOD SIGNATURE ENDS
    }
}
