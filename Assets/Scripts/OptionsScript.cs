using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionsScreenScript : MonoBehaviour
{
public void loadStartScreen(){
        SceneManager.LoadScene("StartScreen");
    }

public void loadCalibration(){
        SceneManager.LoadScene("CalibrationScreen");
    }
}
