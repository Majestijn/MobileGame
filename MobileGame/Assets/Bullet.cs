using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	[SerializeField] private float bulletSpeed = 30f;

    void Update()
    {
        transform.Translate(transform.forward * bulletSpeed * Time.deltaTime, Space.World);
    }

	public void SetTarget(Transform target)
	{
		Quaternion lookRotation = Quaternion.LookRotation(target.position - transform.position, Vector3.up);
        transform.rotation = lookRotation;
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
