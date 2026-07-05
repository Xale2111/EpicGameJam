using UnityEngine;

public class EndpanelManager : MonoBehaviour {
    public static EndpanelManager _instance;

    [SerializeField] GameObject _endPanelObject;
    [SerializeField] GameObject _confettiCannon;

    void Awake() {
        if(_instance == null) {
            _instance = this;
        }
        else {
            Destroy(gameObject);
        }
    }

    public void EndGame() {
        _endPanelObject.SetActive(true);
        _confettiCannon.SetActive(true);
    }
}