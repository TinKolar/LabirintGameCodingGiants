using UnityEngine;
using UnityEngine.Rendering;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] int timeToEnd;

    bool gamePaused = false;

    bool endGame = false;
    bool win = false;

    public int points = 0;
    public int redKey = 0;
    public int greenKey = 0;
    public int goldKey = 0;

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

        InvokeRepeating("Stopper", 2, 1);
    }

    void Update()
    {
        PauseCheck();
        PickUpCheck();
    }

    public void AddKey(KeyColor color)
    {
        if (color == KeyColor.Red)
        {
            redKey++;
        }
        else if (color == KeyColor.Green) 
        {
            greenKey++;
        }
        else if (color == KeyColor.Gold)
        {
            goldKey++;
        }
    }

    public void AddPoints(int point)
    {
        points += point;
    }
    public void AddTime(int time)
    {
        timeToEnd += time;
    }
    public void FreezTime(int freez)
    {
        CancelInvoke("Stopper");
        InvokeRepeating("Stopper",freez, 1);
    }

    void Stopper()
    {


        timeToEnd--;
        //Debug.Log("Time: " + timeToEnd + " s");
        //Debug.Log($"Time: {timeToEnd} s");

        if(timeToEnd <= 0){
            timeToEnd = 0;
            endGame = true;
        }
        if (endGame) 
        {
            EndGame();
        }

    }

    public void PauseGame()
    {
        Debug.Log("Pause game!");
        Time.timeScale = 0f;
        gamePaused = true;
    }

    public void ResumeGame()
    {
        Debug.Log("Resume game!");

        Time.timeScale = 1f;
        gamePaused = false;
    }

    void PauseCheck()
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
    }
    public void EndGame()
    {
        CancelInvoke("Stopper");
        if (win)
        {
            Debug.Log("YOU WIN!!!!");
        }
        else
        {
            Debug.Log("YOU Loost!!!!");

        }
    }

    void PickUpCheck()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            Debug.Log("Time to end: "+ timeToEnd);
            Debug.Log("Key red: "+ redKey+" green: " + greenKey+" gold: "+ goldKey);
            Debug.Log("points: " + points);
        }
    }
}
