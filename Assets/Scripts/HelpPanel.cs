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

public class HelpPanel : MonoBehaviour
{
    public static HelpPanel Instance;
    
    [SerializeField] private GameObject _panel;
    [SerializeField] private Image _helpImage;
    [SerializeField] private TextMeshProUGUI _helpText;
    [SerializeField] private AnimalHelp[] _animalHelp;
    
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
