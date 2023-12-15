using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Scrollbar))]
public class SlidingScrollView : MonoBehaviour
{

    [SerializeField] private float speed;

    private Scrollbar scrollbar;

    private void Start()
    {
        scrollbar = gameObject.GetComponent<Scrollbar>();
        scrollbar.value = 1;
    }

    private void FixedUpdate()
    {
        if (scrollbar.value <= 0)
        {
            scrollbar.value = 1;
        }
        else
        {
            scrollbar.value -= speed;
        }
    }
}
