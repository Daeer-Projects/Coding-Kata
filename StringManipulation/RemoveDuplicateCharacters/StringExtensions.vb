Imports System.Runtime.CompilerServices

Public Module StringExtensions
    ''' A collection of string extensions.
    ''' This Is taken from the test example I was asked during my interview.
    '''
    ''' Given a string And an optional count value, can you process the input string
    ''' And output a single (unless defined) character for each of the characters
    ''' in the input string.
    ''' <param name="inputString"> The input that we are processing. </param>
    ''' <param name="itemCount"> The count of characters we can accept before we ignore the character. </param>
    ''' <exception cref="ArgumentNullException"> inputString is null or empty. </exception>
    ''' <exception cref="ArgumentOutOfRangeException"> itemCount is less than zero. </exception>
    ''' <returns>
    ''' The processed string.
    ''' Examples:
    ''' "AASDEDFFRDSSSSEDSW" using the default count of 1, should return "ASDEDFRDSEDSW"
    ''' "AAAASDEEEERFFDRETYTGGFDDD" using a count of 2, should return "AASDEERFFDRETYTGGFDD"
    ''' "AAAASDEEEERFFDRETYTGGFDDD" using a count of 3, should return "AAASDEEERFFDRETYTGGFDDD"
    ''' </returns>
    <Extension()>
    Public Function Manipulate(ByVal inputString As String, ByVal itemCount As Integer) As String
        Dim result = String.Empty
        Dim count = 1
        ' ToDo: Update based on new requirements.

        For Each element As Char In inputString
            If (result.Any() AndAlso result.Last() = element) Then
                If (count < itemCount) Then
                    result += element
                End If
                count += 1
            Else
                result += element
                count = 1
            End If
        Next

        Return result
    End Function
End Module
