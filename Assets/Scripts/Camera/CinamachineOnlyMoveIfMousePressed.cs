using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinamachineOnlyMoveIfMousePressed : MonoBehaviour, AxisState.IInputAxisProvider
{
    public float GetAxisValue(int axis)
    {
        if (Input.GetMouseButton(0))
        {
            string axisName = axis == 0 ? "Horizontal" : "Vertical";
            float value = Input.GetAxisRaw(axisName);

            return value;
        }

        else return 0;
    }
}
