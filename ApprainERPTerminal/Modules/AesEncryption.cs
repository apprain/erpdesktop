using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace ApprainERPTerminal.Modules
{

    public class AesEncryption
    {
        // Derive 32-byte key from password using SHA-256 (same as PHP)
        private static byte[] DeriveKey(string password)
        {
            using var sha256 = SHA256.Create();
            return sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
        }

        // Encrypt: returns raw byte array [IV + HMAC + Ciphertext]
        public static byte[] Encrypt(string plainText, string password)
        {
            byte[] key = DeriveKey(password);
            byte[] iv = RandomNumberGenerator.GetBytes(16);
            byte[] encrypted;

            using (Aes aes = Aes.Create())
            {
                aes.Key = key;
                aes.IV = iv;
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;

                using var ms = new MemoryStream();
                using var encryptor = aes.CreateEncryptor();
                using var cryptoStream = new CryptoStream(ms, encryptor, CryptoStreamMode.Write);
                using (var sw = new StreamWriter(cryptoStream))
                {
                    sw.Write(plainText);
                }
                encrypted = ms.ToArray();
            }

            // HMAC = SHA256(ciphertext + IV)
            byte[] dataToHash = new byte[encrypted.Length + iv.Length];
            Buffer.BlockCopy(encrypted, 0, dataToHash, 0, encrypted.Length);
            Buffer.BlockCopy(iv, 0, dataToHash, encrypted.Length, iv.Length);

            using var hmac = new HMACSHA256(key);
            byte[] hmacBytes = hmac.ComputeHash(dataToHash);

            // Final output: IV + HMAC + Ciphertext
            byte[] output = new byte[iv.Length + hmacBytes.Length + encrypted.Length];
            Buffer.BlockCopy(iv, 0, output, 0, iv.Length);
            Buffer.BlockCopy(hmacBytes, 0, output, iv.Length, hmacBytes.Length);
            Buffer.BlockCopy(encrypted, 0, output, iv.Length + hmacBytes.Length, encrypted.Length);

            return output;
        }

        // Decrypt
        public static string Decrypt(byte[] input, string password)
        {
            byte[] key = DeriveKey(password);
            byte[] iv = new byte[16];
            byte[] hmac = new byte[32];
            byte[] cipherText = new byte[input.Length - 48];

            Buffer.BlockCopy(input, 0, iv, 0, 16);
            Buffer.BlockCopy(input, 16, hmac, 0, 32);
            Buffer.BlockCopy(input, 48, cipherText, 0, cipherText.Length);

            // Recalculate HMAC and verify
            byte[] dataToHash = new byte[cipherText.Length + iv.Length];
            Buffer.BlockCopy(cipherText, 0, dataToHash, 0, cipherText.Length);
            Buffer.BlockCopy(iv, 0, dataToHash, cipherText.Length, iv.Length);

            using var hmacSha256 = new HMACSHA256(key);
            byte[] computedHmac = hmacSha256.ComputeHash(dataToHash);

            if (!CryptographicOperations.FixedTimeEquals(hmac, computedHmac))
            {
                return null; // Authentication failed
            }

            using (Aes aes = Aes.Create())
            {
                aes.Key = key;
                aes.IV = iv;
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;

                using var ms = new MemoryStream(cipherText);
                using var decryptor = aes.CreateDecryptor();
                using var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read);
                using var sr = new StreamReader(cs);
                return sr.ReadToEnd();
            }
        }

        // Example Usage
       /* public static void Main()
        {
            string password = "your-password";
            string plaintext = "Hello AES with HMAC!";

            byte[] encrypted = Encrypt(plaintext, password);
            string decrypted = Decrypt(encrypted, password);

            Console.WriteLine("Encrypted (base64): " + Convert.ToBase64String(encrypted));
            Console.WriteLine("Decrypted: " + decrypted);
        }*/
    }

}
