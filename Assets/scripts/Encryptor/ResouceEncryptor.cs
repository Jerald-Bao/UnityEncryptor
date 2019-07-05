using System.Collections;
using System.Collections.Generic;
using System.Net.Security;
using UnityEngine;
using System.Reflection;

public class ResourceEncryptor
{
    public static EncryptionInfo Encrypt(GameObject go)
    {
        
        var components = go.GetComponents<Component>();
        foreach (var component in components)
        {
            if (component is AudioSource)
            {
                var audioSource = (AudioSource) component;
                float[] audioData;
                if (audioSource.clip != null)
                {
                }

                //if (audioSource.)
            }
        }

        return null;
    }
}
