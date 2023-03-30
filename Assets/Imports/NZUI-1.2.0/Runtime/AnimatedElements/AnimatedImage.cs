using UnityEngine;
using UnityEngine.UI;

namespace Nazio_LT.Tools.UI
{
    [RequireComponent(typeof(Image))]
    [AddComponentMenu("Nazio_LT/UI/Animated Image")]
    public class AnimatedImage : AnimatedUIElement
    {
        private Image image;

        public override void Init()
        {
            base.Init();

            image = GetComponent<Image>();
        }

        public override void FadeAnim(float _t)
        {
            Color _imageColor = image.color;
            _imageColor.a = _t;
            image.color = _imageColor;
        }
    }
}