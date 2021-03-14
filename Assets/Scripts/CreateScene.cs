using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CreateScene : MonoBehaviour
{
    public int minX, minY, maxX, maxY;
    public GameObject wall;
    public GameObject player;

    public GameObject playerPrefab;
    public GameObject playerWeapon;
    public GameObject enemyControllerPrefab;
    public GameObject camera;

    public Texture2D[] tilesetRaw;

    TileGrid grid;

    // Start is called before the first frame update
    private void Start()
    {
        CreateGrid();
        CreateWalls();
        SpawnPlayer();
        CreateEnemyController();
    }

    private void CreateGrid()
    {
        // Create a grid
        grid = gameObject.AddComponent<TileGrid>();
        grid.Init(minX, minY, maxX, maxY, tilesetRaw);
    }

    private void CreateWalls()
    {
        bool[,] gridValues = LandscapeUtil.CreateMountains(maxX-minX, maxY-minY);

        for (int i = minX; i < maxX; i++)
            for (int j = minY; j < maxY; j++)
            {
                if (gridValues[i-minX,j-minY])
                    grid.AddTile(wall, i, j);
            }

        for (int i = minX; i < maxX; i++)
            for (int j = minY; j < maxY; j++)
            {
                grid.UpdateTile(i,j);
            }
    }

    private void SpawnPlayer()
    {
        int xPlayer = 0;
        int i = 0;
        while (grid.Tiles[-minX, i])
        {
            i++;
        }
        int yPlayer = i + minY + (int)playerPrefab.GetComponent<SpriteRenderer>().sprite.rect.height / 16;

        playerPrefab.transform.position = new Vector3(xPlayer, yPlayer);
        player = Instantiate(playerPrefab);
        playerWeapon.transform.position = new Vector3(xPlayer, yPlayer);
        Instantiate(playerWeapon);

        // Set camera
        GameObject currentCamera = Instantiate(camera);
        currentCamera.GetComponent<CameraController>().target = player;
    }


    private void CreateEnemyController()
    {
        GameObject enemyController = Instantiate(enemyControllerPrefab);
        enemyController.GetComponent<EnemyController>().Init(player, grid);
    }
}
