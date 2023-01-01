using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    //enum contains all the types of drop items
    public enum ItemType
    {
        ExtraBomb,
        BlastRadius,
        SpeedIncrease,
    }
    //Type will contain the item type in unity depands on the gameobject
    public ItemType Type;
    
    //Each drop item will give diffrent boost to the player
    private void OnItemPickup(GameObject player)
    {
        switch(Type)
        {
            case ItemType.ExtraBomb:
                player.GetComponent<BombController>().AddBomb();
            break;
            case ItemType.BlastRadius:
                player.GetComponent<BombController>().explosionRadius++;
            break;
            case ItemType.SpeedIncrease:
                player.GetComponent<PlayerMovement>().Speed++;
            break;
        }
        //after the player picks the item,the item disappeares
        Destroy(gameObject);
    }

    //If the player wants to pick the item then trigger the OnItemPickup function
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            OnItemPickup(other.gameObject);
        }
    }
}
