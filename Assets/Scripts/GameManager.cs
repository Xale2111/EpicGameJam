using UnityEngine;

public class GameManager : MonoBehaviour {
    public DrawingDataSaver _drawingDataSaver;

    public static GameManager _instance;

    public bool _isPlayerDrawing = false;

    void Start() {
        if(_instance == null) {
            _instance = this;
        }
        else {
            Destroy(gameObject);
        }

        _drawingDataSaver = new DrawingDataSaver();
        _drawingDataSaver.Initialize();
    }


}