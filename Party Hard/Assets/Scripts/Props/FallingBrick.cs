using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingBrick : MonoBehaviour
{
    Rigidbody body;
    bool TouchedTheGround = false;
    bool mightTouchGround = false;
    float SecondsToWait;

    private void Start()
    {
        body = GetComponent<Rigidbody>();
        StartCoroutine(WaitToAllowBodyDeletion());
    }

    IEnumerator WaitToAllowBodyDeletion()
    {
        yield return new WaitForSeconds(SecondsToWait);

        mightTouchGround = true;

        yield return null;
    }

    private void Update()
    {
        if (TouchedTheGround && mightTouchGround)
        {
            Destroy(body);
            Destroy(this);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            print("player died");
        }
        else if (collision.gameObject.CompareTag("Floor"))
        {
            TouchedTheGround = true;
            //Destroy(body);
        }
    }
}
