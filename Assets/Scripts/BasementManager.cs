using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

public class BasementManager : MonoBehaviour
{
    float deathTimer = 0.0f;

    private int peopleCount;

    Basement basement;
    List<Transform> respawnPoints;
    List<NPC> npcs;

    NPC npcPrefab;

	void Start () {
        npcPrefab = Resources.Load<NPC>("Prefabs/NPC");
        var basementPrefab = Resources.Load<Basement>("Prefabs/Basement");

        basement = Instantiate<Basement>(basementPrefab);

        var respawners = basement.transform.FindChild("Respawners");

        npcs = new List<NPC>();

        respawnPoints = new List<Transform>();

        var allChildren = respawners.transform.Cast<Transform>().Select(t => t.gameObject).ToArray();
        foreach (GameObject t in allChildren)
        {
            respawnPoints.Add(t.transform);
        }
    }

    void Update()
    {

        foreach (Transform t in respawnPoints)
        {   if(peopleCount > 5)
            {
                break;
            }
            SpawnPerson(t.position);
            peopleCount++;
        }
    }
    private void SpawnPerson(Vector3 position)
    {
        var npc = Instantiate<NPC>(npcPrefab);
        npc.transform.position = position;
        npc.SetBasementToWanderAround(basement);
        npcs.Add(npc);
    }

    public List<NPC> GetNPCList()
    {
        return npcs;
    }
}
