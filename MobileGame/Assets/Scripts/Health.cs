using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    private short health = 100;

    void Update()
    {
        if (health <= 0)
            Die();
    }

    public void DeductHealth(short amount)
    {
        health -= amount;

        if (health <= 0)
            Die();
    }

    public void SetHealth(short amount)
    {
        health = amount;
    }

    public void Die()
    {
        if (GetComponent<Enemy>() != null)
            GetComponent<Enemy>().InvokeOnDiedEvent();
        Destroy(gameObject);
    }
}
