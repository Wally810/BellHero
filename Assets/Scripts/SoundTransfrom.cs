using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTransfrom : MonoBehaviour
{
    public GameObject targetPrefab;
    
    public int[] testHz = new int[] { 
        32, 34, 36, 38, 41, 43, 46, 49, 52, 55, 58, 61,
        65, 69, 73, 77, 82, 87, 92, 98, 104, 110, 116, 123,
        130, 138, 146, 155, 164, 174, 185, 196, 208, 220, 233, 246, 
        260, 276, 292, 310, 328, 348, 370, 392, 416, 440, 466, 492,
        523, 554, 587, 622, 659, 698, 739, 784, 830, 880, 932, 987
        };
    
    //public int[] testHz = new int[] {440};
    public GameObject[] weh;
    private int samplingFreq;
    public int wrappingRate;
    public float soundScale;
    private AudioSource audioSource;
    private float[] frequencyLevels;
    private float[] frequencyLevelsSmooth;
    private int lastFoundPos;
    private float[] sample;
    public float lowerBound;
    public float upperBound;
    // Start is called before the first frame update
    void Start()
    {
        lastFoundPos = 0;
        Debug.Log(Microphone.devices[0]);
        Microphone.GetDeviceCaps("",out int minCap, out int maxCap);
        Debug.Log(minCap + " " + maxCap);
        samplingFreq = maxCap;
        wrappingRate = (int)(wrappingRate*Math.PI*2);
        frequencyLevels = new float[testHz.Length];
        frequencyLevelsSmooth = new float[testHz.Length];
        weh = new GameObject[testHz.Length];
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = Microphone.Start(Microphone.devices[0], true, 1, samplingFreq);
        Debug.Log(audioSource.clip.samples);
        sample = new float[audioSource.clip.samples * audioSource.clip.channels];
        audioSource.loop = true;

        for (int index = 0; index < weh.Length; index++)
        {
            weh[index] = Instantiate(targetPrefab, new Vector3(index - (weh.Length / 2 - 0.5f), 0, 0), Quaternion.identity);
        }
    }

    float[] sectionProcess(float[] samples, int offset, int sampleLength)
    {
        float[] foundFrequencies = new float[testHz.Length];
        if (offset < sampleLength)
        {
            for (int indexHz = 0; indexHz < testHz.Length; indexHz++)
            {
                for (int pos = (offset); pos < sampleLength; pos++)
                {
                    foundFrequencies[indexHz] += (float)(samples[pos] * Math.Sin((2 * Math.PI / (samplingFreq/testHz[indexHz])) * ( (pos%(samplingFreq/testHz[indexHz])))));
                }
                foundFrequencies[indexHz] = ((float)testHz[indexHz]/(float)samplingFreq)*Math.Abs(foundFrequencies[indexHz]);
                if(foundFrequencies[indexHz]>((float)testHz[indexHz]/(float)samplingFreq)*lowerBound){
                    foundFrequencies[indexHz] = foundFrequencies[indexHz]*10;
                }else{
                    foundFrequencies[indexHz] = 0;
                }
                //foundFrequencies[indexHz] = (float)(foundFrequencies[indexHz] * (4 * Math.PI)/wrappingRate);
                //foundFrequencies[indexHz] = soundScale*foundFrequencies[indexHz]*foundFrequencies[indexHz]/wrappingRate;
                //foundFrequencies[indexHz] = 30*foundFrequencies[indexHz];
                //foundFrequencies[indexHz] = (foundFrequencies[indexHz]+frequencyLevelsSmooth[indexHz])/2;
                //frequencyLevelsSmooth[indexHz] = (foundFrequencies[indexHz]*2)-frequencyLevelsSmooth[indexHz];
            }
        }
        if (offset > sampleLength)
        {
            for (int indexHz = 0; indexHz < testHz.Length; indexHz++)
            {
                for (int pos = offset; pos < samplingFreq; pos++)
                {
                    foundFrequencies[indexHz] += (float)(samples[pos] * Math.Sin((2 * Math.PI / samplingFreq) * (testHz[indexHz] * (pos))));
                }
                for (int pos = 0; pos < sampleLength; pos++)
                {
                    foundFrequencies[indexHz] += (float)(samples[pos] * Math.Sin((2 * Math.PI / samplingFreq) * (testHz[indexHz] * (pos))));
                }
                //foundFrequencies[indexHz] = (float)(foundFrequencies[indexHz] * (4 * Math.PI)/wrappingRate);
                foundFrequencies[indexHz] = soundScale*(foundFrequencies[indexHz]*foundFrequencies[indexHz])/wrappingRate;
                //foundFrequencies[indexHz] = (foundFrequencies[indexHz]+frequencyLevelsSmooth[indexHz])/2;
                //frequencyLevelsSmooth[indexHz] = (foundFrequencies[indexHz]*2)-frequencyLevelsSmooth[indexHz];
            }
        }
        return foundFrequencies;
    }

    // Update is called once per frame
    void Update()
    {
        audioSource.clip.GetData(sample, 0);
        //Debug.Log(Microphone.GetPosition(null)+" "+(wrappingRate+lastFoundPos));
        //If the recording is still below the samplingFreq and is a wrappingRate away from the lastFoundPos 
        if (Microphone.GetPosition(null) > wrappingRate+lastFoundPos)
        {
            //Debug.Log("Weh"+Microphone.GetPosition(null)+" "+(wrappingRate+lastFoundPos));
            frequencyLevels = sectionProcess(sample, lastFoundPos, (wrappingRate+lastFoundPos));
            for (int index = 0; index < weh.Length; index++)
            {
                weh[index].transform.localScale = new Vector3(1, frequencyLevels[index], 1);
                weh[index].transform.position = new Vector3(index - (12 / 2 - 0.5f) - (12)*(int)(index/12), frequencyLevels[index]/2-5, (int)(index/12));
            }
            lastFoundPos = wrappingRate+lastFoundPos;
        }
        //If the recording has wrapped back to 0 and is a wrappingRate awayfrom the lastFoundPos
        else if (Microphone.GetPosition(null) < wrappingRate+lastFoundPos)
        {
            //Debug.Log("WehWeh");
            /*
            frequencyLevels = sectionProcess(sample, lastFoundPos, (wrappingRate+lastFoundPos-samplingFreq));
            for (int index = 0; index < weh.Length; index++)
            {
                //index - (weh.Length / 2 - 0.5f)
                weh[index].transform.localScale = new Vector3(1, frequencyLevels[index], 1);
                weh[index].transform.position = new Vector3(index - (weh.Length / 2 - 0.5f), frequencyLevels[index]/2-5, (int)(index/12));
            }
            */
            if(wrappingRate+lastFoundPos > samplingFreq)
            {
                lastFoundPos = wrappingRate+lastFoundPos-samplingFreq;
            }else{
                lastFoundPos = wrappingRate+lastFoundPos;
            }
        }
        
        
        //Debug.Log(lastFoundPos);
    }
}
