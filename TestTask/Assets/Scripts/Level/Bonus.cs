using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonus : MonoBehaviour
{
    [SerializeField] private int bonusType = 0;
    [SerializeField] protected int value = 0;

    private Animator anim;

    public int BonusType { get => bonusType; }
    public int Value { get => value; }

    private void Awake()
    {
        anim = transform.GetComponentInChildren<Animator>();
    }

    public void MinAndDestroy()
    {
        anim.SetBool("IsMin", true);
        Destroy(gameObject, 0.6f);
    }
}
