using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEngine : MonoBehaviour
{
    //Used in Case the enemy stands on an explosion 
    int amountOfLife;

    // Start is called before the first frame update
    void Start()
    {
        amountOfLife = 1;
    }

    // Update is called once per frame
    void Update()
    {
        //when amountOfLife is equel to 0 then destroy enemy and go to next level becuse there is not other enemys
        if (amountOfLife == 0)
        {
            Destroy(gameObject);
            FindObjectOfType<GameManager>().NextLevel();
        }
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
            amountOfLife = amountOfLife - 1;
        }
    }
}
