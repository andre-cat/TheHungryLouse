using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class VolumeSlider : MonoBehaviour
{
    private Slider slider;

    void Awake()
    {
        slider = gameObject.GetComponent<Slider>();
    }

    void Start()
    {
        slider.value = HungryLouse.Volume;
    }

    void Update()
    {
        SetVolume();
    }

    private void SetVolume()
    {
        HungryLouse.Volume = slider.value;
    }
}
