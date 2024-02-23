using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Transform followCharacter;
    [SerializeField] float distance = 3.5f;
    [SerializeField] float rotationSpeed = 2f;
    [SerializeField] float minVerticalAngle = -10;
    [SerializeField] float maxVerticalAngle = 45;

    [SerializeField] Vector2 farmingOffset;

    [SerializeField] bool invertX;
    [SerializeField] bool invertY;

    [SerializeField] float invertXVal;
    [SerializeField] float invertYVal;

    float rotationX;
    float rotationY;
    void Start()
    {
        // Cursor.visible = false;
        // Cursor.lockState = CursorLockMode.Locked;

    }

    // Update is called once per frame
    void Update()
    {

        invertXVal = invertX ? -1 : 1;
        invertYVal = invertY ? -1 : 1;

        rotationX += Input.GetAxis("Mouse Y")*rotationSpeed*invertYVal;
        rotationX = Mathf.Clamp(rotationX, minVerticalAngle, maxVerticalAngle);

        rotationY += Input.GetAxis("Mouse X")*rotationSpeed*invertXVal;

        var targetRotation = Quaternion.Euler(rotationX,rotationY,0);
        var focusPosition = followCharacter.position + new Vector3(farmingOffset.x, farmingOffset.y);

        transform.position = focusPosition - targetRotation * new Vector3(0,0,distance);
        transform.rotation = targetRotation;
    } 

    public Quaternion PlannerRotation => Quaternion.Euler(0, rotationY, 0);
}
