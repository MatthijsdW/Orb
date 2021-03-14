using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectedWall : MonoBehaviour
{

    private SpriteRenderer spriteRenderer;

    public TilesetWall tileset;

    public void Init(TilesetWall tileset)
    {
        this.tileset = tileset;

        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = RandomSprite(tileset.NESW);
        }
    }

    private Sprite RandomSprite(List<Sprite> sprites)
    {
        int random = Random.Range(0, sprites.Count - 1);
        return sprites[random];
    }

    public void UpdateSprite(TileGrid grid, int x, int y)
    {
        List<Sprite> Sprites = DetermineCorrectSprite(grid, x, y);
        spriteRenderer.sprite = RandomSprite(Sprites);
    }

    public List<Sprite> DetermineCorrectSprite(TileGrid grid, int x, int y)
    {
        bool N = false;
        bool E = false;
        bool S = false;
        bool W = false;

        // Check adjacent tiles
        if (y + 1 < grid.maxY - grid.minY)
            N = grid.Tiles[x, y + 1] != null;

        if (x + 1 < grid.maxX - grid.minX)
            E = grid.Tiles[x + 1, y] != null;

        if (y > 0)
            S = grid.Tiles[x, y - 1] != null;

        if (x > 0)
            W = grid.Tiles[x - 1, y] != null;

        // Determine the correct tile

        // no connections
        if (!N && !E && !S && !W)
            return tileset.X;

        // one connections
        if (N && !E && !S && !W)
            return tileset.N;
        if (!N && E && !S && !W)
            return tileset.E;
        if (!N && !E && S && !W)
            return tileset.S;
        if (!N && !E && !S && W)
            return tileset.W;

        // two connections: line
        if (!N && E && !S && W)
            return tileset.EW;
        if (N && !E && S && !W)
            return tileset.NS;

        // two connections: corners
        if (N && E && !S && !W)
            return tileset.NE;
        if (!N && E && S && !W)
            return tileset.ES;
        if (!N && !E && S && W)
            return tileset.SW;
        if (N && !E && !S && W)
            return tileset.NW;

        // three connections
        if (N && E && S && !W)
            return tileset.NES;
        if (!N && E && S && W)
            return tileset.ESW;
        if (N && !E && S && W)
            return tileset.NSW;
        if (N && E && !S && W)
            return tileset.NEW;

        // four connections (plateau and hole)
        return tileset.NESW;
    }

}
