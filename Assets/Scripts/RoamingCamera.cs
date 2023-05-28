using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class RoamingCamera : MonoBehaviour
{
    [SerializeField] float _speed;
    [SerializeField] float _rotationSpeed;
    [SerializeField] Transform _follow;
    [SerializeField] CinemachineVirtualCamera _cam;
    [SerializeField] Transform _referenceRot;
    [SerializeField] Rigidbody _rb;

    CinemachinePOV _pov;
    Vector2 _move;
    float _verticalMovementInput;

    void Awake()
    {
        Cursor.lockState = CursorLockMode.Confined;
        if (_cam.GetCinemachineComponent<CinemachinePOV>() == null) { Debug.LogError($"No CinemachinePOV Component could be found on {this.GetType().ToString()} `{this.name}`"); }
        else _pov = _cam.GetCinemachineComponent<CinemachinePOV>();
    }
    void OnEnable()
    {
        Cinemachine.CinemachineBrain brain = Camera.main.GetComponent<Cinemachine.CinemachineBrain>();
        if(brain.ActiveVirtualCamera != null){
            GameObject currentCam = brain.ActiveVirtualCamera.VirtualCameraGameObject;
            if (currentCam != null)
            {
                _follow.position = currentCam.transform.position;
                _follow.eulerAngles = new Vector3(0, currentCam.transform.eulerAngles.y, 0);
            } 
        }
    }
    void Update()
    {
        HandleInput();
    }

    void FixedUpdate()
    {
        float multiplier = _speed;

        _referenceRot.eulerAngles = Vector3.zero;
        _referenceRot.rotation *= Quaternion.AngleAxis(_pov.m_HorizontalAxis.Value, Vector3.up);
        _referenceRot.rotation *= Quaternion.AngleAxis(_pov.m_VerticalAxis.Value, Vector3.right);

        _rb.velocity = (_referenceRot.right * _move.x + _referenceRot.forward * _move.y + Vector3.up * _verticalMovementInput) * multiplier;
    }
    void HandleInput()
    {
        _move = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            _verticalMovementInput -= 1;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _verticalMovementInput += 1;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            _verticalMovementInput += 1;
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            _verticalMovementInput -= 1;
        }
    }
}
