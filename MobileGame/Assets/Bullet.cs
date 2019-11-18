using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	[SerializeField] private float bulletSpeed = 10f;

    void Update()
    {
        transform.Translate(transform.forward * bulletSpeed * Time.deltaTime);
    }

	public void SetTarget(Transform target)
	{
		transform.LookAt(target);
	}

	private void OnTriggerEnter(Collider collision)
	{
		if (collision.transform.CompareTag("Enemy"))
		{
			collision.transform.GetComponent<Health>().DeductHealth(5);
			Destroy(gameObject);
		}
	}
}
