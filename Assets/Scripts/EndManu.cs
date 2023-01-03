using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndManu : MonoBehaviour
{
    //load End manu in the following states:
    //1)After all the levels Completed.

    public void Quit()
    {
        SceneManager.LoadScene("Start Screen");
    }
}
