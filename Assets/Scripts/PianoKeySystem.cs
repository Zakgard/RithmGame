using UnityEngine;

public class PianoKeySystem : MonoBehaviour
{
    [SerializeField] private int _pointsPerRightClick;
    [SerializeField] private bool _isTest;

    public bool isBig;
    private bool _isOnPointsState;

    private void Start()
    {
        _isOnPointsState = false;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        _isOnPointsState = true;                 
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        if (isBig)
        {
            InputSystem.instance.DestroyLine();
           // StopCoroutine(InputSystem.Instance.WriteLine(Vector2.zero));
        }
        if (!SessionManager.IsClicked && !_isTest)
        {
            SessionManager.IsLost= true;
            SessionManager.Instance.OnGameLost?.Invoke();
        }
        else
        {
            if(_isOnPointsState)
            {
                SessionManager.Points += _pointsPerRightClick;
                SessionManager.Instance.OnGetPoints?.Invoke();
            }
        }
        SessionManager.ObjectsOnscene[SessionManager.ObjectsOnscene.IndexOf(gameObject)] = null;
        SessionManager.IsClicked = false;
    }
}
