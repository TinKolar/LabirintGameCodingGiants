using UnityEngine;

public class Lock : MonoBehaviour
{

    public Door[] doors;
    public KeyColor myColor;
    bool isLocked=false;
    public Animator key;
    

    bool iCanOpen=false;

    void Start()
    {
        key = GetComponent<Animator>();
    }

    void Update()
    {
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
