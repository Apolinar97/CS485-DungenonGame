using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{

    public Transform target;
    private Vector3 offset;

    private float rotationSpeed = 1f;
    private float minY = -60f;
    private float maxY = 60f;
    private float rotationY = 0f;
    private float rotationX = 2f;
    private float zoomInOutSpeed = 20f;
    [SerializeField] private float lookSensitivity;
    [SerializeField] private float smoothing;

    private Vector2 smoothedVelocity;
    private Vector2 currentLookingPosition;
    private GameObject character;

    // Use this for initialization
    void Start()
    {
        character = transform.parent.gameObject;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        //offset = transform.position - target.position;
    }

    // Update is called once per frame
    void Update()
    {
        /* Camera follows the target */
        //transform.LookAt(target, Vector3.up);
        transform.position = target.position + offset;

        /* Camera rotation */
        Vector2 inputValues = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        inputValues = Vector2.Scale(inputValues, new Vector2(lookSensitivity * smoothing, lookSensitivity * smoothing));
        smoothedVelocity.x = Mathf.Lerp(smoothedVelocity.x, inputValues.x, 1f / smoothing);
        smoothedVelocity.y = Mathf.Lerp(smoothedVelocity.y, inputValues.y, 1f / smoothing);

        currentLookingPosition += smoothedVelocity;
        transform.localRotation = Quaternion.AngleAxis(-currentLookingPosition.y, Vector3.right);

        character.transform.localRotation = Quaternion.AngleAxis(currentLookingPosition.x, character.transform.up);

        /* Camera zoom in & zoom out */
        float scrollWheel = Input.GetAxis("Mouse ScrollWheel");
        if (scrollWheel != 0)
        {
            transform.position += transform.forward * scrollWheel * zoomInOutSpeed * Time.deltaTime;
            offset = transform.position - target.position;
        }
    }
}