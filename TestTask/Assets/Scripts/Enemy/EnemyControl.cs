using System;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    private int hp;
    private int damage;
    private int price;
    private int exp;
    private float radius = 1.5f;
    private LevelControl levelControl;
    private EnemyMovement enemyMovement;
    private ArmTrigger armTrigger;

    public float Radius { get { return radius; } }

    private void Awake()
    {
        enemyMovement = GetComponent<EnemyMovement>();
        armTrigger = GetComponentInChildren<ArmTrigger>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Attack(Transform target)
    {
        if (enemyMovement != null) enemyMovement.Attack(target, damage);
    }

    public void SetParams(LevelControl lc, int hp, int dmg, int rad, int prc, int exp)
    {
        levelControl = lc;
        damage = dmg;
        radius = rad;
        this.hp = hp;
        this.exp = exp;
        price = prc;
        if (armTrigger != null) armTrigger.SetDamage(damage);
    }

    public void SetParams(LevelControl lc, EnemyInfo ei)
    {
        levelControl = lc;
        hp = ei.Hp;
        exp = ei.Exp;
        radius = ei.Radius;
        damage = ei.Damage;
        price = ei.Price;
        if (armTrigger != null) armTrigger.SetDamage(damage);
    }

    public void ChangeHP(int zn)
    {
        if ((zn < 0) && ((hp + zn) <= 0))
        {   //  убит
            hp = 0;
            if (levelControl != null) levelControl.EnemyDestroy(price, exp);
            enemyMovement.EnemyDead();
            Destroy(gameObject, 1.5f);
        }
        else
        {
            hp += zn;
        }
    }
}

[Serializable]
public class EnemyInfo
{
    private string nameEnemy;
    private int hp;
    private int damage;
    private int price;
    private int exp;
    private float radius;

    public string NameEnemy { get => nameEnemy; }
    public int Hp { get => hp; }
    public int Damage { get => damage; }
    public int Price { get => price; }
    public int Exp { get => exp; }
    public float Radius { get => radius; }

    public EnemyInfo() { }
    public EnemyInfo(string nm, int hp, int dmg, int prc, int exp, float rad)
    {
        nameEnemy = nm;
        this.hp = hp;
        this.damage = dmg;
        this.price = prc;
        this.exp = exp;
        this.radius = rad;
    }
}
