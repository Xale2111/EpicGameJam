using System;
using TMPro;
using Unity.Cinemachine;
using UnityEngine;

enum BobState
{
    Hat,
    Tshirt,
    Pants,
    Shoes,
    Circus
}

[System.Serializable]
public struct BobPosition
{
    public Vector2 position;
    public bool beenInPosition;
}

public class BobManager : MonoBehaviour
{
    [SerializeField] private GameObject hat;
    [SerializeField] private GameObject tshirt;
    [SerializeField] private GameObject pants;
    [SerializeField] private GameObject shoes;
    [SerializeField] private GameObject feet;

    [Header("Bob positions")][Space(10)]
    [SerializeField] private BobPosition[] positions;
    [SerializeField] private Transform circusTpPoint;
    
    [Header("Dialogs")][Space(10)]
    [SerializeField] private string[] dialogLines;
    [SerializeField] private TextMeshProUGUI dialogueText;
    
    Transform player;
    
    int currentPosition = 0;
    int currentDialogLine = 0;
    
    bool isInCircus = false;
    
    BobState currentBobState = BobState.Hat;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (positions.Length > 0)
        {
            transform.position = positions[currentPosition].position;
        }
        hat.SetActive(true);
        tshirt.SetActive(true);
        pants.SetActive(true);
        shoes.SetActive(true);
        feet.SetActive(false);
        
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void NextOutfit()
    {
        switch (currentBobState)
        {
            case BobState.Hat:
                HideHat();
                break;
            case BobState.Tshirt:
                HideTshirt();
                break;
            case BobState.Pants:
                HidePants();
                break;
            case BobState.Shoes:
                HideShoes();
                ShowFeet();
                break;
            case BobState.Circus:
                transform.position = circusTpPoint.position;
                isInCircus = true;
                break;
            default:
                break;
        }

        currentBobState++;
        currentDialogLine++;
        positions[currentPosition].beenInPosition = true;
        
    }

    private void Update()
    {
        if (isInCircus)
            return;
        int closestPositionIndex = positions.Length - 1;

        foreach (BobPosition position in positions)
        {
            if (!position.beenInPosition)
            {
                closestPositionIndex = Array.IndexOf(positions, position);
                break;
            }
        }

        foreach (BobPosition position in positions)
        {
            if (Vector2.Distance(player.position, position.position) < Vector2.Distance(player.position, positions[closestPositionIndex].position))
            {
                if (!position.beenInPosition)
                {
                    closestPositionIndex = Array.IndexOf(positions, position);
                }
            }
        }
        currentPosition = closestPositionIndex;
        transform.position = positions[closestPositionIndex].position;
    }

    public void SetDialogLine()
    {
        dialogueText.text = dialogLines[currentDialogLine];
    }

    public void HideHat()
    {
        hat.SetActive(false);
    }
    public void HideTshirt()
    {
        tshirt.SetActive(false);
    }
    public void HidePants()
    {
        pants.SetActive(false);
    }

    public void HideShoes()
    {
        shoes.SetActive(false);
    }
    
    public void ShowFeet()
    {
        feet.SetActive(true);
    }
}
