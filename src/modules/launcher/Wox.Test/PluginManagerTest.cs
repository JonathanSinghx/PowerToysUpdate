﻿// Copyright (c) Microsoft Corporation
// The Microsoft Corporation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PowerLauncher.Plugin;
using Wox.Plugin;

namespace Wox.Test
{
    [TestClass]
    public class PluginManagerTest
    {
        [DataTestMethod]
        [DataRow(">", "dummyQueryText", "dummyTitle", "> dummyQueryText")]
        [DataRow(">", null, "dummyTitle", "> dummyTitle")]
        [DataRow(">", "", "dummyTitle", "> dummyTitle")]
        public void QueryForPluginSetsActionKeywordWhenQueryTextDisplayIsEmpty(string actionKeyword, string queryTextDisplay, string title, string expectedResult)
        {
            // Arrange
            var query = new Query
            {
                ActionKeyword = actionKeyword,
            };
            var metadata = new PluginMetadata
            {
                ID = "dummyName",
                IcoPathDark = "dummyIcoPath",
                IcoPathLight = "dummyIcoPath",
                ExecuteFileName = "dummyExecuteFileName",
                PluginDirectory = "dummyPluginDirectory",
            };
            var result = new Result()
            {
                QueryTextDisplay = queryTextDisplay,
                Title = title,
            };
            var results = new List<Result>() { result };
            var pluginMock = new Mock<IPlugin>();
            pluginMock.Setup(r => r.Query(query)).Returns(results);
            var pluginPair = new PluginPair(metadata)
            {
                Plugin = pluginMock.Object,
                IsPluginInitialized = true,
            };

            // Act
            var queryOutput = PluginManager.QueryForPlugin(pluginPair, query);

            // Assert
            Assert.AreEqual(expectedResult, queryOutput[0].QueryTextDisplay);
        }

        [DataTestMethod]
        [DataRow("", true)]
        [DataRow(null, true)]
        [DataRow(">", false)]
        public void QueryDefaultResultsForPlugin(string actionKeyword, bool emptyResults)
        {
            // Arrange
            var query = new Query(string.Empty, actionKeyword);
            var metadata = new PluginMetadata
            {
                ID = "dummyName",
                IcoPathDark = "dummyIcoPath",
                IcoPathLight = "dummyIcoPath",
                ExecuteFileName = "dummyExecuteFileName",
                PluginDirectory = "dummyPluginDirectory",
                ActionKeyword = ">",
                IsGlobal = true,
            };
            var result = new Result()
            {
                QueryTextDisplay = "dummyQueryText",
                Title = "dummyTitle",
            };
            var results = new List<Result>() { result };
            var pluginMock = new Mock<IPlugin>();
            pluginMock.Setup(r => r.Query(query)).Returns(results);
            var pluginPair = new PluginPair(metadata)
            {
                Plugin = pluginMock.Object,
                IsPluginInitialized = true,
            };

            // Act
            var queryOutput = PluginManager.QueryForPlugin(pluginPair, query);

            // Assert
            Assert.AreEqual(queryOutput.Count == 0, emptyResults);
        }
    }
}
