using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteScroller : MonoBehaviour {

    public float tempo;

    public bool noteSpawned;

    public KeyCode noteSpawner;

    // Start is called before the first frame update
    void Start() {
        tempo = tempo / 60f;
    }

    // Update is called once per frame
    void Update() {

        if(!noteSpawned){
            if(Input.GetKeyDown(noteSpawner)){
                GameObject clone = Instantiate(gameObject, transform.position, Quaternion.identity);

                NoteScroller clonedNote = clone.GetComponent<NoteScroller>();

                if (clonedNote != null) {
                    clonedNote.noteSpawned = true;

                    //Link Tempo to original tempo later
                    clonedNote.tempo = 120;
                }
            }
        } else {
            transform.position -= new Vector3(0f, tempo * Time.deltaTime, 0f);
        }
        
    }
}
