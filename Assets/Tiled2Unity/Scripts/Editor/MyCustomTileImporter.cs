using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;

[Tiled2Unity.CustomTiledImporter]
class MyCustomTileImporter : Tiled2Unity.ICustomTiledImporter
{
    public void HandleCustomProperties(UnityEngine.GameObject gameObject,
        IDictionary<string, string> props)
    {
        if (!props.ContainsKey("spawner"))
            return;
        // Are we spawning an enemy?
        if (props["spawner"] != "enemy")
            return;

        // Load the prefab assest and Instantiate it
        string prefabPath = "Assets/Resources/Prefabs/Spawner.prefab";
        UnityEngine.Object spawn =
            AssetDatabase.LoadAssetAtPath(prefabPath, typeof(GameObject));
        if (spawn != null)
        {
            var spawnInstance = (GameObject)GameObject.Instantiate(spawn);
            spawnInstance.name = spawn.name;

            spawnInstance.transform.parent = gameObject.transform;
            spawnInstance.transform.localPosition = Vector2.zero;            
        }
    }

    public void CustomizePrefab(UnityEngine.GameObject prefab)
    {
        // Do nothing
    }
}
