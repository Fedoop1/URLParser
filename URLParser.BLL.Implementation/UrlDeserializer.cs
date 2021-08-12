using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using NLog;
using NLog.Config;
using NLog.Targets;
using URLParser.BLL.Contract;
using URLParser.BLL.Implementation.Entities;

namespace URLParser.BLL.Implementation
{
    /// <summary>
    /// Class that deserialize URL information from string format to <see cref="Url"/> class.
    /// </summary>
    /// <seealso cref="URLParser.BLL.Contract.IDeserializer&lt;IEnumerable&lt;URLParser.BLL.Implementation.Entities.Url&gt;&gt;" />
    public class UrlDeserializer : IDeserializer<string, IEnumerable<Url>>
    {
        private readonly ILogger logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="UrlDeserializer"/> class with specified logger instance.
        /// </summary>
        /// <param name="logger">The logger instance.</param>
        /// <exception cref="System.ArgumentNullException">Throws when logger is null.</exception>
        public UrlDeserializer(ILogger logger) => this.logger =
            logger ?? throw new ArgumentNullException(nameof(logger), "Logger can't be null");

        /// <summary>
        /// Initializes a new instance of the <see cref="UrlDeserializer"/> class with default logger.
        /// </summary>
        public UrlDeserializer()
        {
            var logConfiguration = new LoggingConfiguration();
            logConfiguration.AddTarget(new ConsoleTarget("Console") { Layout = "${time} | ${level:uppercase=true} | ${logger} | ${message}" });
            logConfiguration.AddRule(LogLevel.Error, LogLevel.Fatal, "Console");
            LogManager.Configuration = logConfiguration;
            this.logger = LogManager.GetLogger("Console");
        }

        /// <summary>
        /// Gets or sets the URL records delimiter.
        /// </summary>
        /// <value>
        /// The delimiter.
        /// </value>
        public char Delimiter { get; set; } = '\r';

        /// <summary>
        /// Deserializes the specified data from one string format to <see cref="Url"/>.
        /// </summary>
        /// <param name="data">The URL data separated by delimiter.</param>
        /// <returns>
        /// Sequence of deserialized <see cref="Url"/> valid objects, if <see cref="Url"/> is invalid - log information into log instance.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">Throws when source data is null.</exception>
        public IEnumerable<Url> Deserialize(string data)
        {
            if (string.IsNullOrEmpty(data))
            {
                throw new ArgumentNullException(nameof(data), "Source data can't be null or empty");
            }

            var result = new List<Url>();
            var urls = data.Split(this.Delimiter);
            int row = 0;

            foreach (var urlData in urls)
            {
                ++row;
                (string scheme, string host, string urlPath) = ExtractUrlData(urlData);
                var parameters = ExtractQueryStringSegments(urlPath);
                var urlSegments = ExtractUrlSegments(urlPath);
                var url = new Url(scheme, host, urlSegments, parameters);

                if (this.IsValidUrl(url, row))
                {
                    result.Add(url);
                }
            }

            return result;
        }

        private static (string sсheme, string host, string urlPath) ExtractUrlData(string url)
        {
            var urlSegments = url.Trim().Split('/', 3, StringSplitOptions.RemoveEmptyEntries);

            const int SchemeSegmentIndex = 0;
            const int HostSegmentIndex = 1;
            const int UrlPathIndex = 2;

            var schemaRegEx = new Regex(@"^(http|https|ftp){1}:?");
            var hostRegEx = new Regex(@"[a-zA-Z0-9@:%._\+~#=]{1,256}\.[a-zA-Z0-9()]{1,6}");

            return urlSegments.Length switch
            {
                2 => (schemaRegEx.IsMatch(urlSegments[SchemeSegmentIndex]) ? urlSegments[SchemeSegmentIndex] : null, hostRegEx.IsMatch(urlSegments[HostSegmentIndex]) ? urlSegments[HostSegmentIndex] : null, null),
                3 => (schemaRegEx.IsMatch(urlSegments[SchemeSegmentIndex]) ? urlSegments[SchemeSegmentIndex] : null, hostRegEx.IsMatch(urlSegments[HostSegmentIndex]) ? urlSegments[HostSegmentIndex] : null, urlSegments[UrlPathIndex]),
                _ => (null, null, null),
            };
        }

        private static string[] ExtractUrlSegments(string url) => url switch
        {
            null => null,
            _ when url.Contains('?') => url.Substring(0, url.IndexOf('?')).Split('/', StringSplitOptions.RemoveEmptyEntries),
            _ => url.Split('/', StringSplitOptions.RemoveEmptyEntries)
        };

        private static Dictionary<string, string> ExtractQueryStringSegments(string url)
        {
            if (string.IsNullOrEmpty(url) || !url.Contains('?'))
            {
                return null;
            }

            var result = new Dictionary<string, string>();

            const int segmentKeyIndex = 0;
            const int segmentValueIndex = 1;

            foreach (var segment in url[(url.IndexOf('?') + 1) ..].Split('&'))
            {
                var segmentPair = segment.Split('=');
                result[segmentPair[segmentKeyIndex]] = segmentPair[segmentValueIndex];
            }

            return result;
        }

        private bool IsValidUrl(Url url, int row)
        {
            bool isValid = true;

            if (string.IsNullOrEmpty(url.Host) || string.IsNullOrEmpty(url.Scheme))
            {
                this.logger.Error($"Row {row} invalid URL.");
                isValid = false;
            }

            return isValid;
        }
    }
}
