# SauceBucket


## !!Refer To These Handbooks Before Starting!!

### --> [Project Structure](PROJECT_STRUCTURE.md) 
### --> [Contributing Handbook](CONTRIBUTING_HANDBOOK.md) 
### --> [Avalonia Navigation & Debugging Guide](AVALONIA_NAV_DEBUG_GUIDE.md) 

## Getting started

### Avalonia Configuration

**Step 1: Clone and enter repo**
```bash
git clone https://github.com/NepSauce/saucebucket.git
cd saucebucket
```

**Step 2: Install .NET 9 SDK**
```bash
# Avalonia doesn't require workloads - just .NET SDK
dotnet --version
```

**Step 3: Restore & build**
```bash
dotnet restore
dotnet build
```

**Step 4: Run the application**

**Desktop (Windows, macOS, Linux):**
```bash
dotnet run
```

**Development with Hot Reload:**
```bash
dotnet watch run
```

**Platform-specific builds:**
```bash
# Windows
dotnet publish -c Release -r win-x64 --self-contained

# macOS
dotnet publish -c Release -r osx-x64 --self-contained

# Linux
dotnet publish -c Release -r linux-x64 --self-contained
```

### Troubleshoot
- **If NuGet packages missing:**
```bash
dotnet restore
```

- **Clean build issues:**
```bash
dotnet clean
dotnet restore
dotnet build
```

- **Hot reload not working:**
```bash
dotnet watch run
```

- **Platform-specific issues:**
```bash
# Check installed .NET versions
dotnet --list-sdks

# Check runtime versions
dotnet --list-runtimes
```
