using System.Collections.Generic;

namespace ExtensionsFramework
{
    public class GreatestCommonDivisor
    {
        public int generalizedGCD(int num, int[] arr)
        {
            return FindGreatestCommonDivisor(arr, num);
        }

        private int FindGreatestCommonDivisor(IReadOnlyList<int> arrayOfNumbers, int number)
        {
            var result = arrayOfNumbers[0];
            for (var index = 1; index < number; index++)
            {
                result = GetGreatestCommonDivisor(arrayOfNumbers[index], result);

                if (result == 1) return 1;
            }

            return result;
        }

        private int GetGreatestCommonDivisor(int arrayItem, int currentResult)
        {
            while (true)
            {
                if (arrayItem == 0) return currentResult;
                var item = arrayItem;
                arrayItem = currentResult % arrayItem;
                currentResult = item;
            }
        }
    }
}
