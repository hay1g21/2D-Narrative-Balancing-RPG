using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalanceSpawnPoints : MonoBehaviour
{
    //needs to store which balance levels the thing spawns on

    public List<int> spawnPointAccepts;

    public List<int> getSpawns()
    {
        return spawnPointAccepts;
    }
}
