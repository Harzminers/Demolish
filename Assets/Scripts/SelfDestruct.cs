using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    [SerializeField] float _time;
    void Start()
    {
        StartCoroutine(WaitThenDestruct());    
    }

    IEnumerator WaitThenDestruct() 
    {
        yield return new WaitForSeconds(_time);
        Destroy(gameObject);
    }
}
