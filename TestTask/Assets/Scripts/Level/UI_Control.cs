using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_Control : MonoBehaviour
{
    [SerializeField] private Text txtExp;
    [SerializeField] private Text txtMany;
    [SerializeField] private Text txtEnergy;
    [SerializeField] private Text txtHP;

    [SerializeField] private Text txtAptechka;
    [SerializeField] private GameObject aptechkaPanel;

    [SerializeField] private GameObject storePanel;
    [SerializeField] private Button btnSellApple;
    [SerializeField] private Button btnSellAptechka;
    [SerializeField] private Text txtCold;

    [SerializeField] private GameObject lossPanel;
    [SerializeField] private GameObject winPanel;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ViewExp(0);
        ViewMany(0);
        ViewEnergy(0);
        ViewHP(0);
        ViewAptechka(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ViewLossPanel()
    {
        lossPanel.SetActive(true);
    }

    public void ViewExp(int exp)
    {
        if (txtExp != null) txtExp.text = exp.ToString();
    }

    public void ViewMany(int many)
    {
        if (txtMany != null) txtMany.text = many.ToString();
    }

    public void ViewEnergy(int energy)
    {
        if (txtEnergy != null) txtEnergy.text = energy.ToString();
    }

    public void ViewHP(int hp)
    {
        if (txtHP) txtHP.text = hp.ToString();
    }

    public void ViewAptechka(int value)
    {
        if (value > 0)
        {
            aptechkaPanel.SetActive(true);
            txtAptechka.text = value.ToString();
        }
        else
        {
            aptechkaPanel.SetActive(false);
        }
    }

    public void ViewStore(int gold, bool isMaxHP)
    {
        txtCold.text = $"Всего :  {gold}";
        btnSellAptechka.interactable = (gold >= 100);
        btnSellApple.interactable = (gold >= 50) && (false == isMaxHP);
        storePanel.SetActive(true);
    }
}
