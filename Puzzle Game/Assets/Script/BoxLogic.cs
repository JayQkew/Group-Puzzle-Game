using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxLogic : MonoBehaviour
{
    [SerializeField] private GameObject box;
    [SerializeField] public bool boxIsMoving = false;
    [SerializeField] public BoxType boxType = new BoxType();

    #region Can Move bools
    [SerializeField] public bool canMoveUp = true;
    [SerializeField] public bool canMoveDown = true;
    [SerializeField] public bool canMoveRight = true;
    [SerializeField] public bool canMoveLeft = true;
    #endregion
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "box")
        {
            Debug.Log("Touching box");
            switch (collision.collider.gameObject.GetComponent<BoxLogic>().boxType)
            {
                case BoxType.A:
                    if (boxType == BoxType.B && PlayerController.Instance.boxA_moving && !boxIsMoving)
                    {
                        Debug.Log("case 1(A)");
                        PlayerController.Instance.MoveBox2(box, PlayerController.Instance.boxA_targetPosition + Vector3.up);
                    }
                    break;
                case BoxType.B:
                    if (boxType == BoxType.A && PlayerController.Instance.boxB_moving && !boxIsMoving)
                    {
                        Debug.Log("case 2(B)");

                    }

                    break;
                default:
                    break;
            }
        }
    }
}

public enum BoxType
{
    A,
    B
}