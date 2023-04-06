using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Ball : MonoBehaviour
{

    public Rigidbody2D rb2D;
    public float speed;
    public Vector2 velocity;
    public bool gameStarted;
    public int leftPlayerScore = 0;
    public int rightPlayerScore = 0;
    public TextMeshProUGUI leftPlayerText;
    public TextMeshProUGUI rightPlayerText;

    // Start is called before the first frame update
    void Start()
    {
        gameStarted = true;
        rb2D = GetComponent<Rigidbody2D>();
      

    }
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space) && gameStarted)
        {
            ResetAndSetRandomVelocity();
            gameStarted = false;    
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        rb2D.velocity = Vector2.Reflect(velocity, collision.contacts[0].normal);
        velocity = rb2D.velocity;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(transform.position.x > 0)
        {
            leftPlayerScore += 1;
            leftPlayerText.text = leftPlayerScore.ToString();
            Debug.Log(leftPlayerScore + " : " + rightPlayerScore);
        }

        if(transform.position.x < 0)
        {
            
            rightPlayerScore += 1;
            rightPlayerText.text = rightPlayerScore.ToString();
            Debug.Log(leftPlayerScore + " : " + rightPlayerScore);
        }
        ResetBall();
        gameStarted = true;
    }

    private void ResetBall()
    {
        rb2D.velocity = Vector2.zero;
        transform.position = Vector2.zero;
    }
    

    private void ResetAndSetRandomVelocity()
    {
        ResetBall();
        rb2D.velocity = GenerateRandomVector2Without0(true) * speed;
        velocity = rb2D.velocity;
    }


    private Vector2 GenerateRandomVector2Without0(bool returnNormalized)
    {
        Vector2 newRandomVector = new Vector2();

        bool shouldXBeLessThanZero = Random.Range(0, 100) % 2 == 0;
        newRandomVector.x = shouldXBeLessThanZero ? Random.Range(-.8f, -.1f) : Random.Range(.1f,.8f);

        bool shouldYBeLessThanZero = Random.Range(0, 100) % 2 == 0;
        newRandomVector.y = shouldYBeLessThanZero ? Random.Range(-.8f, -.1f) : Random.Range(.1f,.8f);

        return returnNormalized ?  newRandomVector.normalized : newRandomVector;


    }

    
    
}
