//using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class BonusesSpawnControl : MonoBehaviour
{
    [SerializeField] private GameObject[] bonusesPrefabs;
    [SerializeField] private float spawnBonusDelay = 5f;

    private PlayerControl playerControl;

    private List<Vector3> spawnPoints = new List<Vector3>();
    private List<BonusControl> listBonuses = new List<BonusControl>();

    private float timer = 5f;
    private int oldNumBonus = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > 0) timer -= Time.deltaTime;
        else
        {
            timer = spawnBonusDelay;
            GenerateBonus();
        }
        
    }

    public void SetSpawnPoints(List<Vector3> points)
    {
        spawnPoints.Clear();
        foreach (Vector3 point in points)
        {
            spawnPoints.Add(new Vector3(point.x, point.y + 1, point.z));
        }
    }

    private void GenerateBonus()
    {
        int[] numsBonuses = new int[24] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 3, 3, 3, 3, 3, 4, 4, 4, 5};
        int numRndBonus;
        do
        {
            numRndBonus = numsBonuses[Random.Range(0, numsBonuses.Length)];
        } while (oldNumBonus == numRndBonus);
        oldNumBonus = numRndBonus;
        List<Vector3> spawnPos = new List<Vector3>();
        foreach (Vector3 point in spawnPoints)
        {
            bool isNot = true;
            foreach (BonusControl bc in listBonuses)
            {
                if (bc.Position == point) { isNot = false; break; }
            }
            if (isNot) { spawnPos.Add(point); }
        }
        if (spawnPos.Count > 0)
        {
            print($"spawnPos.Count = {spawnPos.Count}   numRndBonus = {numRndBonus}");
            Vector3 pos = spawnPos[Random.Range(0, spawnPos.Count)];
            GameObject bonus = Instantiate(bonusesPrefabs[numRndBonus - 1], pos, Quaternion.identity);
            BonusControl newBC = bonus.GetComponent<BonusControl>();
            if (newBC == null) newBC = bonus.transform.GetChild(0).gameObject.GetComponent<BonusControl>();
            if (newBC != null)
            {
                listBonuses.Add(newBC);
                newBC.SetControls(playerControl, GetComponent<BonusesSpawnControl>());
            }
        }
    }

    public void RemoveBonusControl(BonusControl bc)
    {
        listBonuses.Remove(bc);
    }

    public void SetPlayerControl(PlayerControl control)
    {
        playerControl = control;
    }
}
