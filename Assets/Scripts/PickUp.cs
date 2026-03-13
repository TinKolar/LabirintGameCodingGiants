using UnityEngine;

public class PickUp : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
  
    public virtual void Picked()
    {
        Debug.Log($"Picked up {this.gameObject.name}!");
        Destroy(this.gameObject);
    }
    public void Rotation()
    {
        transform.Rotate(new Vector3(0, 5f, 0));
    }
}
