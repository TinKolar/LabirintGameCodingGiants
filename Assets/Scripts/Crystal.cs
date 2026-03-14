using UnityEngine;

public class Crystal : PickUp
{
    public int points = 5;

    public override void Picked()
    {
        GameManager.Instance.AddPoints(points);
        base.Picked();
    }

    void Update()
    {
        Rotation();
    }
}
