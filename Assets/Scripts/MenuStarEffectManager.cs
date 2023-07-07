using UnityEngine;

public class MenuStarEffectManager : MonoBehaviour
{
    [SerializeField] private float _minSize;
    [SerializeField] private float _maxSize;
    [SerializeField] private GameObject _destructionEffect;

    private float _currnetSize;
    private Transform _starTR;

    private void Start()
    {
        _starTR = GetComponent<Transform>();
        _starTR.localScale = new Vector2(_minSize, _minSize);
        _currnetSize = _minSize;        
    }

    private void FixedUpdate()
    {
        if(_starTR != null && _currnetSize < _maxSize)
        {
            _starTR.localScale = new Vector2(_currnetSize, _currnetSize);
            _currnetSize *= 1.005f ;
        }

        if(_currnetSize >= _maxSize)
        {
            Instantiate(_destructionEffect, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        MenuStarSpawner.Instance.ClearListPosition(this.gameObject);
    }
}
