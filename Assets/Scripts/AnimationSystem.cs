using UnityEngine;

public class AnimationSystem : MonoBehaviour
{
    [SerializeField] private GameObject _streakGO;
    [SerializeField] private string _animationParameter;
    [SerializeField] private float _activeDelay;

    private Animator _animator;

    public static AnimationSystem Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void ActiveStreak()
    {
        _streakGO.SetActive(true);
        if(_animator != null )
        {
            _animator.SetBool(_animationParameter, true);
        }

        Invoke("DisableStreak", _activeDelay);
    }

    private void DisableStreak()
    {
        _streakGO.SetActive(false);
        if(_animator != null)
        {
            _animator.SetBool(_animationParameter, false);
        }
    }
}
