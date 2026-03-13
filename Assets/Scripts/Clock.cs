using UnityEngine;

public class Clock : PickUp
{
    public bool addTime;
    public uint time = 5;

    public override void Picked()
    {
        int sign;
        if (addTime)
        {
            sign = 1;
        }
        else
        {
            sign = -1;
        }
        GameManager.Instance.AddTime((int)time*sign);
        base.Picked();
    }
    void Update()
    {
        Rotation();
    }
}
