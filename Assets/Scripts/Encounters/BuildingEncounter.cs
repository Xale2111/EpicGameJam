using UnityEngine;

public class BuildingEncounter : EventTriggerer {
    [SerializeField] DrawnElement _element;
    [SerializeField] Animals _animal;
    [SerializeField] GameObject _prefab;

    public override void EventTrigger() {
        DecoreDrawerManager._instance.SetDrawingObjective(_element, _animal, transform.position, _prefab);
        DecoreDrawerManager._instance.ShowPanel();
        DecoreDrawerManager._instance.EnableElementbuildingButton();

        GameManager._instance._isPlayerDrawing = true;

        Destroy(gameObject);
    }
}