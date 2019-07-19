using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public MultipleTargetsCamera FullScreenCamera;

    #region Singleton

    static CameraManager instance = null;
    public static CameraManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<CameraManager>();
            }

            return instance;
        }
    }

    #endregion

    public void RegisterPlayer(Transform player)
    {
        // We don't want to have the same player multiple times as a target
        if (!FullScreenCamera.Targets.Contains(player))
        {
            // We use it as a target
            FullScreenCamera.Targets.Add(player);
        }
    }

    public void UnRegisterPlayer(Transform player)
    {
        if (FullScreenCamera.Targets.Contains(player))
        {
            FullScreenCamera.Targets.Remove(player);
        }
    }

    public void SetRotation(Vector3 newRot)
    {
        FullScreenCamera.transform.eulerAngles = newRot;
    }

    public void SetOffset(Vector3 newOffset)
    {
        FullScreenCamera.offset = newOffset;
    }

    public void RevertToOriginalRotation()
    {
        FullScreenCamera.RevertToOriginRotation();
    }

    public void RevertToOriginOffset()
    {
        FullScreenCamera.RevertToOriginOffset();
    }
}
