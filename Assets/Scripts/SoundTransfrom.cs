using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTransfrom : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(Microphone.devices[0]);
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.clip = Microphone.Start(Microphone.devices[0], false, 10, 44100);
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
