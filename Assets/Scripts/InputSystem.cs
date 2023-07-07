using UnityEngine;

public class InputSystem : MonoBehaviour
{
    [SerializeField] private float _timeToHoldLongKeys;

    private LongKeyEffector _effector;
    private float _currentholdingTime;
    private bool _isHolding;
    private bool _isHoldingButton;
    private GameObject _currnetTarget;

    private Vector2 _point;
    private Camera _camera;

    public static InputSystem instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        _camera = Camera.main;
        _currentholdingTime = 0.0f;
        _isHolding = false;
    }

    private void Update()
    {
        if(_currnetTarget!= null)
        {
            if(_currnetTarget.CompareTag("long"))
                _timeToHoldLongKeys = _currnetTarget.GetComponent<PianoKeySystem>().timeToHold;
        }

        if (Input.GetMouseButtonDown(0))
        {
            _isHoldingButton = true;
            _point = _camera.ScreenToWorldPoint(Input.mousePosition);
            CheckHit();
        }

        if(Input.GetMouseButtonUp(0))
        {
            _isHoldingButton = false;
        }

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                _point = _camera.ScreenToWorldPoint(touch.position);
                CheckHit();
            }
        }
    }

    private void FixedUpdate()
    {     
        if (_isHolding)
        {
            if (_isHoldingButton)
            {              
                _effector.ChangeValue(true, 0);
            }

            _currentholdingTime += Time.fixedDeltaTime;
            if (_currentholdingTime >= _timeToHoldLongKeys -.22f)
            {                
                _isHolding = false;
                if (_effector != null)
                {
                    //_effector.UnHold();
                }
                SessionManager.Instance.DestroyPianoKey(true, _currnetTarget);
                _currentholdingTime= 0;
            }          
        }
    }

    private void CheckHit()
    {
        RaycastHit2D hit = Physics2D.Raycast(_point, Vector2.zero);    

        if (hit.collider != null && SessionManager.IsStarted)
        {
            GameObject hitObject = hit.collider.gameObject;
            _currnetTarget = hitObject;
            
            if (hitObject.CompareTag("short"))
            {
                SessionManager.Instance.DestroyPianoKey(true, _currnetTarget);
                EffectsSystem.Instance.SpawnDesctructionEffects(hit.point);
            }
            else if (hitObject.CompareTag("long"))
            {
                _isHolding = true;
                _effector = _currnetTarget.GetComponent<LongKeyEffector>();
                if(_effector != null)
                {
                    //_effector.Hold(hit.point);
                }               
                _currentholdingTime = 0f;
                _effector.ChangeValue(true, .1f);
            }
        }
    }
}
