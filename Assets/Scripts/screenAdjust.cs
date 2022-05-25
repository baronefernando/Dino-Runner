using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class screenAdjust : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Camera.main.aspect = 1920f / 1080f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
