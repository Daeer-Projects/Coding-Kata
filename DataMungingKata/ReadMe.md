# Data Munging Kata

This is the fourth kata in the list.  However, only the second kata that involved coding.

## Part One

Weather Data.

I've downloaded the file, and the purpose of this kata is described as:

> In weather.dat you’ll find daily weather data for Morristown, NJ for June 2002. Download this text file, then write a program to output the day number (column one) with the smallest temperature spread (the maximum temperature is the second column, the minimum the third column).

### Objective

* I need to see a day number in the output.
* This day number will correspond with the day that has the smallest temperature spread.
* This data is to be read in from a file.

### Process

To start with, I need to read in the data from the file.

#### File reader

The file system will be able to open the file and read in the data row by row.  To make this testable, I will need to use an interface for the file system.  I believe this is achievable using System.IOAbstractions.

I will need to process the rows from the file, and extract them into an object that I can process.

Looking at the file, the first two rows, I can ignore.

The rest of the data, is what I need to read / extract.  This means, I need to know the positions of the columns for each part of the data.

#### Columns

For this project, I only need to read in the first three columns.

| Column | Description |Start | Length |
|--------|-------------|------|--------|
| Dy     | Day         | 1    | 4      |
| Mxt    | Max Temp    | 6    | 6      |
| Mnt    | Min Temp    | 11   | 6      |

The rest, we can ignore.

We must note, that the min and max temps can have decimal points.

