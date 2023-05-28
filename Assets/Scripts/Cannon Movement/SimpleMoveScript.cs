using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMoveScript : MonoBehaviour
{
    [SerializeField] KeyCode _up = KeyCode.Space;
    [SerializeField] KeyCode _down = KeyCode.LeftShift;
    [SerializeField] string _forward = "w";
    [SerializeField] string _backwards = "s";
    [SerializeField] string _right = "d";
    [SerializeField] string _left = "a";
    [SerializeField] float _speed = 5;

    SelectableObject _sb;
    bool _isSelected => _sb.IsSelected;

    void Start()
    {
        _sb = GetComponentInChildren<SelectableObject>();
    }
    void Update()
    {
        if (!_isSelected) return;

        if (Input.GetKey(_up)) 
        {
            transform.Translate(new Vector3(0, 1 * _speed,0) * Time.deltaTime);
        }
        if (Input.GetKey(_down))
        {
            transform.Translate(new Vector3(0, -1 * _speed, 0) * Time.deltaTime);
        }
        if (Input.GetKey(_right))
        {
            transform.Translate(new Vector3(1 * _speed,0,0) * Time.deltaTime);
        }
        if (Input.GetKey(_left))
        {
            transform.Translate(new Vector3(-1 * _speed, 0, 0) * Time.deltaTime);
        }
        if (Input.GetKey(_forward))
        {
            transform.Translate(new Vector3(0, 0, 1 * _speed) * Time.deltaTime);
        }
        if (Input.GetKey(_backwards))
        {
            transform.Translate(new Vector3(0, 0, -1 * _speed) * Time.deltaTime);
        }
    }
}
