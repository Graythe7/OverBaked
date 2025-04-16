using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 5f;
    public Rigidbody2D rb;
    private Vector2 movement;

    public LayerMask interactableLayers;
    private Vector2 lastInteractDirection;

    private GameObject grabbedObject = null;
    public Transform grabPoint;

    private void Update()
    {
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");

        if (Input.GetKeyDown(KeyCode.E))
        {
            HandleInteraction();
        }
 
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }

    private void HandleInteraction()
    {
        Vector2 moveDirection = new Vector2(movement.x, movement.y);

        if(moveDirection != Vector2.zero)
        {
            lastInteractDirection = moveDirection; //so later even when we are not moving, raycast detect objs
        }

        float interactDistance = 2f;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, lastInteractDirection, interactDistance, interactableLayers);

        if (hit.collider != null)
        {
            //grab an item
            if (grabbedObject == null)
            {
                grabbedObject = hit.collider.gameObject; //so player could carry only one obj
                grabbedObject.GetComponent<Rigidbody2D>().isKinematic = true;
                grabbedObject.GetComponent<Collider2D>().enabled = false;
                grabbedObject.transform.position = grabPoint.position;
                //grabbedObject.transform.SetParent(transform);//set player as the parent, not sure if this is necessary
            }
            //Drop an item
            else if (grabbedObject != null)
            {
                grabbedObject.GetComponent<Rigidbody2D>().isKinematic = false;
                grabbedObject.GetComponent<Collider2D>().enabled = true;
                //grabbedObject.transform.SetParent(null);
                grabbedObject = null;
            }
        }
    }

}
