using System.Collections.Generic;
using UnityEngine;

public class EffectsSystem : MonoBehaviour
{
    [SerializeField] private List<GameObject> _objectDestructionEffects;
    [SerializeField] private List<GameObject> _touchEffets;
    [SerializeField] private List<GameObject> _starEffects;


    public static List<GameObject> ObjectDestructionEffects;

    public static EffectsSystem Instance;
    public static int EffectIndex;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        EffectIndex = 0;
        ObjectDestructionEffects = new List<GameObject>();
        SessionManager.Instance.OnGameLost += DestroyAllEffects;
        SessionManager.Instance.OnGameWon += DestroyAllEffects;
    }

    private void OnDestroy()
    {
        SessionManager.Instance.OnGameLost -= DestroyAllEffects;
        SessionManager.Instance.OnGameWon -= DestroyAllEffects;
    }

    public void SpawnDesctructionEffects(Vector2 position)
    {
        ObjectDestructionEffects.Add(Instantiate(_objectDestructionEffects[Random.Range(0, _objectDestructionEffects.Count)], position, Quaternion.identity));
    }

    public void SpawnTouchEffects(GameObject gameObject)
    {
        ObjectDestructionEffects.Add(Instantiate(_touchEffets[Random.Range(0, _touchEffets.Count)], gameObject.transform.position, Quaternion.identity));
    }

    public void SpawnStartEffets(GameObject gameObject)
    {
        var system = gameObject.GetComponent<StarSystem>();
        system.SpawnStar();
        EffectIndex++;
    }

    private void DestroyAllEffects()
    {
        for(int i = 0; i < ObjectDestructionEffects.Count; i++)
        {
            Destroy(ObjectDestructionEffects[i]);
        }
    }
}
