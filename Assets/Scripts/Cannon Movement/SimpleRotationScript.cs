using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleRotationScript : MonoBehaviour
{
    public float _rotationSpeed;
    [SerializeField] Transform _objectToRotateVertically;
    [SerializeField] Transform _objectToRotateHorizontally;
    SelectableObject _sb;
    bool _isSelected => _sb.IsSelected;
    void Start()
    {
        _sb = GetComponentInChildren<SelectableObject>();
    }
    void Update()
    {
        if (!_isSelected) return;
        Vector3 input = new Vector3(0,Input.GetAxis("Horizontal"), -Input.GetAxis("Vertical"));
        float multiplier = _rotationSpeed * Time.deltaTime;
        _objectToRotateHorizontally.eulerAngles += new Vector3(0,0,input.z* multiplier);
        _objectToRotateVertically.eulerAngles   += new Vector3(0,input.y* multiplier,0);
    }
}
