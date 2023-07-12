using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class UiOnShapeCaster : MonoBehaviour
{
    [field: SerializeField]
    private Camera UICamera { get; set; }
    [field: SerializeField]
    private GraphicRaycaster CanvasRaycaster { get; set; }
    [field: SerializeField]
    private RectTransform UIMainRectTransform { get; set; }

    [field: Space]
    [field: SerializeField]
    private Camera SceneCamera { get; set; }
    [field: SerializeField]
    private Renderer TargetRenderer { get; set; }
    [field: SerializeField]
    private int RendererMaterialId { get; set; } = 0;
    [field: SerializeField]
    private Collider TargetMeshCollider { get; set; }

    [field: Space]
    [field: Space]
    [field: SerializeField]
    private Vector2Int RenderTextureSize { get; set; }
    [field: SerializeField]
    private bool ExecuteInDebug { get; set; }
    [field: SerializeField]
    [field: Tooltip("Warning, using shared material sets texture for entire material. Make sure to use custom separate material.")]
    private bool UseSharedMaterial { get; set; }

    public static UiOnShapeCaster CurrentMouseoveredUiOnShape { get; private set; }
    public Vector2 RepositionedCursorPosition { get; private set; }
    private RenderTexture CurrentRendererTexture { get; set; }

    protected virtual void Start ()
    {
        SetupTexture();
    }

    protected virtual void Update ()
    {
        RaycastOnTargetShape();
    }

    protected virtual void OnValidate ()
    {
        HandleDebug();
    }

    private void HandleDebug ()
    {
        if (ExecuteInDebug == true)
        {
            SetupTexture();
        }
        else
        {
            DisposeDebug();
        }
    }

    private void SetupTexture ()
    {
        if (CurrentRendererTexture != null)
        {
            CurrentRendererTexture.Release();
        }
        UICamera.enabled = true;

       CurrentRendererTexture = new RenderTexture(RenderTextureSize.x, RenderTextureSize.y, 16, RenderTextureFormat.ARGB32);
        CurrentRendererTexture.Create();
        UICamera.targetTexture = CurrentRendererTexture;

        SetMaterialTexture(CurrentRendererTexture, UseSharedMaterial);
    }

    private void DisposeDebug ()
    {
        if (CurrentRendererTexture != null)
        {
            CurrentRendererTexture.Release();
        }
        SetMaterialTexture(null, true);
    }

    private void SetMaterialTexture (RenderTexture textureToSet, bool useSharedMaterial)
    {
        if (useSharedMaterial == true)
        {
            TargetRenderer.sharedMaterials[RendererMaterialId].mainTexture = textureToSet;
        }
        else
        {
            TargetRenderer.materials[RendererMaterialId].mainTexture = textureToSet;
        }
    }

    private void RaycastOnTargetShape ()
    {
        Ray ray = SceneCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit) && hit.collider == TargetMeshCollider)
        {
            Vector2 textureCoord = hit.textureCoord;
            textureCoord = new Vector2(textureCoord.x * UIMainRectTransform.rect.width, textureCoord.y * UIMainRectTransform.rect.height);
            RepositionedCursorPosition = textureCoord;
            CurrentMouseoveredUiOnShape = this;
            CanvasRaycaster.enabled = true;
        }
        else
        {
            if (CurrentMouseoveredUiOnShape == this)
            {
                CurrentMouseoveredUiOnShape = null;
            }
            CanvasRaycaster.enabled = false;
        }
    }

    //private void TryToAttachProcessor ()
    //{
    //    var action = 
    //    action.ApplyBindingOverride(new InputBinding { overrideProcessors = "ShapeRepositionProcessor()" });
    //}
}
