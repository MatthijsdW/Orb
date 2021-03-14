using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject player;
    public GameObject zombiePrefab;

    public List<GameObject> CurrentEnemies { get; set; } = new List<GameObject>();

    TileGrid grid;


    public void Init(GameObject player, TileGrid grid)
    {
        this.player = player;
        this.grid = grid;
    }

    private void Update()
    {
        if (CurrentEnemies.Count < 5 && Random.value > 0.999)
            SpawnEnemy();
    }

    private void SpawnEnemy()
    {
        if (player == null)
            return;

        int playerX = (int)player.transform.position.x;

        int xEnemy = 0;
        if (playerX - 20 < grid.minX)
        {
            xEnemy = playerX + 20;
        }
        else if (playerX + 20 > grid.maxX || Random.value > 0.5)
        {
            xEnemy = playerX - 20;
        }
        else
        {
            xEnemy = playerX + 20;
        }

        int i = 0;
        while (grid.Tiles[-grid.minX + xEnemy, i])
        {
            i++;
        }
        int yPlayer = i + grid.minY + (int)zombiePrefab.GetComponent<SpriteRenderer>().sprite.rect.height / 16;

        zombiePrefab.transform.position = new Vector3(xEnemy, yPlayer);
        CurrentEnemies.Add(Instantiate(zombiePrefab));
    }
}
