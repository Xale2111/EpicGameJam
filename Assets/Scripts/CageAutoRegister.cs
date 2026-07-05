using UnityEngine;

public class CageAutoRegister : MonoBehaviour
{
    [SerializeField] Animals _animalType;
    public GameObject _cageDrawer;

    private void Start() {
        GameManager._instance.RegisterMyselfCageVersion(transform, _animalType);
        _cageDrawer = GetComponentInChildren<HabitatEncounter>().gameObject;
        _cageDrawer.SetActive(false);
    }
}
