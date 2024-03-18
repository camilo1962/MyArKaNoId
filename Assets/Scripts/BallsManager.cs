using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BallsManager : MonoBehaviour
{
    #region Singleton

    private static BallsManager _instance;
    public static BallsManager Instance => _instance;
    private void Awake()
    {
        if (_instance != null)
            Destroy(gameObject);
        else
            _instance = this;
    }
    #endregion

    [SerializeField]
    private Ball ballPrefab;
    private Ball initBall;
    private Rigidbody2D initBallRb;
    public float initBallSpeed;
    public int maxBalls;
    public List<Ball> balls { get; set; }

    private void Start()
    {
        InitBall();
    }

    private void Update()
    {
        if(!GameManager.Instance.isPlayingNow)
        {
            Vector3 paddlePos = Paddle.Instance.gameObject.transform.position;
            Vector3 ballPos = new Vector3(paddlePos.x, paddlePos.y + 0.35f, 0);
            initBall.transform.position = ballPos;

            if(Input.GetMouseButton(0))
            {
                GameManager.Instance.isPlayingNow = true;
                initBallRb.isKinematic = false;
                initBallRb.AddForce(new Vector2(0, initBallSpeed));
            }
        }
    }

    public void SpawnBalls(Transform transform, int count, bool isSuperBall)
    {
        for(int i = 0; i < count; i++)
        {
            if (balls.Count >= maxBalls) { return; }

            Ball newBall = Instantiate(ballPrefab, transform.position, Quaternion.identity);
            if(isSuperBall)
            {
                newBall.StartSuperBall();
            }
            Rigidbody2D newBallRb = newBall.GetComponent<Rigidbody2D>();
            newBallRb.isKinematic = false;
            newBallRb.AddForce(new Vector2(UnityEngine.Random.Range(-1000, 1000), initBallSpeed));
            balls.Add(newBall);
        }
    }
    private void InitBall()
    {
        Vector3 paddlePos = Paddle.Instance.gameObject.transform.position;
        Vector3 startPos = new Vector3(paddlePos.x, paddlePos.y + 0.35f, 0);
        initBall = Instantiate(ballPrefab, startPos, Quaternion.identity);
        initBall.name = "Default Ball";
        initBallRb = initBall.GetComponent<Rigidbody2D>();
        balls = new List<Ball> { initBall };
    }

    public void ResetBalls()
    {
        foreach(var ball in balls.ToList())
        {
            Destroy(ball.gameObject);
        }

        InitBall();
    }
}
