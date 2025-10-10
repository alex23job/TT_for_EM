//using NUnit.Framework;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    [SerializeField] private int spawnID = 0;
    [SerializeField] private int numberEnemyArm = 0;

    private GameObject enemyPrefab;
    private EnemyInfo enemyInfo;
    private List<Vector3> path = new List<Vector3>();
    private bool isPatrouille = true;
    private float spawnDelay = 0;
    private float timer;
    private bool isPause = false;
    private Vector3 spawnPosition = Vector3.zero;
    private Vector2 delta = Vector2.zero;
    private LevelControl levelControl = null;
    private bool isBoss = false;

    public Vector3 SpawnPos { get => spawnPosition; }
    public bool IsBossSpawn { get => isBoss; }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (transform.localScale.x > 140) isBoss = true;
        isPause = true;
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
        if (numberEnemyArm > 0)
        {
            this.enemyInfo = new EnemyInfo(enemyInfo.NameEnemy, enemyInfo.Hp + 5 * numberEnemyArm, enemyInfo.Damage, enemyInfo.Price + 3 * numberEnemyArm,
                enemyInfo.Exp + 5 * numberEnemyArm, enemyInfo.Radius + 0.2f * numberEnemyArm, numberEnemyArm);
        }
        enemyInfo.SetNumberArm(numberEnemyArm);
        if (spawnPosition == Vector3.zero)
        {
            //spawnPosition = transform.parent.position;
            spawnPosition = transform.position;
            spawnPosition.x += delta.x;
            spawnPosition.y = 0.25f;
            spawnPosition.z += delta.y;
            
            if (Mathf.Abs(transform.forward.x) > 0.5f)
            {
                path.Add(new Vector3(spawnPosition.x + 2 * transform.forward.x, spawnPosition.y, spawnPosition.z - 1.2f));
                path.Add(new Vector3(spawnPosition.x + 2 * transform.forward.x, spawnPosition.y, spawnPosition.z + 1.2f));
                spawnPosition.x += 1.5f * transform.forward.x;
                if (isBoss) spawnPosition.x += 1.5f * transform.forward.x;
            }
            if (Mathf.Abs(transform.forward.z) > 0.5f)
            {
                path.Add(new Vector3(spawnPosition.x - 1.2f, spawnPosition.y, spawnPosition.z + 2 * transform.forward.z));
                path.Add(new Vector3(spawnPosition.x + 1.2f, spawnPosition.y, spawnPosition.z + 2 * transform.forward.z));
                spawnPosition.z += 1.5f * transform.forward.z;
                if (isBoss) spawnPosition.z += 1.5f * transform.forward.z;
            }
            //print($"TrPos={transform.position} SpawnPos={SpawnPos}  forward={transform.forward} pathCount={path.Count} p1={path[0]} p2={path[1]}");
            //if (path.Count > 2) print($"p3={path[2]} p4={path[3]}");
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

    public void TranslatePath(List<Vector3> newPath, bool isPatrouille = false)
    {
        path = newPath;
        this.isPatrouille = isPatrouille;
        /*StringBuilder sb = new StringBuilder();
        for (int i = 0; i < path.Count; i++) sb.Append($"<{path[i]}> ");
        print( sb.ToString() );*/
    }

    public void SetDelaySpawn(float delay)
    {
        spawnDelay = delay;
        //timer = spawnDelay;
        timer = 0.25f;
    }

    public void SetPause(bool pause)
    {
        isPause = pause;        
    }

    private void Spawn()
    {
        //Vector3 pos = transform.position;
        Vector3 pos = spawnPosition;
        pos.y = 0.25f;
        //GameObject enemy = Instantiate(enemyPrefab, transform.position, transform.rotation);
        GameObject enemy = Instantiate(enemyPrefab, pos, transform.rotation);
        if (isBoss)
        {
            enemy.transform.localScale = enemy.transform.localScale * 1.3f;
            timer = 300f;
            enemyInfo = new EnemyInfo("Boss", 500, 50, 1000, 1000, 3, 3);
            enemy.GetComponent<EnemyControl>().SetParams(levelControl, enemyInfo);
        }
        //print($"Enemy spawn position = <{enemy.transform.position}> (rot=<{enemy.transform.rotation.eulerAngles}>)     point[0] = <{path[0]}>");
        enemy.GetComponent<EnemyMovement>().SetPath(path, isPatrouille);
        enemy.GetComponent<SelectArm>().SetArmDamage(new int[] { 2, 4, 7, 10 });
        enemy.GetComponent<EnemyControl>().SetParams(levelControl, enemyInfo);
    }

    private void OnTriggerEnter(Collider other)
    {
        isPause = false;
    }

    private void OnTriggerExit(Collider other)
    {
        isPause = true;
    }
}
