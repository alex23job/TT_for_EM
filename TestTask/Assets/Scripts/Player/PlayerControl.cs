using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] private int maxEnergy = 100;
    [SerializeField] private int recoverableEnergy = 1;
    [SerializeField] private int maxHP = 100;
    [SerializeField] private LevelControl levelControl;

    private PlayerShooting shooting;

    private int currentHP;
    private float timer = 0.5f;
    private int countAptechka = 0;

    public int CurrentHP { get => currentHP; }
    public bool IsMaxHP { get { return (currentHP == maxHP); }  }

    private void Awake()
    {
        shooting = GetComponent<PlayerShooting>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Invoke("TranslateParams", 0.2f);
        currentHP = maxHP;
        ViewHP();
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > 0) timer -= Time.deltaTime;
        else
        {
            timer = 0.5f;
            //if ((transform.position.x > -1f && transform.position.x < 1f) && (transform.position.z > -1f && transform.position.z < 1f)) shooting.ChangeEnergy(recoverableEnergy);
        }
    }

    private void TranslateParams()
    {
        SendShootingParams();
        ViewHP();
    }

    private void SendShootingParams()
    {
        //shooting.SetEnergy(120);    //  это пока, а так из GM надо взять
    }

    public void ChangeHP(int zn)
    {
        if (currentHP + zn > maxHP) currentHP = maxHP;
        else if (currentHP + zn < 0)
        {   //  наш защитник убит !!!
            currentHP = 0;
            PlayerMovement playerMovement = gameObject.GetComponent<PlayerMovement>();
            if (playerMovement != null) playerMovement.PlayerLoss();
            Invoke("ViewLossPanel", 2f);
        }
        else currentHP += zn;
        if ((currentHP < (maxHP / 2)) && (countAptechka > 0))
        {   //  использование аптечки
            countAptechka--;
            currentHP += 20;
            levelControl.ViewAptechka(countAptechka);
        }
        ViewHP();
    }

    public void ViewLossPanel()
    {
        if (levelControl != null) levelControl.ViewLossPanel("Ваш защитник убит !!!");
    }

    public void AddingAptecka()
    {
        countAptechka++;
        ChangeHP(0);
        levelControl.ViewAptechka(countAptechka);
    }

    public void AddingStoreAptechka()
    {
        AddingAptecka();
        levelControl.SellStory(100);
    }

    public void SellApple()
    {
        ChangeHP(10);
        levelControl.SellStory(50);
    }

    public void AddingMany(int zn)
    {
        levelControl.ChangeMany(zn);
    }

    public void AddingEnergy(int zn)
    {
        shooting.ChangeEnergy(zn);
    }

    public void SetMaxHP(int newMaxHP)
    {
        maxHP = newMaxHP;
        ChangeHP(0);
    }

    private void ViewHP()
    {
        if (levelControl != null) levelControl.ViewPlayerHP(currentHP);
    }

    public void ViewStore()
    {
        if (levelControl != null)
        {
            levelControl.ViewStore();
        }
    }
}
