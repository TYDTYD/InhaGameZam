using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathMover : MonoBehaviour
{
    public enum MovementType { BackAndForth, Loop }
    public enum AccelerationType { ConstantSpeed, EaseOut }

    [Header("Path")]
    public List<Transform> PathPoints;
    public MovementType PathCycle = MovementType.BackAndForth;

    [Header("Movement")]
    public float Speed = 2f;
    public AccelerationType Acceleration = AccelerationType.ConstantSpeed;
    public float MinDistanceToTarget = 0.05f;

    private int _currentIndex = 0;
    private int _direction = 1;

    void Update()
    {
        if (PathPoints == null || PathPoints.Count < 2) return;

        Transform target = PathPoints[_currentIndex];
        Vector3 moveDir = (target.position - transform.position);

        if (moveDir.magnitude < MinDistanceToTarget)
        {
            switch (PathCycle)
            {
                case MovementType.Loop:
                    _currentIndex = (_currentIndex + 1) % PathPoints.Count;
                    break;
                case MovementType.BackAndForth:
                    if (_currentIndex == PathPoints.Count - 1) _direction = -1;
                    if (_currentIndex == 0) _direction = 1;
                    _currentIndex += _direction;
                    break;
            }
            return;
        }

        float step = Speed * Time.deltaTime;
        switch (Acceleration)
        {
            case AccelerationType.ConstantSpeed:
                transform.position = Vector3.MoveTowards(transform.position, target.position, step);
                break;
            case AccelerationType.EaseOut:
                transform.position = Vector3.Lerp(transform.position, target.position, step);
                break;
        }
    }
}
