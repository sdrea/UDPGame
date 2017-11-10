using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour {

    public Transform lookAt;
    public Transform camTransform;
    private Quaternion rotation = Quaternion.Euler(0, 0, 0);

    private float distance = 10.0f;
    private float currentX = 0.0f;
    private float currentY = 10.0f;

    private void Start()
    {
		
        camTransform = transform;

    }

    private void Update()
    {
       // Nothing
    }

    private void LateUpdate()
    {
        Vector3 dir = new Vector3(0, 0, -distance);

        if (Input.GetMouseButton(1))
            
        {
            currentY -= Input.GetAxis("Mouse Y") * 5.0f;
                                  
        }

        rotation = Quaternion.Euler(currentY, currentX, 0);
        currentX = lookAt.rotation.eulerAngles.y;
        camTransform.position = lookAt.position + rotation * dir;
        camTransform.LookAt(lookAt.position);

    }

}
