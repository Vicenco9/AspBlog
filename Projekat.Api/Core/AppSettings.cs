namespace Projekat.Api.Core
{
    public class AppSettings
    {
        public string DbConnectionString { get; set; }
        public JwtSettings JwtSettings { get; set; }
        public EmailOptions EmailOptions { get; set; }
        public string ApplicationInstance { get; set; }
    }

    public class EmailOptions
    {
        public string EmailFrom { get; set; }
        public string EmailPassword { get; set; }
        public int EmailPort { get; set; }
        public string EmailHost { get; set; }
    }
}
