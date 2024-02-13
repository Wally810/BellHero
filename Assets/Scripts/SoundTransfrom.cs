using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTransfrom : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(Microphone.devices[0]);
        //AudioSource audioSource = GetComponent<AudioSource>();
        StartCoroutine(Example());
        /*
        audioSource.clip = Microphone.Start(Microphone.devices[0], true, 1, 44100);
        audioSource.Play();
        Debug.Log(audioSource.clip.length);
        float[] samples = new float[audioSource.clip.samples * audioSource.clip.channels];
        audioSource.clip.GetData(samples, 0);
        Debug.Log(samples.Length);
        */
    }
    IEnumerator Example()
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.clip = Microphone.Start(Microphone.devices[0], true, 1, 44100);
        audioSource.Play();
        Debug.Log(audioSource.clip.length);

        yield return new WaitForSeconds(2.5f);
        float[] samples = new float[audioSource.clip.samples * audioSource.clip.channels];
        audioSource.clip.GetData(samples, 0);
        Debug.Log(samples.Length);
        foreach (float weh in samples)
        {
            Debug.Log(weh);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
