using Cinemachine;
using System;
using UnityEngine;
using UnityEngine.UI;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform orientation;
    [NonSerialized] public Transform player;

    public CinemachineFreeLook basicCamera;
    public CinemachineVirtualCamera aimedCamera;
    public Image crosshair;

    public float rotationSpeed;

    private void Start()
    {
        player = FindAnyObjectByType<Player>().transform;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        SetActiveCamera(player.GetComponent<Player>().currentWeaponType);
    }

    private void Update()
    {
        Vector3 viewDir = player.position - new Vector3(transform.position.x, player.position.y, transform.position.z);
        orientation.forward = viewDir.normalized;

        SetActiveCamera(player.GetComponent<Player>().currentWeaponType);
    }

    private void SetActiveCamera(WeaponType weaponType)
    {
        if (weaponType == WeaponType.Basic)
        {
            basicCamera.Priority = 10;
            aimedCamera.Priority = 5;

            crosshair.gameObject.SetActive(false);
        }
        else if (weaponType == WeaponType.Aimed)
        {
            aimedCamera.Priority = 10;
            basicCamera.Priority = 5;

            crosshair.gameObject.SetActive(true);
        }
    }
}
