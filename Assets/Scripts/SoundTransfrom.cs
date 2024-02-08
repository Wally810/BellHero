using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTransfrom : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        foreach( var weh in Microphone.devices)
        {
            Debug.Log(weh);
        }   
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
