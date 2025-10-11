using UnityEngine;

public class SpawnBonus : MonoBehaviour
{
    [SerializeField] private int numBonus;
    [SerializeField] private GameObject prefabBonus;

    private GameObject bonus = null;
    private float timer = 1f;
    private float spawnDelay = 5f;
    private bool isTaskSpawn = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            timer = 1f;
            CheckAndSpawnBonus();
        }
    }

    private void CheckAndSpawnBonus()
    {
        if ((numBonus > 0) && (bonus  == null))
        {
            if (false == isTaskSpawn)
            {
                Invoke("SpawnNewBonus", spawnDelay);
                isTaskSpawn = true;
            }
        }
    }

    private void SpawnNewBonus()
    {
        bonus = Instantiate(prefabBonus, transform.position, Quaternion.identity);
        isTaskSpawn = false;
    }

    public void SetSpawnDelay(float delay)
    {
        spawnDelay = delay;
    }
}
