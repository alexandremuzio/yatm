using System;
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
        playerPrefab = Resources.Load<Player>("Prefabs/MonstahPlayer");

        startPositionList = new List<Transform>();
        players = new List<Player>();
        var startingPositions = Resources.Load<GameObject>("Prefabs/PlayerStartingPositions");

        var allChildren = startingPositions.transform.Cast<Transform>().Select(t => t.gameObject).ToArray();
        foreach (GameObject go in allChildren)
        {
            startPositionList.Add(go.transform);
        }
    }

    public Player CreatePlayer(bool isMonster)
    {
        var player = NormalPlayer.Create(startPositionList[players.Count].position, isMonster);
        players.Add(player);

        return player;
    }

    public List<Player> GetPlayerList()
    {
        return players;
    }
}