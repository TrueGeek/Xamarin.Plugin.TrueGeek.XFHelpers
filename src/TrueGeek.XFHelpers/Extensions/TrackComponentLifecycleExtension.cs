using System;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using TrueGeek.XFHelpers.Views;
using Xamarin.Forms;

namespace TrueGeek.XFHelpers.Extensions
{

    public static class TrackComponentLifecycleExtension
    {

        // refactored from https://stackoverflow.com/a/49398138/208863, thanks to Yuanyi Wu!

        public static void TrackComponentLifecycle(this Element element, EventHandler onAppearing = null, EventHandler onDisappearing = null)
        {

            var task = new Task(() =>
            {

                var page = GetPage(element);

                if (page == null) return;

                if (onAppearing != null)
                {

                    page.Appearing += onAppearing;

                    // if the page has already appeared before this was attached then go ahead and fire onAppearing now
                    if (page.GetType() == typeof(TGBasePage) && (page as TGBasePage).HasAppeared)
                    {
                        onAppearing?.Invoke(null, new EventArgs());
                    }

                }

                if (onDisappearing != null) page.Disappearing += onDisappearing;

            });

            task.Start();

            return;

        }

        public static Page GetPage(this Element element, int timeout = -1)
        {

            try
            {

                if (element is Page) return (Page)element;

                Element el = element;

                while (!(el is Page))
                {

                    if (el.Parent == null)
                    {

                        // we don't (yet) have a parent, so we need to wait for one
                        var signal = new AutoResetEvent(false);

                        PropertyChangedEventHandler handler = (object sender, PropertyChangedEventArgs args) =>
                        {

                            Element senderElement = (Element)sender;
                            if (args.PropertyName == "Parent" && senderElement.Parent != null)
                            {
                                signal.Set();
                            }

                        };

                        el.PropertyChanged += handler;

                        var gotSignal = signal.WaitOne(timeout);
                        if (!gotSignal)
                        {
                            return null;
                        }

                        el.PropertyChanged -= handler;

                    }

                    // go further up the UI tree to find the find a Page
                    el = el.Parent;

                }

                return (Page)el;

            }
            catch (Exception ex)
            {

                return null;

            }

        }

    }

}
