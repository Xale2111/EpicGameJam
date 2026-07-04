using UnityEngine;
using TMPro;

public class DialogueDisplayer : MonoBehaviour {
    [SerializeField] string _DialogueContent;
    
    TMP_Text _dialogueHolder;

    GameObject _panel;

    void Awake() {
        _panel = FindAnyObjectByType<DisplayPanelDialogue>().gameObject;
        _dialogueHolder = _panel.GetComponentInChildren<TMP_Text>();

        
    }


    private void OnTriggerEnter2D(Collider2D collision) {
        _dialogueHolder.text = _DialogueContent;
        _panel.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D collision) {
        _dialogueHolder.text = "";
        _panel.SetActive(false);
    }
}