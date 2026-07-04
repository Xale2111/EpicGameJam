using UnityEngine;
using UnityEngine.UIElements;

public struct Animal {
    public Sprite _body;
    public Sprite _habitat;
    public Sprite _food;
}

[CreateAssetMenu(fileName = "DrawingDataSaver", menuName = "Scriptable Objects/DrawingDataSaver")]
public class DrawingDataSaver : ScriptableObject {
    bool _initilized = false;

    public enum Animals {
        LAMA,
        CONDOR,
        Length
    }

    public Animal[] _animals;

    public void Initialize() {
        if(_initilized)
            return;

        _animals = new Animal[(int)Animals.Length];
        _initilized = true;
    }
}
