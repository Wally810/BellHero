using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTransform : MonoBehaviour
{
    public static int samplingFreq;
    public static float sensitivity = 40;
    private AudioSource audioSource;
    public static float[] samples;
    void Start()
    {
        Debug.Log(Microphone.devices[0]);
        Microphone.GetDeviceCaps("",out int minCap, out int maxCap);
        Debug.Log(minCap + " " + maxCap);
        samplingFreq = maxCap;
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = Microphone.Start(Microphone.devices[0], true, 1, samplingFreq);
        samples = new float[audioSource.clip.samples * audioSource.clip.channels];
        audioSource.loop = true;
    }
    public static float freqFinder(int targetFrequency, int start, int end){
        float foundFrequency = 0f;
        for (int pos = (start); pos < end; pos++)
        {
            foundFrequency += (float)(samples[pos] * Math.Sin((2 * Math.PI / (samplingFreq/targetFrequency)) * ( (pos%(samplingFreq/targetFrequency)))));
        }
        //Debug.Log(foundFrequency);
        foundFrequency = sensitivity*((float)targetFrequency/(float)samplingFreq)*Math.Abs(foundFrequency);
        //Debug.Log(foundFrequency);
        return foundFrequency;
    }
    // Update is called once per frame
    
    void Update()
    {
        audioSource.clip.GetData(samples, 0);
    }
    
}
