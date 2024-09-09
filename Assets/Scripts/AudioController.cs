using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class AudioController : MonoBehaviour
{
    public static AudioController instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;

            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public AudioSource mainMenuTrack; // main menu bgm
    public AudioSource[] courseTracks; // course bgm

    private bool courseTrackPlaying;
    private int currentTrack; // track array element

    public AudioSource[] sfx; // sound effects

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(courseTrackPlaying) // while course bgm is playing
        {
            if (!courseTracks[currentTrack].isPlaying) // if the current track has ended,
            {
                currentTrack++; // load the next track

                if (currentTrack >= courseTracks.Length) // once the last track has played,
                {
                    currentTrack = 0; // go back to the first track
                }

                courseTracks[currentTrack].Play(); // play the soundtrack
            }
        }
    }

    public void PlayMainMenuTrack()
    {
        mainMenuTrack.Play();

        // ensures that only the main menu bgm is playing
        courseTrackPlaying = false; 
        courseTracks[currentTrack].Stop();
    }

    public void PlayCourseTrack()
    {
        mainMenuTrack.Stop(); // stops main menu bgm

        courseTrackPlaying = true;
        if (!courseTracks[currentTrack].isPlaying)
        {
            courseTracks[currentTrack].Play();
        }
    }

    public void PlaySFX(int sfxToPlay)
    {
        sfx[sfxToPlay].Stop();
        sfx[sfxToPlay].Play();
    }
}
