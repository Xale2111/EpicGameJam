using UnityEngine;

public class PlayerController : MonoBehaviour {
    [SerializeField] Transform _playerVisual;

    [SerializeField] public SpriteRenderer _playerBody;
    [SerializeField] public SpriteRenderer _playerHat;

    [SerializeField, Range(0f, 10f)] float _speed = 7f;
    [SerializeField] float _bobbingAngle = 25f;
    float _currentAngle = 0f;
    float _angleModifier = 1f;
    bool _sprinting = false;
    void Update() {
        if(GameManager._instance._isPlayerDrawing) {
            return;
        }

        if(Input.GetKeyDown(KeyCode.LeftShift)) {
            _sprinting = true;
        }
        else if(Input.GetKeyUp(KeyCode.LeftShift)) {
            _sprinting = false;
        }

        Vector2 input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;

        transform.Translate(input * _speed * Time.deltaTime * (_sprinting ? 2f : 1f));

        if(input.sqrMagnitude > 0f) {
            if(_currentAngle == _bobbingAngle * _angleModifier) {
                _angleModifier *= -1f;
            }
            else {
                _currentAngle = Mathf.Lerp(_currentAngle, _bobbingAngle * _angleModifier, 0.1f * (_sprinting ? 2f : 1f));
                if(Mathf.Abs(_currentAngle - _bobbingAngle * _angleModifier) < 0.1f) {
                    _currentAngle = _bobbingAngle * _angleModifier;
                }
                _playerVisual.rotation = Quaternion.Euler(0f, 0f, _currentAngle);
            }
        }
        else {
            _playerVisual.rotation = Quaternion.Lerp(_playerVisual.rotation, Quaternion.identity, 0.1f);
            _currentAngle = 0f;
        }
    }
}