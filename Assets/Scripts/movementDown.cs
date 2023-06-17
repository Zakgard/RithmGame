using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movementDown : MonoBehaviour
{
    

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * Time.deltaTime * BitAnalyzer._speed);
    }
}
