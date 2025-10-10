using UnityEngine;

public class SpawnBonus : MonoBehaviour
{
    [SerializeField] private int numBonus;
    [SerializeField] private GameObject prefabBonus;

    private GameObject bonus = null;
    private float timer = 1f;

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
            CheckAndSpawnBonus();
        }
    }

    private void CheckAndSpawnBonus()
    {
        if ((numBonus > 0) && (bonus  == null))
        {
            bonus = Instantiate(prefabBonus, transform.position, Quaternion.identity);
        }
    }
}
