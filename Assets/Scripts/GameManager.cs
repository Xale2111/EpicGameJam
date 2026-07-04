using UnityEngine;

public class GameManager : MonoBehaviour {
    public DrawingDataSaver _drawingDataSaver;

    public static GameManager _instance;

    public bool _isPlayerDrawing = false;

    Animals _currentAnimalBeingDrawn = Animals.NONE;

    DrawnElement _currentDrawnElement = DrawnElement.NONE;

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

    public void ElementDrawn(DrawnElement element, Animals animal) {
        _currentAnimalBeingDrawn = animal;
        _currentDrawnElement = element;
    }

    public void SaveDrawing(Sprite spriteToSave) {
        switch(_currentDrawnElement) {
            case DrawnElement.BODY:
            _drawingDataSaver._animals[(int)_currentAnimalBeingDrawn]._body = spriteToSave;
            break;
            case DrawnElement.HABITAT:
            _drawingDataSaver._animals[(int)_currentAnimalBeingDrawn]._habitat = spriteToSave;
            break;
            case DrawnElement.FOOD:
            _drawingDataSaver._animals[(int)_currentAnimalBeingDrawn]._food = spriteToSave;
            break;
            case DrawnElement.CHAPITEAU:
            _drawingDataSaver._chapiteau = spriteToSave;
            break;
            case DrawnElement.NONE:
            break;
        }
    }
}