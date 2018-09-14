using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallAttackController : MonoBehaviour
{
    private bool waiting;
    private bool canDamage;

    private Material wallMaterial;

    private PlayerController playerReference;

    private void Awake()
    {
        playerReference = FindObjectOfType<PlayerController>();
        wallMaterial = GetComponent<Renderer>().material;

        waiting = false;
        canDamage = true;
    }

    private void Update()
    {
        if(waiting && canDamage)
        {
            StartCoroutine(WaitingTimer());
        }
    }

    IEnumerator WaitingTimer()
    {
        canDamage = false;

        wallMaterial.color = Color.black;

        yield return new WaitForSeconds(3.0f);

        waiting = false;
        canDamage = true;

        wallMaterial.color = Color.red;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player" && waiting == false)
        {
            waiting = true;

            playerReference.score--;
        }
    }
}
