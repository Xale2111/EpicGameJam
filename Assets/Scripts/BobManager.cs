using Unity.Cinemachine;
using UnityEngine;

public class BobManager : MonoBehaviour
{
    [SerializeField] private GameObject hat;
    [SerializeField] private GameObject tshirt;
    [SerializeField] private GameObject pants;
    [SerializeField] private GameObject shoes;
    [SerializeField] private GameObject feet;

    [SerializeField] private Transform[] positions;
    
    
    int currentPosition = 0;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (positions.Length > 0)
        {
            transform.position = positions[currentPosition].position;
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
            transform.position = positions[currentPosition].position;
            currentPosition++;
        }
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
