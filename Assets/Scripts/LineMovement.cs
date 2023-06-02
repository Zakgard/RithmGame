using UnityEngine;

public class LineMovement : MonoBehaviour
{
    [SerializeField] private float _movementSpeed;


    private void FixedUpdate()
    {
        if(SessionManager.IsStarted)
            transform.Translate(Vector2.down * _movementSpeed);
    }
}
