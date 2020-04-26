Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Threading.Tasks
Imports Microsoft.AspNetCore.Builder
Imports Microsoft.AspNetCore.Hosting
Imports Microsoft.AspNetCore.Mvc
Imports Microsoft.Extensions.Configuration
Imports Microsoft.Extensions.DependencyInjection
Imports Microsoft.Extensions.Logging
Imports Microsoft.Extensions.Options
Imports Microsoft.AspNetCore.Authentication
Imports Microsoft.Extensions.Hosting
Imports Microsoft.EntityFrameworkCore
Imports Microsoft.AspNetCore.Mvc.ModelBinding.Binders
Imports Quartz.Spi
Imports Quartz
Imports Quartz.Impl
Imports Microsoft.AspNetCore.Http

Public Class Startup
    Public Sub New(ByVal configuration As IConfiguration)
        configuration = configuration
    End Sub

    Public ReadOnly Property Configuration As IConfiguration

    Public Sub ConfigureServices(ByVal services As IServiceCollection)

        services.AddSingleton(Of IJobFactory, JobFactory)

        services.AddSingleton(Of ISchedulerFactory, StdSchedulerFactory)

        services.AddHostedService(Of QuartzHostedService)()

        services.AddSingleton(Of HelloJob)

        Dim MyJob = New JobSchedule(GetType(HelloJob), "0/10 * * * * ?") 'every 10 seconds
        services.AddSingleton(MyJob)

    End Sub

    Public Sub Configure(ByVal app As IApplicationBuilder, ByVal env As IHostEnvironment)
        If env.IsDevelopment() Then
            app.UseDeveloperExceptionPage()
        End If

        app.Run(Function(context)
                    Return context.Response.WriteAsync("JobSchedule Started.")
                End Function)
    End Sub
End Class

