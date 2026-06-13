using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
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

    public TMP_Text timeText;
    public TMP_Text goldKeyText;
    public TMP_Text greenKeyText;
    public TMP_Text redKeyText;
    public TMP_Text crystalText;
    public Image snowFlake;

    public GameObject infoPanel;
    public TMP_Text pauseEnd;
    public TMP_Text reloadInfo;
    public TMP_Text useInfo;


    public void FreezTime(int freez)
    {
        CancelInvoke("Stopper");
        snowFlake.enabled = true;
        InvokeRepeating("Stopper", freez, 1);
    }
    public void AddPoints(int point)
    {
        points += point;
        crystalText.text = points.ToString();
    }
    public void AddTime(int addTime)
    {
        timeToEnd += addTime;
        timeText.text = timeToEnd.ToString();
    }
    public void AddKey(KeyColor color)
    {
        switch (color)
        {
            case KeyColor.Red:
                redKey++;
                redKeyText.text = redKey.ToString();
                break;
            case KeyColor.Green:
                greenKey++;
                greenKeyText.text = greenKey.ToString();
                break;
            case KeyColor.Gold:
                goldKey++;
                goldKeyText.text = goldKey.ToString();
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

        snowFlake.enabled = false;
        timeText.text=timeToEnd.ToString();
        infoPanel.SetActive(false);
        pauseEnd.text = "Pause";
        reloadInfo.text = "";
        SetUseInfo("");

        LessTimeOff();
        audioSource = GetComponent<AudioSource>();
        InvokeRepeating("Stopper", 2, 1);
    }

    public void SetUseInfo(string v)
    {
        useInfo.text=v;
    }

    void Update()
    {
        if (endGame)
        {
            if (Input.GetKeyDown(KeyCode.Y))
            {
                SceneManager.LoadScene(0);
            }
            if (Input.GetKeyDown(KeyCode.N)) 
            {
                Application.Quit();
            }
        }

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
        timeText.text = timeToEnd.ToString();
        snowFlake.enabled=false;
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
        infoPanel.SetActive(true);
        Debug.Log("Pause Game");
        musicScript.OnPauseGame();
        Time.timeScale = 0f;
        gamePaused = true;
    }
    public void ResumeGame()
    {
        PlayClip(resumeClip);
        infoPanel.SetActive(false);

        Debug.Log("Resume Game");
        musicScript.OnResumeGame();
        Time.timeScale = 1f;
        gamePaused = false;
    }
    public void EndGame()
    {
        CancelInvoke("Stopper");
        infoPanel.SetActive(true);
        if (win)
        {
            PlayClip(winClip);
            pauseEnd.text = "You Win!!";
            reloadInfo.text = " Reload? Y/N";
            Debug.Log("You Win!!! Reload?");
        }
        else
        {
            PlayClip(loseClip);
            pauseEnd.text = "You Lose! :(";
            reloadInfo.text = " Reload? Y/N";
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
    public void WinGame()
    {
        win = true;
        endGame = true;
    }
}