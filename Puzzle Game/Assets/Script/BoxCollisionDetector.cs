using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxCollisionDetector : MonoBehaviour
{
    [SerializeField] private BoxLogic boxLogic;
    [SerializeField] private CollisionDirection collisionDirection;


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "wall")
        {
            switch (collisionDirection)
            {
                case CollisionDirection.Up:
                    boxLogic.canMoveUp = false;
                    break;
                case CollisionDirection.Down:
                    boxLogic.canMoveDown = false;
                    break;
                case CollisionDirection.Left:
                    boxLogic.canMoveLeft = false;
                    break;
                case CollisionDirection.Right:
                    boxLogic.canMoveRight = false;
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
            switch (collisionDirection)
            {
                case CollisionDirection.Up:
                    boxLogic.canMoveUp = true;
                    break;
                case CollisionDirection.Down:
                    boxLogic.canMoveDown = true;
                    break;
                case CollisionDirection.Left:
                    boxLogic.canMoveLeft = true;
                    break;
                case CollisionDirection.Right:
                    boxLogic.canMoveRight = true;
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