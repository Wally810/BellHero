using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClonedNoteScroller : MonoBehaviour
{
    public float tempo;
    public RectTransform clonedNoteRectTransform;

    // Start is called before the first frame update
    void Start()
    {
        //tempo = tempo / 60f;
    }

    // Update is called once per frame
    void Update()
    {
        clonedNoteRectTransform.anchoredPosition -= new Vector2(0f, tempo * Time.deltaTime);
    }
}
