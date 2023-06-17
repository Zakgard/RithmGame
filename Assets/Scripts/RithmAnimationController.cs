using UnityEngine;

public class RithmAnimationController : MonoBehaviour
{
    [SerializeField] private float _sizeChangeSpeed;
    [SerializeField] private float _maxHeight;
    [SerializeField] private float _minHeight;
    [SerializeField] private bool _startsWithGrowing;

    private bool _isGrowing;
    private float _timeCounter;
    private float _xScales;
    private float _zScale;

    private void Start()
    {
        _xScales= transform.localScale.x;
        _zScale = transform.localScale.z;
        _isGrowing = _startsWithGrowing;
    }

    private void Update()
    {
        if (_isGrowing)
        {
            if(transform.localScale.y >= _maxHeight)
            {
                _isGrowing = false;
            }
            else
            {
                transform.localScale = new Vector3(_xScales, transform.localScale.y + _sizeChangeSpeed * Time.deltaTime, _zScale);
            }
        }
        else
        {
            if(transform.localScale.y <= _minHeight)
            {
                _isGrowing = true;
            }
            else
            {
                transform.localScale = new Vector3(_xScales, transform.localScale.y - _sizeChangeSpeed * Time.deltaTime, _zScale);
            }
        }
    }
}
