using UnityEngine;

public class LongKeyEffector : MonoBehaviour
{
    [SerializeField] private GameObject _line;
    [SerializeField] private GameObject _circle;
    [SerializeField] private GameObject _point;
    private Vector2 _finalPoint;
    private float _speed;
    private float _size;
    [SerializeField] private float _progressSteps;
    [SerializeField] private float stepSize;
    [SerializeField] private float _rotationSpeed;

    private float _xPos;
    float _FillRateValue;

    private float _progressBorder;
    private Transform _lineTransform;
    private Vector3 _originalLineScale;
    
    Material material;

    private void Start()
    {
        _size = transform.localScale.y;
        _lineTransform = _line.transform;
        _originalLineScale = _lineTransform.localScale;
        _circle.transform.localScale = new Vector2(_circle.transform.localScale.x, _circle.transform.localScale.y * 7 / _size);
        _speed = GetComponent<PianoKeyMovement>()._movementSpeed;
        _progressSteps = (_progressSteps/ _speed * 10) / (14.0f / _size);
        material = GetComponent<Renderer>().material;
        _progressBorder = .6f;
        material.SetFloat("_ProgressBorder", _progressBorder);

        _FillRateValue = -_progressBorder;
        material.SetFloat("_FillRate", _FillRateValue);
        stepSize = (2*_progressBorder) / _progressSteps;

        _finalPoint.y = GetComponent<RectTransform>().rect.yMax;
        _xPos = transform.position.x;
        _FillRateValue = -.6f;
    }

    private void Update()
    {
        if (_circle.active)
        {
            float scaleMultipliar = 1.0f - ((_FillRateValue+.6f)/ 1.2f);
            _lineTransform.localScale = new Vector3(_originalLineScale.x, _originalLineScale.y * scaleMultipliar, _originalLineScale.z);
            _circle.transform.position = new Vector3(_xPos, _circle.transform.position.y, -5.0f);
        }
    }

    public void Hold(Vector2 position)
    {
        if (!_circle.active)
        {
            _circle.SetActive(true);
        }
    }

    public void UnHold()
    {
        if (_circle.active)
        {
            _circle.SetActive(false);
        }
    }

    public void ChangeValue(bool isIncrease, float extraIncrease)
    {
        if(isIncrease)
        {
            _FillRateValue += stepSize + extraIncrease;
            if (_FillRateValue >= .6f)        
                _FillRateValue= .6f;
        }
        else
        {
            _FillRateValue -= stepSize;
        }
        material.SetFloat("_FillRate", _FillRateValue);
    }
}
