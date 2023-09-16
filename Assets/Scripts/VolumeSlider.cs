using System.Collections;
using System.Collections.Generic;
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
        HungryLouse.audioSource.volume = slider.value;
        HungryLouse.Volume = HungryLouse.audioSource.volume;
    }
}
