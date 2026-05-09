using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public bool isPlayerWalking = false;
    public CharacterController playerController;
    public float speed;
    public float gravity = -19.62f;
    public Vector3 velocity;
    public float sprintSpeed;
    public float walkingSpeed;

    // Start is called before the first frame update
    void Start()
    {
        playerController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(Input.GetKey(KeyCode.LeftShift))
        {
            speed = sprintSpeed;
        }
        else
        {
            speed = walkingSpeed;
        }
        
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 move = transform.right * x + transform.forward * z;
        playerController.Move(move * speed * Time.deltaTime);
        if (Mathf.Abs(Input.GetAxis("Horizontal")) > 0 || Mathf.Abs(Input.GetAxis("Vertical")) > 0)
        {
            Debug.Log("Player is walking");
            isPlayerWalking=true;
        }
        else
        {
            isPlayerWalking = false;
        }
        //simple gravity
        velocity.y +=  gravity * Time.deltaTime;

        playerController.Move(velocity * Time.deltaTime);
    }



}
