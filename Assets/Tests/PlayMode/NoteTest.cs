using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class NoteTest
{
   
    [UnityTest]
    public IEnumerator ChangeNote()
    {
        var gameObject = new GameObject();
        var note = gameObject.AddComponent<NoteScript>();

        note.id = 25;
        note.balanceLevels = new List<int>() { 1, 2, 3, 4, 5, 6 };
        var Square = new GameObject("Square");
        Square.transform.parent = gameObject.transform;

        var spawns = new GameObject("Note25");

        var spawn = new GameObject();
        var spawnPoint = spawn.AddComponent<BalanceSpawnPoints>();


        spawn.name = "Note1-2";
        spawn.transform.parent = spawns.transform;
        spawn.transform.position = new Vector3(1, 1, 1);


        spawnPoint.spawnPointAccepts = note.balanceLevels = new List<int>() { 1, 2, 3, 4, 5, 6 };
       
        note.changeBalance(3);
        yield return new WaitForSeconds(2);

        Assert.AreEqual(new Vector3(1, 1, 1), note.transform.position);
    }
}
