using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class implement the base class for all the props present on the map.
/// This being a platform, a bomb or a weapon.
/// </summary>
public class GameProps : MonoBehaviour
{
    protected bool isInitialized = false;

    public virtual void Init()
    {
        isInitialized = true;
    }
}
