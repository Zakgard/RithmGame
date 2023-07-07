using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;

public class PianoKeySystem : MonoBehaviour
{
    [SerializeField] private int _pointsPerRightClick;
    
    [SerializeField] private bool _isImmmortal;

    public bool isTest;
    public bool isBig;
    public float timeToHold;
    private bool _isOnPointsState;
    private float _speed;
    private bool _isPaused;

    private void Start()
    {
        _isPaused = false;
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
                AudioManager.Instance.OnPauseMusic();
                SessionManager.Instance.OnGameLost?.Invoke();
            }
            else
            {
                if (_isOnPointsState)
                {
                    SessionManager.Points += _pointsPerRightClick;
                    SessionManager.Instance.OnGetPoints?.Invoke();
                    if (gameObject.CompareTag("short"))
                    {
                        Conductor._shorts.Remove(gameObject);
                    }
                    else
                    {
                        Conductor._longs.Remove(gameObject);
                    }
                    
                }
            }
            SessionManager.IsClicked = false;           
        }        
    }
}
