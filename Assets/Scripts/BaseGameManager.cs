using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BaseGameManager : MonoBehaviour
{
    // go check if there is child of BaseGameManager that imlemented CheckWinState
    // if so, run it
    public virtual void CheckWinState() { }

    public void EndRound()
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
