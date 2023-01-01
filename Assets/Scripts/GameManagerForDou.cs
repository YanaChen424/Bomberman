using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerForDou : BaseGameManager
{
    //list of players
    public GameObject[] players;

    public override void CheckWinState()
    {
        int alivecount = 0;
        //check if the players in the game alive or death,if there is only one player he won the round
        foreach (GameObject player in players)
        {
            if (player.activeSelf)
            {
                alivecount++;
            }
        }
        if (alivecount <= 1)
        {
            Invoke(nameof(EndRound), 3f);
        }
    }
}
