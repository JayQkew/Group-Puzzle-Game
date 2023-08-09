using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    private void BoxMovement(GameObject box1, GameObject box2)
    {
        if (Input.GetKey(KeyCode.W) && !boxA_moving && boxA_script.canMoveUp)
        {
            StartCoroutine(MoveBox1(box1, Vector3.up));
            boxA_script.canMoveDown = true;
        }
        if (Input.GetKey(KeyCode.S) && !boxA_moving && boxA_script.canMoveDown)
        {
            StartCoroutine(MoveBox1(box1, Vector3.down));
            boxA_script.canMoveUp = true;
        }
        if (Input.GetKey(KeyCode.D) && !boxB_moving && boxB_script.canMoveRight)
        {
            StartCoroutine(MoveBox2(box2, Vector3.right));
            boxB_script.canMoveLeft = true;
        }
        if (Input.GetKey(KeyCode.A) && !boxB_moving && boxB_script.canMoveLeft)
        {
            StartCoroutine(MoveBox2(box2, Vector3.left));
            boxB_script.canMoveRight = true;
        }
    }

    public IEnumerator MoveBox1(GameObject box, Vector3 direction)
    {
        boxA_script.boxIsMoving = true;
        boxA_moving = true;

        float elapsedTime = 0;

        boxA_originPosition = box.transform.position;

        boxA_targetPosition = boxA_originPosition + direction;

        while (elapsedTime < moveIntervalTime)
        {
            box.transform.position = Vector3.Lerp(boxA_originPosition, boxA_targetPosition, (elapsedTime / moveIntervalTime));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        box.transform.position = boxA_targetPosition;

        boxA_script.boxIsMoving = false;
        boxA_moving = false;
    }
    public IEnumerator MoveBox2(GameObject box, Vector3 direction)
    {
        boxB_script.boxIsMoving = true;
        boxB_moving = true;

        float elapsedTime = 0;

        boxB_originPosition = box.transform.position;

        boxB_targetPosition = boxB_originPosition + direction;

        while (elapsedTime < moveIntervalTime)
        {
            box.transform.position = Vector3.Lerp(boxB_originPosition, boxB_targetPosition, (elapsedTime / moveIntervalTime));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        box.transform.position = boxB_targetPosition;

        boxB_script.boxIsMoving = false;
        boxB_moving = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(boxB_spawnInt + new Vector3(0.5f, 0.5f, 0), new Vector3(1, 1, 0));
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxA_spawnInt + new Vector3(0.5f, 0.5f, 0), new Vector3(1, 1, 0));
    }
}
