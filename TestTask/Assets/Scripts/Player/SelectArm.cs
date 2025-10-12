using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectArm : MonoBehaviour
{
    [SerializeField] private GameObject[] arms;

    private int[] armDamage = null;
    private int currentArmIndex = 0;
    private bool isPlayerArm = false;
    private bool[] playerArm;

    public int ArmIndex { get => currentArmIndex; }

    // Start is called before the first frame update
    void Start()
    {
        playerArm = new bool[arms.Length];
        for (int i = 0; i < arms.Length; i++)
        {
            if (i == 0) playerArm[i] = true;
            else playerArm[i] = false;
        }
        PlayerControl playerControl = gameObject.GetComponent<PlayerControl>(); 
        if (playerControl != null) isPlayerArm = true;
    }

    public void SetArmDamage(int[] dmg)
    {
        armDamage = new int[dmg.Length];
        for (int i = 0; i < armDamage.Length; i++) armDamage[i] = dmg[i];
        arms[currentArmIndex].GetComponent<BulletControl>().SetDamage(armDamage[0], true, isPlayerArm);
    }

    public void SelectCurrentArm(int index)
    {
        arms[currentArmIndex].SetActive(false);
        if ((index >= 0) && (index < arms.Length))
        {
            currentArmIndex = index;
            if ((armDamage != null) && (index >= 0) && (index < armDamage.Length))
            {
                arms[currentArmIndex].GetComponent<BulletControl>().SetDamage(armDamage[index], true, isPlayerArm);
                ArmTrigger armTrigger = arms[currentArmIndex].GetComponent<ArmTrigger>();
                if (armTrigger != null) armTrigger.SetDamage(armDamage[index]);
            }
        }
        arms[currentArmIndex].SetActive(true);
    }

    public void NextArm()
    {
        int newIndex = -1;
        for (int i = 0; i < playerArm.Length; i++) 
        {
            if ((i > currentArmIndex) && playerArm[i])
            {
                newIndex = i;
                break;
            }
        }
        if ((newIndex == -1) && (currentArmIndex > 0) && playerArm[0]) newIndex = 0;
        if (newIndex != -1) SelectCurrentArm(newIndex);
    }

    public void SetAvailableArm(int index)
    {
        if ((index >= 0) && (index <= playerArm.Length)) playerArm[index] = true;
    }
}
