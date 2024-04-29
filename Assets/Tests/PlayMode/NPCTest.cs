using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
public class NPCTest
{
    
    [UnityTest]
    public IEnumerator ChangeNPC()
    {
        var gameObject = new GameObject();
        var npc = gameObject.AddComponent<NPCScript>();
        var box = gameObject.AddComponent<BoxCollider2D>();
        var SpriteRenderer = gameObject.AddComponent<SpriteRenderer>();
        var Coll = new GameObject("Coll");
        Coll.transform.parent = gameObject.transform;
        npc.NPCname = "Guy";
       
        npc.balanceLevels = new List<int>() { 1, 2, 3, 4, 5, 6 };
        

        var spawns = new GameObject("GuySpawn");

        var spawn = new GameObject();
        var spawnPoint = spawn.AddComponent<BalanceSpawnPoints>();


        spawn.name = "Goblin1-2";
        spawn.transform.parent = spawns.transform;
        spawn.transform.position = new Vector3(1, 1, 1);


        spawnPoint.spawnPointAccepts = new List<int>() { 4, 5, 6 };

        npc.changeBalance(5);
        yield return new WaitForSeconds(2);

        Assert.AreEqual(new Vector3(1, 1, 1), npc.transform.position);
    }
    [UnityTest]
    public IEnumerator ChangeNPC2()
    {
        var gameObject = new GameObject();
        var npc = gameObject.AddComponent<NPCScript>();
        var box = gameObject.AddComponent<BoxCollider2D>();
        var SpriteRenderer = gameObject.AddComponent<SpriteRenderer>();
        var Coll = new GameObject("Coll");
        Coll.transform.parent = gameObject.transform;
        npc.NPCname = "Guy";

        npc.balanceLevels = new List<int>() { 1, 2, 3, 4, 5, 6 };


        var spawns = new GameObject("GuySpawn");

        var spawn = new GameObject();
        var spawnPoint = spawn.AddComponent<BalanceSpawnPoints>();


        spawn.name = "Goblin1";
        spawn.transform.parent = spawns.transform;
        spawn.transform.position = new Vector3(2, 2, 2);


        spawnPoint.spawnPointAccepts = new List<int>() { 1, 2, 3 };

        npc.changeBalance(3);
        yield return new WaitForSeconds(2);

        Assert.AreEqual(new Vector3(2, 2, 2), npc.transform.position);
    }

    [UnityTest]
    public IEnumerator ChangeNPC3()
    {
        var gameObject = new GameObject();
        var npc = gameObject.AddComponent<NPCScript>();
        var box = gameObject.AddComponent<BoxCollider2D>();
        var SpriteRenderer = gameObject.AddComponent<SpriteRenderer>();
        var Coll = new GameObject("Coll");
        Coll.transform.parent = gameObject.transform;
        npc.NPCname = "Guy";

        npc.balanceLevels = new List<int>() { 1, 2, 3, 4, 5, 6 };


        var spawns = new GameObject("GuySpawn");

        var spawn = new GameObject();
        var spawnPoint = spawn.AddComponent<BalanceSpawnPoints>();


        spawn.name = "Goblin1";
        spawn.transform.parent = spawns.transform;
        spawn.transform.position = new Vector3(3, 3, 3);


        spawnPoint.spawnPointAccepts = new List<int>() { 1, 2, 3 };

        npc.changeBalance(3);
        yield return new WaitForSeconds(2);

        Assert.AreEqual(new Vector3(3, 3, 3), npc.transform.position);
    }
}
