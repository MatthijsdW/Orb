using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandscapeUtil
{

    public static bool[,] CreateMountains(int x, int y)
    {
        bool[,] mountains = new bool[x,y];

        float scale = Random.Range(0.01f,0.011f);
        int offset = Random.Range(0, 1000);
        for (int i = 0; i < x; i++)
        {
            float rawHeight = Mathf.PerlinNoise(i * scale + offset, 0.0f);
            int height = (int) (rawHeight * (y));
            for (int j = 0; j < y; j++)
            {
                if (j > height)
                    mountains[i,j] = false;
                else
                    mountains[i,j] = true;
            }
        }

        return mountains;
    }
}
