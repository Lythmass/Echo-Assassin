using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class HoverEffects : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    TMP_Text text;
    Material materialInstance;
    Color outlineColor;
    ButtonActions buttonActions;

    void Awake()
    {
        text = GetComponent<TMP_Text>();
        materialInstance = text.fontMaterial;
        outlineColor = materialInstance.GetColor(ShaderUtilities.ID_OutlineColor);
        buttonActions = FindAnyObjectByType<ButtonActions>();
    }

    void Update()
    {
        if (!buttonActions)
            return;
        if (buttonActions.GetIsChangingMenus())
        {
            StartFade(0f);
            buttonActions.SetIsChangingMenusFalse();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        StartFade(1f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        StartFade(0f);
    }

    public void StartFade(float alpha)
    {
        if (alpha == 1f)
        {
            AudioManager.instance.PlayUIHoverSFX();
        }
        Color c = outlineColor;
        c.a = alpha;
        materialInstance.SetColor(ShaderUtilities.ID_FaceColor, c);
    }
}
