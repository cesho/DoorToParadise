using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AvatarSelection : MonoBehaviour
{
    [SerializeField] public GameObject[] avatars; 

    public static int avatarIndex;

    audioManager audioManager;


    private void Start()
    {
         audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<audioManager>();
    }

    // To change Avatar based on clicking the buttons
    public void ChangeAvatar(int index)
    {
        for (int i = 0; i < avatars.Length; i++)
        {
            avatars[i].SetActive(false); 
        }
        avatarIndex = index; // assign the selected index
        avatars[index].SetActive(true); // enable the disabled sprites on click
        audioManager.PlaySFX(audioManager.avatarSelect);
    }

    // To Enter game when play button is pressed
    public void StartGame()
    {
        if (avatarIndex == 0 || avatarIndex == 1)
        {
            SceneManager.LoadScene(3); //Load the GameScene
            PlayerPrefs.SetInt("AvatarIndex", avatarIndex); //set the index to use in next scene
        }
    }

}
