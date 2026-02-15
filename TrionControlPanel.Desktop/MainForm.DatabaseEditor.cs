// =============================================================================
// MainForm.DatabaseEditor.cs
// Purpose: RealmList and Account management UI logic
// Related UI: TabDatabaseEditor, RealmList fields, Account creation/GM fields
// Dependencies: RealmListManager, AccountManager, AccessManager
// =============================================================================

using System.Globalization;
using TrionControlPanel.Desktop.Extensions.Classes.Monitor;
using TrionControlPanel.Desktop.Extensions.Classes.Network;
using TrionControlPanel.Desktop.Extensions.Database;
using TrionControlPanel.Desktop.Extensions.Modules;
using TrionControlPanel.Desktop.Extensions.Modules.Lists;
using TrionControlPanel.Desktop.Extensions.Notification;
using TrionControlPanelDesktop.Extensions.Modules;
using static TrionControlPanel.Desktop.Extensions.Modules.Enums;
using static TrionControlPanel.Desktop.Extensions.Notification.AlertBox;

namespace TrionControlPanelDesktop
{
    /// <summary>
    /// Partial class containing database editor functionality.
    /// Handles RealmList CRUD operations and Account management.
    /// </summary>
    public partial class MainForm
    {
        #region RealmList Loading
        // ─────────────────────────────────────────────────────────────────────

        /// <summary>
        /// Loads all realm lists from the database into the combo boxes.
        /// Supports different database schemas for Trinity-based and Mangos-based cores.
        /// </summary>
        private async Task LoadRealmList()
        {
            // Clear existing items
            CBoxGMRealmSelect.Items.Clear();
            CBOXReamList.Items.Clear();

            // Add "All" option for GM realm selection
            CBoxGMRealmSelect.Items.Add("All");
            CBoxGMRealmSelect.SelectedIndex = 0;

            try
            {
                // Load realms based on selected core type
                if (IsTrinityBasedCore())
                {
                    var realmLists = await RealmListManager.GetRealmListsAsync<Realmlist.TrinityBased>(settings);
                    foreach (var realm in realmLists)
                    {
                        CBoxGMRealmSelect.Items.Add(realm.ID);
                        CBOXReamList.Items.Add(realm.Name);
                        TrionLogger.Log($"Realm List Loaded {realm.Name}");
                    }
                }
                else if (IsMangosBasedCore())
                {
                    var realmLists = await RealmListManager.GetRealmListsAsync<Realmlist.MangosBased>(settings);
                    foreach (var realm in realmLists)
                    {
                        CBoxGMRealmSelect.Items.Add(realm.ID);
                        CBOXReamList.Items.Add(realm.Name);
                        TrionLogger.Log($"Realm List Loaded {realm.Name}");
                    }
                }

                // Select first item if available
                if (CBOXReamList.Items.Count > 0) CBOXReamList.SelectedIndex = 0;
                if (CBoxGMRealmSelect.Items.Count > 0) CBoxGMRealmSelect.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                TrionLogger.Log(ex.Message, "ERROR");
            }
        }

        /// <summary>
        /// Loads the internal and external IP addresses into their respective fields.
        /// </summary>
        private async Task LoadIPAdress()
        {
            TXTInternIP.Text = NetworkManager.GetInternalIpAddress();
            TXTPublicIP.Text = await NetworkManager.GetExternalIpAddress(Links.APIRequests.GetExternalIPv4());
        }

        /// <summary>
        /// Checks if the selected core is Trinity-based (uses Trinity realm structure).
        /// </summary>
        private bool IsTrinityBasedCore()
        {
            return settings.SelectedCore == Cores.TrinityCore ||
                   settings.SelectedCore == Cores.TrinityCore335 ||
                   settings.SelectedCore == Cores.TrinityCoreClassic ||
                   settings.SelectedCore == Cores.AzerothCore ||
                   settings.SelectedCore == Cores.CypherCore;
        }

        /// <summary>
        /// Checks if the selected core is Mangos-based (uses Mangos realm structure).
        /// </summary>
        private bool IsMangosBasedCore()
        {
            return settings.SelectedCore == Cores.CMaNGOS ||
                   settings.SelectedCore == Cores.VMaNGOS;
        }

        #endregion

        #region RealmList Selection and Display
        // ─────────────────────────────────────────────────────────────────────

        /// <summary>
        /// Handles realm list combo box selection change.
        /// </summary>
        private void CBOXReamList_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectRealmList();
        }

        /// <summary>
        /// Loads the selected realm's data into the edit fields.
        /// Enables/disables fields based on the selected core type.
        /// </summary>
        private async void SelectRealmList()
        {
            if (IsTrinityBasedCore())
            {
                var realmLists = await RealmListManager.GetRealmListsAsync<Realmlist.TrinityBased>(settings);
                var selected = realmLists.Find(r =>
                    r.Name.ToString(CultureInfo.InvariantCulture) == CBOXReamList.SelectedItem?.ToString());

                if (selected != null)
                {
                    // Populate fields with selected realm data
                    TXTRealmID.Text = selected.ID.ToString(CultureInfo.InvariantCulture);
                    TXTRealmName.Text = selected.Name;
                    TXTRealmLocalAddress.Text = selected.LocalAddress;
                    TXTRealmSubnetMask.Text = selected.LocalSubnetMask;
                    TXTRealmPort.Text = selected.Port.ToString(CultureInfo.InvariantCulture);
                    TXTRealmGameBuild.Text = selected.GameBuild.ToString(CultureInfo.InvariantCulture);
                    TXTRealmAddress.Text = selected.Address;
                    TXTRealmAddress.Hint = translator.Translate("TXTRealmAddress");

                    // Enable all fields for Trinity-based cores
                    SetRealmFieldsEnabled(true, true, true);
                }
            }
            else if (IsMangosBasedCore())
            {
                var realmLists = await RealmListManager.GetRealmListsAsync<Realmlist.MangosBased>(settings);
                var selected = realmLists.Find(r =>
                    r.Name.ToString(CultureInfo.InvariantCulture) == CBOXReamList.SelectedItem?.ToString());

                if (selected != null)
                {
                    // Populate fields (Mangos doesn't have local address/subnet)
                    TXTRealmName.Text = selected.Name;
                    TXTRealmLocalAddress.Text = "N/A";
                    TXTRealmSubnetMask.Text = "N/A";
                    TXTRealmPort.Text = selected.Port.ToString(CultureInfo.InvariantCulture);
                    TXTRealmGameBuild.Text = selected.GameBuild.ToString(CultureInfo.InvariantCulture);
                    TXTRealmAddress.Text = selected.Address;
                    TXTRealmAddress.Hint = translator.Translate("TXTRealmAddress");

                    // Disable local address fields for Mangos-based cores
                    SetRealmFieldsEnabled(true, false, false);
                }
            }
        }

        /// <summary>
        /// Helper to enable/disable realm edit fields.
        /// </summary>
        private void SetRealmFieldsEnabled(bool basic, bool localAddress, bool subnetMask)
        {
            TXTRealmName.Enabled = basic;
            TXTRealmPort.Enabled = basic;
            TXTRealmGameBuild.Enabled = basic;
            TXTRealmAddress.Enabled = basic;
            TXTRealmLocalAddress.Enabled = localAddress;
            TXTRealmSubnetMask.Enabled = subnetMask;
        }

        #endregion

        #region RealmList CRUD Operations
        // ─────────────────────────────────────────────────────────────────────

        /// <summary>
        /// Handles the Edit button click - toggles edit mode for realm data.
        /// </summary>
        private async void BTNEditRealmlistData_Click(object sender, EventArgs e)
        {
            if (_editRealmList)
            {
                // Save changes and exit edit mode
                await SaveRealmListData();
                BTNEditRealmlistData.Text = translator.Translate("BTNEditRealmlistDataON");
                BTNEditRealmlistData.Refresh();
                SetRealmFieldsReadOnly(true);
                _editRealmList = false;
            }
            else
            {
                // Enter edit mode
                BTNEditRealmlistData.Text = translator.Translate("BTNEditRealmlistDataOFF");
                BTNEditRealmlistData.Refresh();
                SetRealmFieldsReadOnly(false);
                _editRealmList = true;
            }
        }

        /// <summary>
        /// Handles the Create button click - toggles create mode for new realm.
        /// </summary>
        private async void BTNCreateRealmList_Click(object sender, EventArgs e)
        {
            if (_editRealmList)
            {
                // Create new realm and exit create mode
                await CreateRealmListData();
                BTNCreateRealmList.Text = translator.Translate("BTNCreateRealmlistDataON");
                BTNCreateRealmList.Refresh();
                SetRealmFieldsReadOnly(true);
                _editRealmList = false;
                await LoadRealmList();
            }
            else
            {
                // Enter create mode - clear fields for new entry
                TXTRealmName.Text = string.Empty;
                TXTRealmID.Text = string.Empty;
                TXTRealmLocalAddress.Text = string.Empty;
                TXTRealmPort.Text = string.Empty;
                TXTRealmAddress.Text = string.Empty;
                BTNCreateRealmList.Text = translator.Translate("BTNCreateRealmlistDataOFF");
                BTNCreateRealmList.Refresh();
                SetRealmFieldsReadOnly(false);
                _editRealmList = true;
            }
        }

        /// <summary>
        /// Handles the Delete button click - removes the selected realm.
        /// </summary>
        private async void BTNDeleteRealmList_Click(object sender, EventArgs e)
        {
            var id = Convert.ToInt32(TXTRealmID.Text, CultureInfo.InvariantCulture);
            await RealmListManager.DeleteRealmListAsync(settings, id);
            await LoadRealmList();
        }

        /// <summary>
        /// Handles the Refresh button click - reloads realm list from database.
        /// </summary>
        private async void BTNForceRefresh_Click(object sender, EventArgs e)
        {
            await LoadRealmList();
        }

        /// <summary>
        /// Sets all realm edit fields to read-only or editable.
        /// </summary>
        private void SetRealmFieldsReadOnly(bool readOnly)
        {
            TXTRealmName.ReadOnly = readOnly;
            TXTRealmLocalAddress.ReadOnly = readOnly;
            TXTRealmSubnetMask.ReadOnly = readOnly;
            TXTRealmPort.ReadOnly = readOnly;
            TXTRealmGameBuild.ReadOnly = readOnly;
            TXTRealmAddress.ReadOnly = readOnly;
        }

        /// <summary>
        /// Saves the current realm data to the database (UPDATE operation).
        /// </summary>
        private async Task SaveRealmListData()
        {
            if (IsTrinityBasedCore())
            {
                await AccessManager.SaveData(SqlQueryManager.UpdateRealmList(settings.SelectedCore), new
                {
                    ID = TXTRealmID.Text,
                    Name = TXTRealmName.Text,
                    Address = TXTRealmAddress.Text,
                    LocalAddress = TXTRealmLocalAddress.Text,
                    LocalSubnetMask = TXTRealmSubnetMask.Text,
                    Port = TXTRealmPort.Text,
                    GameBuild = TXTRealmGameBuild.Text
                }, AccessManager.ConnectionString(settings, settings.AuthDatabase));
            }
            else if (IsMangosBasedCore())
            {
                await AccessManager.SaveData(SqlQueryManager.UpdateRealmList(settings.SelectedCore), new
                {
                    ID = TXTRealmID.Text,
                    Name = TXTRealmName.Text,
                    Address = TXTRealmAddress.Text,
                    Port = TXTRealmPort.Text,
                    GameBuild = TXTRealmGameBuild.Text
                }, AccessManager.ConnectionString(settings, settings.AuthDatabase));
            }
        }

        /// <summary>
        /// Creates a new realm in the database (INSERT operation).
        /// </summary>
        private async Task CreateRealmListData()
        {
            if (IsTrinityBasedCore())
            {
                await AccessManager.SaveData(SqlQueryManager.CreateRealmList(settings.SelectedCore), new
                {
                    Name = TXTRealmName.Text,
                    Address = TXTRealmAddress.Text,
                    LocalAddress = TXTRealmLocalAddress.Text,
                    LocalSubnetMask = TXTRealmSubnetMask.Text,
                    Port = TXTRealmPort.Text,
                    GameBuild = TXTRealmGameBuild.Text
                }, AccessManager.ConnectionString(settings, settings.AuthDatabase));
            }
            else if (IsMangosBasedCore())
            {
                await AccessManager.SaveData(SqlQueryManager.UpdateRealmList(settings.SelectedCore), new
                {
                    Name = TXTRealmName.Text,
                    Address = TXTRealmAddress.Text,
                    Port = TXTRealmPort.Text,
                    GameBuild = TXTRealmGameBuild.Text
                }, AccessManager.ConnectionString(settings, settings.AuthDatabase));
            }
        }

        #endregion

        #region IP Address Operations
        // ─────────────────────────────────────────────────────────────────────

        /// <summary>
        /// Sets the realm address to the public IP or DDNS domain.
        /// </summary>
        private async void BTNOpenPublic_Click(object sender, EventArgs e)
        {
            // Use public IP if no DDNS domain configured
            if (string.IsNullOrEmpty(settings.DDNSDomain) &&
                !string.IsNullOrEmpty(TXTPublicIP.Text) &&
                TXTPublicIP.Text != "0.0.0.0")
            {
                await RealmListManager.UpdateRealmListAddressAsync(
                    int.Parse(TXTRealmID!.Text, CultureInfo.InvariantCulture),
                    TXTPublicIP.Text,
                    settings);
                await LoadRealmList();
                return;
            }

            // Use DDNS domain if configured and valid
            if (!string.IsNullOrEmpty(settings.DDNSDomain) &&
                NetworkManager.IsDomainName(settings.DDNSDomain))
            {
                await RealmListManager.UpdateRealmListAddressAsync(
                    int.Parse(TXTRealmID!.Text, CultureInfo.InvariantCulture),
                    settings.DDNSDomain,
                    settings);
                await LoadRealmList();
                return;
            }

            MaterialSkin.Controls.MaterialMessageBox.Show("");
        }

        /// <summary>
        /// Sets the realm address to the internal/LAN IP.
        /// </summary>
        private async void BTNOpenIntern_Click(object sender, EventArgs e)
        {
            TXTRealmAddress.Text = TXTInternIP.Text;
            await SaveRealmListData();
        }

        /// <summary>
        /// Toggles visibility of the public IP (password mask).
        /// </summary>
        private void BTNReviveIP_Click(object sender, EventArgs e)
        {
            TXTPublicIP.PasswordChar = TXTPublicIP.PasswordChar == '⛊' ? '\0' : '⛊';
        }

        #endregion

        #region Account Management
        // ─────────────────────────────────────────────────────────────────────

        /// <summary>
        /// Creates a new game account with the specified credentials.
        /// </summary>
        private async void BTNAccountCreate_Click(object sender, EventArgs e)
        {
            var result = await AccountManager.CreateAccount(
                TXTBoxCreateUserUsername.Text,
                TXTBoxCreateUserPassword.Text,
                TXTBoxCreateUserEmail.Text,
                settings);

            if (result == AccountOpResult.Ok)
            {
                AlertBox.Show(translator.Translate("AccountSuccessCreate"), NotificationType.Info, settings);
            }
            else
            {
                AlertBox.Show(translator.Translate("AccountFaildCreate"), NotificationType.Info, settings);
            }

            // Clear input fields
            TXTBoxCreateUserUsername.Text = string.Empty;
            TXTBoxCreateUserPassword.Text = string.Empty;
            TXTBoxCreateUserEmail.Text = string.Empty;
        }

        /// <summary>
        /// Sets the GM (Game Master) level for an account.
        /// </summary>
        private async void BTNGMCreate_Click(object sender, EventArgs e)
        {
            int userID = await AccountManager.GetUserID(TXTBoxGMUsername.Text, settings);
            int gmLevel = CBOXAccountSecurityAccess.SelectedIndex;

            // Parse realm ID from selection
            int realmId = CBoxGMRealmSelect.SelectedItem?.ToString() switch
            {
                "ALL" => -1,  // Apply to all realms
                string s when int.TryParse(s, out var id) => id,
                _ => 0
            };

            var result = await AccountManager.SetGMLevel(userID, gmLevel, realmId, settings);

            if (result == AccountOpResult.GMSet)
            {
                AlertBox.Show("Set", NotificationType.Info, settings);
            }
            else
            {
                AlertBox.Show("Faild", NotificationType.Info, settings);
            }

            TXTBoxGMUsername.Text = string.Empty;
        }

        #endregion
    }
}
