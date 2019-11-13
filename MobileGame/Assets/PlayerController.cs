using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    Vector3 direction;
    Vector3 origin;
    Rigidbody rigidBody;

    [SerializeField] private GameObject walkingPrefab;
    [SerializeField] private GameObject canvas;

    GameObject joystickObject;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        CheckForMouseInput();
    }

    void CheckForMouseInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            origin = Input.mousePosition;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                //origin = hit.point;
                if (canvas.transform.childCount == 0)
                {
                    Vector3 screenPosition = Camera.main.WorldToScreenPoint(hit.point);
                    GameObject obj = Instantiate(walkingPrefab, screenPosition, Quaternion.identity, canvas.GetComponent<RectTransform>());
                    joystickObject = obj;
                }

            }
        }

        if (Input.GetMouseButton(0))
        {
            direction = origin - Input.mousePosition;
            direction.z = direction.y;
            direction.y = 0f;
            direction.Normalize();

            rigidBody.MovePosition(transform.position - direction * 3f * Time.deltaTime);
        }

        if (Input.GetMouseButtonUp(0))
        {
            Destroy(joystickObject);
        }
    }
}
