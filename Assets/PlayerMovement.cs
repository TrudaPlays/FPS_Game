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

    // Start is called before the first frame update
    void Start()
    {
        playerController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 move = transform.right * x + transform.forward * z;
        playerController.Move(move * speed * Time.deltaTime);
        if (move.magnitude > 0.01f)
        {
            isPlayerWalking=true;
        }
        //simple gravity
        velocity.y +=  gravity * Time.deltaTime;

        playerController.Move(velocity * Time.deltaTime);
    }



}
