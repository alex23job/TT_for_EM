using System;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    private int hp;
    private float maxHP = 1f;
    private int damage;
    private int price;
    private int exp;
    private float radius = 1.5f;
    private LevelControl levelControl;
    private EnemyMovement enemyMovement;
    private ArmTrigger armTrigger;
    private EnemyHP enemyHP;
    private GameObject enemyViewHP= null;

    public float Radius { get { return radius; } }

    private void Awake()
    {
        enemyMovement = GetComponent<EnemyMovement>();
        armTrigger = GetComponentInChildren<ArmTrigger>();
        enemyHP = GetComponentInChildren<EnemyHP>();
        enemyViewHP = transform.GetChild(0).gameObject;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemyViewHP.SetActive(false);
        //Test();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Test()
    {
        radius = 1.5f;
        hp = 50;
        maxHP = hp;
        price = 10;
        exp = 10;
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
        maxHP = hp;
        this.exp = exp;
        price = prc;
        if (armTrigger != null) armTrigger.SetDamage(damage);
    }

    public void SetParams(LevelControl lc, EnemyInfo ei)
    {
        levelControl = lc;
        hp = ei.Hp;
        maxHP = hp;
        exp = ei.Exp;
        radius = ei.Radius;
        damage = ei.Damage;
        price = ei.Price;
        if (ei.NumArm > 0) gameObject.GetComponent<SelectArm>().SelectCurrentArm(ei.NumArm);
        if (armTrigger != null) armTrigger.SetDamage(damage);
    }

    public void ChangeHP(int zn)
    {
        if ((zn < 0) && ((hp + zn) <= 0))
        {   //  убит
            hp = 0;
            if (levelControl != null)
            {
                levelControl.EnemyDestroy(price, exp);
                price = 0;
                exp = 0;
            }
            enemyViewHP.SetActive(false);
            enemyMovement.EnemyDead();
            Destroy(gameObject, 1.5f);
        }
        else
        {
            hp += zn;
            enemyViewHP.SetActive(true);
            enemyHP.ViewHP((float)hp / maxHP);
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
    private int numArm;

    public string NameEnemy { get => nameEnemy; }
    public int Hp { get => hp; }
    public int Damage { get => damage; }
    public int Price { get => price; }
    public int Exp { get => exp; }
    public float Radius { get => radius; }

    public int NumArm { get => numArm; }

    public EnemyInfo() { }
    public EnemyInfo(string nm, int hp, int dmg, int prc, int exp, float rad, int numArm = 0)
    {
        nameEnemy = nm;
        this.hp = hp;
        this.damage = dmg;
        this.price = prc;
        this.exp = exp;
        this.radius = rad;
        this.numArm = numArm;
    }

    public void SetNumberArm(int num)
    {
        this.numArm = num;
    }
}
