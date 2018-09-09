using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public Text scoreText;

    private Rigidbody playerRigidBody;

    public float speed;

    private float moveHorizontal;
    private float moveVertical;
    private int score;

    private Vector3 moving;

    private void Awake()
    {
        playerRigidBody = GetComponent<Rigidbody>();

        score = 0;
    }

    private void Update()
    {
        if (score < 9)
        {
            scoreText.text = "Player Score: " + score;
        }
        if (score >= 9)
        {
            scoreText.text = "You Win";

            Time.timeScale = 0;
        }
    }

    private void FixedUpdate()
    {
        moveHorizontal = Input.GetAxis("Horizontal");
        moveVertical = Input.GetAxis("Vertical");

        moving = new Vector3(moveHorizontal, 0.0f, moveVertical);

        playerRigidBody.AddForce(moving * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "PickUp")
        {
            other.gameObject.SetActive(false);

            score++;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            collision.gameObject.GetComponent<MeshRenderer>().enabled = false;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        collision.gameObject.GetComponent<MeshRenderer>().enabled = true;
    }
}
