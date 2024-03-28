using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteObject : MonoBehaviour {

    public bool canBeDeleted = false;
    public float confidenceNumber;
    
    public int startRecording;
    public int recordingLength;

    public int noteFreqNum;
    public int noteFreq;

    public float sec;

    public int lengthCounter;

    public int[] frequencyArray = {
        299, 940, 980, 1030, 277, 293, 311,
        329, 349, 370, 392, 415, 440, 466,
        493, 523, 554, 587, 622, 659, 698};

    /*public int[] frequencyArray = {
        220, 233, 246, 261, 277, 293, 311,
        329, 349, 370, 392, 415, 440, 466,
        493, 523, 554, 587, 622, 659, 698};*/

    // Start is called before the first frame update
    void Start() {
        startRecording = -1;
        recordingLength = 3148;

        lengthCounter = 0;

        //Debug.Log("Note number: " + noteFreqNum);

        noteFreq = frequencyArray[noteFreqNum - 1];

        //Debug.Log("Note frequency: " + noteFreq);

        sec = 0.5f;
    }

    // Update is called once per frame
    void Update() {

        /*if (transform.position.y <= -Screen.height * 0.2f){
            gameObject.SetActive(false);
        }
        else if (transform.position.y <= Screen.height * 0.005f){
            Perfect = true;
        }
        else if (transform.position.y <= Screen.height * 0.04f){
            Good = true;
        }*/

        if (canBeDeleted){

            

            //Debug.Log(Microphone.GetPosition(null));
            //Debug.Log(SoundTransform.samples[Microphone.GetPosition(null)]);
            //Debug.Log(SoundTransform.samplingFreq);

            if(startRecording == -1){
                startRecording = Microphone.GetPosition(null);
            }

            if(startRecording != -1){
                if(
                    Microphone.GetPosition(null) > startRecording + recordingLength * lengthCounter 
                    && lengthCounter<(int) (sec/((float)recordingLength/48000))
                    ){

                    Debug.Log("LengthCounter:" + lengthCounter);

                    //for( int i = 0; i < (int) (sec/((float)recordingLength/48000)); i++ ){
                        confidenceNumber = SoundTransform.freqFinder(
                            noteFreq, 
                            startRecording + recordingLength * lengthCounter, 
                            startRecording + recordingLength * (lengthCounter+1)
                        );
                        //Debug.Log("Confidence Number for " + lengthCounter + " : " + confidenceNumber);
                        if(confidenceNumber > 0.3){
                            Debug.Log("Confidence Number reached, Object deleted at: " + confidenceNumber);
                            gameObject.SetActive(false);
                        }
                        lengthCounter +=1;
                    //}

                    //Debug.Log(startRecording);
                    //Debug.Log(startRecording + recordingLength);
                    //confidenceNumber = SoundTransform.freqFinder(noteFreq, startRecording, (startRecording + recordingLength));
                    /*
                    Debug.Log("Good: " + confidenceNumber[2]);
                    Debug.Log("Great: " + confidenceNumber[1]);
                    Debug.Log("Perfect: " + confidenceNumber[0]);
                    */
                    

                } else if (
                    Microphone.GetPosition(null) > startRecording + recordingLength*lengthCounter - SoundTransform.samples.Length
                    && lengthCounter<(int) (sec/((float)recordingLength/48000))
                    ){
                    
                    //for( int i = 0; i < (int) (sec/((float)recordingLength/48000)); i++ ){
                        confidenceNumber = SoundTransform.freqFinder(
                            noteFreq, 
                            startRecording + recordingLength * lengthCounter, 
                            startRecording + recordingLength * (lengthCounter+1)
                        );
                        //Debug.Log("Confidence Number for " + lengthCounter + " : " + confidenceNumber);
                        if(confidenceNumber > 0.3){
                            Debug.Log("Confidence Number reached, Object deleted at: " + confidenceNumber);
                            gameObject.SetActive(false);
                        }
                        lengthCounter +=1;
                    //}

                    //confidenceNumber = SoundTransform.freqFinder(noteFreq, startRecording, (startRecording + recordingLength));
                    /*
                    Debug.Log("Good: " + confidenceNumber[2]);
                    Debug.Log("Great: " + confidenceNumber[1]);
                    Debug.Log("Perfect: " + confidenceNumber[0]);
                    */
                    
                }
            }

            /*
            if (confidenceNumber >= 1){
                Debug.Log("Confidence Number reached, Object deleted.");
                gameObject.SetActive(false);
            }
            */

        }

        if (transform.position.y <= Screen.height * 0.12f){
            canBeDeleted = true;
        }

        if (transform.position.y <= -Screen.height * 0.2f){
            gameObject.SetActive(false);
        }
        
    }
}
