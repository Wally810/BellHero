using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SongSelectionScript : MonoBehaviour
{
    public void loadStartScreen(){
        SceneManager.LoadScene("StartScreen");
    }
}
