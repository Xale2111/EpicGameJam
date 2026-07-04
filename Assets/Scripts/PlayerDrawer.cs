using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDrawer : MonoBehaviour{
    [Header("Références")]
    public GameObject drawingPanel;
    public DrawingPad drawingPad;
    [SerializeField] RawImage _playerDisplay;
    [SerializeField] GameObject _bodyTemplate;

    SpriteRenderer _playerBodyRenderer;
    SpriteRenderer _playerHatRenderer;

    [SerializeField] GameObject _bodyText;
    [SerializeField] GameObject _hatText;

    bool _isBodyDrawn = false;

    [SerializeField] GameObject playerDrawingCanevas;

    private void Start() {
        GameManager._instance._isPlayerDrawing = true;
        PlayerController player = FindAnyObjectByType<PlayerController>();
        _playerBodyRenderer = player._playerBody;
        _playerHatRenderer = player._playerHat;
    }

    void Update() {
        if(_isBodyDrawn)
            _playerDisplay.rectTransform.anchoredPosition = Vector2.Lerp(_playerDisplay.rectTransform.anchoredPosition, new Vector2(0f, -384f), 0.05f);
    }

    /// <summary>À appeler depuis le bouton "Valider" du popup.</summary>
    public void ValidateDrawing() {
        if(!drawingPad.HasEnoughContent()) return;

        if(!_isBodyDrawn) {
            GameManager._instance.ElementDrawn(DrawnElement.PLAYER_BODY, Animals.NONE);
        }
        else {
            GameManager._instance.ElementDrawn(DrawnElement.PLAYER_HAT, Animals.NONE);
        }

        Sprite sprite;

        if(_isBodyDrawn) {
            GameManager._instance._isPlayerDrawing = false;
            HidePanel();
            sprite = drawingPad.CreateSprite();
            SetPlayerPart(sprite);

            Destroy(playerDrawingCanevas.gameObject);
        }
        else {
            Tuple<Sprite, Texture2D> tuple = drawingPad.GetSpriteAndTexture();
            sprite = tuple.Item1;
            _playerDisplay.gameObject.SetActive(true);
            _playerDisplay.texture = tuple.Item2;

            SetPlayerPart(sprite);

            _hatText.SetActive(true);
            _bodyText.SetActive(false);
            _bodyText.SetActive(false);
        }

        drawingPad.ClearCanvas();
    }

    private void SetPlayerPart(Sprite sprite) {
        if(!_isBodyDrawn) {
            _playerBodyRenderer.sprite = sprite;
            _isBodyDrawn = true;
        }
        else {
            _playerHatRenderer.sprite = sprite;
        }
    }

    public void ShowPanel() {
        drawingPanel.SetActive(true);
    }

    public void HidePanel() {
        drawingPanel.SetActive(false);
    }
}
