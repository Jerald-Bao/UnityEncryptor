using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

public class Protector
{
    //private const int blockSize = 128;
    //private const PaddingMode paddingMode = PaddingMode.ANSIX923;
    //private const CipherMode cipherMode = CipherMode.CBC;
    //private const int keySize =  ;

    private static byte[] Key;
    private static byte[] IV;

    private static string keyfilename = "/key.aes";
    private static string ivfilename = "/iv.aes";

    private static Protector _instance;
    public static Protector Instance
    {
        get
        {
            if (_instance == null)
            {
                string keypath = Application.dataPath + keyfilename;
                string ivpath = Application.dataPath + ivfilename;
                if (File.Exists(keypath) && File.Exists(ivpath))
                {
                    byte[] key = File.ReadAllBytes(keypath);
                    byte[] iv = File.ReadAllBytes(ivpath);
                    _instance = new Protector(key, iv);
                }
                else
                {
                    using (RijndaelManaged myRijndael = new RijndaelManaged())
                    {
                        myRijndael.GenerateKey();
                        myRijndael.GenerateIV();
                        _instance = new Protector(myRijndael.Key, myRijndael.IV);
                        File.WriteAllBytes(keypath, myRijndael.Key);
                        File.WriteAllBytes(ivpath, myRijndael.IV);
                    }
                }
            }
            return _instance;
        }
    }

    private Protector(byte[] encryptionKey, byte[] encryptionIV)
    {
        Key = encryptionKey;
        IV = encryptionIV;
    }
    
    public byte[] Encrypt(byte[] encryptData)
    {
        return Encrypt(encryptData, Key, IV);
    }

    private byte[] Encrypt(byte[] encryptData, byte[] encryptionKey, byte[] encryptionIV)
    {
        if (encryptData == null)
            return null;

        if (encryptionKey == null || encryptionKey.Length <= 0)
            return null;

        if (encryptionIV == null || encryptionIV.Length <= 0)
            return null;

        using (var rijnDeal = new RijndaelManaged())
        {
            ////Define encryption options
            //rijnDeal.BlockSize = blockSize;
            //rijnDeal.Padding = paddingMode;
            //rijnDeal.Mode = cipherMode;
            //rijnDeal.KeySize = keySize;
            rijnDeal.Key = encryptionKey;
            rijnDeal.IV = encryptionIV;

            //Create Memory Stream & Encryptor
            using (var buffer = new MemoryStream())
            {
                var encryptor = rijnDeal.CreateEncryptor();
                using (var cryptoStream = new CryptoStream(buffer, encryptor, CryptoStreamMode.Write))
                {
                    cryptoStream.Write(encryptData, 0, encryptData.Length);
                    cryptoStream.FlushFinalBlock();

                    return buffer.ToArray() ?? default(byte[]);
                }
            }
        }
    }

    public byte[] Decrypt(byte[] cipherData)
    {
        return Decrypt(cipherData, Key, IV);
    }

    private byte[] Decrypt(byte[] cipherData, byte[] encryptionKey, byte[] encryptionIV)
    {
        if (cipherData == null || cipherData.Length <= 0)
            return null;

        if (encryptionKey == null || encryptionKey.Length <= 0)
            return null;

        if (encryptionIV == null || encryptionIV.Length <= 0)
            return null;

        using (var rijnDeal = new RijndaelManaged())
        {
            ////Define encryption options
            //rijnDeal.BlockSize = blockSize;
            //rijnDeal.Padding = paddingMode;
            //rijnDeal.Mode = cipherMode;
            //rijnDeal.KeySize = keySize;
            //rijnDeal.Key = Encoding.UTF8.GetBytes(encryptionkey);

            rijnDeal.Key = encryptionKey;
            rijnDeal.IV = encryptionIV;

            //Create Memory Stream & Decryptor
            using (var decryptMemoryStream = new MemoryStream(cipherData))
            {
                var decryptor = rijnDeal.CreateDecryptor();
                using (var cryptoStream = new CryptoStream(decryptMemoryStream, decryptor, CryptoStreamMode.Read))
                {
                    using (StreamReader stream = new StreamReader(cryptoStream))
                    {
                        byte[] plainBytes = new byte[cipherData.Length];
                        int DecryptedCount = cryptoStream.Read(plainBytes, 0, plainBytes.Length);
                        return plainBytes;
                    }
                }
            }
        }
    }
}
