using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Store : MonoBehaviour
{
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
        if (other.CompareTag("Player"))
        {            
            PlayerControl playerControl = other.gameObject.GetComponent<PlayerControl>();
            //print($"Store other=<{other.name}> tag=<{other.tag}> PC=<{playerControl}>");
            if (playerControl != null)
            {
                playerControl.ViewStore();
            }
        }
    }
}
