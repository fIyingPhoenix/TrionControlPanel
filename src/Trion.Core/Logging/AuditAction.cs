namespace Trion.Core.Logging;

public static class AuditAction
{
    public const string LoginSuccess    = "LOGIN_SUCCESS";
    public const string LoginFailure    = "LOGIN_FAILURE";
    public const string LoginLockout    = "LOGIN_LOCKOUT";
    public const string TokenRefresh    = "TOKEN_REFRESH";
    public const string TokenRevoke     = "TOKEN_REVOKE";
    public const string Logout          = "LOGOUT";

    public const string ServerStart     = "SERVER_START";
    public const string ServerStop      = "SERVER_STOP";
    public const string ServerRestart   = "SERVER_RESTART";
    public const string ServerCrash     = "SERVER_CRASH";

    public const string ServiceStart    = "SERVICE_START";
    public const string ServiceStop     = "SERVICE_STOP";
    public const string ServiceRestart  = "SERVICE_RESTART";

    public const string AccountCreate   = "ACCOUNT_CREATE";
    public const string AccountDelete   = "ACCOUNT_DELETE";
    public const string AccountGmChange = "ACCOUNT_GM_CHANGE";

    public const string BanApplied      = "BAN_APPLIED";
    public const string BanRemoved      = "BAN_REMOVED";
    public const string TicketClosed    = "TICKET_CLOSED";

    public const string BackupStarted   = "BACKUP_STARTED";
    public const string BackupCompleted = "BACKUP_COMPLETED";
    public const string BackupFailed    = "BACKUP_FAILED";
    public const string RestoreStarted  = "RESTORE_STARTED";

    public const string UpdateApplied   = "UPDATE_APPLIED";
    public const string UpdateFailed    = "UPDATE_FAILED";
    public const string UpdateRolledBack = "UPDATE_ROLLED_BACK";

    public const string DdnsUpdated     = "DDNS_UPDATED";
    public const string PnLinked        = "PN_LINKED";
    public const string PnUnlinked      = "PN_UNLINKED";
}
