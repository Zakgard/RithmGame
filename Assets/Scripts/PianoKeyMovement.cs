using UnityEngine;

public class PianoKeyMovement : MonoBehaviour
{
    private float _movementSpeed;

    private Rigidbody2D _rigidbody;

    private void Start()
    {
        _movementSpeed = Conductor.MovementSpeed;
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    { 
        if (SessionManager.IsStarted)
        {
            _rigidbody.velocity = Vector3.down * _movementSpeed;
        }
    }
}
