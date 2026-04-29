#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace DragDropGame.Editor
{
    public static class PatternConfigCreator
    {
        [MenuItem("Assets/Create/MiniGame/Pattern")]
        public static void CreatePattern()
        {
            PatternConfig asset = ScriptableObject.CreateInstance<PatternConfig>();
            asset.patternID = GetNextPatternID();
            asset.patternName = "New Pattern";

            string path = AssetDatabase.GenerateUniqueAssetPath("Assets/NewPattern.asset");
            AssetDatabase.CreateAsset(asset, path);
            AssetDatabase.SaveAssets();
            EditorUtility.FocusProjectWindow();
            Selection.activeObject = asset;
        }

        private static int GetNextPatternID()
        {
            // Простейший способ — считать все ассеты и найти максимальный ID
            string[] guids = AssetDatabase.FindAssets("t:PatternConfig");
            int maxId = 0;
            foreach (string guid in guids)
            {
                string path = AssetDatabase.GUIDToAssetPath(guid);
                PatternConfig pattern = AssetDatabase.LoadAssetAtPath<PatternConfig>(path);
                if (pattern != null && pattern.patternID > maxId)
                    maxId = pattern.patternID;
            }
            return maxId + 1;
        }
    }
}
#endif