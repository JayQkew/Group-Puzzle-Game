using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class PlayerController : MonoBehaviour
{
    #region Game Objects:
    [Header("Boxes")]
    [SerializeField] public GameObject box_A;
    [SerializeField] public GameObject box_B;

    private Vector3 box1_originPosition;
    private Vector3 box2_originPosition;

    private Vector3 box1_targetPosition;
    private Vector3 box2_targetPosition;

    [SerializeField] public bool box1_moving;
    [SerializeField] public bool box2_moving;

    private Vector3 boxA_spawn;
    private Vector3 boxB_spawn;

    [Header("Boxes Spawn")]
    [SerializeField] private Vector3Int boxA_spawnInt;
    [SerializeField] private Vector3Int boxB_spawnInt;
    #endregion

    [SerializeField] private Grid grid;
    [SerializeField] private GridLayout gridLayout;
    [SerializeField] private float moveIntervalTime; // grid size

    private void Start()
    {
        boxA_spawn = gridLayout.GetLayoutCellCenter() + grid.CellToWorld(boxA_spawnInt);
        boxB_spawn = gridLayout.GetLayoutCellCenter() + grid.CellToWorld(boxB_spawnInt);

        box_A.transform.position = boxA_spawn;
        box_B.transform.position = boxB_spawn;
    }
    private void Update()
    {
        BoxMovement(box_A, box_B);
    }

    private void BoxMovement(GameObject box1, GameObject box2)
    {
        if (Input.GetKey(KeyCode.W) && !box1_moving)
        {
            StartCoroutine(MoveBox1(box1, Vector3.up));
        }
        if (Input.GetKey(KeyCode.S) && !box1_moving)
        {
            StartCoroutine(MoveBox1(box1, Vector3.down));
        }
        if (Input.GetKey(KeyCode.D) && !box2_moving)
        {
            StartCoroutine(MoveBox2(box2, Vector3.right));
        }
        if (Input.GetKey(KeyCode.A) && !box2_moving)
        {
            StartCoroutine(MoveBox2(box2, Vector3.left));
        }
    }

    private IEnumerator MoveBox1(GameObject box, Vector3 direction)
    {
        box1_moving = true;

        float elapsedTime = 0;

        box1_originPosition = box.transform.position;

        box1_targetPosition = box1_originPosition + direction;

        while (elapsedTime < moveIntervalTime)
        {
            box.transform.position = Vector3.Lerp(box1_originPosition, box1_targetPosition, (elapsedTime / moveIntervalTime));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        box.transform.position = box1_targetPosition;

        box1_moving = false;
    }
    private IEnumerator MoveBox2(GameObject box, Vector3 direction)
    {
        box2_moving = true;

        float elapsedTime = 0;

        box2_originPosition = box.transform.position;

        box2_targetPosition = box2_originPosition + direction;

        while (elapsedTime < moveIntervalTime)
        {
            box.transform.position = Vector3.Lerp(box2_originPosition, box2_targetPosition, (elapsedTime / moveIntervalTime));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        box.transform.position = box2_targetPosition;

        box2_moving = false;
    }
}
