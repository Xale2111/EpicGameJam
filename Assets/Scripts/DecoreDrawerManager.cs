using UnityEngine;

public class DecoreDrawerManager : MonoBehaviour {
    public static DecoreDrawerManager _instance;

    DrawingPad _drawingPad;

    DrawnElement _element = DrawnElement.NONE;
    Animals _animal = Animals.NONE;
    Vector3 _position = Vector3.zero;

    [SerializeField] GameObject drawingPanel;
    GameObject _prefab;

    [SerializeField] GameObject _elementButton;
    [SerializeField] GameObject _animalButton;

    private void Start() {
        if(_instance == null) {
            _instance = this;
        }
        else {
            Destroy(gameObject);
        }

        _drawingPad = drawingPanel.GetComponentInChildren<DrawingPad>();
    }

    public void ValidateDrawing() {
        if(!_drawingPad.HasEnoughContent()) return;

        GameManager._instance.ElementDrawn(_element, _animal);

        Sprite sprite = _drawingPad.CreateSprite();
        SpawnNewElement(sprite); 

        _drawingPad.ClearCanvas();
        HidePanel();
        GameManager._instance._isPlayerDrawing = false;
    }

    void SpawnNewElement(Sprite sprite) {
        GameObject element = Instantiate(_prefab, _position, Quaternion.identity);

        element.GetComponent<SpriteRenderer>().sprite = sprite;
    }

    public void ShowPanel() {
        drawingPanel.SetActive(true);
    }

    void HidePanel() {
        drawingPanel.SetActive(false);
    }


    public void SetDrawingObjective(DrawnElement element, Animals animal, Vector3 pos, GameObject prefab) {
        _element = element;
        _animal = animal;
        _position = pos;
        _prefab = prefab;
    }

    public void EnableElementbuildingButton() {
        _animalButton.SetActive(false);
        _elementButton.SetActive(true);
    }
    
}
