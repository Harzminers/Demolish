using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallLauncher : MonoBehaviour
{
    [SerializeField] GameObject _ballPrefab;
    [SerializeField] GameObject _trajectoryEndPoint;
    [SerializeField] Transform _ballStartingPos;
    [SerializeField] Transform _cannonModel;
    [SerializeField] Transform _target;
    [SerializeField] LineRenderer _trajectoryLineRenderer;
    [SerializeField] float _gravity = -9.81f;
    [SerializeField] float _maxHeightMultiplier = 1.2f;
    [SerializeField] Vector3 _startVelocity;
    [SerializeField] int _resolution = 30;
    [SerializeField] LayerMask _projectileLayermask;
    public Vector3 endPos => _target.position;
    public Vector3 startPos => _ballStartingPos.position;
    public float distance;

    Vector3 _currentStartVelocity;
    Vector3 _oldTargetPos = new Vector3();

    private void Start()
    {
        Physics.gravity = _gravity*Vector3.up;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            LaunchBall();
        }
        float distanceY = _target.position.y - _ballStartingPos.position.y;

        if (_target != null)
        {
            if (!_trajectoryLineRenderer.enabled) 
            {
                _trajectoryLineRenderer.enabled = true;
            }
            _currentStartVelocity = AdjustCannon(_target.position, _maxHeightMultiplier * (Mathf.Abs(distanceY) + 5) * -Mathf.Sign(_gravity));
        }
        else 
        {
            _trajectoryLineRenderer.enabled = false;
            _trajectoryEndPoint.SetActive(false);
        }
    }
    void LaunchBall() 
    {
        GameObject ball = Instantiate(_ballPrefab, _ballStartingPos.position, Quaternion.identity);
        Rigidbody ballRB = ball.GetComponent<Rigidbody>();

        ballRB.velocity = _currentStartVelocity;
    }
   
    public Vector3 AdjustCannon(Vector3 target,float h) 
    {
        float displacementY = target.y - _ballStartingPos.position.y;
        Vector3 displacementXZ = new Vector3(target.x - _ballStartingPos.position.x, 0, target.z - _ballStartingPos.position.z);
        float time = Mathf.Sqrt((-2 * h) / _gravity) + Mathf.Sqrt((2 * (displacementY - h)) / _gravity);
        Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2 * _gravity * h);
        Vector3 velocityXZ = displacementXZ / time;


        Vector3 resultingVelocity = velocityXZ + velocityY * -Mathf.Sign(_gravity);

        if (_oldTargetPos != null)
        {
            if (_oldTargetPos != target)
            {
                Quaternion rot = Quaternion.LookRotation(resultingVelocity, Vector3.up);
                
                transform.rotation = rot;
            }
        }
        _oldTargetPos = _target.position;

        if (float.IsNaN(resultingVelocity.x)) 
        {
            System.Diagnostics.Debugger.Break();
        }

        DrawTrajectory(resultingVelocity, _resolution,time);

        return resultingVelocity;
    }

    void DrawTrajectory(Vector3 velocity,int resolution,float time)
    {
        int res = (int)(resolution * time);
        _trajectoryLineRenderer.positionCount = (res+1);

        _trajectoryLineRenderer.SetPosition(0, _ballStartingPos.position);
        Vector3 oldPos = _ballStartingPos.position;
        float simulatedTimePassed = 0;
        RaycastHit hit;
        
        for (int i = 1; i <= res; i++) 
        {
            simulatedTimePassed = (float)((float)i / (float)(res) * (float)time);

            Vector3 newPos = transform.position + velocity * simulatedTimePassed + (Vector3.up * .5f * _gravity * (simulatedTimePassed * simulatedTimePassed));
            if (Physics.Linecast(oldPos, newPos,out hit, _projectileLayermask)) 
            {
                _trajectoryLineRenderer.SetPosition(i, hit.point);
                oldPos = newPos;
                _trajectoryLineRenderer.positionCount = (i + 1);
                _trajectoryEndPoint.SetActive(true);
                _trajectoryEndPoint.transform.position = hit.point;
                return;
            }

            _trajectoryLineRenderer.SetPosition(i,newPos);

            oldPos = newPos;
        }

        _trajectoryEndPoint.SetActive(true);
        _trajectoryEndPoint.transform.position = oldPos;
    }
}
