Imports System
Imports System.Collections.Generic
Imports System.Threading
Imports System.Threading.Tasks
Imports Microsoft.Extensions.Hosting
Imports Quartz
Imports Quartz.Spi

Public Class QuartzHostedService
    Implements IHostedService

    Private ReadOnly _schedulerFactory As ISchedulerFactory
    Private ReadOnly _jobFactory As IJobFactory
    Private ReadOnly _jobSchedules As IEnumerable(Of JobSchedule)

    Public Sub New(ByVal schedulerFactory As ISchedulerFactory, ByVal jobSchedules As IEnumerable(Of JobSchedule), ByVal jobFactory As IJobFactory)
        _schedulerFactory = schedulerFactory
        _jobSchedules = jobSchedules
        _jobFactory = jobFactory
    End Sub

    Public Property Scheduler As IScheduler

    Private Async Function StartAsync(cancellationToken As CancellationToken) As Task Implements IHostedService.StartAsync
        Scheduler = Await _schedulerFactory.GetScheduler(cancellationToken)
        Scheduler.JobFactory = _jobFactory

        For Each jobSchedule In _jobSchedules
            Dim job = CreateJob(jobSchedule)
            Dim trigger = CreateTrigger(jobSchedule)
            Await Scheduler.ScheduleJob(job, trigger, cancellationToken)
        Next

        Await Scheduler.Start(cancellationToken)
    End Function

    Private Async Function StopAsync(cancellationToken As CancellationToken) As Task Implements IHostedService.StopAsync
        Await Scheduler?.Shutdown(cancellationToken)
    End Function

    Private Shared Function CreateTrigger(ByVal schedule As JobSchedule) As ITrigger
        Return TriggerBuilder.Create().WithIdentity($"{schedule.JobType.FullName}.trigger").WithCronSchedule(schedule.CronExpression).WithDescription(schedule.CronExpression).Build()
    End Function

    Private Shared Function CreateJob(ByVal schedule As JobSchedule) As IJobDetail
        Dim jobType = schedule.JobType
        Return JobBuilder.Create(jobType).WithIdentity(jobType.FullName).WithDescription(jobType.Name).Build()
    End Function

End Class
