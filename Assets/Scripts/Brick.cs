using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    public int hitsToDestroy;
    private string color;
    private SpriteRenderer sr;

    public static event Action<Brick> onBrickDestroy;

    private void Start()
    {
        if (!sr) sr = GetComponent<SpriteRenderer>();

        color = BricksManager.Instance.GenerateColor();
        sr.sprite = BricksManager.Instance.GetSprite(color, hitsToDestroy);
        sr.sortingLayerName = "Brick";
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Ball")
        {
            bool isSuperBall = other.gameObject.GetComponent<Ball>().isSuperBall;
            HitBrick(isSuperBall);
        }
    }

    private void HitBrick(bool isSuperBall)
    {
        hitsToDestroy--;
        AudioManager.Instance.BreakBrick();

        if (hitsToDestroy <= 0 || isSuperBall)
        {
            BuffsManager.Instance.GenerateBuff(this.transform);
            BricksManager.Instance.remainingBricks.Remove(this);
            onBrickDestroy?.Invoke(this);
            Destroy(gameObject);
        }
        else
            sr.sprite = BricksManager.Instance.GetSprite(color, hitsToDestroy);
    }
}
