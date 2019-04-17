# String Manipulation

This is an example of a test I was given during an interview.

## Question

Given a string and an optional count value, can you process the input string
and output a single (unless defined) character for each of the characters
in the input string.

### Example

* "AASDEDFFRDSSSSEDSW" using the default count of 1, should return "ASDEDFRDSEDSW"
* "AAAASDEEEERFFDRETYTGGFDDD" using a count of 2, should return "AASDEERFFDRETYTGGFDD"
* "AAAASDEEEERFFDRETYTGGFDDD" using a count of 3, should return "AAASDEEERFFDRETYTGGFDDD"

## Attempts

I've made two attempt so far.  They are not elegant, and could be improved.

My next attempt will try to use linq.  As I have the unit tests working properly, I can attempt to see how it works.

I've added a VB version as that was how the original question was written.  However, this is a slightly different question.

## New Requirements

There is going to be a change to the question.

### The new requirement

Remove duplicate characters from a given string if they exceed the allowed number of duplicates.

### Examples

RemoveDuplicateCharacters("AAABCDDDBCBC", 0) returns "ABCD"
RemoveDuplicateCharacters("AAABCDDDBCBC", 1) returns "AABCDDBC"
RemoveDuplicateCharacters("AAABCDDDBCBC", 2) returns "AAABCDDDBCBC"

### What does this mean

The allowed count of duplicates is different from the original item count defined in the original attempt.  
It is the TOTAL amount of times a character can be returned in the output.

Even though a character can be in the input string in different places, the count still applies.