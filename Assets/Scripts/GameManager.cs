using UnityEngine;

public class GameManager : MonoBehaviour {
    public DrawingDataSaver _drawingDataSaver;

    public static GameManager _instance;

    public bool _isPlayerDrawing = false;

    Animals _currentAnimalBeingDrawn = Animals.NONE;

    DrawnElement _currentDrawnElement = DrawnElement.NONE;

    void Awake() {
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
            case DrawnElement.PLAYER_BODY:
            _drawingDataSaver._playerBody = spriteToSave;
            break;
            case DrawnElement.PLAYER_HAT:
            _drawingDataSaver._playerHat = spriteToSave;
            break;
        }
    }

    public Sprite GetDrawing(DrawnElement element, Animals animal) {
        switch(_currentDrawnElement) {
            case DrawnElement.BODY:
            return _drawingDataSaver._animals[(int)_currentAnimalBeingDrawn]._body;

            case DrawnElement.HABITAT:
            return _drawingDataSaver._animals[(int)_currentAnimalBeingDrawn]._habitat;

            case DrawnElement.FOOD:
            return _drawingDataSaver._animals[(int)_currentAnimalBeingDrawn]._food;

            case DrawnElement.CHAPITEAU:
            return _drawingDataSaver._chapiteau;

            case DrawnElement.PLAYER_BODY:
            return _drawingDataSaver._playerBody;

            case DrawnElement.PLAYER_HAT:
            return _drawingDataSaver._playerHat;
        }

        return null;
    }
}