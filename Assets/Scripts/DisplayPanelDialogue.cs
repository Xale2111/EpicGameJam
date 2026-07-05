using UnityEngine;

public class DisplayPanelDialogue : MonoBehaviour {
    public static DisplayPanelDialogue _instance;

    void Awake() {
        if(_instance == null) {
            _instance = this;
        }
        else {
            Destroy(gameObject);
        }
    }

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
