using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public abstract class InfoController : MonoBehaviour, IUIController, IUIActivator
{
    protected VisualElement[] _passengers = new VisualElement[4];

    protected abstract void Open(ClickableObjectView clickableObject);
    public abstract void Initialize();
    protected abstract UniTaskVoid ChangeSprite();

    protected virtual Color GetColorFromSprite(Sprite sprite)
    {
        Texture2D texture = sprite.texture;
        int centerX = texture.width / 2;
        int centerY = texture.height / 2;

        Color pixel = texture.GetPixel(centerX, centerY);

        return pixel;
    }
    public virtual void Activate()
    {
        if (!gameObject.activeSelf)
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
