using UnityEngine;

public class PianoKeySystem : MonoBehaviour
{
    [SerializeField] private int _pointsPerRightClick;
    public bool isTest;
    [SerializeField] private bool _isImmmortal;

    public bool isBig;
    public float timeToHold;
    private bool _isOnPointsState;
    private float _speed;

    private void Start()
    {
        float size = GetComponent<BoxCollider2D>().bounds.size.y;
        _speed = GetComponent<PianoKeyMovement>()._movementSpeed;
        timeToHold = size / _speed;
        _isOnPointsState = false;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        _isOnPointsState = true;                 
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if(!_isImmmortal) Destroy(gameObject);
    }

    private void OnDestroy()
    {
        if (SessionManager.IsStarted)
        {
            if (!SessionManager.IsClicked && !isTest)
            {
                SessionManager.IsLost = true;
                SessionManager.Instance.OnGameLost?.Invoke();
            }
            else
            {
                if (_isOnPointsState)
                {
                    SessionManager.Points += _pointsPerRightClick;
                    SessionManager.Instance.OnGetPoints?.Invoke();
                }
            }
            SessionManager.IsClicked = false;           
        }        
    }
}
