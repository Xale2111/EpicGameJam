using UnityEngine;

public class PlayerController : MonoBehaviour {
    [SerializeField] Transform _playerVisual;

    [SerializeField, Range(0f, 5f)] float _speed = 1f;
    [SerializeField, Range(0f, 5f)] float _bobbingSpeed = 1f;
    [SerializeField] float _bobbingAngle = 25f;
    float _currentAngle = 0f;
    float _angleModifier = 1f;

    void Update() {
        Vector2 input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;

        transform.Translate(input * _speed * Time.deltaTime);

        if(input.sqrMagnitude > 0f) {
            if(_currentAngle == _bobbingAngle * _angleModifier) {
                _angleModifier *= -1f;
            }
            else {
                _currentAngle = Mathf.Lerp(_currentAngle, _bobbingAngle * _angleModifier, 0.1f);
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