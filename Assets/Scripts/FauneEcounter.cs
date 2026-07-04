using UnityEngine;

public class FauneEcounter : EventTriggerer {
    [SerializeField] Animals _animalToEncounter = Animals.NONE;
    bool _isTrigger = false;

    public override void EventTrigger() {
        if(_isTrigger)
            return;

        GameManager._instance._isPlayerDrawing = true;
        GameManager._instance.ElementDrawn(DrawnElement.BODY, _animalToEncounter);
        _isTrigger = true;
    }
}