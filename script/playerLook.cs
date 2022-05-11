using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerLook : MonoBehaviour
{
    public float M_speed = 100;

    public Transform P_body;

    float xRotation = 0f;


    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }

    // Update is called once per frame
    void Update()
    {
        float mousex = Input.GetAxis("Mouse X") * M_speed * Time.deltaTime;
        float mousey = Input.GetAxis("Mouse Y") * M_speed * Time.deltaTime;

        xRotation -= mousey;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        transform.localRotation = Quaternion.Euler(xRotation, 0f , 0f);

        P_body.Rotate(Vector3.up * mousex);
    }
}
