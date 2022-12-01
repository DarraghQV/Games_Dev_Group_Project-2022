using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

//the master script to spawn the tiles 
//can set the amount of layers and reduces the size of the circle by each level down

//---------------IMPORTANT---------------------
//****Add TileMapMaster to an "EmptyGameObject" or what ever the game manager is and CALL CREATE MAP FROM THE MANAGER****
//****Add set tile in the properties tab to Assets/TileMap/Tile/PF_TilePrefab.prefab****

//---------------Functions---------------------
// ----> TileMapMaster.CreateMap(float radius,int level,int radius_dec_per_level,float distance_between_levels);    returns a boolean if the map has been created
// ----> TileMapMaster.ClearMap()                                                                                   returns a boolean if map has been sucessfully cleared
// ----> TileMapMaster.is_Map_Created                                                                               returns a boolean to see if the map has data in it 

//---------------Default values----------------
// float radius = 30;                                                         how big the circles should be
// int levels = 3;                                                            how many levels in the stack
// int radius_dec_per_level = 15;                                             how much the radius of the circles decreses per level going down
// float distance_between_levels = 20;                                        how far away each slice should be from each other
// float tile_Size = 2.5f;                                                    size of the tiles x&y
// Quaternion Tile_rotation = new Quaternion(0.70711f, 0, 0, -0.70711f);      rotation so tiles lay flat



public class TileMapMaster : MonoBehaviour
{
   
    public Object tile;
    public float radius = 30;
    public int levels = 3;
    public int radius_dec_per_level = 15;
    public float distance_between_levels = 20;

    public List<TileScript> Tiles;
    private Vector2 grid_Size;
    private float tile_Size = 2.5f;
    private Quaternion Tile_rotation = new Quaternion(0.70711f,0,0,-0.70711f); // rotates the spawned tile so it lies flat ... may god have mercy on you if you change this
    private bool is_map_created = false;

    private void Start()
    {
    
    }

    public bool CreateMap(float radius,int levels,int radius_dec_per_level,float distance_between_levels) 
    {
        grid_Size = new Vector2(radius * 2, radius * 2);
        radius = radius + 1;

        for (int z = 0; z < levels; z++) // some magic... ignore
        {
            for (int x = 0; x < grid_Size.x; x++)
            {
                for (int y = 0; y < grid_Size.y; y++)
                {
                    Vector3 location = new Vector3(x * tile_Size, z * distance_between_levels, y * tile_Size);

                    location = new Vector3(location.x - (((tile_Size * grid_Size.x) / 2) - 1.25f), location.y - ((distance_between_levels * levels) - distance_between_levels), location.z - (((tile_Size * grid_Size.y) / 2) - 1.25f));

                    float i = levels - z - 1;


                    if (vec3ToVec2Magnitude(location) <= (radius * 2) - (radius_dec_per_level * i))
                    {
                        SpawnTile(location);
                    }
                }
            }
        }

        if (Tiles.Count > 0) 
        {
            is_map_created = true;
            return true;
        }
        if (Tiles.Count <= 0 && radius > 0) 
        {
            print("error at TileMapMaster/CreateMap as the tiles is less than 0");
            is_map_created = false;
            return false;
        }
        if (radius < 1) 
        {
            print("Tiles wont be spawned as the radius is too small");
            is_map_created = true;
            return true;
        }

        return false;
    }


    internal void SpawnTile(Vector3 location) 
    {
        GameObject spawnedTile = Instantiate(tile, location, Tile_rotation,transform) as GameObject; // adds the currently spawned tile to a list of spawned tiles
        TileScript current_tile = spawnedTile.GetComponent<TileScript>();
        current_tile.Set_Manager(this);
        Tiles.Add(current_tile);
    }

    internal float vec3ToVec2Magnitude(Vector3 loc) //function to mesure the size of a 3d vector on a plane  
    {
        Vector3 loc2= new Vector3(loc.x,0,loc.z);//ignores the up vector of the given 3 vector so basicly it mesures size on a 2D plane
        float result = loc2.magnitude;
        return result;
    }

     internal void RemoveTileFromList(TileScript tile) //removes the destroyed tile from the list of tiles
     {
        Tiles.Remove(tile);
     }

    public bool is_Map_Created()
    { 
        return is_map_created; 
    }

    public bool ClearMap() 
    {
        foreach(TileScript t in Tiles)
        {
            t.Destroy_Tile(false);        
        }
        Tiles.Clear();

        if (Tiles.Count == 0)
        {
            is_map_created = false;
            return true;
        }
        else if (Tiles.Count > 0)
        {
            return false;
        }
        else if (Tiles.Count < 0)
        {
            print("List of Tiles is negative @ TileMapMaster/ClearMap"); //Something has gone terrably wrong as this is not supposed to be possable
        }

        return false;
    }
}
