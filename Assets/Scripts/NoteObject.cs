using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteObject : MonoBehaviour {

    public bool canBeDeleted = false;
    public float confidenceNumber = 0;
    public KeyCode noteToPlay;

    public bool Good;
    public bool Perfect;
    
    public int startRecording;
    public int recordingLength;

    // Start is called before the first frame update
    void Start() {
        startRecording = -1;
        recordingLength = 3148;
    }

    // Update is called once per frame
    void Update() {

        Good = false;
        Perfect = false;

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
                if(Microphone.GetPosition(null) > startRecording + recordingLength){
                    //Debug.Log(startRecording);
                    //Debug.Log(startRecording + recordingLength);
                    confidenceNumber = SoundTransform.freqFinder(220, startRecording, (startRecording + recordingLength));
                    Debug.Log("Reached If statement: " + confidenceNumber);
                } else if (Microphone.GetPosition(null) > startRecording + recordingLength - SoundTransform.samples.Length){
                    confidenceNumber = SoundTransform.freqFinder(220, startRecording, (startRecording + recordingLength));
                    Debug.Log("Reached If Else statement: " + confidenceNumber);
                }
            }

            if (confidenceNumber >= 100){
                gameObject.SetActive(false);
            }

        }

        if (transform.position.y <= Screen.height * 0.09f){
            canBeDeleted = true;
        }

        if (transform.position.y <= -Screen.height * 0.2f){
            gameObject.SetActive(false);
        }
        
    }

    /*
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Activator") {
            canPress = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.tag == "Activator") {
            canPress = false;
        }
    }
    */
}
