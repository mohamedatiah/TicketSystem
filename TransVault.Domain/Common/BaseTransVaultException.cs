namespace TransVault.Domain.Common
{
    public class BaseTransVaultException : Exception
    {

        public BaseTransVaultException(string message) : base(message) { }
        public BaseTransVaultException(string message, Exception innerException) : base(message, innerException) { }
    }
}
