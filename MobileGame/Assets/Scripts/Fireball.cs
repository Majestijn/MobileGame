using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    Rigidbody2D rigidBody;
    [SerializeField] private float force;
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        //rigidBody.AddForce(transform.up * force, ForceMode2D.Impulse);
        rigidBody.AddRelativeForce(transform.up * force, ForceMode2D.Impulse);
    }

    void Update()
    {
        transform.rotation = Quaternion.Euler(transform.rotation.x, 0, transform.rotation.z);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.transform.CompareTag("Enemy"))
        {
            Health health = col.transform.GetComponent<Health>();

            if (health)
            {
                health.DeductHealth(20);
            }
        }

        Destroy(gameObject);
    }
}
