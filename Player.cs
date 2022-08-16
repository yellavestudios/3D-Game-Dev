using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //wsad keys for movement
    //input system , horizontal and verticle axis
    //direction = vector 3 movement
    // velocity = direction * speed

    private CharacterController _charactercontroller;

    //check if player is grounded
    private bool groundedPlayer;
    [SerializeField]
    private float playerSpeed = 6.0f;
    [SerializeField]
    private float jumpHeight = 8.0f;
    [SerializeField]
    private float gravity = 20.0f;

    private Vector3 direction;
    private Vector3 playerVelocity; //velocity = direction * speed

    private Camera _mainCamera;

    private void Start()
    {
        _charactercontroller = gameObject.GetComponent<CharacterController>();

        if (_charactercontroller == null)
        {
            Debug.LogError("Character Controller is NULL!");
        }

        _mainCamera = Camera.main;

        if (_mainCamera == null)
        {
            Debug.LogError("Main Camera is NULL!");
        }
    }

    void Update()
    {
        //y mouse, get component of camera, apply rotation of x from the camera
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");


        //x mouse, apply player rot y == look left and right
        Vector3 currentRotation = transform.localEulerAngles; //current rotation
        currentRotation.y += mouseX;
        transform.localRotation = Quaternion.AngleAxis(currentRotation.y, Vector3.up);  //new rotation 

        //look up and down
        Vector3 currentCameraRotation = _mainCamera.gameObject.transform.localEulerAngles;
        currentCameraRotation.x -= mouseY;
        //_mainCamera.gameObject.transform.localEulerAngles = currentCameraRotation;
        _mainCamera.gameObject.transform.localRotation = Quaternion.AngleAxis(currentCameraRotation.x, Vector3.right);

        

        CalculateMovement();
    }

    private void CalculateMovement()
    {
        if (_charactercontroller.isGrounded == true)
        {

            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            direction = new Vector3(horizontal, 0, vertical);
            playerVelocity = direction * playerSpeed;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                playerVelocity.y = jumpHeight;
            }
        }

        //brings the player back down after jumping, applies gravity
        playerVelocity.y -= gravity * Time.deltaTime;

        playerVelocity = transform.TransformDirection(playerVelocity); //moves player in direction that camera is facing

        _charactercontroller.Move(playerVelocity * Time.deltaTime);
    }

    
}
