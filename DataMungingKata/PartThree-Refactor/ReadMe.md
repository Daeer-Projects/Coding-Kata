# Refactored Version

Due to the nature of what I am trying to achieve in this solution, I think it deserved a read me of its own.

## Purpose

In this solution, I am trying to move more of the code from the components to the core project.  As they stood at the end of part three, there was a lot of code that was required to be written for the components.

## What I want to achieve

I would like to do the following:

1. Extract the code from the readers, mappers and notifiers into the core project.
2. I want to create a generic IsValid() method that the components can use specifying the validator required.


### Readers


### Mappers


### Notifiers


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

