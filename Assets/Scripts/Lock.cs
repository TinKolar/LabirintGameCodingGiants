using UnityEngine;

public class Lock : MonoBehaviour
{

    public Door[] doors;
    public KeyColor myColor;
    bool isLocked=false;
    public Key Animator;
    

    bool iCanOpen=false;

    void Start()
    {
        
    }

    void Update()
    {
        
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
}
