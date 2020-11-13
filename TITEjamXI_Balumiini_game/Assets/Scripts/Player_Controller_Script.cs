using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller_Script : MonoBehaviour
{
    private CharacterController sugarCtrl;
    [Header("Movement")]
    private float xMove = 0f;
    public float moveSpeed = 8f;
    public float inputAcceleration = 0f;
    public float inputThreshold = 0.2f;
    private Vector3 dir = Vector3.zero;
    public bool MoveDemo = false;

    [Header("Jumping and falling")]
    public float jumpHeight = 4f;
    private float yMove = 0f;
    public float groundDist = 0.2f;
    private float gravity = -18.81f;
    public Transform groundCheck; //object at the feet of character
    private bool isGrounded;
    public LayerMask groundMask;

    // Start is called before the first frame update
    void Start()
    {
        sugarCtrl = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDist, groundMask);
        if (!isGrounded)
        {
            yMove += gravity * Time.deltaTime;
        }
        else
        {
            if (dir.y < 0) yMove = -2f;
            if(Input.GetButtonDown("Jump"))
            {
                yMove = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }
        }


        if (MoveDemo)
        {
            if (Input.GetAxis("Horizontal") > inputThreshold || Input.GetAxis("Horizontal") < -inputThreshold) xMove = Input.GetAxis("Horizontal"); else xMove = 0;
            inputAcceleration = Mathf.Clamp(Mathf.Sqrt(Mathf.Pow(xMove, 2f)), 0, 1);
        }
        else
        {
            xMove = Input.GetAxis("Horizontal") * moveSpeed;
        }
        dir.y = yMove;
        dir.x = xMove;
        //transform.Translate(dir.normalized * moveSpeed * Time.deltaTime);
        sugarCtrl.Move(dir * Time.deltaTime);
    }
}
