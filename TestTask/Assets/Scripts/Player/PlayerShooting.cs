using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float shootDelay = 1f;
    [SerializeField] private float speed = 50f;
    [SerializeField] private LevelControl levelControl;

    private int currentEnergy = 100;
    private int maxEnergy = 100;

    private Transform bulletSpawn;
    private int bulletDamage = 5;
    private float timer = 0;
    private Animator anim;
    private bool isShoot = false;

    private void Awake()
    {
        bulletSpawn = transform.GetChild(1).gameObject.GetComponent<Transform>();
        anim = GetComponent<Animator>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > 0) timer -= Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (timer <= 0) Fire();
        }
    }

    private void Fire()
    {
        timer = shootDelay;
        if (currentEnergy > 0)
        {
            currentEnergy--;
            levelControl.ViewPlayerEnergy(currentEnergy);
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, transform.rotation);
            bullet.GetComponent<BulletControl>().SetDamage(bulletDamage);
            bullet.GetComponent<Rigidbody>().AddForce(transform.forward * speed);
            Destroy(bullet, 5f);
            if (isShoot == false)
            {
                //anim.SetFloat("WalkSpeed", 0);
                anim.SetBool("IsShoot", true);
                isShoot = true;
                Invoke("EndShoot", 0.55f);
            }
        }
    }

    private void EndShoot()
    {
        if (isShoot)
        {
            anim.SetBool("IsShoot", false);
            isShoot = false;
        }
    }

    public void SetMoveSpeed(float speed)
    {
        this.speed = speed;
    }

    public void SetShootingDelay(float shootDelay)
    {
        this.shootDelay = shootDelay;
    }

    public void SetBulletDamage(int dmg)
    {
        bulletDamage = dmg;
    }

    public void SetEnergy(int maxEnergy)
    {
        this.maxEnergy = maxEnergy;
        currentEnergy = maxEnergy;
        levelControl.ViewPlayerEnergy(currentEnergy);
    }

    public void ChangeEnergy(int zn)
    {
        if (currentEnergy + zn < 0) currentEnergy = 0;
        else if (currentEnergy + zn > maxEnergy) currentEnergy = maxEnergy;
        else currentEnergy += zn;
        levelControl.ViewPlayerEnergy(currentEnergy);
    }
}
