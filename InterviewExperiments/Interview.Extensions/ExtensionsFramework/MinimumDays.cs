namespace ExtensionsFramework
{
    public class MinimumDays
    {
        // METHOD SIGNATURE BEGINS, THIS METHOD IS REQUIRED
        public int minimumDays(int rows, int columns, int[,] grid)
        {
            var result = 0;
            var currentGrid = grid;
            
            do
            {
                result = UpdateGrid(rows, columns, grid, currentGrid, result);
            } while (!CheckGrid(rows, columns, grid));
            
            return result;
        }

        private int UpdateGrid(int rows, int columns, int[,] grid, int[,] currentGrid, int result)
        {
            for (int rowIndex = 0; rowIndex < rows - 1; rowIndex++)
            {
                for (int columnIndex = 0; columnIndex < columns - 1; columnIndex++)
                {
                    // Is this element a 0 or 1?
                    var element = grid[rowIndex, columnIndex];

                    if (element == 0)
                    {
                        // In-active.
                    }
                    else
                    {
                        // Active, so great!
                        // What can we activate?
                        if (rowIndex == 0)
                        {
                            if (columnIndex == 0)
                            {
                                currentGrid[rowIndex + 1, columnIndex] = 1;
                                currentGrid[rowIndex, columnIndex + 1] = 1;
                            }
                            else if (columnIndex == columns - 1)
                            {
                                currentGrid[rowIndex + 1, columnIndex] = 1;
                                currentGrid[rowIndex, columnIndex - 1] = 1;
                            }
                            else
                            {
                                currentGrid[rowIndex + 1, columnIndex] = 1;
                            }
                        } else if (rowIndex == rows - 1)
                        {
                            if (columnIndex == 0)
                            {
                                currentGrid[rowIndex - 1, columnIndex] = 1;
                                currentGrid[rowIndex, columnIndex + 1] = 1;
                            }
                            else if (columnIndex == columns - 1)
                            {
                                currentGrid[rowIndex - 1, columnIndex] = 1;
                                currentGrid[rowIndex, columnIndex - 1] = 1;
                            }
                            else
                            {
                                currentGrid[rowIndex - 1, columnIndex] = 1;
                            }
                        }
                        else
                        {
                            currentGrid[rowIndex + 1, columnIndex] = 1;
                            currentGrid[rowIndex, columnIndex + 1] = 1;
                            currentGrid[rowIndex - 1, columnIndex] = 1;
                            currentGrid[rowIndex, columnIndex - 1] = 1;
                        }
                    }
                }
            }

            return ++result;
        }
        // METHOD SIGNATURE ENDS

        private bool CheckGrid(int rows, int columns, int[,] grid)
        {
            var result = true;

            for (int rowIndex = 0; rowIndex < rows - 1; rowIndex++)
            {
                for (int columnIndex = 0; columnIndex < columns - 1; columnIndex++)
                {
                    var element = grid[rowIndex, columnIndex];
                    if (element == 0)
                    {
                        result = false;
                        break;
                    }
                }
            }

            return result;
        }
	}
}
