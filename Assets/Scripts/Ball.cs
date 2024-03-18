using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public static event Action<Ball> onBallDestroy;
    public bool isSuperBall;
    [SerializeField]
    private SpriteRenderer sr;
    [SerializeField]
    private ParticleSystem particle;
    private float superBallDuration = 10;

    public void DestroyBall()
    {
        onBallDestroy?.Invoke(this);
        Destroy(this.gameObject);
    }

    public void StartSuperBall()
    {
        if(!isSuperBall)
        {
            isSuperBall = true;

            sr.enabled = false;
            particle.gameObject.SetActive(true);
            StartCoroutine(StopSuperBall(superBallDuration));
        }
    }

    private void StopSuperBall()
    {
        if (isSuperBall)
        {
            isSuperBall = false;

            sr.enabled = true;
            particle.gameObject.SetActive(false);
        }
    }

    IEnumerator StopSuperBall(float duration)
    {
        yield return new WaitForSeconds(duration);
        StopSuperBall();
    }
}
