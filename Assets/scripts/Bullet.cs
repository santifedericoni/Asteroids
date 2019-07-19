using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    Rigidbody2D rb2D;

    //The force applied to the bullet
    const float lifeTime = 2;
    const float forceMag = 6f;
    Timer DeathTimer;
    // Start is called before the first frame update
    void Start()

    {
        DeathTimer = gameObject.AddComponent<Timer>();
        DeathTimer.Duration = lifeTime;
        DeathTimer.Run();
    }

    void Update()

    {
        if (DeathTimer.Finished)
            Destroy(gameObject);
    }


    public void ApplyForce(Vector2 direction)
    {
        GetComponent<Rigidbody2D>().AddForce(direction * forceMag, ForceMode2D.Impulse);
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("Asteroid"))
        {
            Destroy(gameObject);
        }
    }
}