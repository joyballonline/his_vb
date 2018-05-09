Public Class PreviewForm
    Public ReadOnly Property viewer As GrapeCity.ActiveReports.Viewer.Win.Viewer
        Get
            Return Me.arvMain
        End Get
    End Property
End Class