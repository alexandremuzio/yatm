using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

class PlayerManager : MonoBehaviour
{
    List<Transform> startPositionList;
    private List<Player> players;

    Player playerPrefab;

    public event EventHandler AllPlayersDiedEvent;
    public event EventHandler MonsterDiedEvent;

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
        player.DiedEvent += OnDiedEvent;
        return player;
    }

    private void OnDiedEvent(object sender, EventArgs e)
    {
        Player player = (Player)sender;
        players.Remove(player);
        Debug.Log(player.GetType());
        Debug.Log(player.IsMonster());

        if (player.IsMonster())
        {
            if (MonsterDiedEvent != null)
            {
                //players win!
                Debug.Log("Players win!");
                MonsterDiedEvent(this, null);
                return;
            }
        }

        int numberOfPlayers = players.Where<Player>(x => !x.IsMonster()).Count<Player>();
        Debug.Log(numberOfPlayers);
        Debug.Log(players.Count);
        if (numberOfPlayers == 0)
        {
            if (AllPlayersDiedEvent != null)
            {
                //monsters win!
                Debug.Log("Monsters Win!");
                AllPlayersDiedEvent(this, null);
            }
        }
    }

    public List<Player> GetPlayerList()
    {
        return players;
    }
}