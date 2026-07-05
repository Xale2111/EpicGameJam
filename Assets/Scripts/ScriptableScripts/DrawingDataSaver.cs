using UnityEngine;
using UnityEngine.UIElements;

public struct Animal {
    public Sprite _body;
    public Sprite _habitat;
    public Sprite _food;
}

public enum Animals {
    LAMA,
    CONDOR,
    TAPIR,
    PINGUIN,
    FLAMINGO,
    Length,
    NONE
}

public enum DrawnElement{
    BODY,
    HABITAT,
    FOOD,
    CHAPITEAU,
    PLAYER_BODY,
    PLAYER_HAT,
    Length,
    NONE
}

[CreateAssetMenu(fileName = "DrawingDataSaver", menuName = "Scriptable Objects/DrawingDataSaver")]
public class DrawingDataSaver : ScriptableObject {
    bool _initilized = false;

    public Animal[] _animals;

    public Sprite _chapiteau;

    public Sprite _playerBody;
    public Sprite _playerHat;


    public void Initialize() {
        if(_initilized)
            return;

        _animals = new Animal[(int)Animals.Length];
        _initilized = true;
    }

    public Sprite GetRandomSprite(){
        while(true) {
            Sprite selectedSprite = null;
            int randomElement = Random.Range(0, (int)DrawnElement.Length);
            int randomAnimal = Random.Range(0, (int)Animals.Length);
            switch((DrawnElement)randomElement) {
                case DrawnElement.BODY:
                selectedSprite = _animals[(int)randomAnimal]._body;
                break;

                case DrawnElement.HABITAT:
                selectedSprite = _animals[(int)randomAnimal]._habitat;
                break;

                case DrawnElement.FOOD:
                selectedSprite = _animals[(int)randomAnimal]._food;
                break;

                case DrawnElement.CHAPITEAU:
                selectedSprite = _chapiteau;
                break;

                case DrawnElement.PLAYER_BODY:
                selectedSprite = _playerBody;
                break;

                case DrawnElement.PLAYER_HAT:
                selectedSprite = _playerHat;
                break;

                default:
                break;
            }

            if(selectedSprite != null)
                return  selectedSprite;
        }
    }

}
