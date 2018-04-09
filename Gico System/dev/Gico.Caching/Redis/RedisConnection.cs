using System;
using System.Collections.Generic;
using StackExchange.Redis;
using Gico.Config;

namespace Gico.Caching.Redis
{
    public class RedisConnection
    {
        public static readonly string ServerIp = ConfigSettingEnum.RedisHostIps.GetConfig();
        public const int DbId = 6;
        public const int LogDbId = 9;

        private const int IoTimeOut = 100000;
        private const int SyncTimeout = 100000;
        private static SocketManager _socketManager;
        private ConnectionMultiplexer _writeConnection;
        private ConnectionMultiplexer[] _readConnections;

        private static volatile RedisConnection _instance;
        public static readonly object SyncLock = new object();
        public static readonly object SyncConnectionLock = new object();
        public static readonly object SyncReadConnectionLock = new object();
        public static string[] ServerIps => ServerIp.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);
        public static int ReadIndexConnection = 0;

        public List<string> GetServer(bool isMaster)
        {
            var config = ConfigurationOptions.Parse(ServerIp);
            config.KeepAlive = 180;
            config.SyncTimeout = SyncTimeout;
            config.AbortOnConnectFail = false;
            config.AllowAdmin = true;
            config.SocketManager = _socketManager;
            var connection = ConnectionMultiplexer.ConnectAsync(config);
            var muxer = connection.Result;
            var endpoint = ServerIp.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);

            try
            {
                foreach (var endPoint in endpoint)
                {
                    var server = muxer.GetServer(endPoint);
                    if (isMaster)
                    {
                        if (server != null && server.IsConnected && !server.IsSlave)
                        {
                            return new List<string>() { endPoint };
                        }
                    }
                    else
                    {
                        List<string> endPoints = new List<string>();
                        if (server != null && server.IsConnected)
                        {
                            endPoints.Add(endPoint);
                        }
                        return endPoints;
                    }


                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            throw new Exception("Redis not found");
        }

        public static RedisConnection Current
        {
            get
            {
                if (_instance == null)
                {
                    lock (SyncLock)
                    {
                        if (_instance == null)
                        {
                            _instance = new RedisConnection();
                        }
                    }
                }

                return _instance;
            }
        }

        private RedisConnection()
        {
            _socketManager = new SocketManager(GetType().Name);
            _writeConnection = GetNewWriteConnection();
            _readConnections = GetNewReadConnections();
        }

        private ConnectionMultiplexer GetNewWriteConnection()
        {
            var writeIp = GetServer(true);
            if (writeIp.Count > 0)
            {
                var config = ConfigurationOptions.Parse(writeIp[0]);
                config.KeepAlive = 180;
                config.SyncTimeout = SyncTimeout;
                config.AbortOnConnectFail = true;
                config.AllowAdmin = true;
                //config.ConnectTimeout = IoTimeOut;
                config.SocketManager = _socketManager;
                config.ConnectRetry = 5;
                //config.CommandMap = CommandMap.Sentinel;
                //var connection = ConnectionMultiplexer.Connect(config/*, logger*/);
                var connection = ConnectionMultiplexer.Connect(config);
                var muxer = connection;
                return muxer;
            }
            else
            {
                throw new Exception("Redis Write not found");
            }
        }
        private ConnectionMultiplexer[] GetNewReadConnections()
        {
            var readIps = GetServer(false);
            if (readIps.Count > 0)
            {
                ConnectionMultiplexer[] connectionMultiplexers = new ConnectionMultiplexer[readIps.Count];
                for (int i = 0; i < readIps.Count; i++)
                {
                    var config = ConfigurationOptions.Parse(readIps[i]);
                    config.KeepAlive = 180;
                    config.SyncTimeout = SyncTimeout;
                    config.AbortOnConnectFail = true;
                    config.AllowAdmin = true;
                    //config.ConnectTimeout = IoTimeOut;
                    config.SocketManager = _socketManager;
                    config.ConnectRetry = 5;
                    //config.CommandMap = CommandMap.Sentinel;
                    //var connection = ConnectionMultiplexer.Connect(config/*, logger*/);
                    var connection = ConnectionMultiplexer.Connect(config);
                    var muxer = connection;
                    connectionMultiplexers[i] = muxer;
                }
                return connectionMultiplexers;
            }
            else
            {
                throw new Exception("Redis Read not found");
            }
        }

        public ConnectionMultiplexer GetWriteConnection
        {
            get
            {
                lock (SyncConnectionLock)
                {
                    if (_writeConnection == null)
                    {
                        _writeConnection = GetNewWriteConnection();
                    }
                    else
                    {
                        if (!_writeConnection.IsConnected)
                            _writeConnection = GetNewWriteConnection();
                    }
                    return _writeConnection;
                }
            }
        }


        public static IDatabase WriteConnection
        {
            get
            {
                var connection = Current.GetWriteConnection.GetDatabase(DbId);
                return connection;
            }
        }

        public static IDatabase GetCurrentWriteConnection(int dbId)
        {
            var connection = Current.GetWriteConnection.GetDatabase(dbId);
            return connection;
        }

        public ConnectionMultiplexer GetReadConnection
        {
            get
            {
                lock (SyncReadConnectionLock)
                {
                    var connection = _readConnections[ReadIndexConnection++];
                    if (ReadIndexConnection >= _readConnections.Length)
                    {
                        ReadIndexConnection = 0;
                    }
                    if (connection == null)
                    {
                        _readConnections = GetNewReadConnections();
                    }
                    else
                    {
                        if (!connection.IsConnected)
                            _readConnections = GetNewReadConnections();
                    }
                    return connection;
                }
            }
        }

        public static IDatabase ReadConnection
        {
            get
            {
                var connection = Current.GetReadConnection.GetDatabase(DbId);
                return connection;
            }
        }

        public static IDatabase GetCurrentReadConnection(int dbId)
        {
            var connection = Current.GetReadConnection.GetDatabase(dbId);
            return connection;
        }

    }

}