using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitController : MonoBehaviour
{

    [SerializeField] private float moveSpeed = 0.5f;

	[SerializeField] private GameObject enemy;
	[SerializeField] private GameObject bulletPrefab;

    Vector3 direction;
    Vector3 origin;

	bool isMoving = false;
	float attackcooldownTimer;

    Animator animator;

    Coroutine dashRoutine;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
		if (!isMoving)
		{
			if (attackcooldownTimer <= 0)
			{
				Debug.Log("AttackNow");
				GameObject obj = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
				obj.GetComponent<Bullet>().SetTarget(enemy.transform);

				attackcooldownTimer = 0.5f;
			}

			attackcooldownTimer -= Time.deltaTime;
		}

        CheckForMouseInput();
    }

    void CheckForMouseInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            direction = Vector3.zero;
            dashRoutine = null;
            origin = Input.mousePosition;

            animator.SetBool("isRunning", true);
        }

        if (Input.GetMouseButtonUp(0))
        {
            direction = origin - Input.mousePosition;
            direction.z = direction.y;
            direction.y = 0f;
            direction.Normalize();
            direction *= moveSpeed;

            Vector3 targetPosition = transform.position - direction;

            if (dashRoutine == null)
            {
                dashRoutine = StartCoroutine(DashRoutine(targetPosition, 0.1f));
            }

            animator.SetBool("isRunning", false);
        }
    }

    IEnumerator DashRoutine(Vector3 targetPos, float time)
    {
		isMoving = true;
        float elapsedTime = 0f;
        Vector3 startPos = transform.position;
        Quaternion startRot = transform.rotation;

        while (elapsedTime < time)
        {
            transform.position = Vector3.Lerp(startPos, targetPos, (elapsedTime / time));
            transform.rotation = Quaternion.Slerp(startRot, Quaternion.LookRotation(-direction, Vector3.up), (elapsedTime / time));
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

		attackcooldownTimer = 1f;
		isMoving = false;
    }
}
