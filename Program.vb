Imports System
Imports System.Collections.Generic
Imports System.IO
Imports System.Linq
Imports System.Threading.Tasks
Imports Microsoft.AspNetCore.Hosting
Imports Microsoft.Extensions.Configuration
Imports Microsoft.Extensions.Hosting
Imports Microsoft.Extensions.Logging


Module Program
    Public Sub Main(args As String())
        CreateHostBuilder(args).Build().Run()
    End Sub

    Public Function CreateHostBuilder(ByVal args As String()) As IHostBuilder
        Return Host.
            CreateDefaultBuilder(args).
            ConfigureWebHostDefaults(Function(webBuilder)
                                         Return webBuilder.UseStartup(Of Startup)()
                                     End Function)
    End Function

End Module
