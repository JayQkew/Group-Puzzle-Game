using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxCollisionDetector : MonoBehaviour
{
    [SerializeField] private BoxLogic boxLogic;
    [SerializeField] private GameObject otherBox;
    [SerializeField] private CollisionDirection collisionDirection;
    [SerializeField] private bool isTouchingBox = false;
    [SerializeField] private bool isTouchingWall = false;


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "wall")
        {
            isTouchingWall = true;
            switch (collisionDirection)
            {
                case CollisionDirection.Up:
                    boxLogic.canMoveUp = false;
                    boxLogic.wallTouchingUp = true;
                    break;
                case CollisionDirection.Down:
                    boxLogic.canMoveDown = false;
                    boxLogic.wallTouchingDown = true;
                    break;
                case CollisionDirection.Left:
                    boxLogic.canMoveLeft = false;
                    boxLogic.wallTouchingLeft = true;
                    break;
                case CollisionDirection.Right:
                    boxLogic.canMoveRight = false;
                    boxLogic.wallTouchingRight = true;
                    break;
                default:
                    break;
            }
        }
        if (collision.gameObject == otherBox)
        {
            isTouchingBox = true;
            switch (collisionDirection)
            {
                case CollisionDirection.Up:
                    if (transform.parent.GetComponent<BoxLogic>().boxType == BoxType.A)
                    {
                        boxLogic.otherBoxTouchingUp = true;
                        boxLogic.canPushOtherUp = true;
                        boxLogic.canPushOtherDown = false;
                        boxLogic.canPushOtherLeft = false;
                        boxLogic.canPushOtherRight = false;
                    }
                    Debug.Log(collision.gameObject);
                    break;
                case CollisionDirection.Down:
                    if (transform.parent.GetComponent<BoxLogic>().boxType == BoxType.A)
                    {
                        boxLogic.otherBoxTouchingDown = true;
                        boxLogic.canPushOtherDown = true;
                        boxLogic.canPushOtherUp = false;
                        boxLogic.canPushOtherLeft = false;
                        boxLogic.canPushOtherRight = false;
                    }
                    Debug.Log(collision.gameObject);
                    break;
                case CollisionDirection.Left:
                    if (transform.parent.GetComponent<BoxLogic>().boxType == BoxType.B)
                    {
                        boxLogic.otherBoxTouchingLeft = true;
                        boxLogic.canPushOtherLeft = true;
                        boxLogic.canPushOtherDown = false;
                        boxLogic.canPushOtherUp = false;
                        boxLogic.canPushOtherRight = false;
                    }
                    Debug.Log(collision.gameObject);
                    break;
                case CollisionDirection.Right:
                    if (transform.parent.GetComponent<BoxLogic>().boxType == BoxType.B)
                    {
                        boxLogic.otherBoxTouchingRight = true;
                        boxLogic.canPushOtherRight = true;
                        boxLogic.canPushOtherDown = false;
                        boxLogic.canPushOtherUp = false;
                        boxLogic.canPushOtherLeft = false;
                    }
                    Debug.Log(collision.gameObject);
                    break;
                default:
                    break;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "wall")
        {
            isTouchingWall = false;
            switch (collisionDirection)
            {
                case CollisionDirection.Up:
                    boxLogic.canMoveUp = true;
                    boxLogic.wallTouchingUp = false;
                    break;
                case CollisionDirection.Down:
                    boxLogic.canMoveDown = true;
                    boxLogic.wallTouchingDown = false;
                    break;
                case CollisionDirection.Left:
                    boxLogic.canMoveLeft = true;
                    boxLogic.wallTouchingLeft = false;
                    break;
                case CollisionDirection.Right:
                    boxLogic.canMoveRight = true;
                    boxLogic.wallTouchingRight = false;
                    break;
                default:
                    break;
            }

        }
        if (collision.gameObject == otherBox)
        {
            isTouchingBox = false;
            switch (collisionDirection)
            {
                case CollisionDirection.Up:
                    boxLogic.otherBoxTouchingUp = false;
                    boxLogic.canPushOtherUp = false;
                    break;
                case CollisionDirection.Down:
                    boxLogic.otherBoxTouchingDown = false;
                    boxLogic.canPushOtherDown = false;
                    break;
                case CollisionDirection.Left:
                    boxLogic.otherBoxTouchingLeft = false;
                    boxLogic.canPushOtherLeft = false;
                    break;
                case CollisionDirection.Right:
                    boxLogic.otherBoxTouchingRight = false;
                    boxLogic.canPushOtherRight = false;
                    break;
                default:
                    break;
            }
        }

    }



}

enum CollisionDirection
{
    Up,
    Down,
    Left,
    Right
}