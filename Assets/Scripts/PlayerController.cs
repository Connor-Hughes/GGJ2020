using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public CharacterController controller;

    public GameController GC;

    public GameObject pressEToRepair;
    public GameObject pressEToBuy;
    private BuildingController BC;

    public GameObject Robot;

    public float speed = 12f;
    public float springSpeed = 24f;
    public float jumpHeight = 3f;
    public float gravity = -9.81f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    private int RobotPrice = 20;

    Vector3 velocity;

    private bool isGrounded;

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        if (Input.GetButton("sprint"))
        {
            controller.Move(move * springSpeed * Time.deltaTime);
        }
        else
        {
            controller.Move(move * speed * Time.deltaTime);
        }

        //jump mechanic, finish map layout then change all tags(layer) to ground 
        if (Input.GetButton("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Building")
        {
            BC = other.GetComponent<BuildingController>();

            if (BC.isFixed == false)
            {
                pressEToRepair.SetActive(true);
            }
        }
        else if(other.tag == "RobotBuy")
        {
            pressEToBuy.SetActive(true);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Building")
        {
            if (Input.GetButtonDown("Repair") && BC.isFixed == false)
            {
                BC.Repair();
                pressEToRepair.SetActive(false);
            }
        }
        else if(other.tag == "RobotBuy")
        {
            if (Input.GetButtonDown("Repair"))
            {
                if (GC.MoneyAmount > RobotPrice)
                {
                    GC.MoneyAmount -= RobotPrice;

                    Instantiate(Robot, other.transform.position, other.transform.rotation);
                    RobotPrice *= 2;
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Building")
        {
            pressEToRepair.SetActive(false);
        }
        else if(other.tag == "RobotBuy")
        {
            pressEToBuy.SetActive(false);
        }
    }
}
