namespace ReportingServerManager.Logic.Shared
{
    public struct Datasource
    {
        public string Name;
        public string ConnectionString;
        public CredentialRetrievalTypes CredentialRetrievalType;
        public bool UsePromptedCredentialsAsWindowsCredentials;
        public string Prompt;
        public string Username;
        public string Password;
        public string Extension;
        public bool Enabled;
        public bool UseStoredCredentialsAsWindowsCredentials;
        public bool SetExecutionContext;
    }
}