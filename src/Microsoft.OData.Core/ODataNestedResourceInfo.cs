//---------------------------------------------------------------------
// <copyright file="ODataNestedResourceInfo.cs" company="Microsoft">
//      Copyright (C) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.
// </copyright>
//---------------------------------------------------------------------

namespace Microsoft.OData.Core
{
    #region Namespaces
    using System;
    using System.Diagnostics;
    using Microsoft.OData.Core.Evaluation;
    #endregion Namespaces

    /// <summary>
    /// Represents a single link.
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("{Name}")]
    public sealed class ODataNestedResourceInfo : ODataItem
    {   
        /// <summary>the metadata builder for this nested resource info.</summary>
        private ODataResourceMetadataBuilder metadataBuilder;

        /// <summary>URI representing the Unified Resource Locator (Url) of the link as provided by the user or seen on the wire (never computed).</summary>
        private Uri url;

        /// <summary>true if the nested resource info has been set by the user or seen on the wire or computed by the metadata builder, false otherwise.</summary>
        private bool hasNestedResourceInfo;

        /// <summary>The association link URL for this navigation link as provided by the user or seen on the wire (never computed).</summary>
        private Uri associationLinkUrl;

        /// <summary>true if the association link has been set by the user or seen on the wire or computed by the metadata builder, false otherwise.</summary>
        private bool hasAssociationUrl;

        /// <summary>Gets or sets a value that indicates whether the nested resource info represents a collection or a resource.</summary>
        /// <returns>true if the nested resource info represents a collection; false if the navigation represents a resource.</returns>
        /// <remarks>This property is required to have a value for ATOM payloads and is optional for JSON payloads.</remarks>
        public bool? IsCollection
        {
            get;
            set;
        }

        /// <summary>Gets or sets the name of the link.</summary>
        /// <returns>The name of the link.</returns>
        public string Name
        {
            get;
            set;
        }

        /// <summary>Gets or sets the URI representing the Unified Resource Locator (URL) of the link.</summary>
        /// <returns>The URI representing the Unified Resource Locator (URL) of the link.</returns>
        public Uri Url
        {
            get
            {
                if (this.metadataBuilder != null)
                {
                    this.url = this.metadataBuilder.GetNavigationLinkUri(this.Name, this.url, this.hasNestedResourceInfo);
                    this.hasNestedResourceInfo = true;
                }

                return this.url;
            }

            set
            {
                this.url = value;
                this.hasNestedResourceInfo = true;
            }
        }

        /// <summary>The association link URL for this navigation link. </summary>
        public Uri AssociationLinkUrl
        {
            get
            {
                if (this.metadataBuilder != null)
                {
                    this.associationLinkUrl = this.metadataBuilder.GetAssociationLinkUri(this.Name, this.associationLinkUrl, this.hasAssociationUrl);
                    this.hasAssociationUrl = true;
                }

                return this.associationLinkUrl;
            }

            set
            {
                this.associationLinkUrl = value;
                this.hasAssociationUrl = true;
            }
        }

        /// <summary>Gets or sets the context url for this nested resource info.</summary>
        /// <returns>The URI representing the context url of the nested resource info.</returns>
        internal Uri ContextUrl { get; set; }

        /// <summary>Gets or sets metadata builder for this nested resource info.</summary>
        /// <returns>The metadata builder used to compute values from model annotations.</returns>
        internal ODataResourceMetadataBuilder MetadataBuilder
        {
            get
            {
                return this.metadataBuilder;
            }

            set
            {
                Debug.Assert(value != null, "MetadataBuilder != null");

                this.metadataBuilder = value;
            }
        }
    }
}
