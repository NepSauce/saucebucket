# SauceBucket


## !!Refer To The Contributing Handbook Before Starting!!


### --> [Contributing Handbook](CONTRIBUTING_HANDBOOK.md) <--


## Getting started

### .NET MAUI Configuration

**Step 1: Clone and enter repo**
```bash
git clone https://github.com/NepSauce/saucebucket.git
cd saucebucket
```

**Step 2: Install .NET 9 SDK and MAUI workload**
```bash
dotnet workload install maui
```

**Step 3: Restore & build**
```bash
dotnet restore
dotnet build
```

**Step 4: Run the application**

Choose your target platform:

- **Windows:**
```bash
dotnet run -f net9.0-windows10.0.19041.0
```

- **Android (emulator/device):**
```bash
dotnet run -f net9.0-android
```

- **iOS (macOS + Xcode required):**
```bash
dotnet run -f net9.0-ios
```

- **Mac Catalyst (macOS required):**
```bash
dotnet run -f net9.0-maccatalyst
```

- **Tizen (requires Tizen workload/SDK):**
```bash
dotnet run -f net9.0-tizen
```

**Multi-target builds:**
```bash
dotnet build -f <TFM>
```

### Key files
- `SauceBucket.csproj` — project config and app icon.
- `AppShell.xaml` — Shell and top-left icon behavior.
- `Resources/Styles/Colors.xaml` — change `Primary` color here.

### Troubleshoot
- **If workloads missing:**
```bash
dotnet workload install maui
```

- **Clean build issues:**
```bash
dotnet clean
dotnet restore
```
