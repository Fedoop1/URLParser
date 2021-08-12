using System;
using System.Collections.Generic;

namespace URLParser.BLL.Implementation.Entities
{
    /// <summary>
    /// Class that represent Uniform Resource Identifier instance.
    /// </summary>
    public class Url : IEquatable<Url>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Url"/> class.
        /// </summary>
        /// <param name="scheme">The scheme value.</param>
        /// <param name="host">The host value.</param>
        /// <param name="urlPath">The URL path collection.</param>
        /// <param name="parameters">The parameters dictionary.</param>
        public Url(string scheme, string host, IReadOnlyCollection<string> urlPath, Dictionary<string, string> parameters)
        {
            this.Scheme = scheme;
            this.Host = host;
            this.UrlPathSegments = urlPath;
            this.Parameters = parameters;
        }

        /// <summary>
        /// Gets the URL scheme.
        /// </summary>
        /// <example>https, http, ftp.</example>
        /// <value>
        /// The URL scheme.
        /// </value>
        public string Scheme { get; }

        /// <summary>
        /// Gets the URL host.
        /// </summary>
        /// <value>
        /// The URL host.
        /// <example>google.com</example>
        /// </value>
        public string Host { get; }

        /// <summary>
        /// Gets the URL path segments.
        /// </summary>
        /// <value>
        /// The URL path segments.
        /// </value>
        public IReadOnlyCollection<string> UrlPathSegments { get; }

        /// <summary>
        /// Gets the URL query parameters.
        /// </summary>
        /// <value>
        /// The URL query parameters.
        /// </value>
        public IReadOnlyDictionary<string, string> Parameters { get; }

        /// <summary>
        /// Converts object to it's string representation.
        /// </summary>
        /// <returns>
        /// A <see cref="string" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return $"Scheme: {this.Scheme}. Host: {this.Host}.";
        }

        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns>
        ///   <see langword="true" /> if the specified object  is equal to the current object; otherwise, <see langword="false" />.
        /// </returns>
        public override bool Equals(object obj) => obj switch
        {
            Url url => this.Equals(url),
            _ => false,
        };

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        ///   <see langword="true" /> if the current object is equal to the <paramref name="other" /> parameter; otherwise, <see langword="false" />.
        /// </returns>
        public bool Equals(Url other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return this.Scheme == other.Scheme && this.Host == other.Host;
        }

        /// <summary>
        /// Returns a hash code for the <see cref="Url"/> instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            return HashCode.Combine(this.Scheme, this.Host, this.UrlPathSegments, this.Parameters);
        }
    }
}
