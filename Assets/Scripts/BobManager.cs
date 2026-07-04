using Unity.Cinemachine;
using UnityEngine;

enum BobState
{
    Hat,
    Tshirt,
    Pants,
    Shoes,
}

public class BobManager : MonoBehaviour
{
    [SerializeField] private GameObject hat;
    [SerializeField] private GameObject tshirt;
    [SerializeField] private GameObject pants;
    [SerializeField] private GameObject shoes;
    [SerializeField] private GameObject feet;

    [SerializeField] private Vector2[] positions;
    
    
    int currentPosition = 0;
    
    BobState currentBobState = BobState.Hat;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (positions.Length > 0)
        {
            transform.position = positions[currentPosition];
            currentPosition++;
        }
        hat.SetActive(true);
        tshirt.SetActive(true);
        pants.SetActive(true);
        shoes.SetActive(true);
        feet.SetActive(false);
    }

    public void NextPosition()
    {
        if (currentPosition < positions.Length)
        {
            transform.position = positions[currentPosition];
            currentPosition++;
        }

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
            
            default:
                break;
        }

        currentBobState++;
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
