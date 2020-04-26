Public Class JobSchedule
    Public ReadOnly Property JobType As Type
    Public ReadOnly Property CronExpression As String

    Public Sub New(ByVal _jobType As Type, ByVal _cronExpression As String)
        JobType = _jobType
        CronExpression = _cronExpression
    End Sub

End Class
