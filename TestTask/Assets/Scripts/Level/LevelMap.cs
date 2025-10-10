using System;
using System.Collections.Generic;
using System.Text;
using Unity.VisualScripting;
using UnityEngine;

public class LevelMap : MonoBehaviour
{
    [SerializeField] private GameObject[] setPrefabs;
    [SerializeField] private GameObject[] enemyList;

    private List<MapLevelInfo> mapLevels = new List<MapLevelInfo>();
    private List<GameObject> sets = new List<GameObject>();
    private List<SpawnEnemy> listSpawns = new List<SpawnEnemy>();
    private List<SpawnBonus> listBonusSpawns = new List<SpawnBonus>();
    private int sizePole = 20;
    private int[] pole;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GenerateAllLevels();
        GenerateMap();
        BeginSpawn();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void GenerateAllLevels()
    {
        if (mapLevels.Count > 0) mapLevels.Clear();
        List<SetsInfo> sets = new List<SetsInfo>();
        sets.Add(new SetsInfo(2, 0, 0, 0));
        sets.Add(new SetsInfo(7, 90, 1, 0));
        sets.Add(new SetsInfo(7, 90, 2, 0));
        sets.Add(new SetsInfo(7, 90, 3, 0));
        sets.Add(new SetsInfo(4, 180, 4, 0));
        sets.Add(new SetsInfo(7, 0, 0, 1));
        sets.Add(new SetsInfo(23, 270, 1, 1));
        sets.Add(new SetsInfo(12, 90, 2, 1));
        sets.Add(new SetsInfo(23, 0, 3, 1));
        sets.Add(new SetsInfo(7, 180, 4, 1));
        sets.Add(new SetsInfo(7, 0, 0, 2));
        sets.Add(new SetsInfo(12, 0, 1, 2));
        sets.Add(new SetsInfo(1, 0, 2, 2));
        sets.Add(new SetsInfo(12, 180, 3, 2));
        sets.Add(new SetsInfo(7, 180, 4, 2));
        sets.Add(new SetsInfo(7, 0, 0, 3));
        sets.Add(new SetsInfo(23, 180, 1, 3));
        sets.Add(new SetsInfo(12, 270, 2, 3));
        sets.Add(new SetsInfo(23, 90, 3, 3));
        sets.Add(new SetsInfo(7, 180, 4, 3));
        sets.Add(new SetsInfo(4, 0, 0, 4));
        sets.Add(new SetsInfo(7, 270, 1, 4));
        sets.Add(new SetsInfo(7, 270, 2, 4));
        sets.Add(new SetsInfo(7, 270, 3, 4));
        sets.Add(new SetsInfo(2, 180, 4, 4));
        mapLevels.Add(new MapLevelInfo(1, sets));
    }

    private void GenerateMap()
    {   //  получить из GM номер уровня, а пока единственный нулевой
        MapLevelInfo mapInfo = mapLevels[0];
        pole = new int[sizePole * sizePole];
        int i, j, x, y, setX, setY;
        for (i = 0; i < sizePole * sizePole; i++) pole[i] = -1;
        int[] tmp;
        
        for (i = 0; i < mapInfo.CountSets; i++)
        {
            setX = 4 * (i % 5); setY = (4 * sizePole) * (i / 5);
            SetsInfo setsInfo = mapInfo.GetSetsInfo(i);
            if (setsInfo != null && (setsInfo.ID > 0) && (setsInfo.ID <= setPrefabs.Length))
            {
                GameObject goSet = Instantiate(setPrefabs[setsInfo.ID - 1]);
                SetForLevelInfo setFor = goSet.GetComponent<SetForLevelInfo>();
                if (setFor != null)
                {
                    setFor.SetParams(setsInfo);
                    tmp = setFor.GetTailsTable();
                    for (j = 0; j < tmp.Length; j++)   //  должно быть 16
                    {
                        x = j % 4; y = j / 4;
                        pole[setY + y * sizePole + setX + x] = tmp[j];
                        if (tmp[j] == 4)
                        {   //  spawn tail
                            //print($"i={i}   x={x} y={y} j={j} name={goSet.transform.GetChild(j).name}");
                            //SpawnEnemy spawn = goSet.transform.GetChild(j).gameObject.GetComponent<SpawnEnemy>();
                            SpawnEnemy spawn = goSet.transform.GetChild(5).gameObject.GetComponent<SpawnEnemy>();
                            if (spawn != null)
                            {
                                spawn.UpdateSpawnPosition((x == 2) ? 1 : -1, (y == 2) ? -1 : 1);
                                spawn.SetLevelControl(gameObject.GetComponent<LevelControl>());
                                //print($"i={i} child5   x={x} y={y} j={j} name={spawn.name}  spawnPos=<{spawn.SpawnPos}>");
                                listSpawns.Add(spawn);
                            }
                            else
                            {
                                spawn = goSet.transform.GetChild(9).gameObject.GetComponent<SpawnEnemy>();
                                spawn.UpdateSpawnPosition((x == 2) ? 1 : -1, (y == 2) ? -1 : 1);
                                spawn.SetLevelControl(gameObject.GetComponent<LevelControl>());
                                //print($"i={i} child9   x={x} y={y} j={j} name={spawn.name}  spawnPos=<{spawn.SpawnPos}>");
                                if (spawn != null) listSpawns.Add(spawn);
                            }
                            //print($"i={i}   pos={spawn.transform.position} name={spawn.name}");
                        }
                    }
                    setFor.SetPositionAndRotation(-16f, 8f, 16f, 8, 0);
                    if (setsInfo.ID > 1)
                    {   //  будем искать RoadEnd
                        SpawnBonus[] spawnBonuses = goSet.GetComponentsInChildren<SpawnBonus>();
                        if (spawnBonuses.Length > 0)
                        {
                            listBonusSpawns.AddRange(spawnBonuses);
                            /*StringBuilder sb = new StringBuilder($"i={i}  count={spawnBonuses.Length} ");
                            for (int k = 0; k < spawnBonuses.Length; k++)
                            {
                                sb.Append($"k={k} name={spawnBonuses[k].name} pos={spawnBonuses[k].transform.position} ");
                            }
                            print(sb.ToString());*/
                        }
                    }
                }
                sets.Add(goSet);
            }
        }
    }

    private void BeginSpawn()
    {
        print($"count spawn = {listSpawns.Count}");
        
        foreach(SpawnEnemy spawn in listSpawns)
        {
            spawn.SetPrefab(enemyList[0], new EnemyInfo("Воин с молотом", 10, 5, 10, 10, 2));
            spawn.SetDelaySpawn(5f);
            List<int> path = GetEnemyPath(spawn.SpawnPos);
            List<Vector3> vectors = new List<Vector3>();
            if (path != null && path.Count > 0)
            {
                foreach (int num in path)
                {
                    vectors.Add(new Vector3(-19f + 2 * (num % sizePole), 1f, 19f - 2 * (num / sizePole)));
                }
                spawn.TranslatePath(vectors);

                List<int> nums = new List<int>();
                for (int i = 0; i < listBonusSpawns.Count; i++)
                {
                    Vector3 pos = listBonusSpawns[i].gameObject.transform.position;
                    pos.y = 1f;
                    foreach (Vector3 vec in vectors)
                    {
                        Vector3 delta = pos - vec;
                        if (delta.magnitude < 0.1f) { nums.Add(i); break; }
                    }
                }
                if (nums.Count > 0)
                {
                    foreach(int num in nums) listBonusSpawns.RemoveAt(num);
                }
            }
        }
        //  передача списка точек для спавна бонусов
        BonusesSpawnControl bsc = GetComponent<BonusesSpawnControl>();
        PlayerControl playerControl = gameObject.GetComponent<LevelControl>().GetPlayerControl();
        bsc.SetPlayerControl(playerControl);
        List<Vector3> spawnPoints = new List<Vector3>();
        foreach(SpawnBonus spawnBonus in listBonusSpawns) spawnPoints.Add(spawnBonus.transform.position);
        if (bsc != null) bsc.SetSpawnPoints(spawnPoints);
    }

    private List<int> GetEnemyPath(Vector3 spawnPos)
    {
        List<int> path = new List<int>();
        int x, y;
        x = Mathf.RoundToInt((spawnPos.x + 19.0f) / 2);
        y = Mathf.RoundToInt((19.0f - spawnPos.z) / 2);
        int[] ends = new int[4]{ 188, 191, 208, 211 };
        print($"startPos={sizePole * y + x} (x={x}, y={y})");
        WavePath wavePath = new WavePath();
        path = wavePath.GetPath(sizePole * y + x, ends, pole, sizePole, sizePole);
        return path;
    }

    private void PauseSpawn(bool isPause)
    {
        foreach (SpawnEnemy spawn in listSpawns)
        {
            spawn.SetPause(isPause);
        }
    }
}

[Serializable]
public class MapLevelInfo
{
    private int level;
    private List<SetsInfo> setTails = new List<SetsInfo>();

    public int LevelNumber {  get { return level; } }
    public int CountSets {  get { return setTails.Count; } }

    public MapLevelInfo() { }
    public MapLevelInfo(int lev, List<SetsInfo> list)
    {
        level = lev;
        setTails = list;
    }

    public MapLevelInfo(string csv, string sep = "#")
    {
        string[] ar = csv.Split(sep, StringSplitOptions.RemoveEmptyEntries);
        if (ar != null && ar.Length > 1)
        {
            level = int.Parse(ar[0]);
            if (int.TryParse(ar[1], out int count))
            {
                for (int i = 0; i < count; i++)
                {
                    setTails.Add(new SetsInfo(ar[i + 2], sep = "="));
                }
            }
        }
    }

    public SetsInfo GetSetsInfo(int index)
    {
        if (index >= 0 && index < setTails.Count)
        {
            return setTails[index];
        }
        return null;
    }

    public string ToCsvString(string sep = "#")
    {
        StringBuilder sb = new StringBuilder($"{level}{sep}{setTails.Count}{sep}");
        for (int i = 0; i < setTails.Count; i++) 
        {
            sb.Append($"{setTails[i].ToCsvString("=")}{sep}");
        }
        return sb.ToString();
    }
}
