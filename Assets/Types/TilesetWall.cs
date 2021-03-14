using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class TilesetWall
{
    // no connections
    public List<Sprite> X = new List<Sprite>();

    // one connection
    public List<Sprite> N = new List<Sprite>();
    public List<Sprite> E = new List<Sprite>();
    public List<Sprite> S = new List<Sprite>();
    public List<Sprite> W = new List<Sprite>();

    // two connections line
    public List<Sprite> NS = new List<Sprite>();
    public List<Sprite> EW = new List<Sprite>();

    // two connections corner
    public List<Sprite> NE = new List<Sprite>();
    public List<Sprite> ES = new List<Sprite>();
    public List<Sprite> SW = new List<Sprite>();
    public List<Sprite> NW = new List<Sprite>();

    // three connections
    public List<Sprite> NES = new List<Sprite>();
    public List<Sprite> ESW = new List<Sprite>();
    public List<Sprite> NSW = new List<Sprite>();
    public List<Sprite> NEW = new List<Sprite>();

    // four connections
    public List<Sprite> NESW = new List<Sprite>();


    public void UnpackTileset(Texture2D tilesetRaw)
    {
        string spriteSheet = AssetDatabase.GetAssetPath(tilesetRaw);
        Sprite[] tileset = AssetDatabase.LoadAllAssetsAtPath(spriteSheet).OfType<Sprite>().OrderBy(x => int.Parse(x.name.Split('_').Last())).ToArray();

        // This assumes a 3x3 block, with lines to the right and bottom, and the 4 connection block in the bottom left corner

        ES.Add(tileset[0]);
        ESW.Add(tileset[1]);
        SW.Add(tileset[2]);
        S.Add(tileset[3]);
        NES.Add(tileset[4]);
        NESW.Add(tileset[5]);
        NSW.Add(tileset[6]);
        NS.Add(tileset[7]);
        NE.Add(tileset[8]);
        NEW.Add(tileset[9]);
        NW.Add(tileset[10]);
        N.Add(tileset[11]);
        E.Add(tileset[12]);
        EW.Add(tileset[13]);
        W.Add(tileset[14]);
        X.Add(tileset[15]);
    }
}
