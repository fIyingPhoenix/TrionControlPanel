; ============================================================
;  Trion Control Panel — Inno Setup Script
;  TrionBlue Dark theme:  #141414 bg · #00D4FF accent
;  Installs to %APPDATA%\Trion — no UAC required
;  Compatible with Inno Setup 6.x
; ============================================================

#define AppName      "Trion Control Panel"
#define AppVersion   "0.5.0"
#define AppPublisher "FlyingPhoenix"
#define AppExeName   "Trion.Desktop.exe"
#define AppURL       "https://flying-phoenix.dev/"
#define CertSubject  "CN=FlyingPhoenix"
#define CertFile     "TrionCert.cer"
#define SourceDir    "..\src\Trion.Desktop\bin\Release\net9.0-windows10.0.17763.0"

; ── TrionBlue Dark colour palette (Windows BGR format) ──────────────────────
;   RGB #141414  →  BGR $141414   background primary
;   RGB #1C1C1C  →  BGR $1C1C1C   background secondary / banner
;   RGB #2E2E2E  →  BGR $2E2E2E   border / track
;   RGB #E8E8E8  →  BGR $E8E8E8   foreground primary
;   RGB #9A9A9A  →  BGR $9A9A9A   foreground secondary
;   RGB #00D4FF  →  BGR $FFD400   accent cyan

; ── Wizard sidebar image ─────────────────────────────────────────────────────
;  Create a 164×314 dark PNG: #141414 fill with the Trion logo centred.
;  Save to Installer\Assets\WizardSidebar.bmp (Inno Setup requires BMP here).
;  Uncomment WizardImageFile below once it exists.
; WizardImageFile = Assets\WizardSidebar.bmp

[Setup]
AppId                     = {{F3A2D1B0-7C4E-4F8A-9B5C-2E6D3A1F8E7D}
AppName                   = {#AppName}
AppVersion                = {#AppVersion}
AppPublisher              = {#AppPublisher}
AppPublisherURL           = {#AppURL}
AppSupportURL             = {#AppURL}
AppUpdatesURL             = {#AppURL}
DefaultDirName            = {userappdata}\Trion
DefaultGroupName          = {#AppName}
DisableProgramGroupPage   = yes
PrivilegesRequired        = lowest
PrivilegesRequiredOverridesAllowed = dialog
OutputDir                 = ..\Build
OutputBaseFilename        = TrionSetup-{#AppVersion}
SetupIconFile             = ..\Resources\icons\logo\favicon.ico
WizardSmallImageFile      = ..\Resources\icons\logo\TrionLogoNew250.png
Compression               = lzma2/ultra64
SolidCompression          = yes
WizardStyle               = modern
UninstallDisplayIcon      = {app}\{#AppExeName}
UninstallDisplayName      = {#AppName}
VersionInfoVersion        = {#AppVersion}
VersionInfoCompany        = {#AppPublisher}
VersionInfoDescription    = {#AppName} Installer
VersionInfoProductName    = {#AppName}
VersionInfoProductVersion = {#AppVersion}

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"
Name: "german";  MessagesFile: "compiler:Languages\German.isl"

[Tasks]
Name: "desktopicon";   Description: "{cm:CreateDesktopIcon}";       GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked
Name: "startmenuicon"; Description: "Create a &Start Menu shortcut"; GroupDescription: "{cm:AdditionalIcons}"

[Files]
; ── Main executable ──────────────────────────────────────────────────────────
Source: "{#SourceDir}\{#AppExeName}";    DestDir: "{app}"; Flags: ignoreversion

; ── Updater ──────────────────────────────────────────────────────────────────
Source: "{#SourceDir}\TrionUpdater.exe"; DestDir: "{app}"; Flags: ignoreversion

; ── Config ───────────────────────────────────────────────────────────────────
Source: "{#SourceDir}\appsettings.json"; DestDir: "{app}"; Flags: ignoreversion onlyifdoesntexist

; ── Localisation ─────────────────────────────────────────────────────────────
Source: "{#SourceDir}\Resources\Localization\Strings.en-US.json"; DestDir: "{app}\Resources\Localization"; Flags: ignoreversion skipifsourcedoesntexist
Source: "{#SourceDir}\Resources\Localization\Strings.de-DE.json"; DestDir: "{app}\Resources\Localization"; Flags: ignoreversion skipifsourcedoesntexist

; ── Self-signed certificate (optional) ───────────────────────────────────────
; Generate: New-SelfSignedCertificate -Subject "CN=FlyingPhoenix" -CertStoreLocation "Cert:\CurrentUser\My" -Type CodeSigningCert
; Export:   Export-Certificate -Cert (Get-Item Cert:\CurrentUser\My\<thumbprint>) -FilePath Installer\TrionCert.cer
; Uncomment when the file exists:
; Source: "{#CertFile}"; DestDir: "{tmp}"; Flags: ignoreversion deleteafterinstall

[Icons]
Name: "{group}\{#AppName}";           Filename: "{app}\{#AppExeName}"; Tasks: startmenuicon
Name: "{group}\Uninstall {#AppName}"; Filename: "{uninstallexe}";      Tasks: startmenuicon
Name: "{autodesktop}\{#AppName}";     Filename: "{app}\{#AppExeName}"; Tasks: desktopicon

[Run]
; ── (Optional) import cert ───────────────────────────────────────────────────
; Filename: "certutil.exe"; Parameters: "-user -addstore TrustedPublisher ""{tmp}\{#CertFile}"""; Flags: runhidden waituntilterminated; StatusMsg: "Installing certificate…"

; ── Launch after install ─────────────────────────────────────────────────────
Filename: "{app}\{#AppExeName}"; Description: "{cm:LaunchProgram,{#StringChange(AppName,'&','&&')}}"; Flags: nowait postinstall skipifsilent

[UninstallRun]
; Filename: "certutil.exe"; Parameters: "-user -delstore TrustedPublisher ""{#CertSubject}"""; Flags: runhidden waituntilterminated

[UninstallDelete]
Type: filesandordirs; Name: "{userappdata}\Trion\logs"
Type: filesandordirs; Name: "{userappdata}\Trion\cache"

; ============================================================
;  [Code] — Dark theme + install guards
; ============================================================
[Code]

// ── TrionBlue Dark palette ────────────────────────────────────────────────────
const
  BG_PRIMARY    = $141414;   // #141414  main background
  BG_SECONDARY  = $1C1C1C;   // #1C1C1C  banner / card surface
  BG_INPUT      = $222222;   // slightly lighter for inputs
  BG_BORDER     = $2E2E2E;   // #2E2E2E  borders / progress track
  FG_PRIMARY    = $E8E8E8;   // #E8E8E8  primary text
  FG_SECONDARY  = $9A9A9A;   // #9A9A9A  secondary / muted text
  ACCENT_CYAN   = $FFD400;   // #00D4FF  progress fill, accent (BGR!)
  CLNONE        = $FF000000; // clNone constant

// ── Apply theme to the wizard form ───────────────────────────────────────────
procedure ApplyTrionTheme();
begin
  // ── Window shell ─────────────────────────────────────────────────────────
  WizardForm.Color                           := BG_PRIMARY;
  WizardForm.Font.Color                      := FG_PRIMARY;
  WizardForm.Font.Name                       := 'Segoe UI';
  WizardForm.Font.Size                       := 9;

  // ── Top banner (page title) ───────────────────────────────────────────────
  WizardForm.MainPanel.Color                 := BG_SECONDARY;
  WizardForm.MainPanel.Font.Color            := FG_PRIMARY;

  WizardForm.PageNameLabel.Font.Color        := FG_PRIMARY;
  WizardForm.PageNameLabel.Font.Style        := [fsBold];

  WizardForm.PageDescriptionLabel.Font.Color := FG_SECONDARY;

  // ── Inner content notebook ───────────────────────────────────────────────
  WizardForm.InnerPage.Color                 := BG_PRIMARY;

  // ── Bottom button strip ──────────────────────────────────────────────────
  WizardForm.BottomPanel.Color               := BG_SECONDARY;
  WizardForm.BottomPanel.Font.Color          := FG_PRIMARY;

  // ── Welcome page ─────────────────────────────────────────────────────────
  WizardForm.WelcomePage.Color               := BG_PRIMARY;
  WizardForm.WelcomeLabel.Font.Color         := FG_PRIMARY;
  WizardForm.WelcomeLabel.Font.Style         := [fsBold];
  WizardForm.WelcomeLabel2.Font.Color        := FG_SECONDARY;

  // ── Finished page ────────────────────────────────────────────────────────
  WizardForm.FinishedPage.Color              := BG_PRIMARY;
  WizardForm.FinishedHeadingLabel.Font.Color := FG_PRIMARY;
  WizardForm.FinishedHeadingLabel.Font.Style := [fsBold];
  WizardForm.FinishedLabel.Font.Color        := FG_SECONDARY;

  // ── Sidebar bitmap area (welcome/finished) ────────────────────────────────
  // Force the bitmap container to match our background so there's no
  // white flash if no custom WizardImageFile is configured.
  WizardForm.WizardBitmapImage.BackColor     := BG_PRIMARY;
  WizardForm.WizardBitmapImage2.BackColor    := BG_PRIMARY;
end;

// ── Apply theme to page-specific controls (called on every page change) ───────
procedure ApplyPageTheme(PageID: Integer);
begin
  // ── Select-directory page ─────────────────────────────────────────────────
  if PageID = wpSelectDir then
  begin
    WizardForm.DirEdit.Color      := BG_INPUT;
    WizardForm.DirEdit.Font.Color := FG_PRIMARY;
  end;

  // ── Select-components page ────────────────────────────────────────────────
  if PageID = wpSelectComponents then
  begin
    WizardForm.ComponentsList.Color      := BG_INPUT;
    WizardForm.ComponentsList.Font.Color := FG_PRIMARY;
  end;

  // ── Select-tasks page ────────────────────────────────────────────────────
  if PageID = wpSelectTasks then
  begin
    WizardForm.TasksList.Color      := BG_INPUT;
    WizardForm.TasksList.Font.Color := FG_PRIMARY;
  end;

  // ── Ready page ───────────────────────────────────────────────────────────
  if PageID = wpReady then
  begin
    WizardForm.ReadyMemo.Color      := BG_INPUT;
    WizardForm.ReadyMemo.Font.Color := FG_SECONDARY;
  end;

  // ── Installing page ───────────────────────────────────────────────────────
  if PageID = wpInstalling then
  begin
    // Cyan accent progress bar (uses Windows PBM_SETBARCOLOR / PBM_SETBKCOLOR)
    WizardForm.ProgressGauge.ForeColor    := ACCENT_CYAN;
    WizardForm.ProgressGauge.BackColor    := BG_BORDER;

    WizardForm.StatusLabel.Font.Color     := FG_PRIMARY;
    WizardForm.FilenameLabel.Font.Color   := FG_SECONDARY;
  end;
end;

// ── Wizard events ─────────────────────────────────────────────────────────────

procedure InitializeWizard();
begin
  ApplyTrionTheme();
  ApplyPageTheme(wpWelcome);
end;

procedure CurPageChanged(CurPageID: Integer);
begin
  ApplyPageTheme(CurPageID);
end;

// ── Prevent downgrading an existing install ───────────────────────────────────
function InitializeSetup(): Boolean;
var
  InstalledVersion: String;
begin
  Result := True;
  if RegQueryStringValue(HKCU,
        'Software\Microsoft\Windows\CurrentVersion\Uninstall\{F3A2D1B0-7C4E-4F8A-9B5C-2E6D3A1F8E7D}_is1',
        'DisplayVersion', InstalledVersion) then
  begin
    if CompareStr(InstalledVersion, '{#AppVersion}') > 0 then
    begin
      MsgBox(
        'A newer version (' + InstalledVersion + ') of {#AppName} is already installed.' + #13#10 +
        'This installer will not downgrade.',
        mbInformation, MB_OK);
      Result := False;
    end;
  end;
end;

// ── Gracefully close the running app before upgrading ─────────────────────────
function PrepareToInstall(var NeedsRestart: Boolean): String;
var
  ResultCode: Integer;
begin
  Result := '';
  // Terminate any running instance before upgrading
  Exec('taskkill.exe', '/IM "{#AppExeName}" /F', '', SW_HIDE, ewWaitUntilTerminated, ResultCode);
  Exec('taskkill.exe', '/IM "TrionControlPanel.WPF.exe" /F', '', SW_HIDE, ewWaitUntilTerminated, ResultCode);
  Sleep(600);
end;
