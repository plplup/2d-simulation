using System;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

//Incorporated pre-written code
public class BaseScriptableObject : ScriptableObject
{
    [Tooltip("Item ID")]
    [SerializeField] private string id;
    public string Id => id;

#if UNITY_EDITOR
    [ContextMenu("Generate ID")]
    private void GenerateGuid()
    {
        id = Guid.NewGuid().ToString();
        EditorUtility.SetDirty(this);
    }
#endif

    public void SetId(string id)
    {
        this.id = id;
    }
}