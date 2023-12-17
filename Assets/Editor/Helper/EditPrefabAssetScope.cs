using UnityEngine;
using System;
using UnityEditor;

/*
 * Wraps editing prefabs functionality so you don't have to write the boilerplate
 * And its less confusing, see example usage at bottom
 */

public class EditPrefabAssetScope : IDisposable {
 
    public readonly string assetPath;
    public readonly GameObject prefabRoot;
 
    public EditPrefabAssetScope(string assetPath) {
        this.assetPath = assetPath;
        prefabRoot = PrefabUtility.LoadPrefabContents(assetPath);
    }
 
    public void Dispose() {
        PrefabUtility.SaveAsPrefabAsset(prefabRoot, assetPath);
        PrefabUtility.UnloadPrefabContents(prefabRoot);
    }
}

/*
 * Example Usage:
 *
 [MenuItem("Examples/Add BoxCollider to Prefab Asset")]
 static void AddBoxColliderToPrefab()
 {
     // Get the Prefab Asset root GameObject and its asset path.
     GameObject assetRoot = Selection.activeObject as GameObject;
     string assetPath = AssetDatabase.GetAssetPath(assetRoot);

     // Modify prefab contents and save it back to the Prefab Asset
     using (var editScope = new EditPrefabAssetScope(assetPath))
     {
         editScope.prefabRoot.AddComponent<BoxCollider>();
     }
 }
 *
 */