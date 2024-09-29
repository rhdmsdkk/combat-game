using System;
using UnityEngine;
public enum CameraStyle { Basic, Aim }

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform orientation;
    public Transform player;
    public Transform playerObject;
    public Transform aimLookAt;
    public Rigidbody rb;

    public Transform basicCamera;
    public Transform aimCamera;

    public float rotationSpeed;

    [NonSerialized] public CameraStyle style;   

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        Vector3 viewDir = player.position - new Vector3(transform.position.x, player.position.y, transform.position.z);
        orientation.forward = viewDir.normalized;

        // rotate
        if (style == CameraStyle.Basic)
        {
            basicCamera.gameObject.SetActive(true);
            aimCamera.gameObject.SetActive(false);

            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");
            Vector3 inputDir = orientation.forward * verticalInput + orientation.right * horizontalInput;

            if (inputDir != Vector3.zero)
            {
                playerObject.forward = Vector3.Slerp(playerObject.forward, inputDir.normalized, Time.deltaTime * rotationSpeed);
            }
        }
        else if (style == CameraStyle.Aim)
        {
            aimCamera.gameObject.SetActive(true);
            basicCamera.gameObject.SetActive(false);

            Vector3 dirToAimLookAt = aimLookAt.position - new Vector3(transform.position.x, aimLookAt.position.y, transform.position.z);
            orientation.forward = dirToAimLookAt.normalized;

            playerObject.forward = dirToAimLookAt.normalized;
        }
    }
}
