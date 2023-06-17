using UnityEngine;

public class PianoKeyMovement : MonoBehaviour
{
    public float _movementSpeed;
    [SerializeField] private bool _isTest;
    [SerializeField] private float _moveSpeed;

    private Rigidbody2D _rigidbody;

    private void Start()
    {
        _movementSpeed = _moveSpeed;
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    { 
        if (SessionManager.IsStarted || _isTest)
        {
            _rigidbody.velocity = Vector3.down * _moveSpeed;
        }
    }
}
