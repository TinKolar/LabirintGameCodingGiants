using UnityEngine;

public class Clock : PickUp
{
    public bool addTime;
    public uint time;
    void Update()
    {
        Rotation();
    }

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
        //int sign=addTime ? 1 : -1;
        GameManager.Instance.AddTime((int)time * sign);
        base.Picked();
    }
}
