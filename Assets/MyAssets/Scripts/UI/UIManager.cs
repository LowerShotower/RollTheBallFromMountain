using UnityEngine;
using System.Collections;

public class UIManager : MonoBehaviour {


    
    public AnimationClip startingAnim;     // animation clip: take its length and us it referencing to animator
    
    public Animator startingScreenAnimator; // Get ref to Starting screen Animator


    private ShowPanels panels;  //  ref to ShowPanels script

    private PlayMusic playMusic;



    void Awake()
    {
        panels = GetComponent<ShowPanels>(); // Get ref to ShowPanels script
        
    }



    IEnumerator Start()
    {
        panels.ShowStartMenu();
        playMusic = FindObjectOfType<PlayMusic>().GetComponent<PlayMusic>();
        startingScreenAnimator.Play("StartingAnim");
        yield return new WaitForSeconds(startingAnim.length);
        panels.HideFirstAnimation();
        

    }


    // If start button of Start menu was clicked
    public void OnStartButtonClicked()
    {
        GameController.SetStartCondition();

        FindObjectOfType<GameController>().InstantiateHero(ScrollContentView.currentCentralObj);

        panels.HideStartMenu();
        panels.ShowGUI();
        playMusic.PlaySelectedMusic(1);
        
    }


    public void EnableBoolInAnimator(Animator anim) 
    {
        anim.SetBool("isEnabled", true);
    }

    public void DisableBoolInAnimator(Animator anim)
    {
        anim.SetBool("isEnabled", false);
    }

    public void Restart()
    {
        // after pressing it hide restartMenu, init gamePlay and reset score
        ShowPanels.panels.HideRestartMenu();
        ShowPanels.panels.ShowGUI();
        PlayMusic.instance.PlaySelectedMusic(1);
        GameController.gamePhase = GamePhase.StartingCondition;
        DataManager.instance.ResetScore();
    }

    public void PauseGame()
    {
        if (!Hero.isDefeated)
        {
            ShowPanels.panels.HideGUI();
            ShowPanels.panels.ShowPause();
            Pause.instance.DoPause();
        }

    }

    public void UnpauseGame()
    {
        ShowPanels.panels.ShowGUI();
        ShowPanels.panels.HidePause();
        Pause.instance.UnPause();
    }

    

}
