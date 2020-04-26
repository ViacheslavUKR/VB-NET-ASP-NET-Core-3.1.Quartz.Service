Imports Microsoft.Extensions.DependencyInjection
Imports Quartz
Imports Quartz.Spi
Imports System

Public Class JobFactory
    Implements IJobFactory

    Private ReadOnly _serviceProvider As IServiceProvider

    Public Sub New(ByVal serviceProvider As IServiceProvider)
        _serviceProvider = serviceProvider
    End Sub

    Private Function NewJob(bundle As TriggerFiredBundle, scheduler As IScheduler) As IJob Implements IJobFactory.NewJob
        Return TryCast(_serviceProvider.GetRequiredService(bundle.JobDetail.JobType), IJob)
    End Function

    Private Sub ReturnJob(job As IJob) Implements IJobFactory.ReturnJob
        'we let the DI container handler this
    End Sub
End Class

