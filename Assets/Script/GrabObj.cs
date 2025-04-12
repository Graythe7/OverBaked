using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabObj : MonoBehaviour
{
    public Transform grabPoint;
    private GameObject heldObject = null;
    private float pickupRange = 1.5f;
    

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if(heldObject == null)
            {
                PickupObj();
            }
            else
            {
                DropObj();
            }
        }

        //constantly update the position of ingredient obj (heldObj) with player 
        if (heldObject != null)
        {
            heldObject.transform.position = grabPoint.position;
        }
    }

    private void PickupObj()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, pickupRange); //to detect all obj in pickupRange area
        foreach (Collider2D hit in hits)
        {
            if (hit.CompareTag("Ingredient"))
            {
                heldObject = hit.gameObject; //saves a copy of it
                heldObject.GetComponent<Rigidbody2D>().isKinematic = true;
                heldObject.GetComponent<Collider2D>().enabled = false;
                heldObject.transform.position = grabPoint.position;
                return; // to stop right after picking up the first valid game object
            }
        }

    }

    private void DropObj()
    {
        heldObject.GetComponent<Rigidbody2D>().isKinematic = false;
        heldObject.GetComponent<Collider2D>().enabled = true;
        heldObject = null;
    }


}
