using System.Security.Cryptography;
using UnityEngine;

public class Lock : MonoBehaviour
{

    public Door[] doors;
    public KeyColor myColor;
    bool isLocked=false;
    public Animator key;

    public Material red;
    public Material green;
    public Material gold;


    public Renderer myLock;

    bool iCanOpen =false;

    void Start()
    {
        key = GetComponent<Animator>();
        SetMyColor();
    }

    void SetMyColor()
    {
        switch (myColor)
        {
            case KeyColor.Red:
                GetComponent<Renderer>().material = red;
                myLock.material = red; //proper color of course
                break;
            case KeyColor.Green:
                GetComponent<Renderer>().material = green;
                myLock.material = green;
                break;
            case KeyColor.Gold:
                GetComponent<Renderer>().material = gold;
                myLock.material = gold;
                break;
        }
    }
    void Update()
    {
        if (iCanOpen && isLocked)
        {
            GameManager.Instance.SetUseInfo("Press E to open lock"); //<-----
        }
        if (Input.GetKeyDown(KeyCode.E) && iCanOpen == true && !isLocked)
        {
            key.SetBool("useKey", CheckKey());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            iCanOpen = true;
            Debug.Log("You can use this Lock");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            iCanOpen = false;
            Debug.Log("You can not use this Lock");
            GameManager.Instance.SetUseInfo("");
        }
    }

    public void UseKey()
    {
        foreach (var door in doors)
        {
            door.Open();
        }
    }

    public bool CheckKey()
    {
        if (GameManager.Instance.redKey > 0 && myColor == KeyColor.Red)
        {
            GameManager.Instance.redKey--;
            GameManager.Instance.redKeyText.text = GameManager.Instance.redKey.ToString();
            isLocked = true;
            return true;
        }
        else if(GameManager.Instance.greenKey > 0 && myColor == KeyColor.Green)
        {
            GameManager.Instance.greenKey--;
            isLocked = true;
            return true;
        }
        else if(GameManager.Instance.goldKey > 0 && myColor == KeyColor.Gold)
        {
            GameManager.Instance.goldKey--;
            isLocked = true;
            return true;
        }
        else
        {
            Debug.Log("You dont have right key!");
            return false;
        }
    }
}
