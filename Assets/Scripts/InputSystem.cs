using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputSystem : MonoBehaviour
{
    [SerializeField] private float _timeToHoldLongKeys;
    [SerializeField] private LineRenderer _renderer;
    [SerializeField] private GameObject _linePrefab;
    [SerializeField] private Color _startColor;
    [SerializeField] private Color _endColor;

    private float _currentholdingTime;
    private bool _isHolding;
    private bool _isHoldingButton;
    private List<GameObject> _line;
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
        _line= new List<GameObject>();
        _camera = Camera.main;
        _currentholdingTime = 0.0f;
        _isHolding = false;
    }

    private void Update()
    {
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

        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if(touch.phase == TouchPhase.Began)
            {
                _point = _camera.ScreenToWorldPoint(touch.position);
                CheckHit();
            }
        }

        if (_isHolding)
        {          
            if(_isHoldingButton)
            {
                Vector2 vector = _camera.ScreenToWorldPoint(Input.mousePosition);
                WriteLine(new Vector2(_currnetTarget.transform.position.x, vector.y));
            }

            _currentholdingTime += Time.deltaTime;
            if (_currentholdingTime >= _timeToHoldLongKeys)
            {
                for(int i = 0; i < _line.Count; i++)
                {
                    Destroy(_line[i]);
                }
                _isHolding = false;
                SessionManager.Instance.DestroyPianoKey(true, _currnetTarget);
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
                WriteLine(new Vector2(_currnetTarget.transform.position.x, hit.point.y));
                _isHolding = true;
                _currentholdingTime = 0f;
            }
        }
    }

    public void WriteLine(Vector2 point)
    {
        // _line.Add(Instantiate(_linePrefab, point, Quaternion.identity));
       /* Image image = _currnetTarget.GetComponent<Image>();
        float elapsedTime = 0f;

        while (elapsedTime < _timeToHoldLongKeys)
        {
            float t = elapsedTime / _timeToHoldLongKeys;
            image.color = Color.Lerp(_startColor, _endColor, t);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        image.color = _endColor;
       */
    }

    public void DestroyLine()
    {
        for(int i = 0; i < _line.Count; i++)
        {
            Destroy(_line[i]);
        }
    }
}
