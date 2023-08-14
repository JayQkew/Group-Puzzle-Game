using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockTriggerLogic : MonoBehaviour
{
    [SerializeField] private BoxType boxType = new BoxType();
    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (boxType)
        {
            case BoxType.A:
                if (collision.GetComponent<BoxLogic>().boxType == boxType)
                {
                    EndDestinationManager.Instance.boxA_OnDestination = true;
                }
                break;
            case BoxType.B:
                if (collision.GetComponent<BoxLogic>().boxType == boxType)
                {
                    EndDestinationManager.Instance.boxB_OnDestination = true;
                }
                break;
            default:
                break;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        switch (boxType)
        {
            case BoxType.A:
                if (collision.GetComponent<BoxLogic>().boxType == boxType)
                {
                    EndDestinationManager.Instance.boxA_OnDestination = false;
                }
                break;
            case BoxType.B:
                if (collision.GetComponent<BoxLogic>().boxType == boxType)
                {
                    EndDestinationManager.Instance.boxB_OnDestination = false;
                }
                break;
            default:
                break;
        }
    }
}
