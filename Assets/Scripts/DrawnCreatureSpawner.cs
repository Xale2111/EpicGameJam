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

    [Header("Options")]
    public bool sauvegarderSurDisque = false;
    public string nomFichier = "creature_dessinee";

    private GameObject spawnedCreature;

    /// <summary>À appeler depuis le bouton "Valider" du popup.</summary>
    public void ValiderDessin()
    {
        Sprite sprite = drawingPad.CreateSprite();
        ApplySpriteToCreature(sprite);

        if (sauvegarderSurDisque)
        {
            string path = drawingPad.SaveToDisk(nomFichier);
            Debug.Log("Dessin sauvegardé : " + path);
        }

        // Ferme le popup ici si besoin, ex :
        // drawingPad.transform.root.gameObject.SetActive(false);
    }

    private void ApplySpriteToCreature(Sprite sprite)
    {
        if (spawnedCreature == null)
        {
            Vector3 pos = spawnPoint != null ? spawnPoint.position : Vector3.zero;
            spawnedCreature = Instantiate(creaturePrefab, pos, Quaternion.identity);
        }

        // GetComponentInChildren couvre le cas où le SpriteRenderer est sur un enfant du prefab
        SpriteRenderer sr = spawnedCreature.GetComponentInChildren<SpriteRenderer>();
        if (sr != null)
        {
            sr.sprite = sprite;
        }
        else
        {
            Debug.LogWarning("Aucun SpriteRenderer trouvé sur le prefab '" + creaturePrefab.name + "'.");
        }
    }

    /// <summary>Utile si tu veux permettre au joueur de re-dessiner et remplacer le sprite existant.</summary>
    public GameObject GetSpawnedCreature() => spawnedCreature;
}
