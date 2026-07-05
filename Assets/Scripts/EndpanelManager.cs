using UnityEngine;

public class EndpanelManager : MonoBehaviour {
    public static EndpanelManager _instance;

    [SerializeField] GameObject _endPanelObject;

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
        Destroy(_endPanelObject, 120f);
    }
}