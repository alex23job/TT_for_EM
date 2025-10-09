using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHP : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = new Vector3(1f, 1f, 1f);
    }

    public void ViewHP(float prc)
    {
        transform.localScale = new Vector3(1.01f, prc, 1.01f);
        transform.localPosition = new Vector3(0, (prc - 1.0f) / 2, 0);
    }
}
