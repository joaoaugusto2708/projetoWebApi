namespace projetoWebApi.Logging
{
    //Define a configuração do Provedor de logs personalizados
    public class CustomLoggerProviderConfiguration
    {
        public LogLevel LogLevel { get; set; } = LogLevel.Warning;
        public int EventId { get; set; }
    }
}
