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

    private void Start()
    {
        _charactercontroller = gameObject.GetComponent<CharacterController>();

        if (_charactercontroller == null)
        {
            Debug.LogError("Character Controller is NULL!");
        }
    }

    void Update()
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

        _charactercontroller.Move(playerVelocity * Time.deltaTime);
    }

    
}
