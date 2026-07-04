using UnityEngine;

public class DisplayPanelDialogue : MonoBehaviour {

    void Start() {
        gameObject.SetActive(false);
    }

    public void Display()
    {
        gameObject.SetActive(true);
    }
    
    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
