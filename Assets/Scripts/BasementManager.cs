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
    List<GameObject> npcBodiesPrefabList;

    public event EventHandler<EventArgs> AllCitizensDiedEvent;

    void Start()
    {
        npcPrefab = Resources.Load<NPC>("Prefabs/NPC");
        var basementPrefab = Resources.Load<Basement>("Prefabs/Basement");

        npcBodiesPrefabList = new List<GameObject>();

        for (int i = 0; i < 12; i++)
        {
            npcBodiesPrefabList.Add(Resources.Load<GameObject>("Prefabs/NPC/NPCBody_" + i));
        }

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
        {
            if (peopleCount > 5)
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
        var bodyIndex = UnityEngine.Random.Range(0, npcBodiesPrefabList.Count);
        var body = Instantiate<GameObject>(npcBodiesPrefabList[bodyIndex]);
        body.transform.parent = npc.transform;
        body.transform.localPosition = Vector3.zero;
        npc.SetBasementToWanderAround(basement);
        npcs.Add(npc);
        npc.DiedEvent += OnDiedEvent;
    }

    private void OnDiedEvent(object sender, EventArgs e)
    {
        NPC npc = (NPC)sender;
        npcs.Remove(npc);

        if(npcs.Count == 0)
        {
            if(AllCitizensDiedEvent != null)
            {
                AllCitizensDiedEvent(this, null);
            }
        }
    }

    public List<NPC> GetNPCList()
    {
        return npcs;
    }

}
