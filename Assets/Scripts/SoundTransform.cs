using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTransform : MonoBehaviour
{
    public static int samplingFreq;
    public static float sensitivity = 1.5f;
    private AudioSource audioSource;
    public static float[] samples;
	//public static float greatStart = .1f; 
	//public static float greatEnd = .9f; 
	//public static float perfectStart = .2f;
	//public static float perfectEnd = .8f;
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
        float tempSample = 0f;
		//float greatSubtract = 0f;
		//float perfectSubtract = 0f;
		//float[] scores = new float[] {0,0,0};
		/*
        for (int pos = (start); pos < end; pos++)
        {
            foundFrequency += (float)(samples[pos] * Math.Sin(2 * Math.PI / samplingFreq/targetFrequency *  pos%(samplingFreq/targetFrequency)));
        }
		foundFrequency = sensitivity*((float)targetFrequency/(float)samplingFreq)*Math.Abs(targetFrequency);
		*/
        /*
        if(end > samplingFreq){
            return scores;
        }
        */
		for (int pos = start; pos < end; pos++)
        {
            tempSample = samples[pos%samplingFreq]*sensitivity;
            if(tempSample>1){
                tempSample = 1;
            }else if(tempSample<-1){
                tempSample = -1;
            }
            foundFrequency += (float)(tempSample * Math.Sin((2 * Math.PI / (samplingFreq/targetFrequency)) * ( ((pos%samplingFreq)%(samplingFreq/targetFrequency)))));
			/*
            if((pos%samplingFreq) == (int)(greatStart*(end-start))+start){
				greatSubtract = foundFrequency;
			}else if((pos%samplingFreq) == (int)(perfectStart*(end-start))+start){
				perfectSubtract = foundFrequency;
			}else if((pos%samplingFreq) == (int)(perfectEnd*(end-start))+start){
				scores[0] = sensitivity*((float)targetFrequency/(float)samplingFreq)*Math.Abs(foundFrequency-perfectSubtract);
			}else if((pos%samplingFreq) == (int)(greatEnd*(end-start))+start){
				scores[1] = sensitivity*((float)targetFrequency/(float)samplingFreq)*Math.Abs(foundFrequency-greatSubtract);
			}
            */
        }
		foundFrequency = sensitivity*((float)targetFrequency/(float)samplingFreq)*Math.Abs(foundFrequency);
        return foundFrequency;
    }
    // Update is called once per frame
    
    void Update()
    {
        audioSource.clip.GetData(samples, 0);
    }
    
}