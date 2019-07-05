using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class Loader
{
    public static Texture2D LoadEncryptedTexture(string filepath)
    {
        Texture2D tex = null;
        byte[] fileData;
        if (File.Exists(filepath))
        {
            fileData = File.ReadAllBytes(filepath);
            byte[] decryptedData = Protector.Instance.Decrypt(fileData);
            tex = new Texture2D(2, 2);
            tex.LoadImage(decryptedData);
        }
        return tex;
    }
    public static Texture2D LoadTexture(string filePath)
    {

        Texture2D tex = null;
        byte[] fileData;
        if (File.Exists(filePath))
        {
            fileData = File.ReadAllBytes(filePath);
            tex = new Texture2D(2, 2);

            tex=LoadFromByteArray<Texture2D>(fileData);
        }
        return tex;
    }
    public static T LoadFromByteArray<T>(Byte[] rawData)
    {
        BinaryFormatter bf = new BinaryFormatter();
        using(MemoryStream ms = new MemoryStream(rawData))
        {
            object obj = bf.Deserialize(ms);
            return (T)obj;
        }
    }
}
