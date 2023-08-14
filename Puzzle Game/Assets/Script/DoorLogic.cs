using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorLogic : MonoBehaviour
{
    [SerializeField] private DoorType doorType = new DoorType();
    [SerializeField] private PressurePlateLogic _pressurePlateLogic1;
    [SerializeField] private PressurePlateLogic _pressurePlateLogic2;
    [SerializeField] private bool doorOpen = false;
    private void Update()
    {
        switch (doorOpen)
        {
            case true:
                switch (doorType)
                {
                    case DoorType.Continuous:
                        if (!_pressurePlateLogic1.onPressurePlate)
                        {
                            doorOpen = false;
                            gameObject.GetComponent<BoxCollider2D>().enabled = true;
                        }
                        break;
                    default:
                        break;
                }
                break;
            case false:
                switch (doorType)
                {
                    case DoorType.Static:
                        if (_pressurePlateLogic1.onPressurePlate)
                        {
                            doorOpen = true;
                            gameObject.GetComponent<BoxCollider2D>().enabled = false;
                            gameObject.GetComponent<SpriteRenderer>().color = Color.clear;
                        }
                        break;
                    case DoorType.Continuous:
                        if (_pressurePlateLogic1.onPressurePlate)
                        {
                            doorOpen = true;
                            gameObject.GetComponent <BoxCollider2D>().enabled = false;
                            gameObject.GetComponent<SpriteRenderer>().color = Color.clear;
                        }
                        break;
                    case DoorType.Double:
                        if (_pressurePlateLogic1.onPressurePlate && _pressurePlateLogic2.onPressurePlate)
                        {
                            doorOpen = true;
                            gameObject.GetComponent<BoxCollider2D>().enabled = false;
                            gameObject.GetComponent<SpriteRenderer>().color = Color.clear;

                        }
                        break;
                    default:
                        break;
                }
                break;
        }
    }
}

public enum DoorType
{
    Static,
    Continuous,
    Double
}
