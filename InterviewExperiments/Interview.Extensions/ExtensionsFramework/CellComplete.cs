using System.Collections.Generic;

// 23280720246824
namespace ExtensionsFramework
{
    public class CellComplete
    {
        public int[] cellComplete(int[] states, int days)
        {
            var result = new[] { 0, 0, 0, 0, 0, 0, 0, 0 };

            for (var day = 0; day < days; day++)
            {
                result = SetResult(states, day, result);
            }

            return result;
        }

        private static int[] SetResult(int[] states, int day, int[] result)
        {
            // So this is a loop for each day.
            var currentState = day == 0 ? states : result;
            var tempState = SetTempState(currentState);

            return tempState;
        }

        private static int[] SetTempState(int[] currentState)
        {
            var tempState = new int[8];

            // So we need to go through the array and update the states of the cells.
            for (var element = 0; element < currentState.Length; element++)
            {
                var beforeCell = element == 0 ? 0 : currentState[element - 1];
                var afterCell = element == 7 ? 0 : currentState[element + 1];

                var currentCell = CheckRule(beforeCell, afterCell) ? 0 : 1;

                tempState[element] = currentCell;
            }

            return tempState;
        }

        private static bool CheckRule(int beforeCell, int afterCell)
        {
            return (beforeCell == 0 && afterCell == 0) || (beforeCell == 1 && afterCell == 1);
        }
    }
}
