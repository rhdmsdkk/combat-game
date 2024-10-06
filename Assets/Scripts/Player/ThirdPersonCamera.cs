using Cinemachine;
using UnityEngine;
using UnityEngine.UI;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform basicOrientation;
    public Transform aimedOrientation;
    public Transform player;

    public CinemachineFreeLook basicCamera;
    public CinemachineVirtualCamera aimedCamera;
    public Image crosshair;

    public float rotationSpeed;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        SetActiveCamera(player.GetComponent<Player>().currentWeaponType);
    }

    private void Update()
    {
        Vector3 viewDir = player.position - new Vector3(transform.position.x, player.position.y, transform.position.z);
        basicOrientation.forward = viewDir.normalized;

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
