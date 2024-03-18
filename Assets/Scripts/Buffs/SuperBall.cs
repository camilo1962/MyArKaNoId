using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperBall : Buff
{
    protected override void ApplyBuffEffect()
    {
        foreach(Ball ball in BallsManager.Instance.balls)
        {
            ball.StartSuperBall();
        }
    }
}
