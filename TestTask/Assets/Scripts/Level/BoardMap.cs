using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class BoardMap : MonoBehaviour
{
    [SerializeField] private GameObject prefabEnemy;
    [SerializeField] private GameObject board;
    [SerializeField] private int countCol;
    [SerializeField] private int countRow;
    [SerializeField] private float offsetX;
    [SerializeField] private float offsetZ;

    private List<SpawnEnemy> spawnEnemies = new List<SpawnEnemy>();
    private List<SpawnBonus> spawnBonuses = new List<SpawnBonus>();

    private int[] pole = null;

    // Start is called before the first frame update
    void Start()
    {
        CreateSpawnLists();
        CreatePole();
        BeforeSpawnEnemy();
        BeforeSpawnBonus();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void CreateSpawnLists()
    {
        SpawnEnemy[] tailEnSpawn = board.transform.GetComponentsInChildren<SpawnEnemy>();
        spawnEnemies.Clear();
        spawnEnemies.AddRange(tailEnSpawn);

        SpawnBonus[] tailBonSpawn = board.transform.GetComponentsInChildren<SpawnBonus>();
        spawnBonuses.Clear();
        spawnBonuses.AddRange(tailBonSpawn);

        TempleControl templeControl = board.transform.GetComponentInChildren<TempleControl>();
        if (templeControl != null) templeControl.SetLevelControl(gameObject.GetComponent<LevelControl>());

        print($"spawnEnemies={spawnEnemies.Count}  spawnBonuses={spawnBonuses.Count}  Temple HP={templeControl.TempleHP}");
    }

    private void CreatePole()
    {
        pole = new int[countCol * countRow];
        int i, j, index;
        for (i = 0; i < countRow; i++)
        {
            for (j = 0; j < countCol; j++)
            {
                pole[countCol * i + j] = -1;
            }
        }
        TailRoad[] tailRoads = board.transform.GetComponentsInChildren<TailRoad>();
        foreach (TailRoad tailRoad in tailRoads)
        {
            if (tailRoad != null)
            {
                index = tailRoad.NumberTail(offsetX, offsetZ, countCol);
                if ((index >= 0) && (index < countCol * countRow)) pole[index] = 0;
            }
        }
        /*StringBuilder sb = new StringBuilder();
        for (i = 0; i < pole.Length; i++) sb.Append($"{pole[i]}{((i % countCol == (countCol - 1)) ? "\n" : " ")}");
        //for (i = 0; i < pole.Length; i++) sb.Append($"{pole[i]} ");
        Debug.Log(sb.ToString());*/
    }

    private void BeforeSpawnEnemy()
    {
        LevelControl levelControl = gameObject.GetComponent<LevelControl>();
        foreach(SpawnEnemy spawn in spawnEnemies)
        {
            spawn.SetLevelControl(levelControl);
            if (spawn.IsBossSpawn)
            {
                spawn.SetPrefab(prefabEnemy, new EnemyInfo("Boss", 200, 50, 1000, 1000, 5, 3));
                spawn.SetDelaySpawn(600f);
                List<int> path = GetEnemyPath(spawn.SpawnPos);
                List<Vector3> vectors = new List<Vector3>();
                if (path != null && path.Count > 0)
                {
                    foreach (int num in path)
                    {
                        vectors.Add(new Vector3(offsetX + 2 * (num % countCol), 1f, offsetZ - 2 * (num / countCol)));
                        //print($"{vectors.Count}) numTail={num} point={vectors[vectors.Count - 1]}"); 
                    }
                    spawn.TranslatePath(vectors);
                }
            }
            else
            {
                spawn.SetPrefab(prefabEnemy, new EnemyInfo("Воин с молотом", 10, 2, 10, 10, 2, 2));
                spawn.SetDelaySpawn(7f);
            }
        }
    }

    private void BeforeSpawnBonus()
    {
        foreach(SpawnBonus spawn in spawnBonuses)
        {
            spawn.SetSpawnDelay(15f);
        }
    }

    private List<int> GetEnemyPath(Vector3 spawnPos)
    {
        List<int> path = new List<int>();
        int x, y;
        x = Mathf.RoundToInt((spawnPos.x - offsetX) / 2);
        y = Mathf.RoundToInt((offsetZ - spawnPos.z) / 2);
        int[] ends = new int[4] { 32, 35, 50, 53 };
        print($"startPos={countCol * y + x} (x={x}, y={y})");
        WavePath wavePath = new WavePath();
        path = wavePath.GetPath(countCol * y + x, ends, pole, countRow, countCol);
        return path;
    }
}
