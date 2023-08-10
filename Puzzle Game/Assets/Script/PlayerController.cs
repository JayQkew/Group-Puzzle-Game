using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.UIElements;
using UnityEngine.Windows.Speech;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { get; private set; }

    [SerializeField] private BoxLogic boxA_script;
    [SerializeField] private BoxLogic boxB_script;

    #region Game Objects:
    [Header("Boxes")]
    [SerializeField] public GameObject box_A;
    [SerializeField] public GameObject box_B;

    private Vector3 boxA_originPosition;
    private Vector3 boxB_originPosition;

    public Vector3 boxA_targetPosition;
    public Vector3 boxB_targetPosition;

    [SerializeField] public bool boxA_moving;
    [SerializeField] public bool boxB_moving;

    private Vector3 boxA_spawn;
    private Vector3 boxB_spawn;

    [Header("Boxes Spawn")]
    [SerializeField] private Vector3Int boxA_spawnInt;
    [SerializeField] private Vector3Int boxB_spawnInt;
    #endregion

    [SerializeField] private Grid grid;
    [SerializeField] private GridLayout gridLayout;
    [SerializeField] private float moveIntervalTime; // grid size

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        boxA_spawn = gridLayout.GetLayoutCellCenter() + grid.CellToWorld(boxA_spawnInt);
        boxB_spawn = gridLayout.GetLayoutCellCenter() + grid.CellToWorld(boxB_spawnInt);

        boxA_originPosition = boxA_spawn;
        boxB_originPosition = boxB_spawn;

        box_A.transform.position = boxA_spawn;
        box_B.transform.position = boxB_spawn;
    }
    private void Update()
    {
        BoxMovement(box_A, box_B);
    }

    private void BoxMovement(GameObject boxA, GameObject boxB)
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (!boxA_moving && boxA_script.canMoveUp)
            {
                StartCoroutine(MoveBox1(boxA, boxB, Vector3.up));
                boxA_script.canMoveDown = true;
            }
        }
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (!boxA_moving && boxA_script.canMoveDown)
            {
                StartCoroutine(MoveBox1(boxA, boxB, Vector3.down));
                boxA_script.canMoveUp = true;
            }
        }
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (!boxB_moving && boxB_script.canMoveRight)
            {
                StartCoroutine(MoveBox2(boxB, boxA, Vector3.right));
                boxB_script.canMoveLeft = true;
            }
        }
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (!boxB_moving && boxB_script.canMoveLeft)
            {
                StartCoroutine(MoveBox2(boxB, boxA, Vector3.left));
                boxB_script.canMoveRight = true;
            }
        }
    }

    public IEnumerator MoveBox1(GameObject boxA, GameObject boxB, Vector3 direction)
    {
        if (boxA.GetComponent<BoxLogic>().otherBoxTouchingUp && direction == Vector3.up && !boxB.GetComponent<BoxLogic>().wallTouchingUp && boxB.GetComponent<BoxLogic>().canMoveUp)
        {
            Debug.Log("Here BEEECH");
            boxA_script.boxIsMoving = true;
            boxA_moving = true;

            float elapsedTime = 0;

            boxA_originPosition = new Vector3((float)Math.Round(boxA.transform.position.x, 1), (float)Math.Round(boxA.transform.position.y, 1), 0);

            boxA_targetPosition = boxA_originPosition + direction;

            if (boxA.GetComponent<BoxLogic>().otherBoxTouchingUp && !boxB.GetComponent<BoxLogic>().wallTouchingUp && boxA.GetComponent<BoxLogic>().canPushOtherUp)
            {
                boxB_originPosition = new Vector3((float)Math.Round(boxB.transform.position.x, 1), (float)Math.Round(boxB.transform.position.y, 1), 0);
                boxB_targetPosition = boxB_originPosition + direction;
            }

            while (elapsedTime < moveIntervalTime)
            {
                boxA.transform.position = Vector3.Lerp(boxA_originPosition, boxA_targetPosition, elapsedTime / moveIntervalTime);

                if (boxA.GetComponent<BoxLogic>().otherBoxTouchingUp && !boxB.GetComponent<BoxLogic>().wallTouchingUp && boxA.GetComponent<BoxLogic>().canPushOtherUp)
                {
                    boxB.transform.position = Vector3.Lerp(boxB_originPosition, boxB_targetPosition, elapsedTime / moveIntervalTime);
                }
                else if (boxA.GetComponent<BoxLogic>().otherBoxTouchingDown && !boxB.GetComponent<BoxLogic>().wallTouchingDown && boxA.GetComponent<BoxLogic>().canPushOtherDown)
                {
                    boxB.transform.position = Vector3.Lerp(boxB_originPosition, boxB_targetPosition, elapsedTime / moveIntervalTime);
                }

                elapsedTime += Time.deltaTime;
                yield return null;
            }

            boxA.transform.position = boxA_targetPosition;

            if (boxA.GetComponent<BoxLogic>().otherBoxTouchingUp && !boxB.GetComponent<BoxLogic>().wallTouchingUp && boxA.GetComponent<BoxLogic>().canPushOtherUp)
            {
                boxB.transform.position = boxB_targetPosition;
            }

            boxA_script.boxIsMoving = false;
            boxA_moving = false;
        }

        else if (boxA.GetComponent<BoxLogic>().otherBoxTouchingDown && direction == Vector3.down && !boxB.GetComponent<BoxLogic>().wallTouchingDown && boxB.GetComponent<BoxLogic>().canMoveDown)
        {
            boxA_script.boxIsMoving = true;
            boxA_moving = true;

            float elapsedTime = 0;

            boxA_originPosition = new Vector3((float)Math.Round(boxA.transform.position.x, 1), (float)Math.Round(boxA.transform.position.y, 1), 0);

            boxA_targetPosition = boxA_originPosition + direction;

            if (boxA.GetComponent<BoxLogic>().otherBoxTouchingDown && !boxB.GetComponent<BoxLogic>().wallTouchingDown && boxA.GetComponent<BoxLogic>().canPushOtherDown)
            {
                boxB_originPosition = new Vector3((float)Math.Round(boxB.transform.position.x, 1), (float)Math.Round(boxB.transform.position.y, 1), 0);
                boxB_targetPosition = boxB_originPosition + direction;
            }

            while (elapsedTime < moveIntervalTime)
            {
                boxA.transform.position = Vector3.Lerp(boxA_originPosition, boxA_targetPosition, elapsedTime / moveIntervalTime);

                if (boxA.GetComponent<BoxLogic>().otherBoxTouchingDown && !boxB.GetComponent<BoxLogic>().wallTouchingDown && boxA.GetComponent<BoxLogic>().canPushOtherDown)
                {
                    boxB.transform.position = Vector3.Lerp(boxB_originPosition, boxB_targetPosition, elapsedTime / moveIntervalTime);
                }

                elapsedTime += Time.deltaTime;
                yield return null;
            }

            boxA.transform.position = boxA_targetPosition;

            if (boxA.GetComponent<BoxLogic>().otherBoxTouchingDown && !boxB.GetComponent<BoxLogic>().wallTouchingDown && boxA.GetComponent<BoxLogic>().canPushOtherDown)
            {
                boxB.transform.position = boxB_targetPosition;
            }

            boxA_script.boxIsMoving = false;
            boxA_moving = false;
        }

        else if (boxA.GetComponent<BoxLogic>().touchingOtherBox)
        {
            if (boxA.GetComponent<BoxLogic>().canMoveUp && boxA.GetComponent<BoxLogic>().canMoveUp && direction == Vector3.up && !boxA.GetComponent<BoxLogic>().otherBoxTouchingUp)
            {
                Debug.Log("Here BEECH");
                boxA_script.boxIsMoving = true;
                boxA_moving = true;

                float elapsedTime = 0;

                boxA_originPosition = new Vector3((float)Math.Round(boxA.transform.position.x, 1), (float)Math.Round(boxA.transform.position.y, 1), 0);

                boxA_targetPosition = boxA_originPosition + direction;

                while (elapsedTime < moveIntervalTime)
                {
                    boxA.transform.position = Vector3.Lerp(boxA_originPosition, boxA_targetPosition, elapsedTime / moveIntervalTime);

                    elapsedTime += Time.deltaTime;
                    yield return null;
                }

                boxA.transform.position = boxA_targetPosition;

                boxA_script.boxIsMoving = false;
                boxA_moving = false;
            }
            else if (boxA.GetComponent<BoxLogic>().canMoveDown && boxB.GetComponent<BoxLogic>().canMoveDown && direction == Vector3.down && !boxA.GetComponent<BoxLogic>().otherBoxTouchingDown)
            {
                boxA_script.boxIsMoving = true;
                boxA_moving = true;

                float elapsedTime = 0;

                boxA_originPosition = new Vector3((float)Math.Round(boxA.transform.position.x, 1), (float)Math.Round(boxA.transform.position.y, 1), 0);

                boxA_targetPosition = boxA_originPosition + direction;

                while (elapsedTime < moveIntervalTime)
                {
                    boxA.transform.position = Vector3.Lerp(boxA_originPosition, boxA_targetPosition, elapsedTime / moveIntervalTime);

                    elapsedTime += Time.deltaTime;
                    yield return null;
                }

                boxA.transform.position = boxA_targetPosition;

                boxA_script.boxIsMoving = false;
                boxA_moving = false;
            }
        }

        else if (!boxA.GetComponent<BoxLogic>().touchingOtherBox)
        {
            boxA_script.boxIsMoving = true;
            boxA_moving = true;

            float elapsedTime = 0;

            boxA_originPosition = new Vector3((float)Math.Round(boxA.transform.position.x, 1), (float)Math.Round(boxA.transform.position.y, 1), 0);

            boxA_targetPosition = boxA_originPosition + direction;

            while (elapsedTime < moveIntervalTime)
            {
                boxA.transform.position = Vector3.Lerp(boxA_originPosition, boxA_targetPosition, elapsedTime / moveIntervalTime);

                elapsedTime += Time.deltaTime;
                yield return null;
            }

            boxA.transform.position = boxA_targetPosition;

            boxA_script.boxIsMoving = false;
            boxA_moving = false;
        }


    }
    public IEnumerator MoveBox2(GameObject boxB, GameObject boxA, Vector3 direction)
    {
        if (boxB.GetComponent<BoxLogic>().touchingOtherBox && direction == Vector3.right && !boxA.GetComponent<BoxLogic>().wallTouchingRight && boxA.GetComponent<BoxLogic>().canMoveRight)
        {
            boxB_script.boxIsMoving = true;
            boxB_moving = true;

            float elapsedTime = 0;

            boxB_originPosition = new Vector3((float)Math.Round(boxB.transform.position.x, 1), (float)Math.Round(boxB.transform.position.y, 1), 0);

            boxB_targetPosition = boxB_originPosition + direction;

            if (boxB.GetComponent<BoxLogic>().otherBoxTouchingRight && !boxA.GetComponent<BoxLogic>().wallTouchingRight && boxB.GetComponent<BoxLogic>().canPushOtherRight)
            {
                boxA_originPosition = new Vector3((float)Math.Round(boxA.transform.position.x, 1), (float)Math.Round(boxA.transform.position.y, 1), 0);
                boxA_targetPosition = boxA_originPosition + direction;
            }

            while (elapsedTime < moveIntervalTime)
            {
                boxB.transform.position = Vector3.Lerp(boxB_originPosition, boxB_targetPosition, (elapsedTime / moveIntervalTime));

                if (boxB.GetComponent<BoxLogic>().otherBoxTouchingRight && !boxA.GetComponent<BoxLogic>().wallTouchingRight && boxB.GetComponent<BoxLogic>().canPushOtherRight)
                {
                    boxA.transform.position = Vector3.Lerp(boxA_originPosition, boxA_targetPosition, elapsedTime / moveIntervalTime);
                }

                elapsedTime += Time.deltaTime;
                yield return null;
            }

            boxB.transform.position = boxB_targetPosition;
            if (boxB.GetComponent<BoxLogic>().otherBoxTouchingRight && !boxA.GetComponent<BoxLogic>().wallTouchingRight && boxB.GetComponent<BoxLogic>().canPushOtherRight)
            {
                boxA.transform.position = boxA_targetPosition;
            }

            boxB_script.boxIsMoving = false;
            boxB_moving = false;
        }
        else if (boxB.GetComponent<BoxLogic>().touchingOtherBox && direction == Vector3.left && !boxA.GetComponent<BoxLogic>().wallTouchingLeft && boxA.GetComponent<BoxLogic>().canMoveLeft)
        {
            boxB_script.boxIsMoving = true;
            boxB_moving = true;

            float elapsedTime = 0;

            boxB_originPosition = new Vector3((float)Math.Round(boxB.transform.position.x, 1), (float)Math.Round(boxB.transform.position.y, 1), 0);

            boxB_targetPosition = boxB_originPosition + direction;

            if (boxB.GetComponent<BoxLogic>().otherBoxTouchingLeft && !boxA.GetComponent<BoxLogic>().wallTouchingLeft && boxB.GetComponent<BoxLogic>().canPushOtherLeft)
            {
                boxA_originPosition = new Vector3((float)Math.Round(boxA.transform.position.x, 1), (float)Math.Round(boxA.transform.position.y, 1), 0);
                boxA_targetPosition = boxA_originPosition + direction;
            }

            while (elapsedTime < moveIntervalTime)
            {
                boxB.transform.position = Vector3.Lerp(boxB_originPosition, boxB_targetPosition, (elapsedTime / moveIntervalTime));

                if (boxB.GetComponent<BoxLogic>().otherBoxTouchingLeft && !boxA.GetComponent<BoxLogic>().wallTouchingLeft && boxB.GetComponent<BoxLogic>().canPushOtherLeft)
                {
                    boxA.transform.position = Vector3.Lerp(boxA_originPosition, boxA_targetPosition, elapsedTime / moveIntervalTime);
                }

                elapsedTime += Time.deltaTime;
                yield return null;
            }

            boxB.transform.position = boxB_targetPosition;

            if (boxB.GetComponent<BoxLogic>().otherBoxTouchingLeft && !boxA.GetComponent<BoxLogic>().wallTouchingLeft && boxB.GetComponent<BoxLogic>().canPushOtherLeft)
            {
                boxA.transform.position = boxA_targetPosition;
            }

            boxB_script.boxIsMoving = false;
            boxB_moving = false;
        }

        else if (boxB.GetComponent<BoxLogic>().touchingOtherBox)
        {
            if (boxB.GetComponent<BoxLogic>().canMoveRight && boxB.GetComponent<BoxLogic>().canMoveRight && direction == Vector3.right && !boxB.GetComponent<BoxLogic>().otherBoxTouchingRight)
            {
                boxB_script.boxIsMoving = true;
                boxB_moving = true;

                float elapsedTime = 0;

                boxB_originPosition = new Vector3((float)Math.Round(boxB.transform.position.x, 1), (float)Math.Round(boxB.transform.position.y, 1), 0);

                boxB_targetPosition = boxB_originPosition + direction;

                while (elapsedTime < moveIntervalTime)
                {
                    boxB.transform.position = Vector3.Lerp(boxB_originPosition, boxB_targetPosition, elapsedTime / moveIntervalTime);

                    elapsedTime += Time.deltaTime;
                    yield return null;
                }

                boxB.transform.position = boxB_targetPosition;

                boxB_script.boxIsMoving = false;
                boxB_moving = false;
            }
            else if (boxB.GetComponent<BoxLogic>().canMoveLeft && boxA.GetComponent<BoxLogic>().canMoveLeft && direction == Vector3.left && !boxB.GetComponent<BoxLogic>().otherBoxTouchingLeft)
            {
                boxB_script.boxIsMoving = true;
                boxB_moving = true;

                float elapsedTime = 0;

                boxB_originPosition = new Vector3((float)Math.Round(boxB.transform.position.x, 1), (float)Math.Round(boxB.transform.position.y, 1), 0);

                boxB_targetPosition = boxB_originPosition + direction;

                while (elapsedTime < moveIntervalTime)
                {
                    boxB.transform.position = Vector3.Lerp(boxB_originPosition, boxB_targetPosition, elapsedTime / moveIntervalTime);

                    elapsedTime += Time.deltaTime;
                    yield return null;
                }

                boxB.transform.position = boxB_targetPosition;

                boxB_script.boxIsMoving = false;
                boxB_moving = false;
            }

        }

        else if (!boxB.GetComponent<BoxLogic>().touchingOtherBox)
        {
            boxB_script.boxIsMoving = true;
            boxB_moving = true;

            float elapsedTime = 0;

            boxB_originPosition = new Vector3((float)Math.Round(boxB.transform.position.x, 1), (float)Math.Round(boxB.transform.position.y, 1), 0);

            boxB_targetPosition = boxB_originPosition + direction;

            while (elapsedTime < moveIntervalTime)
            {
                boxB.transform.position = Vector3.Lerp(boxB_originPosition, boxB_targetPosition, (elapsedTime / moveIntervalTime));

                elapsedTime += Time.deltaTime;
                yield return null;
            }

            boxB.transform.position = boxB_targetPosition;

            boxB_script.boxIsMoving = false;
            boxB_moving = false;

        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(boxB_spawnInt + new Vector3(0.5f, 0.5f, 0), new Vector3(1, 1, 0));
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxA_spawnInt + new Vector3(0.5f, 0.5f, 0), new Vector3(1, 1, 0));
    }

}


