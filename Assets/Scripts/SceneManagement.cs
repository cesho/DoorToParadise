using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    // To Replay the game
    public void Restart()
    {
        SceneManager.LoadScene(2);
    }

    // To Enter Instruction area when next button is pressed
    public void Instruction()
    {
        SceneManager.LoadScene(1); //Load the GameScene
    }

    // To Enter End Note area when next button is pressed
    public void EndNote()
    {
        SceneManager.LoadScene(4); //Load the GameScene
    }

        // To Enter Menu area when next button is pressed
    public void Menu()
    {       
        SceneManager.LoadScene(2); //Load the GameScene       
    }
}
