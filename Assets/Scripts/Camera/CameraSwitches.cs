using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitches : MonoBehaviour
{
    public static void SelectCamera(GameObject camera) 
    {
        Cinemachine.CinemachineBrain brain = Camera.main.GetComponent<Cinemachine.CinemachineBrain>();
        GameObject currentCam = Camera.main.GetComponent<Cinemachine.CinemachineBrain>().ActiveVirtualCamera?.VirtualCameraGameObject;
        if (camera != currentCam)
        {
            currentCam?.SetActive(false);
            camera.SetActive(true);
        }
    }
}
