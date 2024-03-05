using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class KeyController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler{

    private Image theImage;
    public Sprite defaultImage;
    public Sprite pressedImage;

    public KeyCode noteToPlay;

    // Start is called before the first frame update
    void Start(){
        theImage = GetComponent<Image>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //Changes when the image (the key) is pressed
        theImage.sprite = pressedImage;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        //Changes when the image back (the key) is released
        theImage.sprite = defaultImage;
    }
}
