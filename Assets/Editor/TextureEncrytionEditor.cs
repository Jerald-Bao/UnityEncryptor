using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class TextureEncrytionEditor : Editor
{
    [MenuItem("Tools/Encryt Test Image")]
    private static void Encrypt()
    {
        string filename = "/test.png";
        string outputFilename = "/encrypted.upg";
        string filepath = Application.dataPath + filename;
        string outputFilepath = Application.dataPath + outputFilename;
        byte[] fileData;
        if (File.Exists(filepath))
        {
            fileData = File.ReadAllBytes(filepath);
            byte[] cipherData = Protector.Instance.Encrypt(fileData);
            File.WriteAllBytes(outputFilepath, cipherData);
        }
        else
        {
            Debug.LogError("Failed to encryt image.");
        }
    }

    [MenuItem("Tools/Decrypt Test Image")]
    private static void Decrypt()
    {
        string filename = "/encrypted.upg";
        string outputFilename = "/output.png";
        string filepath = Application.dataPath + filename;
        string outputFilepath = Application.dataPath + outputFilename;
        byte[] fileData;
        if (File.Exists(filepath))
        {
            fileData = File.ReadAllBytes(filepath);
            byte[] cipherData = Protector.Instance.Decrypt(fileData);
            File.WriteAllBytes(outputFilepath, cipherData);
        }
        else
        {
            Debug.LogError("Failed to decrypt image.");
        }
    }
}
