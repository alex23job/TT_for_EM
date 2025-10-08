using UnityEngine;
using UnityEngine.UI;

public class UI_Control : MonoBehaviour
{
    [SerializeField] private Text txtExp;
    [SerializeField] private Text txtMany;
    [SerializeField] private Text txtEnergy;
    [SerializeField] private Text txtHP;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ViewExp(0);
        ViewMany(0);
        ViewEnergy(0);
        ViewHP(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ViewExp(int exp)
    {
        txtExp.text = exp.ToString();
    }

    public void ViewMany(int many)
    {
        txtMany.text = many.ToString();
    }

    public void ViewEnergy(int energy)
    {
        txtEnergy.text = energy.ToString();
    }

    public void ViewHP(int hp)
    {
        txtHP.text = hp.ToString();
    }
}
