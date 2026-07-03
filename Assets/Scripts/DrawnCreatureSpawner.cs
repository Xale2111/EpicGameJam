using UnityEngine;

/// <summary>
/// Récupère le dessin validé par le joueur et l'applique comme sprite
/// sur une instance du prefab (ex: le lama qui accompagne le joueur).
/// </summary>
public class DrawnCreatureSpawner : MonoBehaviour
{
    [Header("Références")]
    public DrawingPad drawingPad;
    public GameObject creaturePrefab;
    public Transform spawnPoint;

    /// <summary>Liste des créatures instanciées, si tu veux garder une trace de toutes.</summary>
    private System.Collections.Generic.List<GameObject> spawnedCreatures = new System.Collections.Generic.List<GameObject>();

    /// <summary>À appeler depuis le bouton "Valider" du popup.</summary>
    public void ValidateDrawing()
    {
        if (!drawingPad.HasEnoughContent()) return;
        Sprite sprite = drawingPad.CreateSprite();
        GameObject nouvelAnimal = SpawnNewCreature(sprite);
        spawnedCreatures.Add(nouvelAnimal);
        drawingPad.ClearCanvas();
        
        // Ferme le popup ici si besoin, ex :
        // drawingPad.transform.root.gameObject.SetActive(false);
    }

    private GameObject SpawnNewCreature(Sprite sprite)
    {
        Vector3 pos = spawnPoint != null ? spawnPoint.position : Vector3.zero;
        GameObject animal = Instantiate(creaturePrefab, pos, Quaternion.identity);

        // GetComponentInChildren couvre le cas où le SpriteRenderer est sur un enfant du prefab
        SpriteRenderer sr = animal.GetComponentInChildren<SpriteRenderer>();
        if (sr != null)
        {
            sr.sprite = sprite;
        }
        else
        {
            Debug.LogWarning("Aucun SpriteRenderer trouvé sur le prefab '" + creaturePrefab.name + "'.");
        }

        return animal;
    }

    /// <summary>Renvoie toutes les créatures instanciées jusqu'ici.</summary>
    public System.Collections.Generic.List<GameObject> GetSpawnedCreatures() => spawnedCreatures;
}