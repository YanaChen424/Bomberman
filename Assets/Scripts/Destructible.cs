using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour
{
    /// <summary>
    /// script active by bomb control
    /// </summary>
    public float destructionTime = 1f;

    [Range(0f, 1f)]
    public float itemSpawnChance = 0.2f;
    //what type of items there is
    public GameObject[] spawnableItems;

    void Start()
    {
        Destroy(gameObject, destructionTime);
    }
    //upon destroying tile put un item  depands on itemSpawnChance
    private void OnDestroy()
    {
        if(spawnableItems.Length > 0 && Random.value<itemSpawnChance)
        {
            int randomIndex=Random.Range(0, spawnableItems.Length);
            Instantiate(spawnableItems[randomIndex],transform.position,Quaternion.identity);
        }
    }
}
