using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int timeToEnd;
    bool gamePaused = false;
    bool endGame = false;
    bool win = false;
    public int redKey = 0;
    public int greenKey = 0;
    public int goldKey = 0;
    public int points = 0;
    AudioSource audioSource;
    public AudioClip resumeClip;
    public AudioClip pauseClip;
    public AudioClip winClip;
    public AudioClip loseClip;
    public MusicScript musicScript;
    bool lessTime = false;
    [Header("URP Post Processing")]
    public Volume volume;
    public VolumeProfile normalProfile;
    public VolumeProfile lessTimeProfile;
    public void FreezTime(int freez)
    {
        CancelInvoke("Stopper");
        InvokeRepeating("Stopper", freez, 1);
    }
    public void AddPoints(int point)
    {
        points += point;
    }
    public void AddTime(int addTime)
    {
        timeToEnd += addTime;
    }
    public void AddKey(KeyColor color)
    {
        switch (color)
        {
            case KeyColor.Red:
                redKey++;
                break;
            case KeyColor.Green:
                greenKey++;
                break;
            case KeyColor.Gold:
                goldKey++;
                break;
        }
    }
    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
        if (timeToEnd <= 0)
        {
            timeToEnd = 100;
        }
        LessTimeOff();
        audioSource = GetComponent<AudioSource>();
        InvokeRepeating("Stopper", 2, 1);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (gamePaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            Debug.Log("Actual Time: " + timeToEnd);
            Debug.Log("Key red: " + redKey + " green: " + greenKey + " gold: " + goldKey);
            Debug.Log("Points: " + points);
        }
    }
    void Stopper()
    {
        timeToEnd--;
        //Debug.Log("Time: " + timeToEnd + " s");
        if (timeToEnd <= 0)
        {
            timeToEnd = 0;
            endGame = true;
        }
        if (endGame)
        {
            EndGame();
        }
        if (timeToEnd < 20 && !lessTime)
        {
            LessTimeOn();
            lessTime = true;
        }
        if (timeToEnd > 20 && lessTime)
        {
            LessTimeOff();
            lessTime = false;
        }
    }
    public void PauseGame()
    {
        PlayClip(pauseClip);
        Debug.Log("Pause Game");
        musicScript.OnPauseGame();
        Time.timeScale = 0f;
        gamePaused = true;
    }
    public void ResumeGame()
    {
        PlayClip(resumeClip);
        Debug.Log("Resume Game");
        musicScript.OnResumeGame();
        Time.timeScale = 1f;
        gamePaused = false;
    }
    public void EndGame()
    {
        CancelInvoke("Stopper");
        if (win)
        {
            PlayClip(winClip);
            Debug.Log("You Win!!! Reload?");
        }
        else
        {
            PlayClip(loseClip);
            Debug.Log("You Lose!!! Reload?");
        }
    }
    public void PlayClip(AudioClip playClip)
    {
        audioSource.clip = playClip;
        audioSource.Play();
    }
    public void LessTimeOn()
    {
        if (musicScript != null)
            musicScript.PitchThis(1.58f);

        if (volume != null && lessTimeProfile != null)
            volume.profile = lessTimeProfile;
    }

    public void LessTimeOff()
    {
        if (musicScript != null)
            musicScript.PitchThis(1f);

        if (volume != null && normalProfile != null)
            volume.profile = normalProfile;
    }
}