using UnityEngine;

public class InspectSystem : MonoBehaviour
{
    public Transform objectToInspect;

    public float rotationSpeed = 100f;

    private Vector3 previousMousePosition; 

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            previousMousePosition = Input.mousePosition;
        }


        if (Input.GetMouseButton(0))
        {
            Vector3 deltaMousePostion = Input.mousePosition - previousMousePosition;
            float rotationX = deltaMousePostion.y * rotationSpeed * Time.deltaTime;
            float rotationY = -deltaMousePostion.x * rotationSpeed * Time.deltaTime;

            Quaternion rotation = Quaternion.Euler(rotationX, rotationY, 0);
            objectToInspect.rotation = rotation * objectToInspect.rotation;

            previousMousePosition = Input.mousePosition;
        }

    }
}
