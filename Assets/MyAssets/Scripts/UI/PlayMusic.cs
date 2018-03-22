using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

public class PlayMusic : MonoBehaviour {

    public static PlayMusic instance;

	public AudioClip[] musicClips;					//Array of music clips to play
	private AudioMixerSnapshot volumeDown;			//NOT USED//Reference to Audio mixer snapshot in which the master volume of main mixer is turned down
    private AudioMixerSnapshot volumeUp;				//NOT USED//Reference to Audio mixer snapshot in which the master volume of main mixer is turned up

    public bool isMuted;


	private AudioSource musicSource;				//Reference to the AudioSource which plays music

	void Awake () 
	{
        instance = this;
		//Get a component reference to the AudioSource attached to the UI game object
		musicSource = GetComponent<AudioSource> ();
        isMuted = false;
	}
	
	//Used if running the game in a single scene, takes an integer music source allowing you to choose a clip by number and play.
	public void PlaySelectedMusic(int musicChoice)
	{
		//Play the music clip at the array index musicChoice
		musicSource.clip = musicClips [musicChoice];

		//Play the selected clip
		musicSource.Play ();
        
	}

	//Call this function to very quickly fade up the volume of master mixer
	public IEnumerator FadeUp()
	{
		//call the TransitionTo function of the audioMixerSnapshot volumeUp;
        while (musicSource.volume < 0.9f)
        {
            musicSource.volume = Mathf.Lerp(0, 1, 40 * Time.fixedDeltaTime);
            yield return null;
        }
        
	}

	//Call this function to fade the volume to silence over the length of fadeTime
	public IEnumerator FadeDown()
	{
		//call the TransitionTo function of the audioMixerSnapshot volumeDown;

        while (musicSource.volume > 0.1f)
        {
            musicSource.volume = Mathf.Lerp(1, 0, 100 * Time.fixedDeltaTime);
            yield return null;
        }
        
	}

    
    public void MuteUnmute()
    {
        if (isMuted )
        {
           // StartCoroutine(FadeUp());
            SetVolume(1);
            musicSource.UnPause();
            
            isMuted = false;
        } else
        if(!isMuted){
           // StartCoroutine(FadeDown());
            SetVolume(0);
            musicSource.Pause();
            
            isMuted = !isMuted;
        }

    }
     public void SetVolume(float val)
    {
         
        musicSource.volume = val;
    }
}


