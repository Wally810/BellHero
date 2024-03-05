using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class SongReader : MonoBehaviour
{

    public string songLocation = "Assets/Songs/testSong.txt";
    public float tempo = 120;
    public float tempoDelay = 0;

    void Start()
    {
        outputSong();
    }

    void outputSong(){
        tempo = 120/60f;
        tempoDelay = tempo;

        if (File.Exists(songLocation))
        {
            //Reads all the text from a txt file
            string text = File.ReadAllText(songLocation);

            //Split text into an array using spaces to know where to split
            string[] letters = text.Split(' ');

            foreach (string letter in letters)
            {
                tempoDelay += tempo;

                if (letter == "A"){
                    //Debug.Log("Letter A detected");
                    StartCoroutine (DelayCoroutine(tempoDelay, "1"));
                }
                if (letter == "B"){
                    //Debug.Log("Letter B detected");
                    StartCoroutine (DelayCoroutine(tempoDelay, "2"));
                }
                if (letter == "C"){
                    //Debug.Log("Letter C detected");
                    StartCoroutine (DelayCoroutine(tempoDelay, "3"));
                }
                if (letter == "D"){
                    //Debug.Log("Letter D detected");
                    StartCoroutine (DelayCoroutine(tempoDelay, "4"));
                }
                if (letter == "E"){
                    //Debug.Log("Letter D detected");
                    StartCoroutine (DelayCoroutine(tempoDelay, "5"));
                }
                if (letter == "F"){
                    //Debug.Log("Letter D detected");
                    StartCoroutine (DelayCoroutine(tempoDelay, "6"));
                }
            }

        }
        else
        {
            Debug.LogError("File not found: " + songLocation);
        }
    }

    IEnumerator DelayCoroutine(float tempo, string noteNum){
        yield return new WaitForSeconds(tempo);
        //Debug.Log("Delayed");

        string referenceNoteName = "Note" + noteNum;
        GameObject referenceNote = GameObject.Find(referenceNoteName);

        NoteSpawner spawnClone = referenceNote.GetComponent<NoteSpawner>();
        spawnClone.spawnClone();
    }
}
