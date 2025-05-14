using System;
using System.Runtime.InteropServices;
using System.Net;
using TrionLibrary.Models;
namespace TrionLibrary.Network
{
    public static class Ports
    {
        [DllImport("iphlpapi.dll", SetLastError = true)]
        private static extern int GetExtendedTcpTable(IntPtr pTcpTable, ref int dwOutBufLen, bool sort, int ipVersion, Enums.TcpTableClass tblClass, int reserved);

        public static TcpProcessRecord[] GetAllTcpConnections()
        {
            var bufferSize = 0;
            var ipVersion = 2; // AF_INET (IPv4)
            var tblClass = Enums.TcpTableClass.TCP_TABLE_OWNER_PID_ALL;

            // First call to determine the size of the table.
            GetExtendedTcpTable(IntPtr.Zero, ref bufferSize, true, ipVersion, tblClass, 0);
            var tcpTablePtr = Marshal.AllocHGlobal(bufferSize);

            try
            {
                // Second call to retrieve the actual table data.
                var result = GetExtendedTcpTable(tcpTablePtr, ref bufferSize, true, ipVersion, tblClass, 0);
                if (result != 0)
                    throw new Exception($"Failed to get TCP table. Error {result}");

                var table = Marshal.PtrToStructure<MIB_TCPTABLE_OWNER_PID>(tcpTablePtr);
                var rowsPtr = (IntPtr)((long)tcpTablePtr + Marshal.SizeOf(table.dwNumEntries));
                var tcpConnections = new TcpProcessRecord[table.dwNumEntries];

                for (var i = 0; i < table.dwNumEntries; i++)
                {
                    var row = Marshal.PtrToStructure<MIB_TCPROW_OWNER_PID>(rowsPtr + i * Marshal.SizeOf(typeof(MIB_TCPROW_OWNER_PID)));
                    tcpConnections[i] = new TcpProcessRecord(new IPAddress(row.dwLocalAddr), row.dwLocalPort, row.dwOwningPid);
                }

                return tcpConnections;
            }
            finally
            {
                Marshal.FreeHGlobal(tcpTablePtr);
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct MIB_TCPROW_OWNER_PID
        {
            public uint dwState;
            public uint dwLocalAddr;
            public int dwLocalPort;
            public uint dwRemoteAddr;
            public int dwRemotePort;
            public int dwOwningPid;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct MIB_TCPTABLE_OWNER_PID
        {
            public int dwNumEntries;
            public MIB_TCPROW_OWNER_PID table;
        }

        public class TcpProcessRecord
        {
            public IPAddress LocalAddress { get; }
            public ushort LocalPort { get; }
            public int ProcessId { get; }

            public TcpProcessRecord(IPAddress localAddress, int localPort, int processId)
            {
                LocalAddress = localAddress;
                LocalPort = (ushort)IPAddress.NetworkToHostOrder((short)localPort);
                ProcessId = processId;
            }
        }
    }
}
