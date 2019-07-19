using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// An asteroid
/// </summary>
public class Asteroid : MonoBehaviour
{
    [SerializeField]
    Sprite asteroidSprite0;
    [SerializeField]
    Sprite asteroidSprite1;
    [SerializeField]
    Sprite asteroidSprite2;
    public AudioClip explotion;
    /// <summary>
    /// Use this for initialization
    /// </summary>
    void Start()
    {
        // set random sprite for asteroid
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        int spriteNumber = Random.Range(0, 3);
        if (spriteNumber < 1)
        {
            spriteRenderer.sprite = asteroidSprite0;
        }
        else if (spriteNumber < 2)
        {
            spriteRenderer.sprite = asteroidSprite1;
        }
        else
        {
            spriteRenderer.sprite = asteroidSprite2;
        }
    }

    /// <summary>
    /// Starts the asteroid moving in the given direction
    /// </summary>
    /// <param name="direction">direction for the asteroid to move</param>
    /// <param name="position">position for the asteroid</param>
    public void Initialize(Direction direction, Vector3 position)
    {
        // set asteroid position
        transform.position = position;

        // set random angle based on direction
        float angle;
        float randomAngle = Random.value * 30f * Mathf.Deg2Rad;
        if (direction == Direction.Up)
        {
            angle = 75 * Mathf.Deg2Rad + randomAngle;
        }
        else if (direction == Direction.Left)
        {
            angle = 165 * Mathf.Deg2Rad + randomAngle;
        }
        else if (direction == Direction.Down)
        {
            angle = 255 * Mathf.Deg2Rad + randomAngle;
        }
        else
        {
            angle = -15 * Mathf.Deg2Rad + randomAngle;
        }

        // apply impulse force to get asteroid moving
        const float MinImpulseForce = 1f;
        const float MaxImpulseForce = 3f;
        Vector2 moveDirection = new Vector2(
            Mathf.Cos(angle), Mathf.Sin(angle));
        float magnitude = Random.Range(MinImpulseForce, MaxImpulseForce);
        GetComponent<Rigidbody2D>().AddForce(
            moveDirection * magnitude,
            ForceMode2D.Impulse);
    }


    void OnCollisionEnter2D(Collision2D col)

    {

        if (col.gameObject.tag == "Bullet")
        {
            if (transform.localScale.x > 0.5)
            {
                //Changing the scale of the asteroid, so they are small
                Vector3 newScale = new Vector3(0.5f, 0.5f, 0.5f);
                transform.localScale -= newScale;

                //destroying asteroid and creating 2 small one
                Destroy(gameObject);

                GameObject minAsteroid = Instantiate(gameObject, transform.position, Quaternion.identity);
                GameObject minAsteroid1 = Instantiate(gameObject, transform.position, Quaternion.identity);

                //Activate components
                minAsteroid.GetComponent<CircleCollider2D>().enabled = true;
                minAsteroid.GetComponent<ScreenWrapper>().enabled = true;
                minAsteroid.GetComponent<Asteroid>().enabled = true;

                minAsteroid1.GetComponent<CircleCollider2D>().enabled = true;
                minAsteroid1.GetComponent<ScreenWrapper>().enabled = true;
                minAsteroid1.GetComponent<Asteroid>().enabled = true;

                //start moving the new object
                float angle = Random.Range(0f, 2f) * Mathf.PI;
                float angle1 = Random.Range(0f, 2f) * Mathf.PI;

                minAsteroid.GetComponent<Asteroid>().StartMoving(angle);
                minAsteroid1.GetComponent<Asteroid>().StartMoving(angle1);

                Destroy(GameObject.FindGameObjectWithTag("Bullet"));
                AudioSource.PlayClipAtPoint(explotion, transform.position);
            }
            else
            {
                Destroy(gameObject);
                Destroy(GameObject.FindGameObjectWithTag("Bullet"));
                AudioSource.PlayClipAtPoint(explotion, transform.position);
            }
        }
    }

    void StartMoving(float angle) {
        const float MinImpulseForce = 1f;
        const float MaxImpulseForce = 3f;
        Vector2 moveDirection = new Vector2(
            Mathf.Cos(angle), Mathf.Sin(angle));
        float magnitude = Random.Range(MinImpulseForce, MaxImpulseForce);
        GetComponent<Rigidbody2D>().AddForce(
            moveDirection * magnitude,
            ForceMode2D.Impulse);
    }
}