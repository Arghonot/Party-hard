using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Contains("Player"))
        {
            GameManager.Instance.GetCurrentRoundManager().OnEnterDeathZone(other.transform);
        }
        else
        {
            Destroy(other.gameObject);
        }
    }
}
