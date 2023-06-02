using UnityEngine;

public class Settings : MonoBehaviour
{
    [SerializeField] private int _targetFrameRate;

    private void Start()
    {
        Application.targetFrameRate= _targetFrameRate;
    }
}
