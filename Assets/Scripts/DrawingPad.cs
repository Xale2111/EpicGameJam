using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// À attacher sur un RawImage (UI). Permet de dessiner dessus au clic/drag
/// et de récupérer le résultat sous forme de Texture2D ou de Sprite.
/// </summary>
[RequireComponent(typeof(RawImage))]
public class DrawingPad : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    private enum Tool { Pencil, Eraser }

    [Header("Réglages de la texture")]
    public int textureSize = 512;
    public Color backgroundColor = new Color(0, 0, 0, 0);

    [Header("Réglages du pinceau")]
    public Color brushColor = Color.black;
    [Range(1, 50)] public int brushSize = 8;
    [SerializeField] private int maxBrushSize = 30;
    [SerializeField] private int minBrushSize = 1;
    [SerializeField] private TextMeshProUGUI sizeButtonText;

    [Header("Outil actif")]
    [SerializeField] private Tool currentTool = Tool.Pencil;

    private Texture2D drawTexture;
    private RawImage rawImage;
    private RectTransform rectTransform;
    private Vector2Int lastPixelPos = -Vector2Int.one;

    private string[] sizeNames = new string[30]
    {
        "c'est pas la taille qui compte...",
        "minuscule",
        "pluscule",
        "toupiti",
        "toujours petit",
        "plus petit",
        "petit",
        "moins petit",
        "par défaut",
        "ça commence à se voir",
        "pas mal",
        "'C'est bien mais ça serait mieux en juste un peu plus grand nan ?'",
        "moyen grand",
        "stabilo",
        "épais",
        "plus épais",
        "encore un peu plus épais",
        "grand",
        "LAAAAARGE",
        "belle taille",
        "ça commence à me plaire",
        "parfait pour un gros trait",
        "pourquoi pas",
        "c'est chiant pour choisir une taille hein ?",
        "grand mais pas mal",
        "ça commence à faire trop là",
        "ridiculement grand",
        "les tailles non plus aucun sens",
        "Vous pensez que c'est le plus gros ? Vous avez tort",
        "inutilisable"
    };

    void Awake()
    {
        rawImage = GetComponent<RawImage>();
        rectTransform = GetComponent<RectTransform>();
        InitTexture();
    }

    private void Start()
    {
        sizeButtonText.text = "Taille (" + brushSize + ")";
    }

    /// <summary>Crée une texture vierge (à appeler aussi pour réinitialiser à une autre taille).</summary>
    public void InitTexture()
    {
        drawTexture = new Texture2D(textureSize, textureSize, TextureFormat.RGBA32, false);
        drawTexture.filterMode = FilterMode.Bilinear;
        drawTexture.wrapMode = TextureWrapMode.Clamp;
        ClearCanvas();
        rawImage.texture = drawTexture;
    }

    /// <summary>Remet la toile à zéro (couleur de fond).</summary>
    public void ClearCanvas()
    {
        Color[] fill = new Color[textureSize * textureSize];
        for (int i = 0; i < fill.Length; i++) fill[i] = backgroundColor;
        drawTexture.SetPixels(fill);
        drawTexture.Apply();
        lastPixelPos = -Vector2Int.one;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        lastPixelPos = -Vector2Int.one;
        PaintAtScreenPoint(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        PaintAtScreenPoint(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        lastPixelPos = -Vector2Int.one;
    }

    private void PaintAtScreenPoint(PointerEventData eventData)
    {
        Vector2 localPoint;
        if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(
                rectTransform, eventData.position, eventData.pressEventCamera, out localPoint))
            return;

        Rect rect = rectTransform.rect;
        float u = (localPoint.x - rect.x) / rect.width;
        float v = (localPoint.y - rect.y) / rect.height;

        if (u < 0 || u > 1 || v < 0 || v > 1) return;

        int px = Mathf.FloorToInt(u * textureSize);
        int py = Mathf.FloorToInt(v * textureSize);
        Vector2Int currentPos = new Vector2Int(px, py);

        // Trace une ligne entre l'ancien point et le nouveau pour éviter les trous
        // quand la souris/le doigt bouge vite entre deux frames.
        if (lastPixelPos.x >= 0)
            DrawLine(lastPixelPos, currentPos);
        else
            DrawBrush(currentPos);

        lastPixelPos = currentPos;
        drawTexture.Apply();
    }

    private void DrawLine(Vector2Int from, Vector2Int to)
    {
        int dist = Mathf.Max(Mathf.CeilToInt(Vector2Int.Distance(from, to)), 1);
        for (int i = 0; i <= dist; i++)
        {
            float t = i / (float)dist;
            Vector2Int p = Vector2Int.RoundToInt(Vector2.Lerp(from, to, t));
            DrawBrush(p);
        }
    }

    private void DrawBrush(Vector2Int center)
    {
        // Le crayon peint avec brushColor, la gomme "peint" avec la couleur de fond
        // (transparente ici), ce qui a pour effet d'effacer le trait.
        Color colorToUse = currentTool == Tool.Eraser ? backgroundColor : brushColor;

        int r = brushSize;
        for (int x = -r; x <= r; x++)
        {
            for (int y = -r; y <= r; y++)
            {
                if (x * x + y * y > r * r) continue; // pinceau rond
                int px = center.x + x;
                int py = center.y + y;
                if (px < 0 || px >= textureSize || py < 0 || py >= textureSize) continue;
                drawTexture.SetPixel(px, py, colorToUse);
            }
        }
    }

    public Texture2D GetTexture() => drawTexture;

    /// <summary>Convertit le dessin actuel en Sprite utilisable sur un SpriteRenderer.</summary>
    public Sprite CreateSprite()
    {
        Texture2D copie = CloneTexture(drawTexture);
        return Sprite.Create(
            copie,
            new Rect(0, 0, copie.width, copie.height),
            new Vector2(0.5f, 0.5f),
            100f // pixels per unit, à ajuster selon ton jeu
        );
    }

    /// <summary>Crée une copie indépendante (nouveaux pixels en mémoire) de la texture donnée.</summary>
    private Texture2D CloneTexture(Texture2D source)
    {
        Texture2D copie = new Texture2D(source.width, source.height, source.format, false);
        copie.filterMode = source.filterMode;
        copie.wrapMode = source.wrapMode;
        copie.SetPixels(source.GetPixels());
        copie.Apply();
        return copie;
    }

    public void ChangeBrushColor(Color color)
    {
        SetToolPencil();
        brushColor = color;
    }

    public void ChangeBrushSize()
    {
        brushSize = Random.Range(minBrushSize, maxBrushSize);
        sizeButtonText.text = sizeNames[brushSize - 1];
    }

    /// <summary>À relier au bouton "Crayon" dans l'UI.</summary>
    public void SetToolPencil()
    {
        currentTool = Tool.Pencil;
    }

    /// <summary>À relier au bouton "Gomme" dans l'UI.</summary>
    public void SetToolEraser()
    {
        currentTool = Tool.Eraser;
    }
}