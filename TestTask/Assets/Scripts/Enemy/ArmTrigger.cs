using UnityEngine;

public class ArmTrigger : MonoBehaviour
{
    private int damage = 0;
    //private bool isAttack = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetDamage(int dmg)
    {
        damage = dmg;
        //print($"molot  =>  damage = {damage}");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("EnemyTarget"))
        {
            //isAttack = true;
            /*if (other.CompareTag("Player"))
            {
                other.gameObject.GetComponent<PlayerControl>().ChangeHP(-damage);
            }*/
            if (other.CompareTag("Temple"))
            {
                other.gameObject.GetComponent<TempleControl>().ChangeHP(-damage);
            }
        }
    }

    /*private void EndAttack()
    {
        isAttack = false;
    }*/
}
