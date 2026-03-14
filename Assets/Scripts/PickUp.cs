using UnityEngine;

public class PickUp : MonoBehaviour
{
    
    void Update()
    {
        Rotation();
    }

    public virtual void Picked()
    {
        Debug.Log($"Picked up : {this.gameObject.name}!");
        Destroy(this.gameObject);
    }

    public void Rotation()
    {
        transform.Rotate(new Vector3(0f,5f,0f));
    }
}
