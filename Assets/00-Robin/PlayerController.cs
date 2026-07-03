using UnityEngine;

public class PlayerController : MonoBehaviour {
    [SerializeField] Transform _playerVisual;

    [SerializeField, Range(0f, 5f)]float _speed = 1f;

    void Update() {
        Vector2 input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;

        transform.Translate(input * _speed * Time.deltaTime);
    }
}