using UnityEngine;
using System.Collections;

public class ShowPanels : MonoBehaviour {

    public GameObject startMenu;
    public GameObject restartMenu;
    public GameObject firstAmim;
    public GameObject inGameUI;
    public GameObject pauseMenu;
    public GameObject ruletkaPanel;

    public static ShowPanels panels;

    private PlayMusic playMusic;


    void Awake() 
    {
        panels = this;
        playMusic = FindObjectOfType<PlayMusic>().GetComponent<PlayMusic>();
    }

	public void ShowStartMenu ()
    {
        startMenu.SetActive(true);
        // set the menu music
        playMusic.PlaySelectedMusic(0);
    }


    public void HideStartMenu()
    {
        startMenu.SetActive(false);
    }


    public void ShowRestartMenu () 
    {
        restartMenu.SetActive(true);

        // set the menu music
        playMusic.PlaySelectedMusic(0);
    
    }


    public void HideRestartMenu()
    {
        restartMenu.SetActive(false); 
    }


    public void ShowFirstAnimation()
    {
        firstAmim.SetActive(true);
    }


    public void HideFirstAnimation()
    {
        firstAmim.SetActive(false);
    }

    public void ShowGUI()
    {
        inGameUI.SetActive(true);
    }


    public void HideGUI()
    {
        inGameUI.SetActive(false);
    }

    public void ShowPause()
    {
        pauseMenu.SetActive(true);
    }


    public void HidePause()
    {
        pauseMenu.SetActive(false);
    }

    public void ShowRuletkaPanel()
    {
        ruletkaPanel.SetActive(true);
    }


    public void HideRuletkaPanel()
    {
        ruletkaPanel.SetActive(false);
    }
}
