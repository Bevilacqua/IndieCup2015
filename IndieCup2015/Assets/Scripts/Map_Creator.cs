using UnityEngine;
using System.Collections;

public class Map_Creator : MonoBehaviour {
    public GameObject tile;

    void Start()
    {
        createMap(5, 2);
    }

    /// <summary>
    /// Creates a hex-map cenetered at (0,0)
    /// </summary>
    /// <param name="maxTiles">The maximum amount of tiles per row. The row with max tiles will be the center row.</param>
    /// <param name="minTiles">The minimum amount of tiles per row.</param>
    public void createMap(int maxTiles, int minTiles)
    {
        int height = ( ( maxTiles + (maxTiles - 1) ) - ( (minTiles - 1) * 2 ) );
        int top = Mathf.FloorToInt(height / 2f);
        int bottom = -top;

        int tilesPerRow = minTiles;

        for (int y = top; y >= bottom; y--)
        {
            if (y > 0) tilesPerRow++;
            else tilesPerRow--;

            for (int x = 0; x < tilesPerRow; x++)
            {
                Instantiate(tile, new Vector3(x * .865f, 0f, y * .75f), tile.transform.localRotation);
            }

            y--;
        }

       

    }
}
