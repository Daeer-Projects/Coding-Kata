using System;

namespace ExtensionsFramework
{
    public class MinimumDays
    {
        public int minimumDays(int rows, int columns, int[,] grid)
        {
            var result = 0;
            
            do
            {
                grid = UpdateGrid(rows, columns, grid);
                result++;
            } while (!CheckGrid(rows, columns, grid));
            
            return result;
        }

        private int[,] UpdateGrid(int rows, int columns, int[,] grid)
        {
            var currentGrid = new int[rows, columns];
            Array.Copy(grid, currentGrid, grid.Length);

            for (var rowIndex = 0; rowIndex < rows; rowIndex++)
            {
                for (var columnIndex = 0; columnIndex < columns; columnIndex++)
                {
                    var element = grid[rowIndex, columnIndex];

                    if (IsElementActive(element))
                    {
                        // Active, so great!
                        UpdateTopRow(rowIndex, currentGrid, columnIndex);
                        UpdateBottomRow(rows, rowIndex, currentGrid, columnIndex);
                        UpdateLeftColumn(columnIndex, currentGrid, rowIndex);
                        UpdateRightColumn(columns, columnIndex, currentGrid, rowIndex);
                    }
                }
            }

            return currentGrid;
        }

        private static bool IsElementActive(int element)
        {
            return element != 0;
        }

        private static void UpdateTopRow(int rowIndex, int[,] currentGrid, int columnIndex)
        {
            if (rowIndex != 0)
            {
                currentGrid[rowIndex - 1, columnIndex] = 1;
            }
        }

        private static void UpdateBottomRow(int rows, int rowIndex, int[,] currentGrid, int columnIndex)
        {
            if (rowIndex != rows - 1)
            {
                currentGrid[rowIndex + 1, columnIndex] = 1;
            }
        }

        private static void UpdateLeftColumn(int columnIndex, int[,] currentGrid, int rowIndex)
        {
            if (columnIndex != 0)
            {
                currentGrid[rowIndex, columnIndex - 1] = 1;
            }
        }

        private static void UpdateRightColumn(int columns, int columnIndex, int[,] currentGrid, int rowIndex)
        {
            if (columnIndex != columns - 1)
            {
                currentGrid[rowIndex, columnIndex + 1] = 1;
            }
        }

        private bool CheckGrid(int rows, int columns, int[,] grid)
        {
            var result = true;

            for (int rowIndex = 0; rowIndex < rows; rowIndex++)
            {
                for (int columnIndex = 0; columnIndex < columns; columnIndex++)
                {
                    var element = grid[rowIndex, columnIndex];
                    if (element == 0)
                    {
                        return false;
                    }
                }
            }

            return result;
        }
	}
}
