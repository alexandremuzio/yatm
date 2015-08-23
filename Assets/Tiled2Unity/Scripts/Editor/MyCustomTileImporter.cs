using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

[Tiled2Unity.CustomTiledImporter]
class MyCustomTileImporter : Tiled2Unity.ICustomTiledImporter
{
    public void HandleCustomProperties(UnityEngine.GameObject gameObject, IDictionary<string, string> customProperties)
    {
        if (!props.ContainsKey("spawn"))
            return;

        if (props["spawn"] != "enemy")
            return;
    }

    public void CustomizePrefab(UnityEngine.GameObject prefab)
    {
        throw new NotImplementedException();
    }
}
