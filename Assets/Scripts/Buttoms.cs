using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Buttoms : MonoBehaviour
{
    [SerializeField] Image up;
    [SerializeField] Image down;
    [SerializeField] Image left;
    [SerializeField] Image right;
    [SerializeField] Image space;

    [SerializeField] private Color usualColor;
    [SerializeField] private Color pressColor;

    private void FixedUpdate()
    {
        Colorize(up, KeyCode.UpArrow);
        Colorize(down, KeyCode.DownArrow);
        Colorize(left, KeyCode.LeftArrow);
        Colorize(right, KeyCode.RightArrow);
        Colorize(space, KeyCode.Space);
    }

    private void Colorize(Image image, KeyCode keyCode)
    {
        if (Input.GetKey(keyCode))
        {
            image.color = pressColor;
        }
        else
        {
            image.color = usualColor;
        }
    }
}
