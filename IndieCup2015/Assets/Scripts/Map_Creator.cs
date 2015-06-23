using UnityEngine;
using System.Collections;

/// <summary>
/// Class of methods to create and alter a map. 
/// Not to be used to manage current map.
/// Use Map_Manager class to call these methods.
/// </summary>
public class Map_Creator : MonoBehaviour {
    public GameObject prefab_defaultTile;
    public GameObject prefab_mapParent;

    /// <summary>
    /// Debugging use only.
    /// </summary>
    void Start()
    {
    }

    /// <summary>
    /// Creates a blank map centered at (0,0) using the default tile gameobject provided as a paramater of this class.
    /// </summary>
    /// <param name="rows">The number of rows in the map.</param>
    /// <param name="cols">The number of columns in the map.</param>
    /// <returns>The gameobject of the map parent object. </returns>
    public GameObject createMap(int rows, int cols)
    {
        GameObject[,] map = new GameObject[cols, rows];
        GameObject mapParent = (GameObject) Instantiate(prefab_mapParent, new Vector3(), prefab_mapParent.transform.localRotation);
        bool offset = false;

        if(rows % 2 == 0) //Ensure that the center tile is at position (0,0)
            offset = true;

        for (int y = 0; y < rows; y++)
        {
            for(int x = 0; x < cols; x++)
            {
                Vector3 location = new Vector3();

                if (!offset) location.Set(((Mathf.FloorToInt(-cols / 2f) * .865f) + (x * .865f)) , 0f, (((rows * .75f) / 2f) - ( y * .75f)));
                else location.Set(((Mathf.FloorToInt(-cols / 2f) * .865f) + (x * .865f)) - (.865f / 2f), 0f, (((rows * .75f) / 2f) - ( y * .75f)));

                GameObject currentTile = (GameObject) Instantiate(prefab_defaultTile, location, prefab_defaultTile.transform.localRotation);
                currentTile.transform.parent = mapParent.transform;

                //Debug: Random tiles are transversable
                if (Random.Range(0,100) % 2 == 0 || x == 3 || x == 1)
                    currentTile.GetComponent<Tile_Info>().setTransversable(true);
                //EndDebug

                currentTile.GetComponent<Tile_Info>().assignInfo(new Vector2(x, y));
//                Debug.Log("Tile (" + x + " , " + y + ") @ - [" + currentTile.transform.position.x + " , " + currentTile.transform.position.z + " ]");
                map[x,y] = currentTile;
            }

            offset = !offset;
        }
            mapParent.GetComponent<Map_Info>().setTileMap(map);
            mapParent.GetComponent<Map_Info>().setDimensions(cols, rows);

            mapParent.GetComponent<Map_Info>().updateScale();

            return mapParent;
    }
}
