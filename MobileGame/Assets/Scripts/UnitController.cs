using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private float moveSpeed = 5f;

    Vector3 direction;
    Vector3 origin;

    [SerializeField] private GameObject walkingPrefab;
    [SerializeField] private GameObject canvas;

    GameObject joystickObject;

    Animator animator;

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
        if (direction.magnitude > 0)
        {
            transform.position = transform.position - direction * moveSpeed * Time.fixedDeltaTime;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(-direction, Vector3.up), Time.fixedDeltaTime * 10f);
        }
    }

    void CheckForMouseInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            origin = Input.mousePosition;

            GameObject obj = Instantiate(walkingPrefab, origin, Quaternion.identity, canvas.GetComponent<RectTransform>());
            joystickObject = obj;

            animator.SetBool("isRunning", true);
        }

        if (Input.GetMouseButton(0))
        {
            direction = origin - Input.mousePosition;
            direction.z = direction.y;
            direction.y = 0f;
            direction.Normalize();
        }

        if (Input.GetMouseButtonUp(0))
        {
            animator.SetBool("isRunning", false);
            direction = Vector3.zero;
            Destroy(joystickObject);
        }
    }
}
