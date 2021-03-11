# Xamarin Forms Helpers

XFHelpers is, at it's core, a base View and ViewModel providing super simple ViewModel based navigation. MVVM using as much built-in-Xamarin as possible without the need for heavy frameworks.

The only requirements are Xamarin.Forms (4.3) and Xamarin.Essentials (1.3.1). The entire package is only 25kb.

Several common converters and helpers are included. Feature requests and PRs are welcome.

A sample project is available in the [/Sample](https://github.com/TrueGeek/Xamarin.Plugin.TrueGeek.XFHelpers/tree/master/Sample) directory.

A change log is available under [Releases](https://github.com/TrueGeek/Xamarin.Plugin.TrueGeek.XFHelpers/releases).

| | | |
| --- | --- | -- |
| XFHelpers | | [![NuGet](https://buildstats.info/nuget/Xamarin.Plugin.TrueGeek.XFHelpers)](https://www.nuget.org/packages/Xamarin.Plugin.TrueGeek.XFHelpers/) |
| XFHelpers.iOS | | [![NuGet](https://buildstats.info/nuget/Xamarin.Plugin.TrueGeek.XFHelpers.iOS)](https://www.nuget.org/packages/Xamarin.Plugin.TrueGeek.XFHelpers.iOS/) |
| XFHelpers.Droid | | [![NuGet](https://buildstats.info/nuget/Xamarin.Plugin.TrueGeek.XFHelpers.Droid)](https://www.nuget.org/packages/Xamarin.Plugin.TrueGeek.XFHelpers.Droid/) |

---

## To Install

1. Install the [XFHelpers NuGet package](https://buildstats.info/nuget/Xamarin.Plugin.TrueGeek.XFHelpers) into your Forms project

2. If you do not plan on using the LongPressEffect nothing further is needed. You only need to install the platform specific NuGet packages if you are going to use LongPressEffect

2. Android

    * Install the [XFHelpers.Droid NuGet package](https://buildstats.info/nuget/Xamarin.Plugin.TrueGeek.XFHelpers.Droid) into your Droid project

    * In [MainActivity](https://github.com/TrueGeek/Xamarin.Plugin.TrueGeek.XFHelpers/blob/master/Sample/TGSample.Android/MainActivity.cs) call `TrueGeek.XFHelpers.Droid.Effects.AndroidLongPressedEffect.Init();` before you call `LoadApplication(new App())`

3. iOS

    * Install the [XFHelpers.Droid NuGet package](https://buildstats.info/nuget/Xamarin.Plugin.TrueGeek.XFHelpers.iOS) into your Droid project

    * In [AppDelegate](https://github.com/TrueGeek/Xamarin.Plugin.TrueGeek.XFHelpers/blob/master/Sample/TGSample.iOS/AppDelegate.cs) call `TrueGeek.XFHelpers.iOS.Effects.iOSLongPressedEffect.Init();` before you call `return base.FinishedLaunching(app, options);`

---

## Features

* [Base View](#BaseView)
    * SafeAreaInsets
    * ClearBackButtonTextOnAllPages
    * IsBusyOverlay
* [Base ViewModel](#BaseViewModel)
    * IsBusy
    * Title
    * DeviceIsTabletOrDesktop    
    * DisplayActionSheet
    * DisplayAlert
    * DisplayPrompt
    * INotifyPropertyChanged
    * ReturnParameters
* [ViewModel Navigation](#ViewModelNavigation)
    * ViewAppearing
    * ViewModelInit
    * ViewDisappearing
    * ReturnParameters
* [i18n Translation](#i18nTranslation)
* [Simple PubSub Service](#SimplePubSubService)
* [Behaviors](#Behaviors)
    * [Event To Command](#EventToCommand)
* [Converters](#Converters)
    * [Inverse Bool](#InverseBool)
    * [String To Bool](#StringToBool)
* [Extensions](#Extensions)
    * [Track Component Lifecycle](#TrackComponentLifecycleExtension)
* [Effects](#Effects)
    * [Long Press](#LongPress)
* [Logging](#Logging)

---

## Base View <a name="#BaseView"></a>

All views must inherit from TGBasePage.

In the XAML this looks like this:

```
<baseView:TGBasePage
    xmlns:baseView="clr-namespace:TrueGeek.XFHelpers.Views;assembly=TrueGeek.XFHelpers"
    x:Class="TGSample.Views.HomePage"
```

and in the code behind simply this

```
public partial class HomePage : TGBasePage
```

**SafeAreaInsets**

SafeAreaInsets is a readonly Thickness. On Android this will always return the default `new Thickness` as it's value isn't needed. On iOS it returns what the device feels the SafeAreaIset is, either 20 or 40px depending on the phone type and wether it has a notch or not.

Use this instead of `ios:Page.UseSafeArea="true"` in your XAML pages to avoid white space on phones without notches. Instead, add padding at the top of your page of the amount returned by this property.

**ClearBackButtonTextOnAllPages**

If you don't like the text before the back button in your navigation pages, then set this in your App.xaml.cs App() function:

`TrueGeek.XFHelpers.Init.ClearBackButtonTextOnAllPages = true;`

**IsBusyOverlay**

When IsBusy is set on the ViewModel (see below) a busy overlay will be shown.  This can be overriden like this, in your App.xaml.cs App() function:

`TrueGeek.XFHelpers.Init.CustomActivityIndicator = new YourCustomComponent();`

If you don't override the component the default is used, which is simply a `new ActivityIndicator()`.

[Example in sample project](https://github.com/TrueGeek/Xamarin.Plugin.TrueGeek.XFHelpers/blob/master/Sample/TGSample/Views/ActivityIndicatorsPage.xaml).

---

## Base ViewModel <a name="#BaseViewModel"></a>

View models must inherit from TGBaseViewModel like this

```C#
public class HomeViewModel : TGBaseViewModel
```

**IsBusy**

If `IsBusy` is set to True the busy overlay will be shown on the page.

**Title**

This is provided so that it can be bound by the page.

**DeviceIsTabletOrDesktop**

This returns true if Xamarin.Essentials returns that either the Device.Idiot is Tablet or Desktop.

**DisplayActionSheet**

```C#
DisplayActionSheet(string title, string cancelButtonText, params string[] buttons);
```

Shows an action sheet using a reference to Application.Current.MainPage.

Note that if `null` is passed as cancelButtonText then Android will display the ActionSheet as a model without the ability to cancel it, even if the user taps on the background.

**DisplayAlert**

```C#
DisplayAlert(string title, string message, string accept)
```

```C#
DisplayAlert(string title, string message, string accept, string cancel)
```

Shows an alert box, with or without a cancel button, using a reference to Application.Current.MainPage.

**DisplayPrompt**

```C#
DisplayPrompt(string title, string message, string accept, string cancel, string placeholder = null, int maxLength = -1, Keyboard keyboard = null)
```

Shows a prompt using a reference to Application.Current.MainPage.

**INotifyPropertyChanged**

BaseViewModel inherits from INotifyPropertyChanged. If you add properties to your view model you can do so like this

```C#
private bool myProperty;
public bool MyProperty
{
    get => myProperty;
    set => SetProperty(ref myProperty, value);
}
```

You can also call OnPropertyChanged() directly

```C#
private bool myProperty;
public bool MyProperty
{
    get => myProperty;
    set
    {
        
        myProperty = value;

        // if you do not pass any value it assumed the enclosing property (MyProperty in this case)
        OnPropertyChanged();

        // the easiest way to notify another property is with a => operator
        OnPropertyChanged(() => my2ndProperty);

        // you can also use the nameof() function
        OnPropertyChanged(nameof(my3rdProperty));

        // this is a bad practice! don't use strings!
        OnPropertyChanged("my4thPropirty");

    }
}
```

---

## ViewModel Navigation <a name="#ViewModelNavigation"></a>

**Initialization**

After setting the MainPage in app.xaml.cs, call `Init.NavigationService`. This function can be added to app.xaml.cs to do this all in one step:

```
public void SetMainPage<T>() where T : Page
{            
    MainPage = new NavigationPage(Activator.CreateInstance<T>());   // instead of a NavigationPage you could also use an AppShell
    TrueGeek.XFHelpers.Init.NavigationService = new TrueGeek.XFHelpers.Services.NavigationService(MainPage.Navigation);
}
```

Then you just call `SetMainPage<HomePage>();`

This initial page will need to have it's ViewModel specifically set in it's XAML, like this:

```
<baseView:TGBasePage
    xmlns:vm="clr-namespace:TGSample.ViewModels"
    ...

    <ContentPage.BindingContext>
        <vm:HomeViewModel />
    </ContentPage.BindingContext>
```

**Assumed Naming Conventions**

It is assumed that:
* There is a one-to-one relationship between a ViewModel and a View.
* The view is named in the format `Views\SamplePage`
* The view model is named in the format `ViewModels\SampleViewModel`

It is possible to override the assumption of the view name (but not the folder) by using `NavigateTo` and passing in the Page type: 

```
NavigateTo<TViewModel, TPage>(object parameters)
```

**Navigation Methods**

`NavigateTo<TViewModel>(object parameters = null)`

Navigates to a ViewModel, optionally sending parameters

`NavigateTo<TViewModel, TPage>(object parameters = null)`

Navigates to a ViewModel overriding the asumption of naming convention for the page name

`NavigateToModal<TViewModel>(object parameters = null, bool useNavigationPage = false, Style navigationPageStyle = null)`

Navigates to a ViewModel as a modal

`NavigateBack()`

Navigates back

`NavigateBackFromModal()`

Navigates back from a modal

`NavigateToRoot()`

Navigates back to the root page

`GetPageForMasterDetail<TViewModel>(object parameters = null, bool useNavigationPage = true, Style navigationPageStyle = null)`

Gets a page that can be used in a master detail page. For example:

```
    var masterPage = NavigationService.GetPageForMasterDetail<HomeViewModel>(null, false);
    var detailPage = NavigationService.GetPageForMasterDetail<DetailView>(null, false);

    var masterDetailPage = new MasterDetailPage()
    {
        Master = masterPage,
        Detail = detailPage,
        Style = (Style)Application.Current.Resources["OurCustomStyle"],
    };

    MainPage = masterDetailPage;
```

**Navigation Events**

Navigation events fire in this order:
1. ViewAppearingAsync
2. ViewModelInit
3. ViewDisappearing

ViewModelInit will include a parameter object that is either null or the value included in navigation (see methods above for how to send parameters).

**ReturnParameters**

Parameters can be sent backwards from one viewmodel to the previous one using `ReturnParameters`. See the sample ReturnParametersViewModel where it sets the return parameter:

```
private async Task SelectValueCommandExecutor(string parameter)
{
    ReturnParameters = parameter;
    await NavigationService.NavigateBack();
}
```

This is read in HomeViewModel this way:

```

private async Task GoToReturnParametersPageExecutor()
{
    NavigationService.OnPageDisappearing += NavigationService_OnPageDisappearing;
    await NavigationService.NavigateTo<ReturnParametersViewModel>();
}

private void NavigationService_OnPageDisappearing(object sender, PageDisappearingEventArgs e)
{
    if (e.ViewModelType == typeof(ReturnParametersViewModel))
    {
        var selectedValue = (string)e.ReturnParameters;
        NavigationService.OnPageDisappearing -= NavigationService_OnPageDisappearing;
    }
}

```

---

## i18n Translation <a name="#i18nTranslation"></a>

Xamarin already provides i18n string translations. Add a `Resources` folder and `AppResources.{LanguageCode}.resx` files for each language you wish to support.

Let XFHelpers know how to access your resource files like this:

```
TrueGeek.XFHelpers.Init.ResourceManager = new ResourceManager("TGSample.Resources.AppResources", typeof(App).GetTypeInfo().Assembly);
```

The language shown defaults to the user's language (`CultureInfo.CurrentCulture.TwoLetterISOLanguageName`). You can override this by setting it manually like this:

```
TrueGeek.XFHelpers.Init.LanguageCode = "de";
```

XFHelpers provides a converter to use in XAML:

```
<baseView:TGBasePage
    ...
    xmlns:converters="clr-namespace:TrueGeek.XFHelpers.Converters;assembly=TrueGeek.XFHelpers">

        <Label Text="{converters:Translate name_of_string_from_resx_file}" />
```

Additionally, you can get string translations with the `GetText()` function:

`TranslatedText = new TrueGeek.XFHelpers.Helpers.TranslationHelper().GetText("name_of_string_from_resx_file");`

---

## Simple PubSub Service <a name="#SimplePubSubService"></a>

Super simple pub / sub service. 

Create a static `TrueGeek.XFHelpers.PubSub.Hub`

Multiple subscribers can `Subscribe()`

When anyone calls `Publish()` all the subscribers are notified

---

## Behaviors <a name="#Behaviors"></a>

**Event To Command** <a name="#EventToCommand"></a>

---

## Converters <a name="#Converters"></a>

**Inverse Bool** <a name="#InverseBool"></a>

Standard inverse bool converter - returns `!true`

**String Bool** <a name="#StringToBool"></a>

Returns `false` if string is null or empty

---

## Extensions <a name="#Extensions"></a>

**Track Component Lifecycle** <a name="#TrackComponentLifecycleExtension"></a>

Extension that adds lifecycle events to custom components. Use like this:

```C#

public MyCustomGridComponent()
{
    InitializeComponent();
   this.TrackComponentLifecycle(OnAppearing, OnDisappearing);
}

private void OnDisappearing(object sender, EventArgs eventArgs)
{
}

private void OnAppearing(object sender, EventArgs eventArgs)
{
}

```

---

## Effects <a name="#Effects"></a>

**Long Press** <a name="#LongPress"></a>

Make sure you've added the NuGet package to your iOS and Android projects!

Use like this:

```
xmlns:effects="clr-namespace:TrueGeek.XFHelpers.Effects;assembly=TrueGeek.XFHelpers"

    <Button
        Command="{Binding PressCommand}"
        effects:LongPressedEffect.Command="{Binding LongPressCommand}"            
        >

        <Button.Effects>
            <effects:LongPressedEffect />
        </Button.Effects>

    </Button>
```

---

## Logging <a name="#Logging"></a>

Any exceptions or warnings that occur within the XFHelpers code will be outputted to an action that you can capture if you are interested.

```
TrueGeek.XFHelpers.Init.LoggingReference => (string message, int logLevel, Dictionary<string, object> parameters)
```

---
