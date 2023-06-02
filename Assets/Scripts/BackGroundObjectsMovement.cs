using UnityEngine;
using System.Collections.Generic;

public class BackGroundObjectsMovement : MonoBehaviour
{
    [SerializeField] private float _topBound;
    [SerializeField] private float _bottomBound;
    [SerializeField] private float _leftBound;
    [SerializeField] private float _rightBound;
    [SerializeField] private float _speed;
    [SerializeField] private List<Vector2> _destinationPoints= new List<Vector2>();

    private Vector2 _direction;
    private Vector3 _rotationAxis = Vector3.forward;

    private void Start()
    {
        GetDestinationPoint();
    }

    private void FixedUpdate()
    {
        if (!transform.position.Equals(_direction))
        {
            transform.position = Vector3.MoveTowards(transform.position, _direction, _speed);
            transform.Rotate(_rotationAxis);
        }
        else
        {
            GetDestinationPoint();
        }        
    }

    private void GetDestinationPoint()
    {
        _direction = _destinationPoints[Random.Range(0, _destinationPoints.Count)];
    }
}
