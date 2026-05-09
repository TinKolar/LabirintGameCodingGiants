using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Lock : MonoBehaviour
{

    public Door[] doors;
    public KeyColor myColor;
    bool locked = false;
    Animator key;

    bool iCanOpen = false;

    private void Start()
    {
        key = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && iCanOpen && !locked)
        {
            key.SetBool("useKey", CheckTheKey());

        }
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            iCanOpen = true;
            Debug.Log("You Can Use Lock");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            iCanOpen = false;
            Debug.Log("You Can not Use Lock");
        }
    }

    public void UseKey()
    {
        foreach (Door door in doors)
        {
            door.Open();
        }
    }

    public bool CheckTheKey()
    {
        if (GameManager.Instance.redKey > 0 && myColor == KeyColor.Red)
        {
            GameManager.Instance.redKey--;
            locked = true;
            return true;
        }
        else if (GameManager.Instance.greenKey > 0 && myColor == KeyColor.Green)
        {
            GameManager.Instance.greenKey--;
            locked = true;
            return true;
        }
        else if (GameManager.Instance.goldKey > 0 && myColor == KeyColor.Gold)
        {
            GameManager.Instance.goldKey--;
            locked = true;
            return true;
        }
        else
        {
            Debug.Log("Nie masz klucza!");
            return false;
        }
    }


}
