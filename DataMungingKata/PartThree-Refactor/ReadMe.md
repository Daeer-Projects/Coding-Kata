# Refactored Version

Due to the nature of what I am trying to achieve in this solution, I think it deserved a read me of its own.

## Purpose

In this solution, I am trying to move more of the code from the components to the core project.  As they stood at the end of part three, there was a lot of code that was required to be written for the components.

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

