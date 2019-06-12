# Refactored Version

Due to the nature of what I am trying to achieve in this solution, I think it deserved a read me of its own.

## Purpose

In this solution, I am trying to move more of the code from the components to the core project.  As they stood at the end of part three, there was a lot of code that was required to be written for the components.

I will also write some kind of documentation for the core project, as if it was going to be consumed by a developer that was creating a file processor.  This will be at the end of these other sections.

## What I want to achieve

I would like to do the following:

1. Extract the code from the readers, mappers and notifiers into the core project.
2. I want to create a generic IsValid() method that the components can use specifying the validator required.

### Readers

The reader was just a call to wrap a function into an async task.

So, the call to the core reader, we would use code like this:

``` csharp
    var file = await Reader.ReadWork(_fileSystem.File, fileLocation).ConfigureAwait(false);
```

The reader interface looks like this:

``` csharp
    public static Task<string[]> ReadWork(IFile fileSystem, string fileLocation)
```

### Mappers

The mapper extraction was a bit more complicated.

#### Processes

1. We still had to wrap it into an async task.
2. We still had to check the string based on the configuration or the component.
3. We still had to convert the item into a type that the component required.
4. We still had to check if that type is valid, based on the component validation.
5. We still had to add the valid type into the result list.

#### Solution

I created a static mapper class in the core project to accept some inputs, and process the data.

The component has to pass in the string[] fileData to the static core mapper.
The component has to define its own CheckItemRow() method and pass that function to the core mapper.
The component has to define its own AddDataItem() method which converts the string into a type, validates it, and then adds the valid type to the results list.

This last sentence seems long, and it sounds as if the method is doing more than one thing.  However, the code is split into other methods.

So, to call the core mapper, we would use code like this:

``` csharp
    var results = await Mapper.MapWork(fileData, CheckItemRow, AddDataItem).ConfigureAwait(false);
```

The mapper interface looks like this:

``` csharp
    public static Task<IList<IDataType>> MapWork(string[] fileData,
        Func<string, bool> checkItemRow,
        Func<string, IList<IDataType>, IList<IDataType>> addDataItem)
```

### Writer

This one looks nicer, but has less code in the core project.  Still working though.

#### Solution - Writer

The writer is a static writer class in the core project.

So, to call the core notify, we would use code like this:

``` csharp
    var result = await Writer.WriteWork<Football, int, string>(data, (int.MaxValue, string.Empty), CurrentRange)
        .ConfigureAwait(false);
```

The writer interface looks like this:

``` csharp
    public static Task<IReturnType> WriterWork<T, TU, TV>(IEnumerable<IDataType> data,
        (TU, TV) defaultParameters,
        Func<(TU, TV), T, (TU, TV)> evaluateCurrentRange) 
        where T: class
        where TU: object
        where TV: object
```

### Processor

I passed the instances of the reader, mapper, and writer into the static processor and returned the results.

So, to call the core processor, we would use code like this:

``` csharp
    var result = await Processor.ProcessorWork(fileLocation, _footballReader, _footballMapper, _footballWriter)
        .ConfigureAwait(false);
```

The processor interface looks like this:

``` csharp
    public static async Task<IReturnType> ProcessorWork(string fileLocation, IReader reader, IMapper mapper, IWriter writer)
```

## Additional Methods

The following is to show some of the problems and solutions that were discovered. 

### Is Valid

I am wondering if it is possible to add an extension to a class or type called IsValid, as I did in part three, but the class or type would be generic.

``` csharp
// Something like this:
public static ValidationResult IsValid<T>(this T component)
```

That would work for making it a generic IsValid method, but how would I supply the validator for that type?

Currently I have:

``` csharp
        public static ValidationResult IsValid<T, TV>(this T component)
            where T: class
            where TV: AbstractValidator<T>, new()
        {
            var validator = new TV();

            var result = validator.Validate(component);

            return result;
        }
```

But, to use it, I have this ugly:

``` csharp
var validationResult = result.IsValid<Football, FootballValidator>();
```

The thing is, the result type is a Football class, so why do I need to define it in the attributes?

#### Solutions

I have a couple of solutions.  Neither one perfect.

Out of the three ideas, I prefer the last one.  The extension with the ```new Validator()``` passed into it.

Tests and code updated to use this version of the extension method.

##### Helper Class

``` csharp
        public static ValidationResult IsValid<T>(T component, AbstractValidator<T> validator)
        {
            var result = validator.Validate(component);

            return result;
        }
```

Used like this:

``` csharp
var result = Component.IsValid(testType, new TestValidator());
```

##### Extension

``` csharp
        public static ValidationResult IsValid<T>(this T component, AbstractValidator<T> validator)
            where T : class
        {
            var result = validator.Validate(component);

            return result;
        }
```

Used like this:

``` csharp
var result = testType.IsValid(new TestValidator());
```

# Documentation

## Contents

* 1 - Introduction
* 2 - Solution Parts
* 3 - Core
* 4 - Component
* 5 - Main Program
* 6 - Example Project
* 7 - References

## 1 - Introduction

Welcome to the Data Munging Core project.

This project is to give you the consumer, a centralised system for processing files and producing an answer.  The file composition, data structure, and data analysis is up to you.

### Basis

The basis of this project is based on the coding kata, "Data Munging".  This kata is to process two different file types and return an answer.  It was divided up into three parts.

* Part One: Extract data from a Weather.dat file and find out which day had the least temperature change.

* Part Two: Extract data from a Football.dat file and find out which team has the smallest difference between for and against goals.

* Part Three: Take the two programs and extract duplicated code into a core project that could be used by both components.

## 2 - Solution Parts

The project that you will create is up to you.  What this core project requires of you to set up are the following:

1. Core - instantiate and set up the core objects.
   1. Event - Message Hub.
   2. Component Register.
   3. Logging - Serilog.
2. Components - create the components.
3. Main Solution - add / reference the components and core in the main solution.

### Core Sections

The core is set up to extract out as much code as possible to make it easier for you to create the components.

* ComponentRegister
* Extensions
* Interfaces
* Processors
* Types
* Validators

### Logging Section

The core project is set up to use Serilog.

### Components

The core project is not limited to how many components can be created and registered.  The limitation will be on how much the hardware it is installed on can handle.

### Main Solution

It is assumed that the solution you are creating needs to process files and produce some kind of assessment on the data contained in the file.

Your solution will need to set up the core project and components so that they can be used effectively.

## 3 - Core

### Component Register

The main part of the core is:

```ComponentRegister.cs```

This is the object that is created with the MessageHub and Logger.  Once created the components can be registered.

The registration process creates the component, and then sets up a subscription and the method the component will execute when that event is raised.

The component register will then set up a subscription for the results that the components raise when they have completed their processing.

#### Creation

To create an instance of the ComponentRegister:

``` csharp
var componentRegister = new ComponentRegister(hub, coreLogger);
```

#### Register Component

The signature of the method:

``` csharp
public bool RegisterComponent(IComponentCreator creator, string fileName)
```

The ```IComponentCreator``` will be discussed later.  The file name is the location of the file the component is going to process.

To register a component, so that it can be used to process files:

``` csharp
if (componentRegister.RegisterComponent(weatherComponentCreator, WeatherConstants.FullFileName))
{
        // all good.
}
else
{
        // do some complaining.
}
```

The registration process will return a true or false based on if the registration process was successful.

#### Register Subscriptions

The ComponentRegister will need to be set up to handle any results raised by the components.

``` csharp
componentRegister.RegisterSubscriptions();
```

### Extensions

The core project has only one extension.

That extension is the "IsValid" extension used by the components when requesting validation of the types used.

Signature:

``` csharp
public static ValidationResult IsValid<T>(this T component, AbstractValidator<T> validator)
        where T : class
```

Usage:

``` csharp
var result = testType.IsValid(new TestValidator());
```

### Interfaces

The core project defines some interfaces that the components will need to use for the abstraction process to work.

* IComponent
* IComponentCreator
* IDataType
* IMapper
* IProcessor
* IReturnType
* IWriter

### Processors

These are the processors that the components processors will use.  They are static classes with the template layout of instructions for the components to define.

* Mapper
* Processor
* Reader
* Writer

#### Mapper

This class executes the mapper code from the component.  It takes the data read in and converts it to specific types, as defined by the components.

##### Signature

``` csharp
public static Task<IList<IDataType>> MapWork(string[] fileData,
        Func<string, bool> checkItemRow,
        Func<string, IList<IDataType>, IList<IDataType>> addDataItem)
```

##### Usage

``` csharp
var results = await Mapper.MapWork(GetData(), CheckItemRow, AddDataItem).ConfigureAwait(false);
```

##### Parameters

* ```string[] fileData```
* ```Func<string, bool> checkItemRow```
* ```Func<string, IList<IDataType>, IList<IDataType>> addDataItem```

###### fileData

This is the string array of data to be mapped.

###### checkItemRow

This is a function, defined in the component, that checks the data meets the criteria before it is added to the mapped data.

**Example:**

``` csharp
private static bool CheckItemRow(string item)
{
        return !item.Equals(string.Empty) &&
                !item.Equals("banana") &&
                !item.Equals("-----------------------------------------");
}
```

###### addDataItem

This is a function, defined in the component, that adds the data to the mapped data.  So, after the data has met the criteria defined, we would then do some validation in this function before fully adding it to the mapped data.

**Example:**

``` csharp
private IList<IDataType> AddDataItem(string item, IList<IDataType> results)
{
        var dataResults = results;
        var data = item.ToTestType();
        if (data.IsValid)
        {
                dataResults.Add(new ContainingDataType { Data = data.TestType });
        }

        return dataResults;
}
```

#### Processor - Core

This class executes the processor code from the component.  The processor executes the code in the other processors.

##### Signature - Processor

``` csharp
public static async Task<IReturnType> ProcessorWork(string fileLocation, IReader reader, IMapper mapper, IWriter writer)
```

##### Usage - Processor

``` csharp
var actual = await Processor.ProcessorWork(input, _reader, _mapper, _writer).ConfigureAwait(false);
```

##### Parameters - Processor

* ```string fileLocation```
* ```IReader reader```
* ```IMapper mapper```
* ```IWriter writer```

These parameters are instances of the processors created by the component.  The fileLocation is just the location of the file that is going to be processed.

#### Reader

This class executes the reader code defined by the component.  It is a simple wrapper on the code to start a new task using the components code.

##### Signature - Reader

``` csharp
public static Task<string[]> ReadWork(IFile fileSystem, string fileLocation)
```

##### Usage - Reader

``` csharp
var actual = await Reader.ReadWork(_file, input).ConfigureAwait(false);
```

##### Parameters - Reader

* ```IFile fileSystem```
* ```string fileLocation```

###### fileSystem

The IFile is an interface defined by using the NuGet package, ```System.IO.Abstractions```.  It is an interface for a wrapper around the basic file system.  The interface is only there to aid in unit testing.

###### fileLocation

The location of the file that we are about to read.

#### Writer - Core

This class executes the writer code defined by the component.  It analyses the mapped data to find out the answer being asked by the component.

##### Signature - Writer

``` csharp
public static Task<IReturnType> WriteWork<T, TU, TV>(IEnumerable<IDataType> data,
        (TU, TV) defaultParameters,
        Func<(TU, TV), T, (TU, TV)> evaluateCurrentRange) 
        where T: class
        where TU: object
        where TV: object
```

##### Usage - Writer

``` csharp
var results = await Writer.WriteWork<TestType, int?, int?>(data, (int.MaxValue, 0), CurrentRange).ConfigureAwait(false);
```

##### Parameters - Writer

* ```IEnumerable<IDataType> data```
* ```(TU, TV) defaultParameters```
* ```Func<(TU, TV), T, (TU, TV)> evaluateCurrentRange)```

###### data

This is the mapped data that is going to be analysed.

###### defaultParameters

This is a tuple of two types.  The types are defined by the component.

The first item in the example provided is the comparison value.

The second item in the example is the data that may end up being the final result.

###### evaluateCurrentRange

This is the function defined by the component that does the analysis of the data.  As the analysis could be anything, most of the code will be contained in the components.

**Example:**

``` csharp
private (int?, int?) CurrentRange<T>((int?, int?) currentRange, T componentType) where T : class
{
        // Casting to expected type.
        var specificType = componentType as TestType;
        var calculation = specificType.CalculateTestIdentity();

        if (calculation < currentRange.Item1)
        {
                currentRange.Item1 = calculation;
                currentRange.Item2 = specificType.TestIdentity;
        }

        return currentRange;
}
```

### Types

The concrete types used to wrap data or the result into objects.

* ContainingDataType
* ContainingResultType

### Validators

There is only one base validator in the core project.  It is expected that the components will create their own validators for their types.

The base validator is the ```BaseStringArrayValidator```

It is used to validate the string array passed into the core Mapper.  Only has the basic checks for the string array.

## 4 - Component

This section is about the components and how they are made up.  I will describe how the components are required to be created.

### Required Parts

The component needs to be created with the following parts:

* Configuration
* Constants
* Extensions
* Processors
* Types
* Validators
* Component Creator

### Configuration

The component will need configuration set up to identify the parts of the file that needs to be extracted into an object for processing.  The file will have headers, data rows, weird rows and footers.

The files will be different for each component, so only the component creators will know how the files will be made up.

### Constants

To avoid magic strings and numbers in the component, it is expected that you will need some kind of constants class.  However, that is up to the developer, and I will not force coding standards onto the developer.  I will just encourage it.

### Extensions - Component

Extension methods are not necessary, but can be quite useful.  The example project uses them in the component to convert a string to the specific type, and a calculation.

### Processors - Component

The component will need to implement the ```IReader```, ```IMapper```, ```IWriter``` and the ```IProcessor```.

### Types - Component

The component will need to define the types that will be used for data collection.  The example project is a class, but a struct could also be used.

The main type will be what the data in the file can be mapped to.  A class or struct that contains all of the data items that will be extracted from the file for use in the calculation.

A version of the IComponent will need to be created.  This IComponent is what is returned when we create the specific component.

### Validators - Component

Any validations that are needed to be run on the components will need to be created.  The core project has a basic string array validator, but you might want to add more validation for the file that you are processing.

The main type you create might need validation, to ensure the data is correct when we populate the component type.

### Component Creator

Each component will need to implement its own creator.  The example components accept parameters and create the concrete versions of the IReader, IMapper, IWriter and IProcessor.  The processors are all created into an IComponent.

#### Signature - Component Creator

``` csharp
public IComponent CreateComponent(IMessageHub hub, string fileName)
```

## 5 - Main Program

This section contains details of what you need to do in your program to use the components and the core project.

* Your application needs to set up logging using Serilog for the core and components to use.  The components set up their own logger in the configuration.  This helps to split the log files up, so all of the logs are not in one place.

* The application will need to instantiate the component creators for each of the components that are going to be created.

* The MessageHub will need to be instantiated.

* The ComponentRegister will need to be instantiated with the MessageHub and core logger.

* The components need to be registered by calling the ```RegisterComponent``` method using the component creator already instantiated.

* The subscriptions will need to be set up in the RegisterComponent by calling the ```RegisterSubscriptions``` method.

* Lastly, the main application will need to start of the processing by publishing a string.

``` csharp
        hub.Publish("Start Processing...");
```

## 6 - Example Project

For the purpose of this document, we are going to use a simple console application that has two components registered.

This sample project is contained in the SampleProject folder.

### Program.Main()

The main entry point into the console application is pretty empty.  We just write to the console that the application is running, and then we use a ```Bootstrapper.cs``` class to get everything going.

### Bootstrapper

* Logger
* MessageHub
* Component Creation
* Register Components
* Register Subscriptions
* Start the processing...

#### Logger

The bootstrapper sets up the log for the application, which it allows the Core to use.

``` csharp
var coreLogger = new LoggerConfiguration()
        .MinimumLevel.Debug()
        .WriteTo.Console()
        .WriteTo.File("coreLog.txt", rollingInterval: RollingInterval.Day)
        .CreateLogger();
```

The logger is using Serilog.  This is a NuGet package that can be installed into your application.

#### MessageHub

This is an "easy" system for publishing and subscribing to events.

Can be found on NuGet.  Just search for Easy.MessageHub, and install from there.

#### Component Creation

The bootstrapper then creates the component creators, which are used when registering the components.

For this project I have two components and their creators.

``` csharp
IComponentCreator weatherComponentCreator = new WeatherComponentCreator();
IComponentCreator footballComponentCreator = new FootballComponentCreator();
```

This instantiates the component creators.

### Component Creation Process

This is how we created the Football Component.  This is an example of how a component can be created and used by the core project.

#### Configuring the Football Component

The Football component needs some configuration to help it work with the core project and read in a file.

The ```Configuration.FootballConfig.cs``` class requires the following details:

* Column start and lengths - for finding the strings that need to be extracted into the Football type.
* Logger - the details of how to set up the logger.
* File System - create an instance of the FileSystem to be used in the processor.

For full details on how this is done, see the example project supplied.

#### Constants - to prevent magic strings

I've set up a constants class that points to the following:

* FullFileName - this is where the file is located for our processing.
* FootballHeader - this is how the header of the file is going to be defined.  We validate that the file we are processing has a header like this, otherwise it fails.
* FootballDivider - the Football file has a divider to show the last three teams, that are in the relegation section.

#### Extensions for the Football Component

For this component we only have two classes to help us process the Football data.

* FootballExtensions
* StringExtensions

##### FootballExtensions

The Football Extensions contains just one method: ```CalculatePointDifference```.  This is to identify how many points difference the data has.

##### StringExtensions

This is the most complicated extension, as it takes in a string, which is a line from the file, and converts it to the Football type.

This involves, using the configuration to locate the parts of the row, that we need to extract.

###### ToFootball

``` csharp
public static FootballValidatorType ToFootball(this string item)
```

If you look at the example project, you will see that the main method calls to three other private methods that help with the extraction process.

The method returns a FootballValidatorType, that includes the Football type itself, if we can extract it, and the validation errors, if it failed.

#### Football Processors

##### The Reader Implementation

As said before, the reader is just a call to the FileSystem to ```ReadAllLines()```.  The reader, passes in the instance of the FileSystem, and the file location to the static core ```Reader.ReadWork()```.

##### The Mapper Implementation

The mapper is what takes the rows of text from the file, and converts them into a processable type.

There's a lot going on in the mapper.  The static core mapper, does a ```foreach``` loop through all of the items in the data that we got from the reader.

For each of the data items, we need to do the following:

* Check that the data row in not a header, or divider.  That is where we use the private method ```CheckItemRow()```.
* Then we need to convert them to a Football type.  This is where we use the extension method.
* If the type has been converted successfully, then we add it to the results that are passed onto the writer.
* If the conversion failed, then we log the details of why it failed.

Look into the sample project and see the ```CheckItemRow()``` and ```AddDataItem()``` methods.  These are the small parts that the static core mapper requires.

##### The Writer Implementation

The writer is the most complicated part of the component.  This is because it is the object that processes the data and produces an answer.  So, most of the code in the writer is component specific, and can't be extracted into the core writer.

To see how the writer in this example was made, lets look at the original version from the Part Three exercise.  This code was the basis for this version.  I've skipped the contract requirements sections, where we throw exceptions if the input parameter is null.  This is just the main part of code to calculate the answer.

``` csharp
var teamWithSmallestPointRange = string.Empty;
var smallestRange = int.MaxValue;

foreach (var data in footballData)
{
        if (type.Data is Football football)
        {
                // Contract requirements. Duplicating the validation here. Should we?
                var footballValidationResult = football.IsValid();
                if (!footballValidationResult.IsValid)
                {
                        throw new ArgumentException(footballValidationResult.Errors.Select(m => m.ErrorMessage).ToString());
                }

                var range = football.CalculatePointDifference();

                if (range < smallestRange)
                {
                        smallestRange = range;
                        teamWithSmallestPointRange = football.TeamName;
                }
        }
}

return teamWithSmallestPointRange;
```

Things to note:

* We keep track of two variables that are set or updated from within the loop.
* We confirm the data type is the correct type.
* We validate the data.
* We make the calculation for points difference.
* We check against the variables and update as necessary.

Comparing that against the current writer and core writer, we can see there is a lot more code.

* The core writer has the default parameters passed into it, which is what we use to store the information.  This replaces the two variables from the original version.
* The core writer uses Linq to Aggregate() through the list.  This is the same as using a for each loop.
* The core does the same validation of the data.
* We calculate the points difference the same way.
* We check against the variables and update as necessary.

So:

* For each -> Linq.Aggregate()
* If type.Data is Football -> type.Data is T componentType
* IsValid() section -> ValidationConfirmation()
* range = football.CalculatePointDifference() -> pointDifference = specificType.CalculatePointDifference()
* If range < smallestRange code -> EvaluateData()

###### Writer Changes Conclusion

As you can see from the comparison, the code that was in the original writer has been extracted out into smaller methods, but it is essentially the same.

When I was going through these changes, I used the unit tests for the original writer, and ensured that they passed as I made the refactoring.  If a test failed, then I assumed my refactoring didn't work, so I changed it to make the test pass again.

## 7 - References

This section is for all of the references and NuGet packages.

### Coding Kata

[Data Munging - Kata 04](http://codekata.com/kata/kata04-data-munging/)

### GitHub Projects

[Easy.MessageHub](https://github.com/NimaAra/Easy.MessageHub)

[Fluent Assertions](https://github.com/fluentassertions/fluentassertions/issues)

[Fluent Validation](https://github.com/JeremySkinner/FluentValidation)

[NSubstitute](https://github.com/nsubstitute/NSubstitute)

[Serilog](https://github.com/serilog/serilog)

[System.IO.Abstractions](https://github.com/System-IO-Abstractions/System.IO.Abstractions)

### NuGet

[Easy.MessageHub](https://www.nuget.org/packages/Easy.MessageHub/)

[Fluent Assertions](https://www.nuget.org/packages/FluentAssertions/)

[Fluent Validation](https://www.nuget.org/packages/FluentValidation/8.5.0-preview1)

[NSubstitute](https://www.nuget.org/packages/NSubstitute/)

[Serilog](https://www.nuget.org/packages/Serilog/2.9.0-dev-01091)

[System.IO.Abstractions](https://www.nuget.org/packages/System.IO.Abstractions/)

### Web Sites

[Fluent Assertions](https://fluentassertions.com/)

[Fluent Validation](https://fluentvalidation.net/)

[NSubstitute](https://nsubstitute.github.io/)
