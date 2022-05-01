namespace CRMContracts.Email
{
    public interface IEmailConfiguration
    {
        public string Mail { get; }
        public string DisplayName { get; }
        public string Password { get; }
        public string Host { get; }
        public string Port { get; }
    }
}
