using System.Linq;

public class MultiBall : Buff
{
    protected override void ApplyBuffEffect()
    {
        if(BallsManager.Instance.balls.Count >= BallsManager.Instance.maxBalls) { return; }
        foreach(Ball ball in BallsManager.Instance.balls.ToList())
        {
            BallsManager.Instance.SpawnBalls(ball.gameObject.transform, 3, ball.isSuperBall);
        }
    }
}
