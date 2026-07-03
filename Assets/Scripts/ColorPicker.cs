using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ColorPicker : MonoBehaviour
{
    [SerializeField] DrawingPad  drawingPad;
    [SerializeField] Color color;
    [SerializeField] Image image;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        image.color = color;
    }

    public void SelectColor()
    {
        drawingPad.ChangeBrushColor(color);
    }
}
