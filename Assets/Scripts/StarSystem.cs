using Unity.Mathematics;
using UnityEngine;

public class StarSystem : MonoBehaviour
{
    [SerializeField] private ParticleSystem _starSpawnEffect;
    [SerializeField] private ParticleSystem _starStayEffect;
    [SerializeField] private RectTransform _transform;
    [SerializeField] private int _currentIndex;
    [SerializeField] private Transform _sliderTR;
    

    public void SpawnStar()
    {
        ParticleSystem particleSystem = Instantiate(_starSpawnEffect, new Vector3(transform.position.x, transform.position.y, -2), quaternion.identity);
        particleSystem.transform.SetParent(_transform, false);
        _starSpawnEffect.Play();
        SetActiveStar();
        
    }

    private void SetActiveStar()
    {
        if (_currentIndex == EffectsSystem.EffectIndex)
        {
            gameObject.SetActive(true);
            _transform.SetAsLastSibling();
        }
    }
}
