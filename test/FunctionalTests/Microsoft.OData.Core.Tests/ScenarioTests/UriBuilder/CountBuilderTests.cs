﻿//---------------------------------------------------------------------
// <copyright file="CountBuilderTests.cs" company="Microsoft">
//      Copyright (C) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.
// </copyright>
//---------------------------------------------------------------------

using System;
using Xunit;

namespace Microsoft.OData.Tests.ScenarioTests.UriBuilder
{
    public class CountBuilderTests : UriBuilderTestBase
    {
        [Fact]
        public void CountWorks()
        {
            Uri queryUri = new Uri("People/$count", UriKind.Relative);
            Uri actualUri = UriBuilder(queryUri, ODataUrlKeyDelimiter.Parentheses, settings);
            Assert.Equal(new Uri("http://gobbledygook/People/$count"), actualUri);
        }

        [Fact]
        public void CountWithFilterWorks()
        {
            Uri queryUri = new Uri("People/$count?$filter=ID eq 123", UriKind.Relative);
            Uri actualUri = UriBuilder(queryUri, ODataUrlKeyDelimiter.Parentheses, settings);
            Assert.Equal(new Uri("http://gobbledygook/People/$count?$filter=ID%20eq%20123"), actualUri);
        }

        [Fact]
        public void CountWithChildCollectionWorks()
        {
            Uri queryUri = new Uri("People?$filter=MyPaintings/$count gt 1", UriKind.Relative);
            Uri actualUri = UriBuilder(queryUri, ODataUrlKeyDelimiter.Parentheses, settings);
            Assert.Equal(new Uri("http://gobbledygook/People?$filter=MyPaintings%2F%24count%20gt%201"), actualUri);
        }

        [Fact]
        public void ExpandCountOnChildCollectionWorks()
        {
	        Uri queryUri = new Uri("People?$expand=MyPaintings/$count", UriKind.Relative);
	        Uri actualUri = UriBuilder(queryUri, ODataUrlKeyDelimiter.Parentheses, settings);
	        Assert.Equal(new Uri("http://gobbledygook/People?$expand=MyPaintings%2F%24count"), actualUri);
        }

        [Fact]
        public void SelectCountOnChildCollectionWorks()
        {
	        Uri queryUri = new Uri("People?$select=MyPaintings/$count", UriKind.Relative);
	        Uri actualUri = UriBuilder(queryUri, ODataUrlKeyDelimiter.Parentheses, settings);
	        Assert.Equal(new Uri("http://gobbledygook/People?$select=MyPaintings%2F%24count"), actualUri);
        }

        // This is not supported, yet
        //[Fact]
        //public void CountWithFilterUsingNamedVariableWorks()
        //{
        //    Uri queryUri = new Uri("People/$filter=@foo/$count?@foo=ID eq 123", UriKind.Relative);
        //    Uri actualUri = UriBuilder(queryUri, ODataUrlKeyDelimiter.Parentheses, settings);
        //    Assert.Equal(new Uri("http://gobbledygook/People/$filter=@foo/$count?@foo=ID%20eq%20123"), actualUri);
        //}

        [Fact]
        public void CountWithChildCollectionFilterWorks()
        {
            Uri queryUri = new Uri("People?$filter=MyPaintings/$count($filter=OpenProperty eq 1) gt 1", UriKind.Relative);
            Uri actualUri = UriBuilder(queryUri, ODataUrlKeyDelimiter.Parentheses, settings);
            Assert.Equal(new Uri("http://gobbledygook/People?$filter=MyPaintings%2F%24count%28%24filter%3D%24it%2FOpenProperty eq 1%29 gt 1"), actualUri);
        }

        [Fact]
        public void CountTrueWorks()
        {
            Uri queryUri = new Uri("People?$count=true", UriKind.Relative);
            Uri actualUri = UriBuilder(queryUri, ODataUrlKeyDelimiter.Parentheses, settings);
            Assert.Equal(new Uri("http://gobbledygook/People?$count=true"), actualUri);
        }

        [Fact]
        public void CountFalseWorks()
        {
            Uri queryUri = new Uri("People?$count=false", UriKind.Relative);
            Uri actualUri = UriBuilder(queryUri, ODataUrlKeyDelimiter.Parentheses, settings);
            Assert.Equal(new Uri("http://gobbledygook/People?$count=false"), actualUri);
        }

        [Fact]
        public void LeadingAndTrailingWhitespaceIsTrimmed()
        {
            Uri queryUri = new Uri("People?$count=   true  ", UriKind.Relative);
            Uri actualUri = UriBuilder(queryUri, ODataUrlKeyDelimiter.Parentheses, settings);
            Assert.Equal(new Uri("http://gobbledygook/People?$count=true"), actualUri);
        }
    }
}
