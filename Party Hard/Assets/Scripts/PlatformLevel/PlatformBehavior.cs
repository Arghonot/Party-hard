using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformBehavior : MonoBehaviour
{
    public float fadeTime;

    float CurrentFadeTime;
    bool asBeenCollided = false;
    LayerMask mask;
    Material mat;

    private void Start()
    {
        mat = GetComponent<MeshRenderer>().material;
        fadeTime = ((FadingPlatformLevel)GameManager.Instance.GetCurrentRoundManager()).PlatformFadeTime;
        mask = ((FadingPlatformLevel)GameManager.Instance.GetCurrentRoundManager()).DetectionMask;
    }

    private void Update()
    {
        var colliders = Physics.OverlapBox(transform.position, Vector3.one * transform.localScale.x, Quaternion.identity, mask);

        if (colliders.Length > 0)
        {
            CurrentFadeTime += Time.deltaTime;
        }

        if (CurrentFadeTime > fadeTime)
        {
            gameObject.SetActive(false);
        }

        mat.color = Color.Lerp(Color.white, Color.red, CurrentFadeTime / fadeTime);
    }
}
