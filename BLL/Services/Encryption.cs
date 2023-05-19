 private string Encrypt(string data, string password, byte[] IV)
        {
            byte[] key = Encoding.UTF8.GetBytes(password);

            //AesManaged
            Aes aesManaged = Aes.Create();
            aesManaged.Key = key;
            aesManaged.IV = IV;


            MemoryStream memoryStream = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream(memoryStream, aesManaged.CreateEncryptor(), CryptoStreamMode.Write);

            byte[] dataBytes = Encoding.UTF8.GetBytes(data);
            cryptoStream.Write(dataBytes, 0, dataBytes.Length);
            cryptoStream.FlushFinalBlock();

            byte[] encryptedData = memoryStream.ToArray();
            return Convert.ToBase64String(encryptedData);

        }

        private string Decrypt(string data, string password, byte[] IV)
        {
            byte[] key = Encoding.UTF8.GetBytes(password);

            Aes aesManaged = Aes.Create();
            aesManaged.Key = key;
            aesManaged.IV = IV;

            MemoryStream memoryStream = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream(memoryStream, aesManaged.CreateDecryptor(), CryptoStreamMode.Write);

            byte[] dataBytes = Convert.FromBase64String(data);
            cryptoStream.Write(dataBytes, 0, dataBytes.Length);
            cryptoStream.FlushFinalBlock();

            byte[] decryptedData = memoryStream.ToArray();
            return UTF8Encoding.UTF8.GetString(decryptedData, 0, decryptedData.Length);


        }
