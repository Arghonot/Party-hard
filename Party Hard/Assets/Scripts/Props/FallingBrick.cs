using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingBrick : MonoBehaviour
{
    Rigidbody body;

    private void Start()
    {
        body = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            print("player died");
        }
        else
        {
            Destroy(body);
        }
    }
}
