<div align="center">

<img src="res/icons/trion-logo/TrionLogoNew.png" width="72" alt="Trion logo" />

# Trion Control Panel

**One-click WoW emulator management — Desktop app for Windows, Web dashboard for any OS**

[![CI](https://github.com/fIyingPhoenix/TrionControlPanel/actions/workflows/dotnet.yml/badge.svg)](https://github.com/fIyingPhoenix/TrionControlPanel/actions/workflows/dotnet.yml)
![Version](https://img.shields.io/badge/version-0.5.0-00D4FF?style=flat)
![.NET](https://img.shields.io/badge/.NET-9.0-512BD4?style=flat)
![Platform](https://img.shields.io/badge/desktop-Windows%2010%2B-0078D4?style=flat)
![Platform](https://img.shields.io/badge/web-Windows%20%7C%20Linux%20%7C%20macOS-22C55E?style=flat)
![License](https://img.shields.io/github/license/fIyingPhoenix/TrionControlPanel)

[Features](#features) · [Architecture](#architecture) · [Getting Started](#getting-started) · [Configuration](#configuration) · [Contributing](#contributing)

</div>

---

## What is Trion?

Trion Control Panel is a hybrid Desktop + Web management suite for running private World of Warcraft emulators. It handles everything from downloading and installing a portable MySQL instance to starting, monitoring, and auto-restarting your World and Logon servers — with a polished dark UI and zero manual server setup.

| Variant | Runs on | Best for |
|---|---|---|
| **Desktop** (WPF) | Windows 10+ | Local development, single-machine server |
| **Web** (Blazor) | Windows · Linux · macOS | Headless / remote server management |

---

## Features

### Server Management
- Start, stop, and restart **Database** (MySQL 8), **World**, and **Logon** servers from a single panel
- **Crash detection** with configurable auto-restart (up to 5 attempts per server)
- Real-time process status polling every 3 seconds

### MySQL — Zero Configuration
- Automatically downloads and extracts **portable MySQL 8** (no system install required)
- Generates a **hardware-tuned `my.ini`** at startup — InnoDB buffer pool sized to 40 % of RAM, thread and IO counts matched to CPU core count
- Full lifecycle: install, start, stop, repair, reinstall — all from the UI

### Emulator Support

Supports installing and managing all major WoW emulator families:

| Expansion | Emulator |
|---|---|
| Classic (1.x) | CMaNGOS / AzerothCore Classic |
| TBC (2.x) | CMaNGOS TBC |
| WotLK (3.x) | TrinityCore · AzerothCore · CMaNGOS WotLK |
| Cataclysm (4.x) | TrinityCore Cata |
| MoP (5.x) | TrinityCore MoP |

### System Monitoring
- Live **CPU, RAM, and disk metrics** updated every second
- Per-process monitoring for emulator and database processes
- Sparkline history charts in the dashboard

### DDNS
- Built-in DDNS client supporting **No-IP**, **DuckDNS**, and **Cloudflare**
- Background polling on a configurable interval (default: 15 min)

### Other
- **Multi-language** UI — drop a `Strings.{locale}.json` file to add a language
- **Dark / Light theme** with a cyan `#00D4FF` accent colour, switchable at runtime
- **System tray** integration with quick start/stop controls
- **Account system** — sign in with your Trion account, supporter tier API key, or continue as guest
- **Self-updater** (`Trion.Updater`) — silent download and hot-swap of new releases

---

## Architecture

```
┌─────────────────────────────────────────────────────────┐
│                      Client Layer                       │
│  ┌─────────────────────┐   ┌──────────────────────────┐ │
│  │   Trion.Desktop     │   │   Trion.Web (Blazor)     │ │
│  │   WPF · Windows     │   │   MudBlazor · Any OS     │ │
│  └─────────┬───────────┘   └────────────┬─────────────┘ │
└────────────┼────────────────────────────┼───────────────┘
             │ DI / in-process            │ HTTP / REST
┌────────────┼────────────────────────────┼───────────────┐
│            │        Service Layer       │               │
│  ┌─────────▼───────────┐  ┌────────────▼─────────────┐ │
│  │    Trion.Core       │  │      Trion.API           │ │
│  │  Logging · Auth     │  │  JWT Auth · REST         │ │
│  │  Monitoring · DDNS  │  │  News · Downloads        │ │
│  │  Orchestration      │  │  Account management      │ │
│  └─────────┬───────────┘  └──────────────────────────┘ │
└────────────┼──────────────────────────────────────────── ┘
             │ Named Pipe (HMAC-signed commands)
┌────────────▼──────────────────────────────────────────── ┐
│              Trion.Agent  (background daemon)            │
│    Process launch · Service control · Process kill       │
│    CommandAllowlist · HmacValidator · AgentPipeServer    │
└────────────┬──────────────────────────────────────────── ┘
             │
┌────────────▼──────────────────────────────────────────── ┐
│              Trion.Platform  (OS abstraction)            │
│    Windows: WMI metrics, DPAPI secret store              │
│    Linux:   /proc metrics, file-based secret store       │
└────────────────────────────────────────────────────────── ┘
```

---

## Project Structure

```
TrionControlPanel/
├── src/
│   ├── Trion.Desktop/      # WPF control panel — Windows 10+ only
│   ├── Trion.Updater/      # WPF self-updater  — Windows only
│   ├── Trion.Web/          # Blazor Server dashboard — cross-platform
│   ├── Trion.API/          # ASP.NET Core REST API  — cross-platform
│   ├── Trion.Agent/        # Background process/service agent — cross-platform
│   ├── Trion.Platform/     # Windows / Linux OS abstraction layer
│   ├── Trion.Core/         # Shared: logging, auth, monitoring, DDNS, DI helpers
│   └── Trion.UI/           # Shared Blazor UI components
├── tests/
│   ├── Trion.Core.Tests/
│   ├── Trion.Agent.Tests/
│   └── Trion.Platform.Tests/
├── installer/              # Inno Setup script (no UAC, installs to %APPDATA%)
├── res/
│   └── icons/trion-logo/   # Shared logo assets linked into WPF projects
└── .github/workflows/      # CI: separate Windows + Linux build jobs
```

---

## Getting Started

### Prerequisites

| | Desktop | Web / API |
|---|---|---|
| OS | Windows 10 build 17763+ | Windows · Linux · macOS |
| Runtime | .NET 9.0 | .NET 9.0 |
| Database | Auto-installed by Trion | Provide a MySQL connection string |

> **Desktop users:** MySQL is downloaded, extracted, and configured automatically on first launch. No manual database setup is required.

---

### Desktop (Windows)

**Option A — Installer** *(recommended)*

1. Download the latest `TrionSetup.exe` from [Releases](https://github.com/fIyingPhoenix/TrionControlPanel/releases)
2. Run the installer — no administrator rights required (installs to `%APPDATA%\Trion`)
3. Launch **Trion Control Panel** from the Start Menu or desktop shortcut

**Option B — Build from source**

```bash
git clone https://github.com/fIyingPhoenix/TrionControlPanel.git
cd TrionControlPanel
dotnet build src/Trion.Desktop/Trion.Desktop.csproj -c Release
dotnet run   --project src/Trion.Desktop/Trion.Desktop.csproj -c Release
```

---

### Web Dashboard + API (Windows / Linux / macOS)

```bash
git clone https://github.com/fIyingPhoenix/TrionControlPanel.git
cd TrionControlPanel

# 1. Configure the API (database, JWT secret) — see Configuration below
# 2. Start the API backend
dotnet run --project src/Trion.API/Trion.API.csproj -c Release

# 3. Start the Blazor web dashboard
dotnet run --project src/Trion.Web/Trion.Web.csproj -c Release
```

Open `http://localhost:5000` in your browser.

---

### Agent *(optional — remote process control)*

The Agent is a background daemon that exposes a HMAC-signed named pipe. Desktop and Web clients use it to launch, monitor, and kill server processes without running a privileged UI.

```bash
dotnet run --project src/Trion.Agent/Trion.Agent.csproj -c Release
```

---

## Configuration

### API — `src/Trion.API/appsettings.json`

```jsonc
{
  "ConnectionStrings": {
    "Default": "Server=localhost;Port=3306;Database=TrionAPI;Uid=trion_user;Pwd=<password>;"
  },
  "AdminApiKey": "<change-this-to-a-strong-random-key>",
  "Jwt": {
    "SecretKey":   "<256-bit-minimum secret — minimum 32 characters>",
    "Issuer":      "TrionAPI",
    "Audience":    "TrionClient",
    "ExpiryHours": 24
  }
}
```

### Web — `src/Trion.Web/appsettings.json`

```jsonc
{
  "Monitoring": {
    "RefreshInterval": "00:00:02",
    "ProcessNameFilter": [ "mysqld", "worldserver", "authserver" ]
  },
  "Emulators": {
    "TrinityCore": {
      "AuthConnectionString":      "Server=...;Database=auth;...",
      "CharacterConnectionString": "Server=...;Database=characters;...",
      "WorldConnectionString":     "Server=...;Database=world;..."
    }
    // AzerothCore and CMaNGOS follow the same shape
  },
  "Ddns": {
    "Provider":     "NoIp",        // NoIp | DuckDns | Cloudflare | None
    "Hostname":     "my.example.com",
    "PollInterval": "00:15:00",
    "Username":     "",
    "Password":     ""
  }
}
```

### Logging — any `appsettings.json`

All projects share the same `Trion.Core.Logging` logger, configurable via the `Logging:Trion` section:

```jsonc
{
  "Logging": {
    "Trion": {
      "Folder":         "",     // empty = <AppDir>/Logs
      "WriteToConsole": true,
      "WriteToFile":    true,
      "ShowDebug":      false,
      "ShowInfo":       true,
      "ShowWarning":    true,
      "ShowError":      true,
      "DaysToCompress": 7,      // GZip logs older than N days
      "DaysToKeep":     30      // Delete logs older than N days
    }
  }
}
```

Log files are written to `Logs/trion-yyyyMMdd.log`, auto-compressed after 7 days, and deleted after 30 days.

---

## Building & Testing

```bash
# Full solution — Windows only (includes WPF Desktop + Updater)
dotnet build src/Trion.sln -c Release
dotnet test  src/Trion.sln -c Release

# Cross-platform — Linux / macOS (skip WPF projects)
dotnet build src/Trion.Core/Trion.Core.csproj   -c Release
dotnet build src/Trion.API/Trion.API.csproj     -c Release
dotnet build src/Trion.Agent/Trion.Agent.csproj -c Release
dotnet build src/Trion.Web/Trion.Web.csproj     -c Release
dotnet test  tests/Trion.Core.Tests/Trion.Core.Tests.csproj -c Release
```

CI runs on every push and PR to `main` and `dev`:

| Job | Runner | Projects built |
|---|---|---|
| `build-windows` | `windows-latest` | Full solution — Desktop, Updater, Web, API, Agent, Core |
| `build-linux` | `ubuntu-latest` | Cross-platform only — Web, API, Agent, Core |

---

## Contributing

1. Fork the repository and create your branch from `dev`
2. Follow the existing patterns: MVVM in Desktop, DI everywhere, `ILogger<T>` for logging
3. Add or update tests for any changed logic in `Trion.Core`, `Trion.Agent`, or `Trion.Platform`
4. Open a pull request targeting `dev` — **not** `main`

For larger changes please open an issue first to discuss the approach.

---

## License

Distributed under the terms of the [LICENSE](LICENSE) file in this repository.

---

<div align="center">

Made with care by [FlyingPhoenix](https://flying-phoenix.dev/)

</div>
