using UnityEngine;

public class FauneEcounter : EventTriggerer {
    [SerializeField] Animals _animalToEncounter = Animals.NONE;

    public override void EventTrigger() {
        GameManager._instance._isPlayerDrawing = true;
        GameManager._instance.ElementDrawn(DrawnElement.BODY, _animalToEncounter);

        HelpPanel.Instance.ChangeHelpAnimal(_animalToEncounter);
        
        DrawnCreatureSpawner._instance.ShowPanel();
        DrawnCreatureSpawner._instance.spawnPoint = transform;

        DrawnCreatureSpawner._instance.EnableAnimalSpawnButton();
        gameObject.SetActive(false);
    }
}