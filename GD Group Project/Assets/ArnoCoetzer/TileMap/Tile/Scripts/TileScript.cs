using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

//Tile script that controles indevidual tiles

//---------------Functions---------------------
//TileScript.TakeDamage(flaot amount)             will reduce the health of the tile by the amount in the form of a float
//TileScript.DestroyTile(bool update_manager)     will destroy the tile with boolean if it should updtate the manager of the destruction of itself(should be true most of the time)

//---------------Default values----------------
// max_health = 100f; // sets the maximum amount of health of the tile 


public class TileScript : MonoBehaviour,Health
{
    //just some parameters
    private float tile_Health;
    private float max_health = 100f;
    TileMapMaster manager;

    private void Start() // will set the tiles health to the max health at spawn of the this tile
    {
        tile_Health = max_health; 
    }
  

    internal void Set_Manager(TileMapMaster master) // sets the tiles manager
    {
        manager = master;
    }

    public void Take_Damage(float amount) // will decrease the health of the tile and destroy it if the health is less or equal to zero
    {
        tile_Health = tile_Health - amount;

        if (tile_Health <= 0)
        {
            Destroy_Tile(true);
        }
      
    }

    internal void Destroy_Tile(bool update_manager) // unalives the tile and updates the Manager
    {
        if (update_manager)
        {
            manager.RemoveTileFromList(this);
            Destroy(this.gameObject);
        }

        else 
        {
            Destroy(this.gameObject);
        }
    }


}
