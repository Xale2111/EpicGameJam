using UnityEngine;
using TMPro;

public class DialogueDisplayer : MonoBehaviour {
    [SerializeField] string _DialogueContent;
    
    TMP_Text _dialogueHolder;

    GameObject _panel;


    void Start() {
        _panel = DisplayPanelDialogue._instance.gameObject;
        _dialogueHolder = _panel.GetComponentInChildren<TMP_Text>();
    }


    private void OnTriggerEnter2D(Collider2D collision) {
        if(!collision.CompareTag("Player"))
            return;

        _dialogueHolder.text = _DialogueContent;
        _panel.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if(!collision.CompareTag("Player"))
            return;

        _dialogueHolder.text = "";
        _panel.SetActive(false);
    }
}