using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyController : MonoBehaviour{

    private SpriteRenderer theSR;
    public Sprite defaultImage;
    public Sprite pressedImage;

    public KeyCode noteToPlay;

    // Start is called before the first frame update
    void Start(){
        theSR = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update(){
        if(Input.GetKeyDown(noteToPlay)){
            theSR.sprite = pressedImage;
        }

        if(Input.GetKeyUp(noteToPlay)){
            theSR.sprite = defaultImage;
        }
    }
}
