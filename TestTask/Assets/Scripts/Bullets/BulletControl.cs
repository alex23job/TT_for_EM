using UnityEngine;

public class BulletControl : MonoBehaviour
{
    private int damage = 0;
    private bool isArm = false;
    private bool isPlayerArm = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetDamage(int dmg, bool isArm = false, bool isPlayerArm = false)
    {
        damage = dmg;
        this.isArm = isArm;
        this.isPlayerArm = isPlayerArm;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (isPlayerArm) return;
            else
            {
                PlayerControl playerControl = other.gameObject.GetComponent<PlayerControl>();
                if (playerControl != null) playerControl.ChangeHP(-damage);
            }
        }
        if (other.CompareTag("Enemy"))
        {
            if (false == isPlayerArm) return;
            print($"name={gameObject.name}  damage={damage}");
            EnemyControl enemyControl = other.gameObject.GetComponent<EnemyControl>();
            if (enemyControl != null) enemyControl.ChangeHP(-damage);
            //Destroy(other.gameObject);
        }
        if (false == isArm) Destroy(gameObject);
    }
}
