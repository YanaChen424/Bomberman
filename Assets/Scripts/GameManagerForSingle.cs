using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerForSingle : BaseGameManager
{
    
    public GameObject player;
    //list of enemies
    public GameObject[] enemies;
    public string nextSceneName;

    public override void CheckWinState()
    {
        //if player is dead then start over
        if (!player.activeSelf)
        {
            Invoke(nameof(StartOver), 3f);
        }

        int aliveEnemies = 0;
        //check if there is an alive enemy
        foreach (var enemy in enemies)
        {
            if (enemy.activeSelf)
            {
                aliveEnemies++;
            }
        }
        //if all the Enemies dead go to next level
        if(aliveEnemies == 0)
        {
            SceneManager.LoadScene(nextSceneName);
        }
    }
}
