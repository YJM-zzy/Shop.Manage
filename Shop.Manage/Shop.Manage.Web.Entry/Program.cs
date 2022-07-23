using Serilog;

var builder = WebApplication.CreateBuilder(args).Inject();
builder.UseSerilogDefault(config =>
{
    string date = DateTime.Now.ToString("yyyy-MM-dd");//按时间创建文件夹
    string outputTemplate = "{NewLine}日期:    {Timestamp:yyyy-MM-dd HH:mm:ss.fff} {NewLine}" +
                             "日志等级:{Level}{NewLine}" +
                             "日志内容:{Message}{NewLine}" +
                             new string('-', 100) +
                             "{NewLine}";
    // 日志输出到控制台
    config.WriteTo.Console(outputTemplate: outputTemplate);

    config.WriteTo.File($"log/{date}.log",
                        outputTemplate: outputTemplate,
                        restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Information,
                        rollingInterval: RollingInterval.Day,
                        encoding: System.Text.Encoding.UTF8);

});
var app = builder.Build();
app.Run();