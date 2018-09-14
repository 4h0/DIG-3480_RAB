using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public static int whichLevel;
    public static int savedScore;

    public Text winText;

    public GameObject pickUpListParent;
    public GameObject harmfulPickUpListParent;

    private Rigidbody playerRigidBody;

    private Material playerMaterial;

    private Vector3 moving;

    private GameController gameController;

    public float speed;

    public int score;
    public int numberOfPickup;
    public int numberOfHarmfulPickup;

    private bool nextLevelCalled;

    private float moveHorizontal;
    private float moveVertical;

    private void Awake()
    {
        gameController = FindObjectOfType<GameController>();

        playerRigidBody = GetComponent<Rigidbody>();
        playerMaterial = GetComponent<Renderer>().material;

        winText.enabled = false;

        score = savedScore;
        nextLevelCalled = false;
    }

    private void Start()
    {
        numberOfPickup = pickUpListParent.transform.childCount;
        numberOfHarmfulPickup = harmfulPickUpListParent.transform.childCount;
    }

    private void Update()
    {
        playerMaterial.color = new Color(this.transform.position.x, this.transform.position.y, this.transform.position.z);

        if(gameController.gameEnd && !nextLevelCalled)
        {
            speed = 0;

            Time.timeScale = 0.1f;

            StartCoroutine(SwitchSceneCountDown());
        }
    }

    private void FixedUpdate()
    {
        moveHorizontal = Input.GetAxis("Horizontal");
        moveVertical = Input.GetAxis("Vertical");

        moving = new Vector3(moveHorizontal, 0.0f, moveVertical);

        playerRigidBody.AddForce(moving * speed);
    }

    IEnumerator SwitchSceneCountDown()
    {
        if (numberOfPickup <= 0)
        {
            nextLevelCalled = true;

            winText.text = "You Finished with a score of: " + score;
            winText.enabled = true;

            whichLevel++;
            Debug.Log(whichLevel);
            savedScore = score;

            yield return new WaitForSeconds(.3f);

            if (whichLevel <= 1)
            {
                SceneManager.LoadScene("NextLevel");
            }
            else
            {
                Application.Quit();
            }
        }
        else
        {
            gameController.scoreText.enabled = false;
            gameController.countText.enabled = false;
            gameController.timerText.enabled = false;

            winText.text = "YOU LOSE";
            winText.enabled = true;

            yield return new WaitForSeconds(.3f);

            Application.Quit();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "PickUp")
        {
            other.gameObject.SetActive(false);

            score++;
            numberOfPickup--;
        }

        if (other.tag == "Harmful")
        {
            other.gameObject.SetActive(false);

            score--;
            numberOfHarmfulPickup--;
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
