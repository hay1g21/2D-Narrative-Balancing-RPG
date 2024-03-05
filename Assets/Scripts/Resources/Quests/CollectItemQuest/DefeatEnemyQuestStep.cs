using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefeatEnemyQuestStep : QuestStepScript
{
    private int enemiesKilled = 0;
    public int enemiesToKill;

    public string enemyType;

    private void Start()
    {
        updateState(); //need to update status right when the step object appears instead of first state change
    }

    private void OnEnable()
    {
        Debug.Log(InventoryManager.instance);
        GameManager.instance.gameEvents.onEnemyKilled += enemyKilled;
    }

    private void OnDisable()
    {
        GameManager.instance.gameEvents.onEnemyKilled -= enemyKilled;
    }

    private void enemyKilled(string enemyType)
    {
        if (enemiesKilled < enemiesToKill && enemyType.Equals(this.enemyType))
        {
            enemiesKilled += 1;
            updateState();
            Debug.Log("Enemy of type " + enemyType + " defeated");
        }

        if (enemiesKilled >= enemiesToKill)
        {
            finishQuestStep();
        }
    }
    //string to rep as state
    private void updateState()
    {
        string state = enemiesKilled.ToString();
        string status = "Killed " + enemiesKilled + "/" + enemiesToKill + " " + enemyType + "s";
        changeState(state, status);
    }
}
