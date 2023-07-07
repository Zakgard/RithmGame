using UnityEngine;
using System.Collections;

namespace EpicToonFX
{
    public class ETFXLightFade : MonoBehaviour
    {
        [Header("Seconds to dim the light")]
        public float life = 0.2f;
        public bool killAfterLife = true;

        private float _currentLifeTime;
        private Light li;
        private float initIntensity;

        // Use this for initialization
        void Start()
        {
            _currentLifeTime = 0.0f;
            if (gameObject.GetComponent<Light>())
            {
                li = gameObject.GetComponent<Light>();
                initIntensity = li.intensity;
            }
            else
                print("No light object found on " + gameObject.name);
        }

        // Update is called once per frame
        void Update()
        {
            _currentLifeTime = _currentLifeTime + Time.deltaTime;
           // li.intensity -= initIntensity * (Time.deltaTime / life);
            if (_currentLifeTime >= life )
            {
                 Destroy(gameObject);
                 Destroy(gameObject.GetComponent<Light>());
            }                 
            
        }
    }
}