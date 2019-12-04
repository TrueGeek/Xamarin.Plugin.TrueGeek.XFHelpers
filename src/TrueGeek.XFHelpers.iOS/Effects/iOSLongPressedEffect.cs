using TrueGeek.XFHelpers.Effects;
using TrueGeek.XFHelpers.iOS.Effects;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ResolutionGroupName("XFHelpers")]
[assembly: ExportEffect(typeof(iOSLongPressedEffect), "LongPressedEffect")]
namespace TrueGeek.XFHelpers.iOS.Effects
{

    public class iOSLongPressedEffect : PlatformEffect
    {

        public static void Initialize() {  }

        private readonly UILongPressGestureRecognizer _longPressRecognizer;

        private bool _attached;

        public iOSLongPressedEffect()
        {
            _longPressRecognizer = new UILongPressGestureRecognizer(HandleLongClick);
        }

        protected override void OnAttached()
        {
            // because an effect can be detached immediately after attached (happens in listview), only attach the handler one time
            if (!_attached)
            {
                Container.AddGestureRecognizer(_longPressRecognizer);
                _attached = true;
            }
        }

        private void HandleLongClick()
        {
            var command = LongPressedEffect.GetCommand(Element);
            command?.Execute(LongPressedEffect.GetCommandParameter(Element));
        }

        protected override void OnDetached()
        {
            if (_attached)
            {
                Container.RemoveGestureRecognizer(_longPressRecognizer);
                _attached = false;
            }
        }

    }

}
