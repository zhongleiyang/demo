namespace Aliyun.OpenServices
{
    using System;
    using System.Runtime.CompilerServices;

    public class ClientConfiguration : ICloneable
    {
        private int _connectionTimeout = 0xea60;
        private static string _defaultUserAgent = ("aliyun-openservices-sdk-dotnet_" + typeof(ClientConfiguration).Assembly.GetName().Version.ToString());
        private int _maxErrorRetry = 3;
        private int _proxyPort = -1;
        private string _userAgent = _defaultUserAgent;

        public object Clone()
        {
            return new ClientConfiguration { ConnectionTimeout = this.ConnectionTimeout, MaxErrorRetry = this.MaxErrorRetry, ProxyDomain = this.ProxyDomain, ProxyHost = this.ProxyHost, ProxyPassword = this.ProxyPassword, ProxyPort = this.ProxyPort, ProxyUserName = this.ProxyUserName, UserAgent = this.UserAgent };
        }

        public int ConnectionTimeout
        {
            get
            {
                return this._connectionTimeout;
            }
            set
            {
                this._connectionTimeout = value;
            }
        }

        public int MaxErrorRetry
        {
            get
            {
                return this._maxErrorRetry;
            }
            set
            {
                this._maxErrorRetry = value;
            }
        }

        public string ProxyDomain { get; set; }

        public string ProxyHost { get; set; }

        public string ProxyPassword { get; set; }

        public int ProxyPort
        {
            get
            {
                return this._proxyPort;
            }
            set
            {
                this._proxyPort = value;
            }
        }

        public string ProxyUserName { get; set; }

        public string UserAgent
        {
            get
            {
                return this._userAgent;
            }
            set
            {
                this._userAgent = value;
            }
        }
    }
}

