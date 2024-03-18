
public class ExtendPaddle : Buff
{
    private float newWidth = 2.5f;
    protected override void ApplyBuffEffect()
    {
        if(Paddle.Instance != null && !Paddle.Instance.isTransforming)
        {
            Paddle.Instance.StartChangeWidthAnim(newWidth);
        }
    }
}
