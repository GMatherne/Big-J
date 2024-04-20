using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float XSens;
    public float YSens;

    public Transform orientation;

    private float XRotation;
    private float YRotation;

    private void Start(){
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update(){
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * XSens * GameManager.xSensitivityMultiplier;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * YSens * GameManager.ySensitivityMultiplier;

        YRotation += mouseX;
        XRotation -= mouseY;
        XRotation = Mathf.Clamp(XRotation, -90f, 90f);

        transform.rotation = Quaternion.Euler(XRotation, YRotation, 0f);
        orientation.rotation = Quaternion.Euler(0f, YRotation, 0f);
    }
}