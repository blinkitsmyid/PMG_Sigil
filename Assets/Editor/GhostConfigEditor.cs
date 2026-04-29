using UnityEditor;
using UnityEngine;

public class GhostConfigEditor
{
    [MenuItem("Assets/Create/Ghost/Create Basic Ghost Config")]
    public static void CreateGhostConfig()
    {
        GhostConfig asset = ScriptableObject.CreateInstance<GhostConfig>();

        AssetDatabase.CreateAsset(asset, "Assets/NewGhostConfig.asset");
        AssetDatabase.SaveAssets();

        EditorUtility.FocusProjectWindow();
        Selection.activeObject = asset;

        Debug.Log("Создан новый GhostConfig");
    }
}