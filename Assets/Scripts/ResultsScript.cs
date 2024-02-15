using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResultsScript : MonoBehaviour
{
    public void loadSongSelectionScreen(){
        SceneManager.LoadScene("SongSelectionScreen");
    }

    public void loadGameScreen(){
        SceneManager.LoadScene("GameScreen");
    }
}
