using System.Collections.Generic;
using System.Linq;
using UnityEngine;

class PlayerManager : MonoBehaviour
{
    List<Transform> startPositionList;
    private List<Player> players;

    Player playerPrefab;

    void Awake()
    {
        playerPrefab = Resources.Load<Player>("Prefabs/Player");

        startPositionList = new List<Transform>();
        players = new List<Player>();
        var startingPositions = Resources.Load<GameObject>("Prefabs/PlayerStartingPositions");

        var allChildren = startingPositions.transform.Cast<Transform>().Select(t => t.gameObject).ToArray();
        foreach (GameObject go in allChildren)
        {
            startPositionList.Add(go.transform);
        }
    }

    public Player CreatePlayer()
    {
        var player = Instantiate<Player>(playerPrefab);
        player.transform.position = startPositionList[players.Count].position;
        players.Add(player);

        return player;
    }
}