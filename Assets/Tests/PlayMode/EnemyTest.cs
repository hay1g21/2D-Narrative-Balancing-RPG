using System.Collections;
using System.Collections.Generic;

using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class EnemyTest { 

    // Start is called before the first frame update
    [UnityTest]
    public IEnumerator MoveNote()
    {
        var gameObject = new GameObject();
        var enemy = gameObject.AddComponent<OverworldEnemy>();
        var box = gameObject.AddComponent<BoxCollider2D>();
        var SpriteRenderer = gameObject.AddComponent<SpriteRenderer>();

        enemy.id = "1";
        enemy.enemyName = "Goblin";
        enemy.balanceLevels = new List<int>() { 1, 2, 3, 4, 5, 6 };
        var Square = new GameObject("Square");
        Square.transform.parent = gameObject.transform;

        var spawns = new GameObject("Goblin1");

        var spawn = new GameObject();
        var spawnPoint = spawn.AddComponent<BalanceSpawnPoints>();


        spawn.name = "Goblin1-2";
        spawn.transform.parent = spawns.transform;
        spawn.transform.position = new Vector3(1, 1, 1);


        spawnPoint.spawnPointAccepts = enemy.balanceLevels = new List<int>() { 1, 2, 3, 4, 5, 6 };
       
        enemy.changeBalance(3);
        yield return new WaitForSeconds(2);

        Assert.AreEqual(new Vector3(1, 1, 1), enemy.transform.position);
    }
}
