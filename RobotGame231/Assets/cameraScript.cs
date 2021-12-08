using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraScript : MonoBehaviour
{
    public Transform target;
    public float targetHeight = 2.0f;
    public float distance = 2.8f;
    public float maxDistance = 10;
    public float minDistance = 0.5f;
    float xSpeed = 250.0f;
    float ySpeed = 120.0f;
    float yMinLimit = -40.0f;
    float yMaxLimit = 80.0f;
    public int zoomRate = 20;
    float rotationDampening = 3.0f;
    float x = 0.0f;
    float y = 0.0f;
    bool isTalking = false;

    // Start is called before the first frame update
    void Start()
    {
        var angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;

        //Makes the rigidBody not change rotation
        if (GetComponent<Rigidbody>())
        {
            GetComponent<Rigidbody>().freezeRotation = true;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void LateUpdate()
    {
        if (!target)
        {
            return;
        }
        if (Input.GetMouseButton(0) || (Input.GetMouseButton(1)))
        {
            x += Input.GetAxis("Mouse X") * ySpeed * 0.02f;
            y -= Input.GetAxis("Mouse Y") * xSpeed * 0.02f;

        }
        else if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") !=0)
        {
            var targetRotationAngle = target.eulerAngles.y;
            var currentRotationAngle = transform.eulerAngles.y;
            x = Mathf.LerpAngle(currentRotationAngle, targetRotationAngle, rotationDampening * Time.deltaTime);

        }

        distance -= (Input.GetAxis("Mouse ScrollWheel") * Time.deltaTime) * zoomRate * Mathf.Abs(distance);
        distance = Mathf.Clamp(distance, minDistance, maxDistance);
        y = ClampAngle(y, yMinLimit, yMaxLimit);
        //Rotate Camera
        Quaternion rotation = Quaternion.Euler(x, y, 0);
        transform.rotation = rotation;

        //Position Camera
        var position = target.position - (rotation * Vector3.forward * distance + new Vector3(0, -targetHeight, 0));
        transform.position = position;

      /*  //Is View Blocked?
        RaycastHit hit;
        Vector3 trueTargetPosition = target.transform.position - new Vector3(0, -targetHeight, 0);
        // Cast Line to Check
        if (Physics.Linecast(trueTargetPosition, transform.position, hit))
        {
            var tempDist = Vector3.Distance(trueTargetPosition, hit.point) -0.28
        }*/
    }

    static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360)
        {
            angle += 360;

        }
        if(angle > 360)
        {
            angle -= 360;
         }
        return Mathf.Clamp(angle, min, max);
    }
}

