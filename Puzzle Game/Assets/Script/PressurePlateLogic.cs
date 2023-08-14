using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlateLogic : MonoBehaviour
{
    [SerializeField] public ColourSpecificColours colourRequired = new ColourSpecificColours();
    [SerializeField] public bool onPressurePlate;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "box")
        {
            switch (colourRequired)
            {
                case ColourSpecificColours.None:
                    onPressurePlate = true;
                    break;
                case ColourSpecificColours.Blue:
                    if (collision.GetComponent<BoxLogic>().boxType == BoxType.B)
                    {
                        onPressurePlate = true;
                    }
                    break;
                case ColourSpecificColours.Red:
                    if (collision.GetComponent<BoxLogic>().boxType == BoxType.A)
                    {
                        onPressurePlate = true;
                    }
                    break;
            }
        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "box")
        {
            switch (colourRequired)
            {
                case ColourSpecificColours.None:
                    onPressurePlate = false;
                    break;
                case ColourSpecificColours.Blue:
                    if (collision.GetComponent<BoxLogic>().boxType == BoxType.B)
                    {
                        onPressurePlate = false;
                    }
                    break;
                case ColourSpecificColours.Red:
                    if (collision.GetComponent<BoxLogic>().boxType == BoxType.A)
                    {
                        onPressurePlate = false;
                    }
                    break;
            }
        }
    }
}

public enum PressurePlateType
{
    Standard,
    ColourSpecific
}

public enum ColourSpecificColours
{
    None,
    Red,
    Blue
}
