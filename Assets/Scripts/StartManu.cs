using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartManu : MonoBehaviour
{
    //load start manu in the following states:
    //1)in the the beginning a game.
    //2)After death of the player.

    public void StartGame()
    {
        SceneManager.LoadScene("Level 1");
    }

}
