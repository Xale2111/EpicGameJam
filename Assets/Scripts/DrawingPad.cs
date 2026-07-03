using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Random = UnityEngine.Random;

/// <summary>
/// À attacher sur un RawImage (UI). Permet de dessiner dessus au clic/drag
/// et de récupérer le résultat sous forme de Texture2D ou de Sprite.
/// </summary>
[RequireComponent(typeof(RawImage))]
public class DrawingPad : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    [Header("Réglages de la texture")]
    public int textureSize = 512;
    public Color backgroundColor = new Color(0,0,0,0);

    [Header("Réglages du pinceau")]
    public Color brushColor = Color.black;
    [Range(1, 50)] public int brushSize = 8;
    [SerializeField] private int maxBrushSize = 16;
    [SerializeField] private int minBrushSize = 2;
    [SerializeField] private TextMeshProUGUI sizeButtonText;

    private Texture2D drawTexture;
    private RawImage rawImage;
    private RectTransform rectTransform;
    private Vector2Int lastPixelPos = -Vector2Int.one;

    void Awake()
    {
        rawImage = GetComponent<RawImage>();
        rectTransform = GetComponent<RectTransform>();
        InitTexture();
    }

    private void Start()
    {
        sizeButtonText.text = "Size (" + brushSize + ")";
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
        int r = brushSize;
        for (int x = -r; x <= r; x++)
        {
            for (int y = -r; y <= r; y++)
            {
                if (x * x + y * y > r * r) continue; // pinceau rond
                int px = center.x + x;
                int py = center.y + y;
                if (px < 0 || px >= textureSize || py < 0 || py >= textureSize) continue;
                drawTexture.SetPixel(px, py, brushColor);
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
        brushColor = color;
    }

    public void ChangeBrushSize()
    {
        brushSize = Random.Range(minBrushSize, maxBrushSize);
        sizeButtonText.text = "Size (" + brushSize + ")";
    }

}
