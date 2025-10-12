using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartCamera : MonoBehaviour
{
    [SerializeField] private Transform targetPoint;
    [SerializeField] private float speed = 4f;
    
    private Vector3 target;
    private bool isMovement = false;

    // Start is called before the first frame update
    void Start()
    {
        target = targetPoint.position;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isMovement)
        {
            Vector3 delta = target - transform.position;
            if (delta.magnitude > 0.5f)
            {
                delta = delta.normalized * speed * Time.deltaTime;
                transform.position += delta;
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }

    public void BeginMovement()
    {
        isMovement = true;
    }
}
