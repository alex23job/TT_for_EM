using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHP : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    public void ViewHP(float prc)
    {
        //print($"prc={prc}");
        transform.localPosition = new Vector3(0, (prc - 1.0f) / 2, 0);
        transform.localScale = new Vector3(1.01f, prc, 1.01f);
    }
}
