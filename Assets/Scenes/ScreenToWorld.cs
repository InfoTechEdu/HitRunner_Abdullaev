using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenToWorld : MonoBehaviour
{
    public Transform fireTransform;

    // Start is called before the first frame update
    void Start()
    {
        
    }


    public float distance = 20;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.LogWarning($"Input.mouse position - {Input.mousePosition}");
            Vector3 worldPoint = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance));
            GameObject go = GameObject.CreatePrimitive(PrimitiveType.Sphere); go.transform.position = worldPoint;
            fireTransform.LookAt(go.transform.position);
            fireTransform.eulerAngles = new Vector3(fireTransform.rotation.eulerAngles.x, fireTransform.rotation.eulerAngles.y, 0);
            return;

            Debug.LogWarning($"World point - {worldPoint}");
            Vector2 touchPos = new Vector2(worldPoint.x, worldPoint.y);
            Debug.LogWarning($"touch pos- {touchPos}");
            fireTransform.LookAt(touchPos);

        }
    }
}
