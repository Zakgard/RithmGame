using UnityEngine;

public class GizmoDrawingManager : MonoBehaviour
{
    [SerializeField] private Vector2 _startPos;
    [SerializeField] private float _lineWigth;
     private float _movementSpeed;
     private Vector2 _moveVector;
    private float length;
    [SerializeField] private float[] _pointsX;
    private bool _isPlaying = false;

    private void Start()
    {
        _movementSpeed = BitAnalyzer.TotalLength / BitAnalyzer.TotalTime;
        length = 0;
        //AudioManager.Instance.PlayMusic();
        _isPlaying= true;
    }

    private void OnDrawGizmos()
    {
        if (_isPlaying)
        {
            Gizmos.color = Color.white;

            Gizmos.DrawLine(_startPos, _moveVector);
        }
       
    }

    private void Update()
    {
        length += _movementSpeed * Time.deltaTime;
        _moveVector.y = _startPos.y + length;
    }
}
