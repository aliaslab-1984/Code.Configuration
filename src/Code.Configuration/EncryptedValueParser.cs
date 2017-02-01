using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.IO;

namespace Code.Configuration
{
    public class EncryptedValueParser : IValueParser
    {
        protected AbstractConfigValueFactory _factory;

        public EncryptedValueParser(AbstractConfigValueFactory factory)
        {
            _factory = factory;
        }

        public bool CheckSyntax(string value)
        {
            return Regex.IsMatch(value, "^cipher:.+$");
        }

        public string Parse(string value)
        {
            string result = Regex.Match(value, "^cipher:(.+)$").Groups[1].ToString();

            using (AesManaged alg = new AesManaged())
            {
                alg.IV = Convert.FromBase64String(_factory.GetValue("constant.config.chiper.IV"));
                alg.Key = Convert.FromBase64String(_factory.GetValue("constant.config.chiper.Key"));

                alg.Mode = CipherMode.CBC;
                alg.Padding = PaddingMode.PKCS7;

                ICryptoTransform decryptor = alg.CreateDecryptor(alg.Key, alg.IV);

                using (MemoryStream msDecrypt = new MemoryStream(Convert.FromBase64String(result)))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            result = srDecrypt.ReadToEnd();
                        }
                    }
                }
            }

            return result;
        }
    }
}
