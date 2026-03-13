using UnityEngine;

public class Key : PickUp
{
    public KeyColor color;

    public override void Picked()
    {
        GameManager.Instance.AddKey(color);
        base.Picked();
    }

    void Update()
    {
        Rotation();
    }
}

public enum KeyColor
{
    Red,
    Green,
    Gold
}
