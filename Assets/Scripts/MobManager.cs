using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class MobManager : MonoBehaviour {

    float timer = 0.0f;
    List<Transform> spawnPoints;
    List<Vector2> path;

    Enemy enemyPrefab;
    GameObject pathPrefab;
    GameObject spawnersPrefab;

    // Use this for initialization
    void Start () {
        enemyPrefab = Resources.Load<Enemy>("Prefabs/Enemy");
        pathPrefab = Resources.Load<GameObject>("Prefabs/Path0");
        spawnersPrefab = Resources.Load<GameObject>("Prefabs/Spawners");

        spawnPoints = new List<Transform>();
        path = new List<Vector2>();

        var allChildren = pathPrefab.transform.Cast<Transform>().Select(t => t.gameObject).ToArray();
        foreach (GameObject t in allChildren)
        {
            path.Add(new Vector2(t.transform.position.x, t.transform.position.y));
        }

        allChildren = spawnersPrefab.transform.Cast<Transform>().Select(t => t.gameObject).ToArray();
        foreach (GameObject go in allChildren)
        {
            spawnPoints.Add(go.transform);
        }
    }

    public void SetSpawnPoints(List<Transform> spawnPoints)
    {
        this.spawnPoints = spawnPoints;
    }

	void Update () {
        timer += Time.deltaTime;
        if(timer >= 3.0f)
        {
            foreach(Transform t in spawnPoints)
            {
                var enemy2 = Instantiate<Enemy>(enemyPrefab);
                enemy2.transform.position = t.position;
                enemy2.SetPathToFollow(path);
            }
            timer -= 3.0f;
        }
	}
}
