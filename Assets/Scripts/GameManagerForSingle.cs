using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerForSingle : BaseGameManager
{
    //list of players
    public GameObject player;
    public GameObject[] enemies;
    public string nextSceneName;

    public override void CheckWinState()
    {
        if (!player.activeSelf)
        {
            Invoke(nameof(StartOver), 3f);
        }

        int aliveEnemies = 0;
        foreach (var enemy in enemies)
        {
            if (enemy.activeSelf)
            {
                aliveEnemies++;
            }
        }
        if(aliveEnemies == 0)
        {
            SceneManager.LoadScene(nextSceneName);
        }
    }
}
