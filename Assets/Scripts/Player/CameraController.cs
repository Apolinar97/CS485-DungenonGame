using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public float cameraMoveSpeed = 120.0f;
    public GameObject cameraFollow;
    public GameObject cameraObject;
    public GameObject playerObject;


    private Vector3 followPostion;
    public float clampAngle = 80.0f;
    public float inputSensitivity = 150.0f;
    public float camxToPlayer;
    public float camyToPlayer;
    public float camzToPlayer;
    public float mouseX;
    public float mouseY;
    public float finalInputx;
    public float finalInputz;
    public float smoothx;
    public float smoothy;
    private float roty = 0.0f;
    private float rotx = 0.0f;


    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Start is called before the first frame update
    void Start()
    {
        Vector3 rot = transform.localRotation.eulerAngles;
        roty = rot.y;
        rotx = rot.x;

    }

    // Update is called once per frame
    void Update()
    {
        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");

        finalInputx = mouseX;
        finalInputz = -mouseY;

        roty += finalInputx * inputSensitivity * Time.deltaTime;
        rotx += finalInputz * inputSensitivity * Time.deltaTime;

        rotx = Mathf.Clamp(rotx, -clampAngle, clampAngle - 20.0f);

        Quaternion localRotation = Quaternion.Euler(rotx, roty, 0.0f);
        Quaternion playerRotation = Quaternion.Euler(0.0f, roty, 0.0f);
        transform.rotation = localRotation;
        playerObject.transform.rotation = playerRotation;

    }

    void LateUpdate()
    {
        CameraUpdate();
    }

    void CameraUpdate()
    {
        Transform target = cameraFollow.transform;

        float step = cameraMoveSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target.position + new Vector3(0, 0, 0), step);
    }

}
