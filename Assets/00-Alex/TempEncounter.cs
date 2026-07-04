using UnityEngine;

public class TempEncounter : EventTriggerer {
    [SerializeField] Animals _animal;
    [SerializeField] DrawnCreatureSpawner drawingManager;

    public override void EventTrigger()
    {
        Debug.Log("I FOUND A NEW FRIEND");
        drawingManager.ShowPanel();
        GameManager._instance._isPlayerDrawing = true;
        gameObject.SetActive(false);
    }


}