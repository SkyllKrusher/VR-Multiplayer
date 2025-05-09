using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;

public class Lever : MonoBehaviour
{
    [SerializeField]
    private GameObject leverOnBorder;
    [SerializeField]
    private GameObject leverOffBorder;
    [SerializeField]
    private Renderer indicatorRenderer;
    [SerializeField]
    private Color onColor = Color.green;
    [SerializeField]
    private Color offColor = Color.red;
    [SerializeField]
    private UnityEvent onLeverOn;
    [SerializeField]
    private UnityEvent onLeverOff;

    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        if (other.gameObject == leverOffBorder)
        {
            LeverOff();
        }
        else if (other.gameObject == leverOnBorder)
        {
            LeverOn();
        }
    }

    private void SetIndicatorColor(Color newColor)
    {
        indicatorRenderer.material.color = newColor;
        // float intensity = 2f;
        indicatorRenderer.material.SetColor("_EmissionColor", newColor);// * Mathf.GammaToLinearSpace(intensity));
    }

    private void LeverOn()
    {
        Debug.Log("On");
        SetIndicatorColor(onColor);
        onLeverOn?.Invoke();
    }

    private void LeverOff()
    {
        Debug.Log("Off");
        SetIndicatorColor(offColor);
        onLeverOff?.Invoke();
    }
}
