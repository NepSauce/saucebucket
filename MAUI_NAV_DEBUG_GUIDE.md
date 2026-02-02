# .NET MAUI Navigation & Debugging Guide

*Comprehensive guide for debugging and navigating .NET MAUI applications*

---

## **Understanding .NET MAUI Project Structure**

### Core MAUI Architecture

```
YourApp/
├── App.xaml & App.xaml.cs          -> Application lifecycle & global resources
├── AppShell.xaml & AppShell.xaml.cs -> Navigation shell & routing
├── MainPage.xaml & MainPage.xaml.cs -> Default startup page
├── MauiProgram.cs                   -> App configuration & DI setup
├── YourApp.csproj                   -> Project configuration & targets
└── Platforms/                       -> Platform-specific code
    ├── Android/                     -> Android-specific implementations
    ├── iOS/                        -> iOS-specific implementations  
    ├── MacCatalyst/                -> macOS-specific implementations
    ├── Windows/                    -> Windows-specific implementations
    └── Tizen/                      -> Tizen-specific implementations
```

### Key MAUI File Types

- `.xaml` + `.xaml.cs` -> UI definition + code-behind
- `MauiProgram.cs` -> Dependency injection, services, configuration
- `Platforms/` -> Platform-specific code and assets
- `Resources/` -> Shared resources (fonts, images, styles)

---

## **Finding Things in MAUI Projects**

### The MAUI Search Strategy

**1. UI/Visual Issues - Start with XAML**
```csharp
// Search for UI elements by name, text content, or style
"x:Name=\"LoginButton\""
"Text=\"Sign In\""
"Style=\"{StaticResource PrimaryButton}\""
```

**2. Navigation Issues - Check AppShell and routing**
```csharp
// Search for routes and navigation patterns
"Shell.TabBarIsVisible"
"GoToAsync"
"//TabBar"
"ContentPage"
```

**3. Platform-specific Issues**
```csharp
// Search in platform folders
"MainActivity"           // Android
"AppDelegate"           // iOS  
"Package.appxmanifest"  // Windows
```

**4. Common MAUI Search Terms**
```csharp
// Services & DI
"AddSingleton"
"AddTransient" 
"AddScoped"

// Data binding
"Binding"
"OneWay"
"TwoWay"
"PropertyChanged"

// MVVM patterns
"ViewModel"
"Command"
"ObservableCollection"
"INotifyPropertyChanged"
```

---

## **MAUI-Specific Debugging Process**

### Step 1: Identify the Issue Type

**UI/Layout Issues:**
- Element not visible? -> Check `IsVisible`, `Opacity`, layout constraints
- Wrong size/position? -> Check `WidthRequest`, `HeightRequest`, margins, padding
- Style not applied? -> Trace resource resolution in Resources/

**Navigation Issues:**
- Page not opening? -> Check route registration in AppShell
- Wrong page displayed? -> Verify `GoToAsync` parameters and Shell hierarchy
- Back navigation broken? -> Check navigation stack and Shell structure

**Data Issues:**
- UI not updating? -> Verify `INotifyPropertyChanged` implementation
- Binding not working? -> Check `BindingContext` and property names
- Collection not updating? -> Use `ObservableCollection<T>`

**Platform-specific Issues:**
- Feature not working on specific platform? -> Check Platforms/ folder
- Permission denied? -> Verify platform permissions and manifests
- Crash on startup? -> Check platform-specific initialization

### Step 2: MAUI Debugging Tools

**Visual Studio Debugging:**
```csharp
// Hot Reload for XAML changes
// Modify XAML and see changes instantly without rebuild

// Live Visual Tree (Windows)
// Debug -> Windows -> Live Visual Tree

// XAML Hot Reload Output
// View -> Output -> Show output from: XAML Hot Reload
```

**Console Debugging:**
```csharp
// Add to MauiProgram.cs for debug builds
#if DEBUG
    builder.Logging.AddDebug();
#endif

// Then use in your code
using Microsoft.Extensions.Logging;

public partial class MainPage : ContentPage
{
    private readonly ILogger<MainPage> _logger;
    
    public MainPage(ILogger<MainPage> logger)
    {
        _logger = logger;
        InitializeComponent();
    }
    
    private void OnButtonClicked(object sender, EventArgs e)
    {
        _logger.LogInformation("Button clicked at {Time}", DateTime.Now);
    }
}
```

**Device/Simulator Debugging:**
```bash
# Check connected devices
dotnet-maui devices

# Run with specific device
dotnet run -f net9.0-android --device "Pixel_5_API_30"
```

### Step 3: Common MAUI Debug Scenarios

**Binding Issues:**
```csharp
// Add to debug binding problems
<Entry Text="{Binding Username}" 
       x:Name="UsernameEntry"/>

// Check in code-behind
System.Diagnostics.Debug.WriteLine($"BindingContext: {BindingContext}");
System.Diagnostics.Debug.WriteLine($"Username: {(BindingContext as ViewModel)?.Username}");
```

**Navigation Issues:**
```csharp
// Debug navigation
public async void NavigateToDetails()
{
    try 
    {
        System.Diagnostics.Debug.WriteLine("Before navigation");
        await Shell.Current.GoToAsync("//details");
        System.Diagnostics.Debug.WriteLine("After navigation");
    }
    catch (Exception ex)
    {
        System.Diagnostics.Debug.WriteLine($"Navigation error: {ex.Message}");
    }
}
```

**Resource Resolution Issues:**
```xml
<!-- Check if resources are found -->
<Button Style="{StaticResource PrimaryButton}" 
        x:Name="TestButton"/>

<!-- Alternative fallback approach -->
<Button Style="{DynamicResource PrimaryButton}" 
        BackgroundColor="Red"  <!-- Fallback if style not found -->
        x:Name="TestButton"/>
```

---

## **MAUI Platform-Specific Debugging**

### Android Debugging

**Common Android Issues:**
```csharp
// Check AndroidManifest.xml permissions
<uses-permission android:name="android.permission.INTERNET" />
<uses-permission android:name="android.permission.ACCESS_NETWORK_STATE" />

// Debug Android lifecycle
public class MainActivity : MauiAppCompatActivity
{
    protected override void OnCreate(Bundle savedInstanceState)
    {
        System.Diagnostics.Debug.WriteLine("Android: OnCreate called");
        base.OnCreate(savedInstanceState);
    }
}
```

**Android Logcat:**
```bash
# View Android logs
adb logcat | grep "YourAppName"

# Or use Visual Studio
# View -> Other Windows -> Device Log
```

### iOS Debugging

**Common iOS Issues:**
```csharp
// Check Info.plist configurations
// Privacy permissions, URL schemes, etc.

// Debug iOS lifecycle
public class AppDelegate : MauiUIApplicationDelegate
{
    protected override MauiApp CreateMauiApp() 
    {
        System.Diagnostics.Debug.WriteLine("iOS: Creating MAUI app");
        return MauiProgram.CreateMauiApp();
    }
}
```

**iOS Simulator Debugging:**
- Use Simulator -> Device -> Console to see logs
- Check device settings for permissions
- Verify provisioning profiles for device testing

### Windows Debugging

**Common Windows Issues:**
```xml
<!-- Check Package.appxmanifest capabilities -->
<Capability Name="internetClient" />
<Capability Name="privateNetworkClientServer" />
```

**Windows Event Logs:**
- Check Windows Event Viewer for application crashes
- Use Visual Studio diagnostics tools for memory/performance

---

## **MAUI Performance Debugging**

### Memory Usage

**Track Memory Leaks:**
```csharp
// Implement IDisposable in ViewModels
public class MyViewModel : INotifyPropertyChanged, IDisposable
{
    private bool disposed = false;
    
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
    
    protected virtual void Dispose(bool disposing)
    {
        if (!disposed)
        {
            if (disposing)
            {
                // Dispose managed resources
                // Unsubscribe from events
            }
            disposed = true;
        }
    }
}
```

**Monitor Collections:**
```csharp
// Use ObservableCollection efficiently
public ObservableCollection<Item> Items { get; } = new();

// For large datasets, consider virtualization
<CollectionView ItemsSource="{Binding Items}"
                x:Name="ItemsList">
    <CollectionView.ItemTemplate>
        <!-- Template here -->
    </CollectionView.ItemTemplate>
</CollectionView>
```

### UI Performance

**Layout Performance:**
```xml
<!-- Prefer specific layouts over nested StackLayouts -->
<!-- Good -->
<Grid RowDefinitions="Auto,*,Auto" 
      ColumnDefinitions="*,Auto">
    <!-- Content here -->
</Grid>

<!-- Avoid excessive nesting -->
<!-- Bad -->
<StackLayout>
    <StackLayout>
        <StackLayout>
            <!-- Deep nesting -->
        </StackLayout>
    </StackLayout>
</StackLayout>
```

**Image Performance:**
```xml
<!-- Optimize images -->
<Image Source="icon.png" 
       WidthRequest="50" 
       HeightRequest="50"
       Aspect="AspectFit" />

<!-- Use appropriate image sizes for each platform -->
<!-- Check Resources/Images/ folder structure -->
```

---

## **Common MAUI Problems & Solutions**

### Problem: "Hot Reload not working"
**Solutions:**
- Ensure you're in Debug configuration
- Check XAML Hot Reload output window for errors
- Try Clean -> Rebuild Solution
- Restart Visual Studio if persisting

### Problem: "Page navigation not working"
**Check:**
```csharp
// 1. Route registration in AppShell.xaml
<ShellContent Route="details" ContentTemplate="{DataTemplate local:DetailsPage}" />

// 2. Navigation call
await Shell.Current.GoToAsync("details"); // Not "//details" for relative navigation

// 3. Shell hierarchy is correct
```

### Problem: "Binding not updating UI"
**Verify:**
```csharp
// 1. INotifyPropertyChanged implementation
private string _username;
public string Username
{
    get => _username;
    set 
    {
        _username = value;
        OnPropertyChanged(); // This must be called!
    }
}

// 2. BindingContext is set
public MyPage()
{
    InitializeComponent();
    BindingContext = new MyViewModel(); // Essential!
}

// 3. Property names match exactly (case-sensitive)
<Entry Text="{Binding Username}" /> <!-- Must match property name exactly -->
```

### Problem: "App crashes on specific platform"
**Debug Steps:**
1. Check platform-specific logs (logcat for Android, Console for iOS)
2. Verify platform permissions in manifests
3. Test platform-specific code paths
4. Check for platform-specific dependencies

### Problem: "Dependency injection not working"
**Verify Registration:**
```csharp
// In MauiProgram.cs
public static MauiApp CreateMauiApp()
{
    var builder = MauiApp.CreateBuilder();
    
    // Register services
    builder.Services.AddSingleton<IMyService, MyService>();
    builder.Services.AddTransient<MyViewModel>();
    
    // Register pages for navigation
    builder.Services.AddTransient<MyPage>();
    
    return builder.Build();
}

// In page constructor
public MyPage(MyViewModel viewModel)
{
    InitializeComponent();
    BindingContext = viewModel; // Inject and set
}
```

---

## **MAUI Development Best Practices**

### Project Organization

```
Views/              -> Pages and user controls
ViewModels/         -> ViewModels (MVVM pattern)  
Models/            -> Data models
Services/          -> Business logic services
Converters/        -> Value converters for binding
Behaviors/         -> Reusable behaviors
Controls/          -> Custom controls
Resources/         -> Shared resources
    Styles/        -> Global styles
    Images/        -> Images and icons
    Fonts/         -> Custom fonts
```

### Code Patterns

**MVVM Implementation:**
```csharp
// Base ViewModel
public class BaseViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;
    
    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    
    protected bool SetProperty<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value))
            return false;
            
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }
}

// Usage in ViewModels
public class MyViewModel : BaseViewModel
{
    private string _title;
    public string Title 
    { 
        get => _title; 
        set => SetProperty(ref _title, value); 
    }
}
```

**Command Implementation:**
```csharp
// In ViewModel
public ICommand SaveCommand => new Command(async () => await SaveAsync());

private async Task SaveAsync()
{
    // Implementation here
}

// In XAML
<Button Text="Save" Command="{Binding SaveCommand}" />
```

### Testing MAUI Apps

**Unit Testing ViewModels:**
```csharp
[Test]
public void Username_SetsCorrectly()
{
    // Arrange
    var viewModel = new LoginViewModel();
    var expectedUsername = "testuser";
    
    // Act
    viewModel.Username = expectedUsername;
    
    // Assert
    Assert.AreEqual(expectedUsername, viewModel.Username);
}
```

**UI Testing (optional with frameworks like Appium):**
```csharp
// Platform-specific UI testing can be set up
// Check platform testing documentation
```

---

## **Quick Reference Commands**

### Build & Run Commands
```bash
# Restore packages
dotnet restore

# Build for specific platform
dotnet build -f net9.0-android
dotnet build -f net9.0-ios

# Run on specific platform  
dotnet run -f net9.0-windows10.0.19041.0
dotnet run -f net9.0-android
dotnet run -f net9.0-ios

# Clean build outputs
dotnet clean
```

### Debugging Commands
```bash
# List available devices
dotnet-maui devices

# Check MAUI workloads
dotnet workload list

# Update MAUI workloads
dotnet workload update

# Repair MAUI installation
dotnet workload repair
```

---

## **When You're Stuck - MAUI Edition**

### Checklist for Common Issues
- [ ] Is Hot Reload enabled and working?
- [ ] Are all NuGet packages restored?
- [ ] Is the correct startup project selected?
- [ ] Are platform-specific permissions configured?
- [ ] Is dependency injection properly configured?
- [ ] Are routes properly registered in AppShell?
- [ ] Is BindingContext set correctly?
- [ ] Are property names matching exactly in bindings?

### MAUI-Specific Resources
- **Microsoft MAUI Documentation:** https://docs.microsoft.com/dotnet/maui/
- **MAUI Community Toolkit:** https://github.com/CommunityToolkit/Maui
- **MAUI Samples:** https://github.com/dotnet/maui-samples
- **Platform Issues:** Check platform-specific documentation

### Getting Help
1. **Check MAUI GitHub Issues:** Common problems often reported
2. **MAUI Discord/Forums:** Active community support  
3. **Platform Documentation:** iOS, Android, Windows specific docs
4. **Visual Studio Feedback:** Report tooling issues

---

## **Remember**

> "MAUI apps are native apps with shared code. Debug the shared logic first, then dive into platform-specific issues."

Every MAUI issue falls into one of these categories:
1. **Shared Code Issue** (business logic, XAML, navigation)
2. **Platform Issue** (permissions, lifecycle, native APIs)  
3. **Tooling Issue** (Hot Reload, build, deployment)

Identify the category first, then apply the appropriate debugging strategy.

**You've got this!** 🚀

---

### Quick Win: Your Next MAUI Debugging Session

1. Reproduce the issue consistently
2. Check the Output window for XAML Hot Reload messages
3. Add logging to MauiProgram.cs
4. Use Debug.WriteLine() to trace execution  
5. Check platform-specific logs if needed
6. Test on different platforms to isolate the issue

The key to MAUI debugging is methodical elimination: shared code first, then platform-specific investigation.