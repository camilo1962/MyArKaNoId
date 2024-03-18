using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    #region Singleton

    private static Paddle _instance;
    public static Paddle Instance => _instance;
    private void Awake()
    {
        if (_instance != null)
            Destroy(gameObject);
        else
            _instance = this;
    }

    #endregion

    private Camera mainCamera;
    private SpriteRenderer sr;
    private BoxCollider2D boxCollider;
    private float paddlePosY;
    private float mousePosPixels;
    private float mousePosX;
    private float defaultPaddleWithInPixels = 192;
    private float defaultClamp = 128;
    public bool isTransforming { get; set; }
    private int extendDuration = 10;
    private float paddleWidth = 2f;

    void Start()
    {
        mainCamera = FindObjectOfType<Camera>();
        sr = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
        paddlePosY = transform.position.y;
        isTransforming = false;
    }

    void Update()
    {
        if (!mainCamera || !sr) return;

        MovePaddle();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag =="Ball")
        {
            Rigidbody2D ballRb = other.gameObject.GetComponent<Rigidbody2D>();
            Vector3 hit = other.contacts[0].point;
            Vector3 paddleCenter = new Vector3(transform.position.x, transform.position.y);

            ballRb.velocity = Vector2.zero;

            float difference = paddleCenter.x - hit.x;

            if (hit.x < paddleCenter.x)
                ballRb.AddForce(new Vector2(-(Mathf.Abs(difference * 100)), BallsManager.Instance.initBallSpeed));
            else
                ballRb.AddForce(new Vector2((Mathf.Abs(difference * 100)), BallsManager.Instance.initBallSpeed));
        }
    }

    private void MovePaddle()
    {
        float paddleShift = (defaultPaddleWithInPixels - ((defaultPaddleWithInPixels / 2) * sr.size.x)) / 2f;
        float leftClamp = defaultClamp - paddleShift;
        float rightClamp = 540 - defaultClamp + paddleShift;
        mousePosPixels = Mathf.Clamp(Input.mousePosition.x, leftClamp, rightClamp);
        mousePosX = mainCamera.ScreenToWorldPoint(new Vector3(mousePosPixels, 0, 0)).x;
        transform.position = new Vector3(mousePosX, paddlePosY, 0);
    }

    public void StartChangeWidthAnim(float newWidth)
    {
        if(newWidth != sr.size.x)
            StartCoroutine(AnimateWidthAnim(newWidth));
    }

    private IEnumerator AnimateWidthAnim(float newWidth)
    {
        isTransforming = true;
        StartCoroutine(ResetPaddleWidth(extendDuration));

        if(newWidth > sr.size.x)
        {
            float currentWidth = this.sr.size.x;
            while (currentWidth < newWidth)
            {
                currentWidth += Time.deltaTime * 2;
                sr.size = new Vector2(currentWidth, sr.size.y);
                boxCollider.size = new Vector2(currentWidth, boxCollider.size.y);
                yield return null;
            }
        }
        else
        {
            float currentWidth = this.sr.size.x;
            while (currentWidth > newWidth)
            {
                currentWidth -= Time.deltaTime * 2;
                sr.size = new Vector2(currentWidth, sr.size.y);
                boxCollider.size = new Vector2(currentWidth, boxCollider.size.y);
                yield return null;
            }
        }
        sr.size = new Vector2(newWidth, sr.size.y);
        boxCollider.size = new Vector2(newWidth, boxCollider.size.y);

        isTransforming = false;
    }

    private IEnumerator ResetPaddleWidth(float buffTime)
    {
        yield return new WaitForSeconds(buffTime);
        StartChangeWidthAnim(paddleWidth);
    }
}
