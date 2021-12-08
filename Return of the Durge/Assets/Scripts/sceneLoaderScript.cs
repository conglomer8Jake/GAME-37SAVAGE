using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneLoaderScript : MonoBehaviour
{
    public void loadSampleScene()
    {
        SceneManager.LoadScene("SampleScene");
    }
    public void exitGame()
    {
        Application.Quit();
    }
}
