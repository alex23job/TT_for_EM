using UnityEngine;

public class LevelControl : MonoBehaviour
{
    [SerializeField] private UI_Control ui_Control;
    [SerializeField] private PlayerControl playerControl;

    private int currentMany = 0;
    private int currentExp = 0;

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
        if (ui_Control !=null) ui_Control.ViewExp(currentExp);
        currentMany += price;
        if (ui_Control != null) ui_Control.ViewMany(currentMany);
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

    public PlayerControl GetPlayerControl()
    {
        return playerControl;
    }
}
