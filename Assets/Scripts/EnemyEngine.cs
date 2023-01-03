using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEngine : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
      
        //call function to move the enemy
        PlayerDir();
           
    }
    //move enemy according to player movement
    void  PlayerDir()
    {
        if (PlayerMovement.xPos > transform.position.x)
        {
            transform.Translate(1 * Time.deltaTime, 0, 0);
        }
        else
        {
            transform.Translate(-1 * Time.deltaTime, 0, 0);
        }
        if (PlayerMovement.yPos > transform.position.y)
        {
            transform.Translate(0, 1 * Time.deltaTime, 0);
        }
        else
        {
            transform.Translate(0, -1 * Time.deltaTime, 0);
        }
    }

    // when an enemy stands on an explosion lower amountOfLife by one
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Explosion")
        {
            gameObject.SetActive(false);
            FindObjectOfType<BaseGameManager>().CheckWinState();
        }
    }
}
