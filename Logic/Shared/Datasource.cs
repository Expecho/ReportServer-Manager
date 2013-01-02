namespace ReportingServerManager.Logic.Shared
{
    public class Datasource
    {
        public string Name { get; set; }
        public string ConnectionString { get; set; }
        public CredentialRetrievalTypes CredentialRetrievalType { get; set; }
        public bool UsePromptedCredentialsAsWindowsCredentials { get; set; }
        public string Prompt { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Extension { get; set; }
        public bool Enabled { get; set; }
        public bool UseStoredCredentialsAsWindowsCredentials { get; set; }
        public bool SetExecutionContext { get; set; }
    }
}