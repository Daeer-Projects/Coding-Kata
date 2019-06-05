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

``` chsarp
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


#### Solution

The writer is a static writer class in the core project.

So, to call the core notify, we would use code like this:

``` csharp
    var result = await Writer.WriteWork<Football, int, string>(data, (int.MaxValue, string.Empty), CurrentRange)
        .ConfigureAwait(false);
```

The notify interface looks like this:

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

Currenlty I have:

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

* 1 -Introduction
* 2 - Solution Parts
* 3 - Example Project
* 4 - Core
* 5 - Component
* 6 - Main Program


## 1 - Introduction

Welcome to the Data Munging Core project.

This project is to give you the consumer, a centralised system for processing files and producing an answer.  The file composition, data structure, and data analysis is up to you.

### Basis

The basis of this project is based on the coding kata, "Data Munging".  This kata is to process two different file types and return an answer.  It was divided up into three parts.

> Part One: Extract data from a Weather.dat file and find out which day had the least temperature change.

> Part Two: Extract data from a Football.dat file and find out which team has the smallest difference between for and against goals.

> Part Three: Take the two programs and extract duplicated code into a core project that could be used by both components.


## 2 - Solution Parts

The project that you will create is up to you.  What this core project requires of you to set up are the following:

> 1. Core - instantiate and set up the core objects.
> * Event - Message Hub.
> * Component Register.

> 3. Logging - Serilog.

> 2. Components - create the components.

> 3. Main Solution - add / reference the components and core in the main solution.


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


## 3 - Example Project

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



## 4 - Core

#### Component Register

The main part of the core is:

```ComponentRegister.cs```

This is the object that is created with the MessageHub and Logger.  Once created the components can be registered.

The registration process creates the component, and then sets up a subscription and the method the component will execute when that event is raised.

The component register will then set up a subscription for the results that the components raise when they have completed their processing.

##### Creation

To create an instance of the ComponentRegister:

``` csharp
var componentRegister = new ComponentRegister(hub, coreLogger);
```

##### Register Component

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

##### Register Subscriptions

The ComponentRegister will need to be set up to handle any results raised by the components.

``` csharp
componentRegister.RegisterSubscriptions();
```

