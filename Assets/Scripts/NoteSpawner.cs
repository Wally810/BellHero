using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteSpawner : MonoBehaviour {

    public float tempo;

    public KeyCode noteSpawner;
    public GameObject notePrefab;

    // Start is called before the first frame update
    void Start() {
        tempo = tempo / 60f;
    }

    // Update is called once per frame
    void Update() {
        
    }

    public void spawnClone(int noteNum){
        //Debug.Log("Clone Spawned");
                
        GameObject clonedNote = Instantiate(notePrefab, transform.position, Quaternion.identity, transform);

        clonedNote.GetComponent<NoteObject>().noteFreqNum=noteNum;
        clonedNote.GetComponent<ClonedNoteScroller>().tempo=120;
    }
}
