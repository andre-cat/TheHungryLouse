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
        Colorize(up, KeyCode.UpArrow, KeyCode.W);
        Colorize(down, KeyCode.DownArrow, KeyCode.S);
        Colorize(left, KeyCode.LeftArrow, KeyCode.A);
        Colorize(right, KeyCode.RightArrow, KeyCode.D);
        Colorize(space, KeyCode.Space, KeyCode.Space);
    }

    private void Colorize(Image image, KeyCode key, KeyCode altKey)
    {
        if (Input.GetKey(key) | Input.GetKey(altKey))
        {
            image.color = pressColor;
        }
        else
        {
            image.color = usualColor;
        }
    }
}
