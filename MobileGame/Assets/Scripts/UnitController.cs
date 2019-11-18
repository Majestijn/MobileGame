using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitController : MonoBehaviour
{

    [SerializeField] private float moveSpeed = 0.5f;

    Vector3 direction;
    Vector3 origin;

    [SerializeField] private GameObject walkingPrefab;
    [SerializeField] private GameObject canvas;

    GameObject joystickObject;

    Animator animator;

    Coroutine dashRoutine;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        CheckForMouseInput();
    }

    void FixedUpdate()
    {
        //if (direction.magnitude > 0)
        //{
        //    transform.position = transform.position - direction * moveSpeed * Time.fixedDeltaTime;
        //    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(-direction, Vector3.up), Time.fixedDeltaTime * 10f);
        //}
    }

    void CheckForMouseInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            direction = Vector3.zero;
            dashRoutine = null;
            origin = Input.mousePosition;

            GameObject obj = Instantiate(walkingPrefab, origin, Quaternion.identity, canvas.GetComponent<RectTransform>());
            joystickObject = obj;

            animator.SetBool("isRunning", true);
        }

        //if (Input.GetMouseButton(0))
        //{
        //    direction = origin - Input.mousePosition;
        //    direction.z = direction.y;
        //    direction.y = 0f;
        //    direction.Normalize();
        //}

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
            Destroy(joystickObject);
        }
    }

    IEnumerator DashRoutine(Vector3 targetPos, float time)
    {
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
    }
}
