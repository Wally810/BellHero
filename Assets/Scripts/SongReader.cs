using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class SongReader : MonoBehaviour
{

    //public string songLocation = "Assets/Songs/" + SongSelectionScript.ClickedButtonName + ".txt";
    public string songLocation = "Assets/Songs/testSong.txt";
    public float tempo = 120;
    public float tempoDelay = 0;

    void Start()
    {
        //songLocation = "Assets/Songs/" + SongSelectionScript.ClickedButtonName + ".txt";
        songLocation = "Assets/Songs/testSong.txt";
        Debug.Log(songLocation + " has been recieved");
        outputSong();
    }

    void outputSong(){
        tempo = 120/60f;
        tempoDelay = tempo;

        if (File.Exists(songLocation))
        {
            
            foreach (string songLine in File.ReadLines(songLocation))
            {
                tempoDelay += tempo;

                //Split text into an array using dash to know where to split
                string[] lineNotes = songLine.Split('-');

                foreach (string SongNote in lineNotes)
                {
                    StartCoroutine (DelayCoroutine(tempoDelay, SongNote));
                }
            }

        }
        else
        {
            Debug.LogError("File not found: " + songLocation);
        }
    }

    IEnumerator DelayCoroutine(float tempo, string note){
        yield return new WaitForSeconds(tempo);
        int noteNum = int.Parse(note);

        //Debug.Log("Delayed");

        string referenceNoteName = "Note" + noteNum;
        GameObject referenceNote = GameObject.Find(referenceNoteName);

        NoteSpawner spawnClone = referenceNote.GetComponent<NoteSpawner>();
        spawnClone.spawnClone(noteNum);
    }
}
