using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class TimelineCannon : MonoBehaviour
{
    [SerializeField] GameObject _cannonProjectile;
    [SerializeField] GameObject _trajectoryEndPoint;
    [SerializeField] Transform _projectileSpawn;
    [SerializeField] Transform _cannonPodest;
    [SerializeField] LineRenderer _trajectoryLineRenderer;
    [SerializeField] float _cannonStrength = 10;
    [SerializeField] float _trajectoryTimeToDraw = 5f;
    [SerializeField] int _resolution = 30;
    [SerializeField] Vector3 _startVelocity;
    [SerializeField] LayerMask _projectileLayermask;

    [SerializeField] Material _selectedMaterial;
    [SerializeField] Material _notSelectedMaterial;
    [SerializeField] Transform _cameraFokus;

    public Transform CameraFokus => _cameraFokus;
    public static TimelineCannon SelectedCannon;

    SelectableObject _sb;

    float _gravity;
    bool _isSelected => _sb.IsSelected;
    float rotationX => transform.eulerAngles.x;
    float rotationY => transform.eulerAngles.y;

    bool _isHighlighted = false;

    Vector3 _currentStartVelocity;

    void Awake()
    {
        _gravity = -Physics.gravity.magnitude;
        SelectableObject.OnObjectSelectionComplete += OnSelectionChange;
        _currentStartVelocity = CalculateStartVelocity();
        DrawTrajectory(_currentStartVelocity, _resolution, _trajectoryTimeToDraw);
        _sb = GetComponentInChildren<SelectableObject>();
    }

    void Update()
    {
        if (_isSelected)
        {
            _currentStartVelocity = CalculateStartVelocity();
            DrawTrajectory(_currentStartVelocity, _resolution, _trajectoryTimeToDraw);
        }
    }

    public void Launch(GameObject projectile) 
    {
        GameObject ball = Instantiate(projectile, _projectileSpawn.position, Quaternion.identity);
        Rigidbody ballRB = ball.GetComponent<Rigidbody>();

        ballRB.velocity = _currentStartVelocity;
    }

    Vector3 CalculateStartVelocity()
    {
        Vector3 resultingVelocity = _projectileSpawn.forward * _cannonStrength;

        return resultingVelocity;
    }
    void OnSelectionChange()
    {
        if(_isSelected)
        {
            _cannonPodest.GetComponent<MeshRenderer>().material = _selectedMaterial;
            BomberGameManager.Instance.SnapCamerasToCannon(this);
            _isHighlighted = true;
        }
        else
        {
            _cannonPodest.GetComponent<MeshRenderer>().material = _notSelectedMaterial;
            _isHighlighted = false;
        }
    }
    void DrawTrajectory(Vector3 velocity, int resolution, float time)
    {
        int res = (int)(resolution * time);
        _trajectoryLineRenderer.positionCount = (res + 1);

        _trajectoryLineRenderer.SetPosition(0, _projectileSpawn.position);
        Vector3 oldPos = _projectileSpawn.position;
        float simulatedTimePassed = 0;
        RaycastHit hit;

        for (int i = 1; i <= res; i++)
        {
            simulatedTimePassed = (float)((float)i / (float)(res) * (float)time);

            Vector3 newPos = _projectileSpawn.position + velocity * simulatedTimePassed + (Vector3.up * .5f * _gravity * (simulatedTimePassed * simulatedTimePassed));
            if (Physics.Linecast(oldPos, newPos, out hit, _projectileLayermask))
            {
                _trajectoryLineRenderer.SetPosition(i, hit.point);
                oldPos = newPos;
                _trajectoryLineRenderer.positionCount = (i + 1);
                _trajectoryEndPoint.SetActive(true);
                _trajectoryEndPoint.transform.position = hit.point;
                return;
            }

            _trajectoryLineRenderer.SetPosition(i, newPos);

            oldPos = newPos;
        }

        _trajectoryEndPoint.SetActive(true);
        _trajectoryEndPoint.transform.position = oldPos;
    }
}
