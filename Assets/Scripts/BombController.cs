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

    private void OnEnable()
    {
        bombsRemaining = bombAmount;
    }


    void Update()
    {
        if (bombsRemaining > 0 && Input.GetKeyDown(inputKey))
        {
            StartCoroutine(PlaceBomb());
        }
    }



    private IEnumerator PlaceBomb()
    {
        Vector2 pos = transform.position;
        pos.x = Mathf.Floor(pos.x) + 0.5f;
        pos.y = Mathf.Floor(pos.y) + 0.5f;

        GameObject bomb = Instantiate(bombPreFab, pos, Quaternion.identity);
        bombsRemaining--;

        yield return new WaitForSeconds(bombFuseTime);

        pos = bomb.transform.position;
        pos.x = Mathf.Floor(pos.x) + 0.5f;
        pos.y = Mathf.Floor(pos.y) + 0.5f;

        Explosion explosion = Instantiate(explosionPreFab, pos, Quaternion.identity);
        explosion.SetActiveRenderer(explosion.start);
        explosion.DestroyAfter(explosionDuration);


        Explode(pos, Vector2.up, explosionRadius);
        Explode(pos, Vector2.down, explosionRadius);
        Explode(pos, Vector2.left, explosionRadius);
        Explode(pos, Vector2.right, explosionRadius);

        Destroy(bomb);
        bombsRemaining++;
    }
    private void Explode(Vector2 position,Vector2 direction,int length)
    {
        if (length <= 0)
        {
            return;
        }
        position += direction;

        if(Physics2D.OverlapBox(position,Vector2.one/2f,0f, explosionLayerMask))
        {
            ClearDestructible(position);
            return;
        }

        Explosion explosion = Instantiate(explosionPreFab, position, Quaternion.identity);
        explosion.SetActiveRenderer(length>1 ? explosion.middle : explosion.end);
        explosion.SetDir(direction);
        explosion.DestroyAfter(explosionDuration);

        Explode(position, direction, length-1);
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.layer==LayerMask.NameToLayer("Bomb"))
        {
            other.isTrigger=false;
        }
    }
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
    public void AddBomb()
    {
        bombAmount++;
        bombsRemaining++;
    }
}
