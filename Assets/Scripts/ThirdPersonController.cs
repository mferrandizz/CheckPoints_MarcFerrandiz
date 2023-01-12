using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonController : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;

    public Transform groundSensor;
    public LayerMask ground;
    public float sensorRadius = 0.1f;

    public float speed = 5;
    public float jumpForce = 20;
    public float jumpHeight = 1;
    private float gravity = -9.81f;
    public bool isGrounded;
    private float turnSmoothVelocity;

    public float turnSmoothTime=0.1f;
    public Transform cam;
    public Transform LookAtTransform;

    public Cinemachine.AxisState xAxis;
    public Cinemachine.AxisState yAxis;

    public GameObject[] cameras;




    // Start is called before the first frame update
    void Awake()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;

    }

    void Update()
    {

        //MovementTPS();
        MovementTPS2();
        Jump();
        
    }

    // Update is called once per frame
    void Movement()
    {
        Vector3 move = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;

        if(move != Vector3.zero) 
        {
            float targetAngle = Mathf.Atan2(move.x, move.z) * Mathf.Rad2Deg + cam.eulerAngles.y;

            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);

            transform.rotation = Quaternion.Euler(0, targetAngle, 0);

            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * transform.forward;
            //Si con el transform.forward no funciona probar con el Vector3.forward
            controller.Move(move * speed * Time.deltaTime);
        }
    }
    void MovementTPS()
    {
        Vector3 move = new Vector3(Input.GetAxisRaw("Horizontal"), 0,Input.GetAxisRaw("Vertical")).normalized;

        if(move != Vector3.zero){

            float targetAngle = Mathf.Atan2(move.x, move.z) * Mathf.Rad2Deg + cam.eulerAngles.y;

            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, cam.eulerAngles.y, ref turnSmoothVelocity, turnSmoothTime); 

            transform.rotation = Quaternion.Euler(0, targetAngle, 0);   

            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * transform.forward;
            //Si con el transform.forward no funciona probar con el Vector3.forward

            controller.Move(moveDirection.normalized * speed * Time.deltaTime);

        }
    }

    void MovementTPS2()
    {
        Vector3 move = new Vector3(Input.GetAxisRaw("Horizontal"), 0,Input.GetAxisRaw("Vertical")).normalized;

        xAxis.Update(Time.deltaTime);
        yAxis.Update(Time.deltaTime);

        transform.rotation = Quaternion.Euler(0, xAxis.Value, 0);

        LookAtTransform.eulerAngles = new Vector3 (yAxis.Value, xAxis.Value, LookAtTransform.eulerAngles.z);

        if(Input.GetButton("Fire2"))
        {
            cameras[0].SetActive(false);
            cameras[1].SetActive(true);
        }else 
        {
            cameras[0].SetActive(true);
            cameras[1].SetActive(false);
        }


        if(move != Vector3.zero){

            float targetAngle = Mathf.Atan2(move.x, move.z) * Mathf.Rad2Deg + cam.eulerAngles.y;

            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, cam.eulerAngles.y, ref turnSmoothVelocity, turnSmoothTime);   

            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * transform.forward;
            //Si con el transform.forward no funciona probar con el Vector3.forward

            controller.Move(moveDirection.normalized * speed * Time.deltaTime);

        }
    }

    void Jump(){
        //isGrounded = controller.isGrounded;
        isGrounded = Physics.CheckSphere(groundSensor.position, sensorRadius, ground);
        if(isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = 0;

        }

        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            //playerVelocity.y += jumpForce;
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -2.0f * gravity);
        }

        playerVelocity.y += gravity * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
        }
    }

