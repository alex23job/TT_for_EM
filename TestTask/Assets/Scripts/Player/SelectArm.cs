using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectArm : MonoBehaviour
{
    [SerializeField] private GameObject[] arms;

    private int[] armDamage = null;
    private int currentArmIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetArmDamage(int[] dmg)
    {
        armDamage = new int[dmg.Length];
        for (int i = 0; i < armDamage.Length; i++) armDamage[i] = dmg[i];
        arms[currentArmIndex].GetComponent<BulletControl>().SetDamage(armDamage[0], true);
    }

    public void SelectCurrentArm(int index)
    {
        arms[currentArmIndex].SetActive(false);
        if ((index >= 0) && (index < arms.Length))
        {
            currentArmIndex = index;
            if ((armDamage != null) && (index >= 0) && (index < armDamage.Length))
            {
                arms[currentArmIndex].GetComponent<BulletControl>().SetDamage(armDamage[index], true);
            }
        }
        arms[currentArmIndex].SetActive(true);
    }
}
