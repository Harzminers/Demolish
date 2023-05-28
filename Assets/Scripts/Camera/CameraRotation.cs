using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    [SerializeField] Transform _rotationPoint;

    Vector3 _oldMousePos;
    Vector3 _oldPos;
    Vector3 _newMousePos;

    float _currentSphereRadius;

    void Start()
    {
        UpdateSphereRadiusAndPosition();
    }
    void UpdateSphereRadiusAndPosition() 
    {
        _currentSphereRadius = Vector3.Distance(transform.position, _rotationPoint.position);
        _oldPos = transform.position;
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _oldMousePos = new Vector2(Input.mousePosition.x,Input.mousePosition.y);
            _oldPos = transform.position;
        }
        if (Input.GetMouseButton(0)) 
        {
            _newMousePos = new Vector2(Input.mousePosition.x, Input.mousePosition.y); 
            Vector2 difference = _newMousePos - _oldMousePos;
            float xRotation = (difference.x / Screen.width) * 360;
            float yRotation = (-difference.y / Screen.height) * 360;
            RotateAround(this.transform,_rotationPoint.position,_rotationPoint.up,xRotation);
            RotateAround(this.transform,_rotationPoint.position,_rotationPoint.right,yRotation);
            _oldMousePos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

            //            transform.rotation = Quaternion.LookRotation(_rotationPoint.position,transform.up);
        }
    }

    void RotateAround(Transform transform, Vector3 pivotPoint, Vector3 axis, float angle)
    {
        Quaternion rot = Quaternion.AngleAxis(angle, axis);
        transform.position = rot * (transform.position - pivotPoint) + pivotPoint;
        transform.rotation = rot * transform.rotation;

        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 0);
    }
}
