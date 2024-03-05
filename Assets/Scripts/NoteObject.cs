using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteObject : MonoBehaviour {

    public bool canPress;
    public KeyCode noteToPlay;

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        if(Input.GetKeyDown(noteToPlay)) {
            if(canPress){
                gameObject.SetActive(false);
            }
        } else if (transform.position.y <= -700){
            gameObject.SetActive(false);
        }
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Activator") {
            canPress = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.tag == "Activator") {
            canPress = true;
        }
    }
}
