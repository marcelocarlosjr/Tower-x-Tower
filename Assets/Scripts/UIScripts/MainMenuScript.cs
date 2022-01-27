using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour
{
    
    public bool creditsMenu = false;
    public bool settingsMenu = false;
    public GameObject MainPanel;
    public GameObject CreditsPanel;
    public GameObject SettingsPanel;
    public Slider bgmSlider;
    public Slider sfxSlider;

    public void gameStart()
    {
        SceneManager.LoadScene("Floor1");
        Time.timeScale = 1;
    }

    public void creditsToggle()
    {
        if(creditsMenu == false)
        {
            MainPanel.SetActive(false);
            CreditsPanel.SetActive(true);
            creditsMenu = true;

        }
        else
        {
            creditsMenu = false;
            MainPanel.SetActive(true);
            CreditsPanel.SetActive(false);
        }
    }

    public void settingsToggle()
    {
        sfxSlider.value = FindObjectOfType<AudioManager>().sfxVolume;
        bgmSlider.value = FindObjectOfType<AudioManager>().musicVolume;


        if (settingsMenu == false)
        {
            MainPanel.SetActive(false);
            SettingsPanel.SetActive(true);
            settingsMenu = true;

        }
        else
        {
            settingsMenu = false;
            MainPanel.SetActive(true);
            SettingsPanel.SetActive(false);
        }
    }

    public void OnButtonClickSound()
    {
        FindObjectOfType<AudioManager>().Play("onButtonClick");
    }
    public void OnButtonHoverSound()
    {
        FindObjectOfType<AudioManager>().Play("onButtonHover");
    }

    public void OnSettingsApply()
    {
        FindObjectOfType<AudioManager>().musicVolume = bgmSlider.value;
        FindObjectOfType<AudioManager>().sfxVolume = sfxSlider.value;
    }

    public void gameExit()
    {
        Application.Quit();
    }

} 
