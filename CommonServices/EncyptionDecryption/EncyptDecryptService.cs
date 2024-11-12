using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CommonServices.EncyptionDecryption
{
    public class EncyptDecryptService : IEncyptDecryptService
    {
        private readonly IConfiguration _configuration;
        private readonly string? _initializationVector;
        private readonly string? _encryptionKey;

        public EncyptDecryptService(IConfiguration configuration)
        {
            _configuration = configuration;
            _initializationVector = _configuration.GetSection("Encryption:Iv").Value;
            _encryptionKey = _configuration.GetSection("Encryption:Key").Value;
        }


        public string DecryptPayload(string encryptedText)
        {
            byte[] key = Encoding.UTF8.GetBytes(_encryptionKey);
            byte[] iv = Encoding.UTF8.GetBytes(_initializationVector);

            using (var aes = new AesManaged())
            {
                aes.Key = key;
                aes.IV = iv;
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;

                // Convert the encrypted data from a hexadecimal string to a byte array
                byte[] encryptedBytes = HexStringToByteArray(encryptedText);
                byte[] decryptedBytes = aes.CreateDecryptor().TransformFinalBlock(encryptedBytes, 0, encryptedBytes.Length);

                return Encoding.UTF8.GetString(decryptedBytes);
            }
        }

        public string EncryptPayload(string plainText)
        {
            byte[] key = Encoding.UTF8.GetBytes(_encryptionKey);
            byte[] iv = Encoding.UTF8.GetBytes(_initializationVector);

            using (var aes = new AesManaged())
            {
                aes.Key = key;
                aes.IV = iv;
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;

                byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);
                byte[] encryptedBytes = aes.CreateEncryptor().TransformFinalBlock(plainTextBytes, 0, plainTextBytes.Length);

                return BitConverter.ToString(encryptedBytes).Replace("-", "");
            }
        }

        private byte[] HexStringToByteArray(string hex)
        {
            int numHexChars = hex.Length;
            byte[] bytes = new byte[numHexChars / 2];
            for (int i = 0; i < numHexChars; i += 2)
            {
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            }
            return bytes;
        }
    }
}
