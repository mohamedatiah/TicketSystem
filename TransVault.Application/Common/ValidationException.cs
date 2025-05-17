using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransVault.Domain.Common;

namespace TransVault.Application.Common
{
    public class ValidationErrorException : BaseTransVaultException
    {
        public ValidationErrorException(string message) : base(message)
        {
        }
        public ValidationErrorException(string message, string context) : base(message, context)
        {
        }
        public ValidationErrorException(string message, string context, Exception innerException) : base(message, context, innerException)
        {
        }
    }
}
