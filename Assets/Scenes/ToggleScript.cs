using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleScript : MonoBehaviour
{
    public GameObject NoteNumbers;
    public GameObject NoteNamesF;
    public GameObject NoteNamesS;
    public GameObject NoteNamesCheck;
    public GameObject FlatsCheck;
    public bool isFlat;
    public bool isNumber;

    public void Start()
    {
        NoteNumbers.gameObject.SetActive(true);
        NoteNamesF.gameObject.SetActive(false);
        NoteNamesS.gameObject.SetActive(false);
        NoteNamesCheck.gameObject.SetActive(false);
        FlatsCheck.gameObject.SetActive(false);
        isFlat = false;
        isNumber = true;
    }

    public void ToggleNoteNames()
    {
       if(!isNumber){ 
        NoteNumbers.SetActive(true);
        NoteNamesS.SetActive(false);
        NoteNamesF.SetActive(false);
        NoteNamesCheck.SetActive(false);
        isNumber = true;
       }else{
            if(!isFlat){
                NoteNamesS.SetActive(true);
                NoteNamesF.SetActive(false);
            }else{
                NoteNamesS.SetActive(false);
                NoteNamesF.SetActive(true);
            }
        NoteNumbers.SetActive(false);
        NoteNamesCheck.SetActive(true);
        isNumber = false;
       }
    }

    public void ToggleFlats()
    {
        if (isNumber){
            if(!isFlat){
                isFlat=true;
                FlatsCheck.SetActive(true);
            }else { 
                isFlat=false;
                FlatsCheck.SetActive(false);
            }
        }else{
            if(!isFlat){
                isFlat = true;
                NoteNamesS.SetActive(false);
                NoteNamesF.SetActive(true);
                FlatsCheck.SetActive(true);
            }else {
                isFlat = false;
                NoteNamesS.SetActive(true);
                NoteNamesF.SetActive(false);
                FlatsCheck.SetActive(false);
            }
        }
    }

    
}
