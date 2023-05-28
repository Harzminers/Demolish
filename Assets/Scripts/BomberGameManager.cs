using System;
using UnityEngine;

public class BomberGameManager : MonoBehaviour
{
    [SerializeField] Cinemachine.CinemachineVirtualCamera _cannonCloseUpCamera;
    [SerializeField] Transform _cannonOrbitCamera;

    public event Action OnAttackCommence;
    public static BomberGameManager Instance;
    void Awake()
    {
        if (Instance != null) 
        {
            Destroy(Instance);
        }
        Instance = this;
    }
    public void CommenceAttack() 
    {
        OnAttackCommence?.Invoke();
    }

    public void SnapCamerasToCannon(TimelineCannon cannon) 
    {
        _cannonCloseUpCamera.transform.SetParent(cannon.CameraFokus.parent.transform);
        _cannonCloseUpCamera.LookAt = cannon.CameraFokus;
        _cannonCloseUpCamera.Follow = cannon.CameraFokus;
        _cannonCloseUpCamera.transform.position = cannon.transform.position;
        _cannonCloseUpCamera.transform.eulerAngles = new Vector3(0,cannon.CameraFokus.parent.eulerAngles.y + 180,0);

        _cannonOrbitCamera.position = cannon.transform.position;
    }
}
