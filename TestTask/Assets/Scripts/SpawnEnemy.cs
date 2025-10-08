//using NUnit.Framework;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    private GameObject enemyPrefab;
    private EnemyInfo enemyInfo;
    private List<Vector3> path = new List<Vector3>();
    private float spawnDelay = 0;
    private float timer;
    private bool isPause = false;
    private Vector3 spawnPosition = Vector3.zero;
    private Vector2 delta = Vector2.zero;
    private LevelControl levelControl = null;

    public Vector3 SpawnPos { get => spawnPosition; }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isPause == false)
        {
            if (timer > 0) timer -= Time.deltaTime;
            else
            {
                timer = spawnDelay;
                Spawn();
            }
        }
    }

    public void SetLevelControl(LevelControl lc)
    {
        levelControl = lc;
    }

    public void SetPrefab(GameObject prefab, EnemyInfo enemyInfo)
    {
        enemyPrefab = prefab;
        this.enemyInfo = enemyInfo;
        if (spawnPosition == Vector3.zero)
        {
            spawnPosition = transform.parent.position;
            spawnPosition.x += delta.x;
            spawnPosition.y = 0.5f;
            spawnPosition.z += delta.y;
            //print($"SpawnPos={SpawnPos}");
        }
    }

    public void UpdateSpawnPosition(int dx, int dy)
    {
        delta = new Vector2( dx, dy );
        /*if (spawnPosition == Vector3.zero)
        {
            spawnPosition = transform.parent.position;
            spawnPosition.x += dx;
            spawnPosition.y = 0.5f;
            spawnPosition.z += dy;
            print($"SpawnPos={SpawnPos}");
        }*/
    }

    public void TranslatePath(List<Vector3> newPath)
    {
        path = newPath;
        /*StringBuilder sb = new StringBuilder();
        for (int i = 0; i < path.Count; i++) sb.Append($"<{path[i]}> ");
        print( sb.ToString() );*/
    }

    public void SetDelaySpawn(float delay)
    {
        spawnDelay = delay;
        timer = spawnDelay;
    }

    public void SetPause(bool pause)
    {
        isPause = pause;
    }

    private void Spawn()
    {
        Vector3 pos = transform.position;
        pos.y = 0.5f;
        //GameObject enemy = Instantiate(enemyPrefab, transform.position, transform.rotation);
        GameObject enemy = Instantiate(enemyPrefab, pos, transform.rotation);
        //print($"Enemy spawn position = <{enemy.transform.position}> (rot=<{enemy.transform.rotation.eulerAngles}>)     point[0] = <{path[0]}>");
        enemy.GetComponent<EnemyMovement>().SetPath(path);
        enemy.GetComponent<EnemyControl>().SetParams(levelControl, enemyInfo);
    }
}
