using UnityEngine;

public class BonusControl : MonoBehaviour
{
    [SerializeField] private int bonusID = 0;

    private BonusesSpawnControl bonusSpawnControl;
    private PlayerControl playerControl;
    private Vector3 _position;
    private bool isSend = false;
    private Animator anim;
    public Vector3 Position { get => _position; }

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _position = transform.position;
        if (transform.parent != null) _position = transform.parent.position;        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetControls(PlayerControl control, BonusesSpawnControl bsc)
    {
        playerControl = control;
        bonusSpawnControl = bsc;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!isSend)
            {
                isSend = true;
                switch(bonusID)
                {
                    case 1: //  many
                        playerControl.AddingMany(5);
                        anim.SetTrigger("MinTrigger");
                        break;
                    case 2: //  energy
                        anim.SetTrigger("MinTrigger");
                        playerControl.AddingEnergy(20);
                        break;
                    case 3: //  apple
                        anim.SetTrigger("MinTrigger");
                        playerControl.ChangeHP(20);
                        break;
                    case 4: //  aptechka
                        anim.SetTrigger("MinTrigger");
                        break;
                    case 5: //  chest
                        anim.SetBool("IsOpen", true);
                        break;
                }
                bonusSpawnControl.RemoveBonusControl(GetComponent<BonusControl>());
                Destroy(gameObject, 1f);
            }
        }
    }
}
