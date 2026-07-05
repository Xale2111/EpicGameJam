using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public struct AnimalHelp
{
    public Animals animal;
    public Sprite blurryImage;
    public string description;
}

[Serializable]
public struct DrawnElementHelp
{
    public DrawnElement element;
    public Sprite blurryImage;
    public string description;
}

[Serializable]
public struct DrawnHabitatHelp {
    public Animals animal;
    public Sprite blurryImage;
    public string description;
}

public class HelpPanel : MonoBehaviour
{
    public static HelpPanel Instance;
    
    [SerializeField] private GameObject _panel;
    [SerializeField] private Image _helpImage;
    [SerializeField] private TextMeshProUGUI _helpText;
    [SerializeField] private AnimalHelp[] _animalHelp;
    [SerializeField] private DrawnElementHelp[] _drawnElementHelp;
    [SerializeField] private DrawnHabitatHelp[] _drawnHabitatHelp;


    public void ShowPanel() {
        _panel.SetActive(true);
    }
    public void HidePanel() {
        _panel.SetActive(false);
    }

    public void ChangeHelpAnimal(Animals helpAnimal)
    {
        int temp = _animalHelp.Select((helpAnimal, index) => new { Animal = helpAnimal.animal, Index = index })
            .Where(a => a.Animal == helpAnimal).Select(a => a.Index).First();

        if (_animalHelp[temp].animal != Animals.NONE)
        {
            _helpImage.sprite = _animalHelp[temp].blurryImage;
            _helpText.text = _animalHelp[temp].description;
        }
    }

    public void ChangeHelpElement(DrawnElement helpElement)
    {
        int temp = _drawnElementHelp.Select((helpElement, index) => new { Element = helpElement.element, Index = index })
            .Where(a => a.Element == helpElement).Select(a => a.Index).First();

        if (_drawnElementHelp[temp].element != DrawnElement.NONE)
        {
            _helpImage.sprite = _drawnElementHelp[temp].blurryImage;
            _helpText.text = _drawnElementHelp[temp].description;
        }
    }

    public void ChangeHelpHabitat(Animals helpAnimal) {
        int temp = _drawnHabitatHelp.Select((drawnHabitatHelp, index) => new { Animal = drawnHabitatHelp.animal, Index = index })
            .Where(a => a.Animal == helpAnimal).Select(a => a.Index).First();

        if(_drawnHabitatHelp[temp].animal != Animals.NONE) {
            _helpImage.sprite = _drawnHabitatHelp[temp].blurryImage;
            _helpText.text = _drawnHabitatHelp[temp].description;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        HidePanel();
    }
}
