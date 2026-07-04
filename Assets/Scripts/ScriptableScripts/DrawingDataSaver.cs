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
}
