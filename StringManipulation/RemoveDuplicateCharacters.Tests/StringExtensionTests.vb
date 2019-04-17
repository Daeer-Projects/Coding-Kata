Imports FluentAssertions
Imports Xunit

Namespace RemoveDuplicateCharacters.Tests
    Public Class StringExtensionTests
        '<InlineData("AAABCDDDBCBC", "AABCDDBC")>
        <Theory()>
        <InlineData("AASDEDFFRDSSSSEDSW", "ASDEDFRDSEDSW")>
        <InlineData("FFDGDGEREGSFGGFGFFGFFFFFSSESSSSSEES", "FDGDGEREGSFGFGFGFSESES")>
        Sub Test_string_with_default_count_returns_expected(ByVal input As String, ByVal expected As String)
            ' Arrange.
            ' Act.
            Dim actual = input.RemoveDuplicateCharacters(1)

            ' Assert.
            actual.Should().Be(expected, "using the default count will return only a single character for each collection in the input string.")
        End Sub

        '<InlineData("AAABCDDDBCBC", 2, "AAABCDDDBCBC")>
        <Theory()>
        <InlineData("AASDEDFFRDSSSSEDSW", 2, "AASDEDFFRDSSEDSW")>
        <InlineData("FFDGDGEREGSFGGFGFFGFFFFFSSESSSSSEES", 3, "FFDGDGEREGSFGGFGFFGFFFSSESSSEES")>
        <InlineData("AAAAAAAAAAAAAAAAAAAA", 7, "AAAAAAA")>
        Sub Test_string_with_defined_count_unless_zero_returns_expected(ByVal input As String, ByVal count As Integer, ByVal expected As String)
            ' Arrange.
            ' Act.
            Dim actual = input.RemoveDuplicateCharacters(count)

            ' Assert.
            actual.Should().Be(expected, "using the defined count will return the amount of characters for each collection in the input string.")
        End Sub
        
        '<InlineData("AAABCDDDBCBC", 0, "ABCD")>
        <Theory()>
        <InlineData("AASDEDFFRDSSSSEDSW", 0, "ASDEDFRDSEDSW")>
        <InlineData("AASDEDFFRDSSSSEDSW", -1, "ASDEDFRDSEDSW")>
        <InlineData("AASDEDFFRDSSSSEDSW", -1000, "ASDEDFRDSEDSW")>
        Sub Test_string_with_defined_count_of_zero_or_less_returns_expected_as_count_of_one(ByVal input As String, ByVal count As Integer, ByVal expected As String)
            ' Arrange.
            ' Act.
            Dim actual = input.RemoveDuplicateCharacters(count)

            ' Assert.
            actual.Should().Be(expected, "using a defined count or zero or less will use one as the count.")
        End Sub
    End Class
End Namespace

