using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager _instance;

    public bool _isPlayerDrawing = false;

    void Start() {
        if(_instance == null) {
            _instance = this;
        }
        else {
            Destroy(gameObject);
        }
    }
}