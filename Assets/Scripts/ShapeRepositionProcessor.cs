#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.InputSystem;

#if UNITY_EDITOR
[InitializeOnLoad]
#endif
public class ShapeRepositionProcessor : InputProcessor<Vector2>
{
#if UNITY_EDITOR
    static ShapeRepositionProcessor ()
    {
        Initialize();
    }
#endif

    [RuntimeInitializeOnLoadMethod]
    static void Initialize ()
    {
        InputSystem.RegisterProcessor<ShapeRepositionProcessor>();
    }

    public override Vector2 Process (Vector2 value, InputControl control)
    {
        if (UiOnShapeCaster.IsRepositionEnabled)
        {
            value = UiOnShapeCaster.RepositionedCursorPosition;
        }

        return value;
    }
}