using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalScript : MonoBehaviour
{


    public GameObject _portalA;
    public GameObject _portalB;

    private GameObject _boxA;
    private GameObject _boxB;

    private void Start()
    {
        _boxA = GameObject.FindGameObjectWithTag("Box A");
        _boxB = GameObject.FindGameObjectWithTag("Box B");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (IsBoxCollidingWithPortal(_boxA, _portalA))
            {
                TeleportBox(_boxA, _portalB);
            }
            else if (IsBoxCollidingWithPortal(_boxB, _portalA))
            {
                TeleportBox(_boxB, _portalB);
            }
            else if (IsBoxCollidingWithPortal(_boxA, _portalB))
            {
                TeleportBox(_boxA, _portalA);
            }
            else if (IsBoxCollidingWithPortal(_boxB, _portalB))
            {
                TeleportBox(_boxB, _portalA);
            }
        }
    }

    private bool IsBoxCollidingWithPortal(GameObject box, GameObject portal)
    {
        if (box == null || portal == null)
        {
            return false;
        }

        Collider2D boxCollider = box.GetComponent<Collider2D>();
        Collider2D portalCollider = portal.GetComponent<Collider2D>();

        return boxCollider.IsTouching(portalCollider);
    }

    private void TeleportBox(GameObject box, GameObject destinationPortal)
    {
        if (box == null || destinationPortal == null)
        {
            return;
        }

        Vector2 otherPortalPosition = destinationPortal.transform.position;
        box.transform.position = new Vector2(otherPortalPosition.x, otherPortalPosition.y);
    }


}

