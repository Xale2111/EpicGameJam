using UnityEngine;

public class TempEncounter : EventTriggerer {
    [SerializeField] Animals _animal;
    

    public override void EventTrigger()
    {
        Debug.Log("I FOUND A NEW FRIEND");
        DrawnCreatureSpawner._instance.ShowPanel();
        GameManager._instance._isPlayerDrawing = true;
        gameObject.SetActive(false);
    }


}