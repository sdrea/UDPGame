using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public bool isGrounded;
    public Vector3 curPos = Vector3.zero;
    public Vector3 lastPos = Vector3.zero;
    public Animator anim;
    public InputField chat;
    bool autorun = false;
  
    void Start()
    {
		
            Camera.main.GetComponent<ThirdPersonCamera>().lookAt = transform;
            anim = GetComponent<Animator>();

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (!autorun) autorun = true;
            else autorun = false;
        }

        var z = Input.GetAxis("Vertical") * Time.deltaTime * 6.0f ;
        var x = Input.GetAxis("Horizontal") * Time.deltaTime * 6.0f ;
        var y = 0.0f;
        
        if (Input.GetMouseButton(1))
        {
            y = Input.GetAxis("Mouse X") * 5.0f;
            transform.Rotate(0, y, 0);

            if (Input.GetMouseButton(0))
            {
                 z = Time.deltaTime * 6.0f;
                
            }
        }

        if (z > 0) autorun = false;

        if (autorun) z = Time.deltaTime * 6.0f;

        curPos = transform.position;

        if ((z != 0 || x != 0) && (!anim.GetCurrentAnimatorStateInfo(0).IsName("Move")))
        {
            //moving
            anim.Play("Move");
        }
        if ((z == 0 && x == 0) && (!anim.GetCurrentAnimatorStateInfo(0).IsName("Idle")))
        {
            //moving
            anim.Play("Idle");
        }
        lastPos = curPos;


        transform.Translate(x, 0, z);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            
           if (transform.position.y < 0)
                GetComponent<Rigidbody>().AddForce(Vector3.up * 200.0f);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}