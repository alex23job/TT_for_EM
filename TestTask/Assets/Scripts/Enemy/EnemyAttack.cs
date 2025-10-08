using System.Text;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private float detectionRadius = 0;
    private float timer = 0.25f;

    private EnemyControl enemyControl;

    private void Awake()
    {
        enemyControl = GetComponent<EnemyControl>();
    }
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
            timer = 0.25f;
            detectionRadius = enemyControl.Radius;
            Transform target = GetTarget();
            if (target != null && enemyControl != null)
            {
                enemyControl.Attack(target);
                //print($"name={target.name} pos={target.position}  layer={LayerMask.NameToLayer("EnemyTarget")}");
            }
        }
    }

    public Transform GetTarget()
    {
        Transform target = null;
        // Получаем ближайших врагов
        Collider[] enemiesInRange = Physics.OverlapSphere(transform.position, detectionRadius, 1 << LayerMask.NameToLayer("EnemyTarget"));

        if (enemiesInRange.Length > 0)
        {
            /*StringBuilder sb = new StringBuilder($"radius={detectionRadius} ");
            foreach (Collider coll in enemiesInRange) { sb.Append($"<{coll.name}> "); }
            print( sb.ToString() );*/

            // Находим ближайшего врага
            target = FindNearest(enemiesInRange);

            // Логика обработки ближайшего врага
            //Debug.Log("Ближайший враг: " + nearestEnemy.name);
        }


        return target;
    }

    Transform FindNearest(Collider[] colliders)
    {
        Transform closest = null;
        float smallestDistance = Mathf.Infinity;

        foreach (Collider collider in colliders)
        {
            float dist = Vector3.Distance(transform.position, collider.transform.position);
            if (dist < smallestDistance)
            {
                smallestDistance = dist;
                closest = collider.transform;
            }
        }

        return closest;
    }

}
