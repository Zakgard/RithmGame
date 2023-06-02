using UnityEngine;

public class CongratsMovement : MonoBehaviour
{
    [SerializeField] private float _speed;

    private void FixedUpdate()
    {
        transform.Translate(Vector2.down * _speed);
    }
}
