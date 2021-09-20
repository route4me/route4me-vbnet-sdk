Imports Xunit

Namespace Route4MeSdkV5UnitTest.V5

    Public NotInheritable Class IgnoreOnWindowsFactAttribute
        Inherits FactAttribute

        Public Sub New()
            If ApiKeys.actualApiKey = ApiKeys.demoApiKey Then
                Skip = "Cannot run the tests with demo API key - change it with your own"
            End If
        End Sub
    End Class

End Namespace


