using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBonus : MonoBehaviour
{
    private PlayerControl playerControl;
    private PlaySounds playSounds;

    private void Awake()
    {
        playerControl = GetComponent<PlayerControl>();
        playSounds = GetComponent<PlaySounds>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        //print($"PlayerBonus OnTriggerEnter other={other.name} tag={other.tag}");
        if (other.CompareTag("Bonus"))
        {
            other.gameObject.tag = "Untagged";
            Bonus bonus = other.transform.parent.gameObject.GetComponent<Bonus>();
            //print($"PC=<{playerControl}>   bonus=<{bonus}>");
            if ((playerControl != null) && (bonus != null))
            {
                //print($"Bonus type={bonus.BonusType} value={bonus.Value}");
                switch(bonus.BonusType)
                {
                    case 0:
                        GetArm(bonus);
                        playSounds.PlayClip(4);
                        break;
                    case 1:
                        playerControl.AddingMany(bonus.Value);
                        playSounds.PlayClip(5);
                        break;
                    case 2:
                        playerControl.ChangeHP(bonus.Value);
                        playSounds.PlayClip(0);
                        break;
                    case 3:
                        playerControl.AddingAptecka();
                        playSounds.PlayClip(1);
                        break;
                }
                bonus.MinAndDestroy();
            }
            else
            {
                Destroy(other.gameObject);
            }
        }
    }

    private void GetArm(Bonus bonus)
    {
        print($"bonus name={bonus.name}");
        SelectArm selectArm = gameObject.GetComponent<SelectArm>();
        if (bonus.name == "Axe")
        {
            selectArm.SetAvailableArm(1);
            selectArm.SelectCurrentArm(1);
        }
        if (bonus.name == "Mace")
        {
            selectArm.SetAvailableArm(2);
            selectArm.SelectCurrentArm(2);
        }
    }
}
