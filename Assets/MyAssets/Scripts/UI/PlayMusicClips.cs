using UnityEngine;
using System.Collections;

public class PlayMusicClips : MonoBehaviour {

	public AudioClip[] MusicClips;

    private AudioListener audioListener;

    void Awake()
    {
        audioListener = GetComponent<AudioListener>();
        
    }
}
