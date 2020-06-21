using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMotion : MonoBehaviour
{
    public PlayerController playerController;
    public float distance = 0;
    public float mouseXSpeedMod = 1;
    public float mouseYSpeedMod = 5;
    float X_anglePercentage = 0;
    float Y_anglePercentage = 0;
    void Start()
    {
        if(distance == 0)
        distance = this.transform.localPosition.z;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }


    void Update()
    {
        float y = Input.GetAxis("Vertical");
        X_anglePercentage = Mathf.Clamp(X_anglePercentage, 0, 1);
        Y_anglePercentage = Mathf.Clamp(Y_anglePercentage, 0.2f, 1);
        if (y == 0)
        {
            if (Input.GetAxis("Mouse X") > 0)
            {
                Move_X(X_anglePercentage -= Time.deltaTime * mouseXSpeedMod);
            }
            if (Input.GetAxis("Mouse X") < 0)
            {
                Move_X(X_anglePercentage += Time.deltaTime * mouseXSpeedMod);
            }
        }
        if (Input.GetAxis("Mouse Y") > 0)
        {
            Move_Y(Y_anglePercentage -= Time.deltaTime * mouseYSpeedMod);
        }
        if (Input.GetAxis("Mouse Y") < 0)
        {
            Move_Y(Y_anglePercentage += Time.deltaTime * mouseYSpeedMod);
        }


    }
    void Move_X(float anglePercentage)
    {
        Vector3 maxpos = Vector3.Slerp(Vector3.left, Vector3.right, anglePercentage);
        maxpos = maxpos * (distance * -1);
        maxpos.y = this.transform.localPosition.y;
        this.transform.localPosition = maxpos;
    }
    void Move_Y(float anglePercentage)
    {
        Vector3 maxpos = new Vector3(this.transform.localPosition.x, Mathf.Lerp(0, Mathf.Abs(distance*2), Y_anglePercentage), this.transform.localPosition.z);
        this.transform.localPosition = maxpos;
    }
    public void ResetCamera()
    {
        
        Move_X(X_anglePercentage = 0.5f);

    }
}
