using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //list of players
    public GameObject[] players;

    public void CheckWinState()
    {
        int alivecount = 0;
        //check if the players in the game alive or death,if there is only one player he won the round
        foreach (GameObject player in players)
        {
            if(player.activeSelf)
            {
                alivecount++;
            }
        }
        if(alivecount<=1)
        {
            Invoke(nameof(EndRound), 3f);
        }
    }
    private void EndRound()
    {
        SceneManager.LoadScene("End Screen");
    }

    public void StartOver()
    {
        SceneManager.LoadScene("Start Screen");
    }
    public void NextLevel()
    {
        SceneManager.LoadScene("Level 2");
    }
}
