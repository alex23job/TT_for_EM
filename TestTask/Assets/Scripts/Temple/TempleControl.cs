using UnityEngine;

public class TempleControl : MonoBehaviour
{
    private Animator _animDoorLeft, _animDoorRight;

    private int maxTempleHP = 1000;
    private int templeHP = 1000;

    private float timer = 1f;
    //private int count = 0;

    private void Awake()
    {
        _animDoorLeft = transform.GetChild(1).GetComponent<Animator>();
        _animDoorRight = transform.GetChild(2).GetComponent<Animator>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //_anim.SetTrigger("StartTrigger");
    }

    // Update is called once per frame
    void Update()
    {
        /*if (timer > 0f) timer -= Time.deltaTime;
        else
        {
            timer = 1f;
            switch(count)
            {
                case 0:
                    _animDoorRight.SetTrigger("DROpenTrigger");
                    break;
                case 1:
                    _animDoorRight.SetTrigger("DRCloseTrigger");
                    break;
                case 2:
                    _animDoorLeft.SetTrigger("DLOpenTrigger");
                    break;
                case 3:
                    _animDoorLeft.SetTrigger("DLCloseTrigger");
                    break;
            }
            count++;
            count %= 4;
            //print(count);
        }*/
    }

    public void ChangeHP(int zn)
    {
        if (templeHP + zn > maxTempleHP) templeHP = maxTempleHP;
        else if (templeHP + zn < 0)
        {   //  наш замок разрушен !!!
            templeHP = 0;
            Destroy(gameObject);
        }
        else templeHP += zn;

    }
}
