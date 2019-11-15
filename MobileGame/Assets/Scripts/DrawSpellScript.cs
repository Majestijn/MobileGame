using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawSpellScript : MonoBehaviour
{
    public Vector3 startPos;
    public Vector3 endPos;

    [SerializeField] private GameObject fireballPrefab;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        if (Input.GetMouseButtonUp(0))
        {
            endPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            endPos.z = 1f;

            Vector3 direction = GetDirectionVector(endPos, startPos, false);

            GameObject obj = Instantiate(fireballPrefab, endPos, Quaternion.identity);

            obj.transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);
        }
    }

    Vector3 GetDirectionVector(Vector3 vectorA, Vector3 vectorB, bool normalized = false)
    {
        return normalized ? (vectorA - vectorB).normalized : vectorA - vectorB;
    }
}