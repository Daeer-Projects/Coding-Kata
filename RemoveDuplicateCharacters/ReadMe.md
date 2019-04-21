# Remove Duplicate Characters

This project is the C# version of a test I was given during an interview.

## Question

Given a string as an input, return a version of that string, but with duplicates removed.  The amount of duplicates allowed
will be configurable as an input to the method.

### Examples

Here are the examples of inputs and expected outputs.  These are the requirements of the method.

* RemoveDuplicateCharacters("AAABCDDDBCBC", 0) returns "ABCD"
* RemoveDuplicateCharacters("AAABCDDDBCBC", 1) returns "AABCDDBC"
* RemoveDuplicateCharacters("AAABCDDDBCBC", 2) returns "AAABCDDDBCBC"

## Solution

### Contract Requirements.

We needed to ensure, two things.

1. If the input string is null or empty, we throw an argument null exception.

As I have made this method an extension, it will always throw a null exception when you attempt to use it.  All, I had to do, was ensure the 
input string was not empty.

2. If the allowed duplicates value is less than zero, we need to throw an argument out of range exception.

### Total Count

As we are allowing duplicates, this means, we need to keep a count of the characters used.  It is easier to have a total count, rather than
the allowed duplicates plus one.

I set the total value right at the start, which means, I don't have to keep re-calculating it.

### Loops.

I needed to keep track of the characters used, and their counts.  To store this information, I went with a dictionary of char and int.

I added to the dictionary, as I found a new character, or I updated the count, if I had already encountered it before.

Before I added the character to the result, I checked to see if it had reached the total allowed.

If the character hadn't reached the limit, I added it to the resultant string, otherwise, I ignored it.

### Other characters

The original question only had characters, I have expanded the code and tests to be able to de-duplicate symbols and numbers.

## Other Questions

What should we do if we don't care about case sensitivity?