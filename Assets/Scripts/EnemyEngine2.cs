using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEngine2 : MonoBehaviour
{
    int amountOfLife;
    int numEnemy;
    void Start()
    {
        amountOfLife = 1;
        numEnemy = 0;
    }

    void Update()
    {
        if (amountOfLife == 0)
        {
            Destroy(gameObject);
            FindObjectOfType<GameManager>().StartOver();

        }
        PlayerDir();


    }
    void PlayerDir()
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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Explosion")
        {
            amountOfLife = amountOfLife - 1;
        }
    }
}
