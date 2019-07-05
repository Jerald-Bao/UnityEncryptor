using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Starter : MonoBehaviour
{
    [SerializeField] private RawImage image;
    private string filename = "/test.png";
    private string encrytedFilename = "/encrypted.upg";
    // Start is called before the first frame update
    void Start()
    {
        //string filepath = Application.dataPath + filename;
        //Texture2D tex = Loader.LoadTexture(filepath);

        string filepath = Application.dataPath + encrytedFilename;
        Texture2D tex = Loader.LoadEncryptedTexture(filepath);
        image.texture = tex;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
