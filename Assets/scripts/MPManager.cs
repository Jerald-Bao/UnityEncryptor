using Boo.Lang;
using UnityEditor;
using UnityEngine;

public class MPManager : MonoBehaviour
{
    public Material Material;

    private List<string> props;

    private List<Texture> texes;

    // Start is called before the first frame update
    void Start()
    {
        GetTexturePropertiesFromShader(Material.shader, Material);
        foreach (var prop in props)
        {
            Debug.Log(prop);
        }
        
        Debug.Log("-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-");

        UnityEngine.Object[] dependencies = EditorUtility.CollectDependencies(new UnityEngine.Object[] {Material});
        foreach (var dependency in dependencies)
        {
            if (dependency is Texture)
            {
                Debug.Log(dependency.name);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void GetTexturePropertiesFromMaterial(Material material)
    {
        string[] textures = Material.GetTexturePropertyNames();
        foreach (var texture in textures)
        {
            Debug.Log(texture);
        }
    }
    
    private void GetTexturePropertiesFromShader(Shader shader, Material material)
    {
        props = new List<string>();
        texes = new List<Texture>();
        for (int i = 0; i < ShaderUtil.GetPropertyCount(shader); i++)
        {
            if (ShaderUtil.GetPropertyType(shader, i) == ShaderUtil.ShaderPropertyType.TexEnv)
            {
                if (ShaderUtil.GetPropertyName(shader, i) != null)
                {
                    props.Add(ShaderUtil.GetPropertyName(shader, i));
                    Texture texture = material.GetTexture(ShaderUtil.GetPropertyName(shader, i));
                    texes.Add(texture);
                }
            }
        }
    }
}