using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class TileGrid : MonoBehaviour
{
    public int minX, minY, maxX, maxY;
    private int tileWidth, tileHeight;
    public GameObject[,] Tiles;
    public TilesetWall tilesetWall;

    public void Init(int minX, int minY, int maxX, int maxY, Texture2D[] tilesetRaw)
    {
        this.minX = minX;
        this.minY = minY;
        this.maxX = maxX;
        this.maxY = maxY;
        Tiles = new GameObject[-minX + maxX, -minY + maxY];

        tilesetWall = new TilesetWall();
        foreach (Texture2D tilesRaw in tilesetRaw)
        {
            tilesetWall.UnpackTileset(tilesRaw);
        }
    }

    public void AddTile(GameObject tile, int x, int y)
    {
        tile.transform.position = new Vector3(x, y);
        Tiles[x - minX, y - minY] = Instantiate(tile);
        Tiles[x - minX, y - minY].GetComponent<ConnectedWall>().Init(tilesetWall);
    }

    public void RemoveTile(int x, int y)
    {
        Destroy(Tiles[x - minX, y - minY]);
        Tiles[x - minX, y - minY] = null;
    }

    public void UpdateTile(int x, int y)
    {
        Tiles[x - minX, y - minY]?.GetComponent<ConnectedWall>().UpdateSprite(this, x - minX, y - minY);
    }
}
