using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    
    public void playGame (){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }

    public void stopGame (){
        Debug.Log("Quit");
        Application.Quit();
    }
}
