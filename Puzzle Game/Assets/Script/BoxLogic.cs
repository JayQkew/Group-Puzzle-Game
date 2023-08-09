using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxLogic : MonoBehaviour
{
    [SerializeField] private GameObject box;
    [SerializeField] public bool boxIsMoving = false;
    [SerializeField] public BoxType boxType = new BoxType();
    [SerializeField] public bool touchingOtherBox = false;

    #region Can Move bools
    [SerializeField] public bool canMoveUp = true;
    [SerializeField] public bool canMoveDown = true;
    [SerializeField] public bool canMoveRight = true;
    [SerializeField] public bool canMoveLeft = true;
    #endregion

    #region Can Push Bool
    [SerializeField] public bool canPushOtherUp = true;
    [SerializeField] public bool canPushOtherDown = true;
    [SerializeField] public bool canPushOtherRight = true;
    [SerializeField] public bool canPushOtherLeft = true;
    #endregion

    #region Box Touching Side
    [SerializeField] public bool otherBoxTouchingUp = false;
    [SerializeField] public bool otherBoxTouchingDown = false;
    [SerializeField] public bool otherBoxTouchingRight = false;
    [SerializeField] public bool otherBoxTouchingLeft = false;
    #endregion

    #region Wall Touching Side
    [SerializeField] public bool wallTouchingUp = false;
    [SerializeField] public bool wallTouchingDown = false;
    [SerializeField] public bool wallTouchingRight = false;
    [SerializeField] public bool wallTouchingLeft = false;
    #endregion

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "box")
        {
            touchingOtherBox = true;

        }

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.collider.tag == "box")
        {
            touchingOtherBox = false;
        }
    }
}

public enum BoxType
{
    A,
    B
}