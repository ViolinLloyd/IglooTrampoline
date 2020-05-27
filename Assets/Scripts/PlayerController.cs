using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Sprite[] sprites;
    private int currentSprite = 0;
    private float secondsPerFrame = 0.25f;
    private float timer = 0.0f;

    private float speed = 3.0f;
    private Rigidbody2D rb;
    private Vector3 lastPos = new Vector3(-10, -10, -10);

    private Camera mainCam;
    [SerializeField]
    private GameObject crosshairs;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        mainCam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        Move();

        timer += Time.deltaTime;
        if(timer >= secondsPerFrame)
        {
            currentSprite = ++currentSprite % sprites.Length;
            timer = 0.0f;
            GetComponent<SpriteRenderer>().sprite = sprites[currentSprite];
        }
    }

    private void Move()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float velocityX = x * speed;
        float velocityY = rb.velocity.y;

        // If player is stuck, help them out with a little upwards motion
        if(velocityX != 0 && gameObject.transform.position == lastPos && transform.position.y <= 0.15f)
        {
            transform.position += new Vector3(0, 0.01f, 0);
        }

        rb.velocity = new Vector2(velocityX, velocityY);
        lastPos = gameObject.transform.position;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.gameObject.CompareTag("Ice"))
        {
            GameObject ice = collision.collider.gameObject;
            Vector3 targetPos = crosshairs.transform.position;
            Vector3 startPos = gameObject.transform.position;
            float Ex = targetPos.x - startPos.x;
            float Ey = Mathf.Max(targetPos.y - startPos.y, 0.01f); // Prevent NaN from negative sqrt or division by zero
            float g = -Physics2D.gravity.y;
            float Fx = ( ( Ex * Mathf.Sqrt(2 * g * Ey) ) + ( Mathf.Sqrt(2 * g * Ey * Ex * Ex - 4 * Ey * g * Ex * Ex / 2) ) ) / (2 * Ey); //Quadratic; physics is fun
            Fx = Mathf.Max( Mathf.Min(Fx, 15.0f), -15.0f); // Prevent launching the ice cubes
            float Fy = Mathf.Sqrt(2 * g * Ey);
            Vector2 force = new Vector2(Fx, Fy);
            ice.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            ice.GetComponent<Rigidbody2D>().AddForce(force, ForceMode2D.Impulse);
        }
    }
}
