using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonServices.EncyptionDecryption
{
    public interface IEncyptDecryptService
    {
        string EncryptPayload(string encryptedText);
        string DecryptPayload(string encryptedText);
    }
}
