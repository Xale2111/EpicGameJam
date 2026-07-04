using UnityEngine;

public class FauneEcounter : EventTriggerer {
    [SerializeField] float EncounterProba = 30f;


    public override void EventTrigger() {
        float result = Random.Range(0f, 100f);

        if(result < EncounterProba) {
            Debug.Log("I FOUND A NEW FRIEND");

            GameManager._instance._isPlayerDrawing = true;
        }
    }

    
}