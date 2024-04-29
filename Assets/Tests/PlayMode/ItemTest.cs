using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;
using UnityEngine.TestTools;

public class ItemTest 
{
    [UnityTest]
    public IEnumerator ChangeItem()
    {
        var gameObject = new GameObject();
        var item = gameObject.AddComponent<Item>();
        var box = gameObject.AddComponent<BoxCollider2D>();
        var SpriteRenderer = gameObject.AddComponent<SpriteRenderer>();

        var Square = new GameObject("Square");
        Square.transform.parent = gameObject.transform;
        var SpriteRenderer2 = Square.AddComponent<SpriteRenderer>();
        item.name = "Potion";
        item.id = 1;
        item.balanceLevels = new List<int>() { 1, 2, 3, 4, 5, 6 };


        var spawns = new GameObject("Item1");

        var spawn = new GameObject();
        var spawnPoint = spawn.AddComponent<BalanceSpawnPoints>();


        spawn.name = "Item1-2";
        spawn.transform.parent = spawns.transform;
        spawn.transform.position = new Vector3(1, 1, 1);


        spawnPoint.spawnPointAccepts = new List<int>() {1,2,3};

        item.changeBalance(1);
        yield return new WaitForSeconds(2);

        Assert.AreEqual(new Vector3(1, 1, 1), item.transform.position);
    }

    [UnityTest]
    public IEnumerator ChangeItem2()
    {
        var gameObject = new GameObject();
        var item = gameObject.AddComponent<Item>();
        var box = gameObject.AddComponent<BoxCollider2D>();
        var SpriteRenderer = gameObject.AddComponent<SpriteRenderer>();

        var Square = new GameObject("Square");
        Square.transform.parent = gameObject.transform;
        var SpriteRenderer2 = Square.AddComponent<SpriteRenderer>();
        item.name = "Potion";
        item.id = 2;
        item.balanceLevels = new List<int>() { 1, 2, 3, 4, 5, 6 };


        var spawns = new GameObject("Item2");

        var spawn = new GameObject();
        var spawnPoint = spawn.AddComponent<BalanceSpawnPoints>();


        spawn.name = "Item1-2";
        spawn.transform.parent = spawns.transform;
        spawn.transform.position = new Vector3(2, 2, 2);


        spawnPoint.spawnPointAccepts = new List<int>() { 1, 2, 3 };

        item.changeBalance(1);
        yield return new WaitForSeconds(2);

        Assert.AreEqual(Square.activeInHierarchy, true);
    }
}
