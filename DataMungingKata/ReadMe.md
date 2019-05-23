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

## Defects

1. Temperature does not parse due to "*" in data row.
> I have noticed, when creating part three, that a couple of the temperatures have an "*" in them.  These currently don't parse as valid floats in this version.
>
> However, the answer is still the same when I have fixed this issue in part three.


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

Let's figure out the requirements a bit more.

> We want the application to *register* *systems* into a *collection*, then *execute* the components, which will involve *reading* in data from a file, and *returning* the *result* of executing a question.

So, each of the previous parts are systems.  We need to register them with something.  Then for each of those systems, we want to execute the process we did in the other parts.

This involves reading in a file from a specific location, extracting parts of the data into an object, and then finding out a piece of information from the data.

Apart from the console application that starts this process, I see three other parts.

* Core - contains all of the interfaces, the registration system, and any other commom part.
* Weather -  the weather component.
* Football - the football component.

I can think of two ways we could go with this.

1. Create a Factory system to execute the different components.
2. Set up an event system to make the components execute the question.
3. Just loop through each process registered.

Which way to go?

I would like to try the event system, and see if I can get things to run asynchronously.

There's a NuGet package that helps with events called Easy.MessageHub.

I'm going to start with doing it simply with a processor, the same way that I did in parts one and two.  Then I'll work on making it an event based system, then the asynchronous changes.


#### Core

We will need the interfaces:

* IReader
* IMapper
* INotify

We will also need:

 * IMessageHub - the event subscribe and publish system.
 * Registration - registers the components defined in the solution.
 * Executor - for each of the components, we want to raise the start event.
 * Logging - need a system to log details of processing (Serilog).
 * Interface Types - for the different types of return, we need something concrete to base them on.


#### Components

These will need to have an implementation of the interfaces.  We did this in part one and two.

I want to add validation for the different parts and types that the components create and use.

The component must have:

* Types - the objects created and used.
* IReader - the system that reads in data from the file.
* IMapper - the system that maps the data from the file into the objects.
* INotify - the system that executes the question against the data collected.
* Validator - validates the types created.
* Configuration - defines the parts of the data that we need to extract into an object.
* Subscriber - we need to subscribe to a starting event.
* Publisher - we need to publish the answer when we have finished.


#### Problems

I have a problem with just starting a project.  I can write some bits like this down, but when it actually comes to writing some code, I struggle.

I guess I will just have to make a start, and see where things go.  If it changes, then I will have to update this read me with the reasons why it changed.

So, onwards we go!


##### First problem:

The weather and football objects are totally different.  How do I make them an IDataType?

The IDataType is part of the Core project.  That project should not care how the components object is defined.

I have an idea, but not sure if it is right.  Not sure what the standard is to handle this problem.


##### Second problem:

My async skills are a bit limited unless using the built in async code.  However, file reading is synchronous, so trying to put it into a task factory.  Need to work on how!

I think to start with, I will leave most of the code as synchronous, just to get it working.  When I have the code working, then I can work on making it asynchronous.

Had to remove the asyncronous code to make it run straight.  So, need to work on getting that correct.  Hhhhhmmmmmmm!

Breaks all of the unit tests converting to synchronous code for now.

This is now working, but I am still not sure if what I have done is correct.  Unit tests work, and running the program works.


##### Third problem:

I am duplicating the validation for the Weather class.  The first time before we add it to the list in the mapper, the second time when we get it out of the data in the notifier.

Should we be doing this twice?  There are arguments for both sides.

For:
1. We should be checking the objects we are working on, and not accepting that the object is valid.  
2. The method does not know that something else has validated it.

Against:
1. The object was validated in the mapper, why do we need to duplicate that check again in the notifier?
2. Additional overhead of re-processing the validation.


##### Fourth problem:

I have noticed that the weather temperatures have an "*" next to them.  I don't think the other parts handle this, but I didn't notice that anything was wrong.  I think the value wouldn't have parsed into a float, so that Weather object would have been ignored and not part of the main calculation.  This would be incorrect.

I will need to fix this issue in this part. - Fixed in part three.


#### Progress

I've set up most of the weather component and the tests for it.  I still need to do the validators, and extract parts of the current code to use those validators.

As I am writing more of the Weather component, I am noticing that there is a lot to write.  Which means, each component will be quite large.

The Weather component is mostly there now, up to the stage where I could use the part one or part two console apps to use it.  I still need to work out how I am going to do the processing part again.

I didn't like calling the working part a "manager" in the other parts.  A processor is what I think it should be, but it will be done differently if we are using events, not just using it as is.

I have part three running the weather component the same way that part one did.  It works and gives me the same answer, even if the code in part one wasn't correct.

The next step is to start getting the event system running.  Seems to be working, as I am stopping the execution until the user chooses to quit.  I get the process running by raising a string event (start processing).

The component process also raises a completed event.

Made some small changes with how the subscriptions and registrations is done.  The Message Hub sets up the subscriptions within the core project now, not the component.

The next step is to clean up the core project and bootstrap.  Which includes some more unit tests.  As I have cleabed up the code a bit in the core, this leaves me with just the ComponentRegister to unit test.

Then add the football component, which will be the same as from part two, but with added validation and tests to keep it inline with the new weather component.

Did an experiment with creating three weather.dat files and seeing what happened.  Got it to work with the new file name / locations.  All looked good.


Working on the Football Component now, and I am seeing duplication of code in the extension methods and validators.  Now, the validators are specific to the component, but the extensions are not.

Can I get the extensions to use the expected validator based on something that is contained in the call?

I have completed the rough football component now. Still needs some clean up and summary blocks.

Next stage is the unit tests for the football component.

Then we can see how much of the code is duplicated, as there is a lot that looks so very similar, as each component does the same thing.

So, now I have got to the stage where the application actually works, and most of the unit tests cover the code.  I just need to finish the football component tests, mainly the processors.  The tests are pretty much the same as the tests for the weather component.

What I am seeing is a lot of similar code, or at worst, duplicated code.

When I have finished the unit tests, then, I can figure out some way of refactoring the components into the core project.


## Kata Questions

1. To what extent did the design decisions you made when writing the original programs make it easier or harder to factor out common code?
2. Was the way you wrote the second program influenced by writing the first?
3. Is factoring out as much common code as possible always a good thing? Did the readability of the programs suffer because of this requirement? How about the maintainability?

### Answers

1. The common code seemed to fit nicely with the idea of the reader, mapper, notify pattern that I used.  What I am not liking is that the code in the components still looks the same.
2. Yes, very much so.  It was almost a copy of the first.
3. Leaving the code with lots of similar code makes it easy to maintain, as each component has a specific template that it needs to follow.  This would make it easy to create new components.  The problem is, the code is a lot to write for a component.
