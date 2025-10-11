using UnityEngine;

public class LevelControl : MonoBehaviour
{
    [SerializeField] private UI_Control ui_Control;
    [SerializeField] private PlayerControl playerControl;

    private int currentMany = 0;
    private int currentExp = 0;
    private int currentPlayerLevel = 0;

    public int CurrentMany { get => currentMany; }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnemyDestroy(int price, int exp)
    {
        currentExp += exp;
        CheckCurrentPlayerLevel();
        if (ui_Control !=null) ui_Control.ViewExp(currentExp);
        currentMany += price;
        if (ui_Control != null) ui_Control.ViewMany(currentMany);
    }

    public void CheckCurrentPlayerLevel()
    {
        int lvl = currentExp / 100;
        if (currentPlayerLevel < lvl)
        {   //  повышение уровня игрока
            currentPlayerLevel = lvl;
            playerControl.SetMaxHP(100 + 10 * lvl);
        }
    }

    public bool CheckMany(int zn)
    {
        return currentMany >= zn;
    }

    public void ChangeMany(int zn)
    {
        currentMany += zn;
        if (ui_Control != null) ui_Control.ViewMany(currentMany);
    }

    public void ViewPlayerEnergy(int energy)
    {
        if (ui_Control != null) ui_Control.ViewEnergy(energy);
    }

    public void ViewPlayerHP(int hp)
    {
        if (ui_Control != null) ui_Control.ViewHP(hp);
    }

    public void ViewAptechka(int count)
    {
        if (ui_Control != null) ui_Control.ViewAptechka(count);
    }

    public void ViewStore()
    {
        ui_Control.ViewStore(currentMany, playerControl.IsMaxHP);
    }

    public void SellStory(int price)
    {
        currentMany -= price;
        ui_Control.ViewMany(currentMany);
        ViewStore();
    }

    public PlayerControl GetPlayerControl()
    {
        return playerControl;
    }

    public void ViewLossPanel()
    {
        ui_Control.ViewLossPanel();
    }
}
