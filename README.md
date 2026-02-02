# SauceBucket


## !!Refer To The These Handbooks Before Starting!!

### --> [Project Structure](PROJECT_STRUCTURE.md) 
### --> [Contributing Handbook](CONTRIBUTING_HANDBOOK.md) 
### --> [MAUI Navigation & Debugging Guide](MAUI_NAV_DEBUG_GUIDE.md) 

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
