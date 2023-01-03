using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using static UnityEditor.PlayerSettings;

public class BombController : MonoBehaviour
{
    [Header("Bomb")]
    public GameObject bombPreFab;
    public KeyCode inputKey = KeyCode.Space;
    public float bombFuseTime = 3f;
    public int bombAmount = 1;
    private int bombsRemaining;

    [Header("Explosion")]
    public Explosion explosionPreFab;
    public LayerMask explosionLayerMask;
    public float explosionDuration = 1f;
    public int explosionRadius = 1;

    [Header("Destructible")]
    public Tilemap destructibleTiles;
    public Destructible destructiblePreFab;

    //To make sure the bombsRemaining is set to how many bombs there is
    private void OnEnable()
    {
        bombsRemaining = bombAmount;
    }


    void Update()
    {
        if (bombsRemaining > 0 && Input.GetKeyDown(inputKey))
        {
            //in unity select the needed key
            //Instead of working with update with only one frame time,working with serval frames time
            StartCoroutine(PlaceBomb());
        }
    }


    //when using Coroutine function you need to use IEnumerator 
    private IEnumerator PlaceBomb()
    {
        Vector2 pos = transform.position;
        pos.x = Mathf.Floor(pos.x) + 0.5f;
        pos.y = Mathf.Floor(pos.y) + 0.5f;

        //put a bomb in pos location
        GameObject bomb = Instantiate(bombPreFab, pos, Quaternion.identity);
        //after puting a bomb lower amount
        bombsRemaining--;

        //Wait until the bomb blow up then continue the function
        yield return new WaitForSeconds(bombFuseTime);

        //get bomb location and round them up according to grid
        pos = bomb.transform.position;
        pos.x = Mathf.Floor(pos.x) + 0.5f;
        pos.y = Mathf.Floor(pos.y) + 0.5f;

        //start explosion in the location of the bomb
        Explosion explosion = Instantiate(explosionPreFab, pos, Quaternion.identity);
        explosion.SetActiveRenderer(explosion.start);
        //destroy explosion after giving time
        explosion.DestroyAfter(explosionDuration);

        //make explosion in diffrent directions according to radius
        Explode(pos, Vector2.up, explosionRadius);
        Explode(pos, Vector2.down, explosionRadius);
        Explode(pos, Vector2.left, explosionRadius);
        Explode(pos, Vector2.right, explosionRadius);
        
        Destroy(bomb);
        //return bombs amount,so the player can put another bomb
        bombsRemaining++;
    }
    private void Explode(Vector2 position,Vector2 direction,int length)
    {
        //Stop explosion when length is 0(exit condition)
        if (length <= 0)
        {
            return;
        }
        //which direction explode
        position += direction;

        if(Physics2D.OverlapBox(position,Vector2.one/2f,0f, explosionLayerMask))
        {
            ClearDestructible(position);
            return;
        }
        //create explosion
        Explosion explosion = Instantiate(explosionPreFab, position, Quaternion.identity);
        //which part of the animation to set
        explosion.SetActiveRenderer(length>1 ? explosion.middle : explosion.end);
        explosion.SetDir(direction);
        explosion.DestroyAfter(explosionDuration);

        //replay the function until length is 0
        Explode(position, direction, length-1);
    }

    // untrigger the bomb when the player putting the bomb
    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.layer==LayerMask.NameToLayer("Bomb"))
        {
            other.isTrigger=false;
        }
    }

    //Doing the destructible animation and clear the tile
    private void ClearDestructible(Vector2 position)
    {
        Vector3Int cell= destructibleTiles.WorldToCell(position);
        TileBase tile= destructibleTiles.GetTile(cell);

        
        if (tile != null)
        { 
            Instantiate(destructiblePreFab,position,Quaternion.identity);
            destructibleTiles.SetTile(cell, null);
        }
    }

    //if the player pickup extra bomb item so add more bomb
    public void AddBomb()
    {
        bombAmount++;
        bombsRemaining++;
    }
}
