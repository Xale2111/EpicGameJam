using UnityEngine;
using UnityEngine.UI;

public class ConfetyCanon : MonoBehaviour {

    [SerializeField] float _cooldownTime = 0.6f;

    float _lastFallTime = 0f;

    [SerializeField] GameObject _fallingObjectPrefab;

    float _startingHeight = 512f;

    float _width = 1400f;

    void FixedUpdate() {
        if(Time.time < _lastFallTime + _cooldownTime)
            return;
        
        Sprite sprite = GameManager._instance._drawingDataSaver.GetRandomSprite();
        Vector2 pos = new Vector2(_startingHeight, Random.Range(-_width / 2f, _width / 2f));

        GameObject particule = Instantiate(_fallingObjectPrefab);

        particule.transform.parent = GetComponentInParent<RectTransform>();

        particule.GetComponent<RectTransform>().anchoredPosition = pos;

        particule.GetComponent<Image>().sprite = sprite;

        Destroy(particule, 6f);
    }
}