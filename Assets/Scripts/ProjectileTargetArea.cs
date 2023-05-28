using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileTargetArea : MonoBehaviour,IProjectileTarget
{
    public void OnProjectileHit() 
    {
        Debug.Log("Target was hit");
    }
}
