using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitController : MonoBehaviour
{

    [SerializeField] private float moveSpeed = 0.5f;

    private Enemy currentTarget;

	[SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;

    Vector3 direction;
    Vector3 origin;

	bool isMoving = false;
	float attackcooldownTimer;

    Animator animator;

    Coroutine dashRoutine;

    void Start()
    {
        EnemySpawner.instance.OnEnemyDied += CheckForNewEnemy;
        animator = GetComponent<Animator>();
        currentTarget = EnemySpawner.instance.GetClosestEnemy(transform);
    }

    void Update()
    {
		if (!isMoving)
		{
			if (attackcooldownTimer <= 0 && currentTarget != null)
			{
				GameObject obj = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
				obj.GetComponent<Bullet>().SetTarget(currentTarget.transform);
                transform.rotation = Quaternion.LookRotation(currentTarget.transform.position - transform.position, Vector3.up);

                attackcooldownTimer = 0.5f;
			}

			attackcooldownTimer -= Time.deltaTime;
		}

        CheckForMouseInput();
    }

    public void CheckForNewEnemy(Enemy enemy)
    {
        if (enemy == currentTarget)
        {
            currentTarget = EnemySpawner.instance.GetClosestEnemy(transform);
        }
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
            currentTarget =  EnemySpawner.instance.GetClosestEnemy(transform);

            direction = origin - Input.mousePosition;
            direction.z = direction.y;
            direction.y = 0f;
            direction.Normalize();
            direction *= moveSpeed;

            transform.position -= direction;
            transform.rotation = Quaternion.LookRotation(-direction, Vector3.up);

            //Vector3 targetPosition = transform.position - direction;

            //if (dashRoutine == null)
            //{
            //    dashRoutine = StartCoroutine(DashRoutine(targetPosition, 0.1f));
            //}

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
