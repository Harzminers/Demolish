using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Cinemachine.CinemachineFreeLook))]
public class FreeLookInkput : MonoBehaviour
{
    Cinemachine.CinemachineFreeLook _freeLookCamera;
    [SerializeField] string _xAxisName;
    [SerializeField] string _yAxisName;
    void Start()
    {
        _freeLookCamera = GetComponent<Cinemachine.CinemachineFreeLook>();
        _freeLookCamera.m_XAxis.m_InputAxisName = "";
        _freeLookCamera.m_YAxis.m_InputAxisName = "";
    }

    void Update()
    {
        if (Input.GetMouseButton(0)) 
        {
            _freeLookCamera.m_XAxis.m_InputAxisValue = Input.GetAxis(_xAxisName);
            _freeLookCamera.m_YAxis.m_InputAxisValue = Input.GetAxis(_yAxisName);
        }
        else 
        {
            _freeLookCamera.m_XAxis.m_InputAxisValue = 0;
            _freeLookCamera.m_YAxis.m_InputAxisValue = 0;
        }
    }
}
