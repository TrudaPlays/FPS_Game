using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    public Transform playerBody;
    public PlayerMovement playerMovement;
    private float xRotation = 0f;
    public float cameraBobHeight;
    public float cameraBobSpeed;
    float defaultPosY = 0f;
    float defaultPosX = 0f;
    float timer = 0;

    // Start is called before the first frame update
    void Start()
    {
        defaultPosY = transform.localPosition.y;
        defaultPosX = transform.localPosition.x;
        playerBody = transform.parent;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        playerMovement = GetComponentInParent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        BounceCamera();
        CameraLook();
        
    }

    public void CameraLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }

    public void BounceCamera()
    {
        if(playerMovement.isPlayerWalking)
        {
            timer += Time.deltaTime * cameraBobSpeed;
            float newY = defaultPosY + Mathf.Sin(timer) * cameraBobHeight;
            float newX = defaultPosX + Mathf.Cos(timer / 2) * cameraBobHeight;
            transform.localPosition = new Vector3(newX, newY, transform.localPosition.z);  
        }
        else
        {
            timer = 0;
            Vector3 targetPos = new Vector3(defaultPosX, defaultPosY, transform.localPosition.z);
            transform.localPosition = Vector3.Lerp(transform.localPosition, targetPos, Time.deltaTime * cameraBobSpeed);
        }
    }
}
