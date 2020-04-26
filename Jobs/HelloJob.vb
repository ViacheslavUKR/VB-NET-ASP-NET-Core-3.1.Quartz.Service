Imports System
Imports System.Net.Http
Imports System.Text
Imports System.Threading
Imports Microsoft.Extensions.DependencyInjection
Imports Microsoft.Extensions.Logging
Imports Newtonsoft.Json
Imports Quartz
Imports Quartz.Spi


Public Class HelloJob
    Implements IJob

    Private ReadOnly _logger As ILogger(Of HelloJob)
    Private Counter As Integer

    Public Sub New(ByVal logger As ILogger(Of HelloJob))
        _logger = logger
    End Sub

    Public Function Execute(context As IJobExecutionContext) As Task Implements IJob.Execute
        Interlocked.Increment(Counter)
        _logger.LogInformation("JobStart (" & Counter.ToString & ") " & Now.ToString)
        Dim Client As HttpClient = New HttpClient()
        Dim newCustomerInfo = New StringContent(
                JsonConvert.SerializeObject(
                        New With {
                        .Id = Counter,
                        .FirstName = context.FireInstanceId,
                        .LastName = context.JobRunTime}),
                        Encoding.UTF8, "application/json")

        Dim createCustomerResponse = Client.PostAsync("http://localhost:44347/api/customers", newCustomerInfo)
        Return Task.CompletedTask
    End Function



End Class
