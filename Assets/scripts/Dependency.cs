using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Dependency: EditorWindow
{
    static GameObject obj = null;


    [MenuItem("Example/Collect Dependencies")]
    static void Init()
    {
        // Get existing open window or if none, make a new one:
        Dependency window = (Dependency)EditorWindow.GetWindow(typeof(Dependency));
        window.Show();
    }

    void OnGUI()
    {
        obj = EditorGUI.ObjectField(new Rect(3, 3, position.width - 6, 20), "Find Dependency", obj, typeof(GameObject)) as GameObject;

        if (obj)
        {
            var dependency = new Dictionary<int, Object>();
            if (GUI.Button(new Rect(3, 25, position.width - 6, 20), "Check Dependencies"))
            {
                CollectDependanciesRecursive(obj, ref dependency);
                foreach (var dep in dependency)
                {
                    Debug.Log(dep);
                }
            }
        }
        else
            EditorGUI.LabelField(new Rect(3, 25, position.width - 6, 20), "Missing:", "Select an object first");
    }

    void OnInspectorUpdate()
    {
        Repaint();
    }
    static public void CollectDependanciesRecursive( UnityEngine.Object obj, ref Dictionary<int, Object> dependencies)
    {
        if (!dependencies.ContainsKey(obj.GetHashCode()))
        {
            
            dependencies.Add(obj.GetHashCode(), obj);
            
            SerializedObject objSO = new SerializedObject(obj);
            SerializedProperty property = objSO.GetIterator();
                
            do
            {
                if ((property.propertyType == SerializedPropertyType.ObjectReference)  
                    &&(property.objectReferenceValue != null)  
                    ) //Don't go back up the hierarchy in transforms
                {
                    CollectDependanciesRecursive(property.objectReferenceValue, ref dependencies);

                    if (obj.GetType() == typeof(Material))
                    {
                        var i = 0;
                        i++;
                    }
                }
            } while (property.Next(true));
   
        }
    }
}