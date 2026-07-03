using UnityEngine;

public class EventTriggerer : MonoBehaviour {
    [Header("Default Params")] 
    [SerializeField] LayerMask _playerLayerMask;

    public virtual void EventTrigger() {

    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if((_playerLayerMask & (1 << collision.gameObject.layer)) != 0) {
            EventTrigger();
        }
    }
}