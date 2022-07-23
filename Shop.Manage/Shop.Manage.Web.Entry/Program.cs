using Serilog;

var builder = WebApplication.CreateBuilder(args).Inject();
builder.UseSerilogDefault(config =>
{
    string date = DateTime.Now.ToString("yyyy-MM-dd");//��ʱ�䴴���ļ���
    string outputTemplate = "{NewLine}����:    {Timestamp:yyyy-MM-dd HH:mm:ss.fff} {NewLine}" +
                             "��־�ȼ�:{Level}{NewLine}" +
                             "��־����:{Message}{NewLine}" +
                             new string('-', 100) +
                             "{NewLine}";
    // ��־���������̨
    config.WriteTo.Console(outputTemplate: outputTemplate);

    config.WriteTo.File($"log/{date}.log",
                        outputTemplate: outputTemplate,
                        restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Information,
                        rollingInterval: RollingInterval.Day,
                        encoding: System.Text.Encoding.UTF8);

});
var app = builder.Build();
app.Run();