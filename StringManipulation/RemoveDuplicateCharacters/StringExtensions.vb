Imports System.Runtime.CompilerServices

Public Module StringExtensions
    ''' <summary>
    ''' Removes duplicate characters from a given string if they exceed the allowed number of duplicates.
    ''' This is a different version from the C#, as we now have some different requirements.
    ''' </summary>
    ''' <param name="originalString"> String to remove duplicates from. </param>
    ''' <param name="allowedDuplicates"> Maximum number of duplicates of each character in the resulting string. </param>
    ''' <exception cref="ArgumentNullException"> originalString is null or empty. </exception>
    ''' <exception cref="ArgumentOutOfRangeException"> allowedDuplicates is less than zero. </exception>
    ''' <returns>
    ''' A string containing only the maximum number of character duplicates for each character in the string.
    ''' Examples:
    ''' RemoveDuplicateCharacters("AAABCDDDBCBC", 0) returns "ABCD"
    ''' RemoveDuplicateCharacters("AAABCDDDBCBC", 1) returns "AABCDDBC"
    ''' RemoveDuplicateCharacters("AAABCDDDBCBC", 2) returns "AAABCDDDBCBC"
    ''' </returns>
    <Extension()> 
    Public Function RemoveDuplicateCharacters(ByVal originalString As String, ByVal allowedDuplicates As Integer) As String
        Dim result = string.Empty
        Dim count = 1
        ' ToDo: Update based on new requirements.

        For Each element As Char In originalString
            If (result.Any() AndAlso result.Last() = element) Then
                If (count < allowedDuplicates) Then
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
