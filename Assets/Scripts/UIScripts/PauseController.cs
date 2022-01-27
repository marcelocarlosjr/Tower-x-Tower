using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PauseController : MonoBehaviour
{
    public bool paused = false;
    public GameObject pauseMenu;
    public GameObject winMenu;

    public void PauseGame()
    {
        paused = true;
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        paused = false;
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void ExitToMainMenu()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
        SceneManager.LoadScene("MainMenu");
    }

    private void Update()
    {
        if (Input.GetAxisRaw("Cancel") == 1)
        {
            if (!paused)
            {
                PauseGame();
            }
            
        }
    }

    public void winGame()
    {
        Time.timeScale = 0;
        winMenu.SetActive(true);
    }

}
