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

### Solution

I have got the base solution completed for part one now.  I still need to do some unit tests, but have a migrane at the moment, so leaving till later.

However, the application does what is expected of it.  For this part, anyway.

The unit tests how now been completed.  They cover the contract requirements that I defined, the simple exceptions, and a simple return for the good path.

The other components have their own tests, so I haven't duplicated them.

## Part Two

Football Data.

I've downloaded the file and added it to the solution.

> The file football.dat contains the results from the English Premier League for 2001/2. The columns labeled ‘F’ and ‘A’ contain the total number of goals scored for and against each team in that season (so Arsenal scored 79 goals against opponents, and had 36 goals scored against them). Write a program to print the name of the team with the smallest difference in ‘for’ and ‘against’ goals.

### Objective

* Print the name of the team.
* This team will need to correspond with the smallest difference in the 'for' and 'against' goals.
* This data will need to be read in from a file.

### Process

The file reader process will be the same as from Part One.  The only difference is the columns I need to read in.

#### Columns

I need just three columns from the data.

| Column  | Description         |Start | Length |
|---------|---------------------|------|--------|
| Team    | Team name           | 8    | 16     |
| For     | For goal points     | 44   | 4      |
| Against | Against goal points | 51   | 3      |

The team is a string and will need to be trimmed.

The other two will need to be converted to int's, as they don't contain any decimal points.

### Solution

The application for Part Two is now complete'ish.

I haven't added all of the summary blocks, but I know they should be there.  I think I will leave till Part Three for the final version... If I get to write another one.

So, what is Part Three?


## Part Three

> Take the two programs written previously and factor out as much common code as possible, leaving you with two smaller programs and some kind of shared functionality.

That sounds interesting.  How can I make some of the configuration systems more generic?  Hmmmmmm...

### Requirements

I thought I would start with figuring out the new set of requirements for this project.

So, what do we want it to do?

1. Read in different types of files.
2. Extract parts of the files / rows into defined classes.
3. Have something that returns the specific question the bit requires.
    a. This could be many different things based on what the part wants.

### Questions

1. How are we going to define the configuration for the file types?
2. How are we going to define the interfaces for the generic process? I'm thinking I keep the IReader and INotify. Might need an IProcessor.
3. How are we going to register the requirement type to the main program?


### Kata Questions

1. To what extent did the design decisions you made when writing the original programs make it easier or harder to factor out common code?
2. Was the way you wrote the second program influenced by writing the first?
3. Is factoring out as much common code as possible always a good thing? Did the readability of the programs suffer because of this requirement? How about the maintainability?

#### Answers

1. To do.
2. Yes, very much so.  It was almost a copy of the first.
3. To do.
