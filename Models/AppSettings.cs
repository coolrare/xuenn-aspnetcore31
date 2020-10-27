namespace api1.Models
{
    public interface IAppSettings
    {
        int TimeoutSeconds { get; set; }
        string PartnerUrlBase { get; set; }
    }

    public class AppSettings : IAppSettings
    {
        public int TimeoutSeconds { get; set; }

        public string PartnerUrlBase { get; set; }
    }

}