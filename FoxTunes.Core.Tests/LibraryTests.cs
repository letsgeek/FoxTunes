﻿using FoxDb;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoxTunes
{
    [TestFixture(SQLITE)]
    [TestFixture(SQLSERVER)]
    public class LibraryTests : DatabaseTests
    {
        public LibraryTests(long configuration)
            : base(configuration)
        {

        }

        [Test]
        public async Task CanAddFilesToLibrary()
        {
            await this.Core.Managers.Library.Clear();
            await this.Core.Managers.Library.Add(new[]
            {
                TestInfo.AudioFileNames[0],
                TestInfo.AudioFileNames[2],
                TestInfo.AudioFileNames[3]
            });
            this.AssertLibraryItems(
                TestInfo.AudioFileNames[0],
                TestInfo.AudioFileNames[2],
                TestInfo.AudioFileNames[3]
            );
            this.AssertLibraryHierarchy(
                TestInfo.AudioFileNames[0],
                TestInfo.AudioFileNames[2],
                TestInfo.AudioFileNames[3]
            );
            await this.Core.Managers.Library.Clear();
        }

        protected virtual void AssertLibraryItems(params string[] fileNames)
        {
            using (var database = this.Core.Factories.Database.Create())
            {
                using (var transaction = database.BeginTransaction(database.PreferredIsolationLevel))
                {
                    var set = database.Set<LibraryItem>(transaction);
                    var query = (
                        from element in set
                        select element
                    ).ToArray();
                    Assert.AreEqual(fileNames.Length, query.Length);
                    for (var a = 0; a < fileNames.Length; a++)
                    {
                        Assert.AreEqual(fileNames[a], query[a].FileName);
                    }
                }
            }
        }

        protected virtual void AssertLibraryHierarchy(params string[] fileNames)
        {
            using (var database = this.Core.Factories.Database.Create())
            {
                using (var transaction = database.BeginTransaction(database.PreferredIsolationLevel))
                {
                    var set = database.Set<LibraryHierarchy>(transaction);
                    foreach (var hierarchy in set)
                    {
                        var nodes = this.Core.Components.LibraryHierarchyBrowser.GetNodes(hierarchy);
                        this.AssertLibraryHierarchy(hierarchy, nodes, fileNames);
                    }
                }
            }
        }

        private void AssertLibraryHierarchy(LibraryHierarchy hierarchy, IEnumerable<LibraryHierarchyNode> nodes, string[] fileNames)
        {
            var selector = default(Func<LibraryHierarchyNode, IEnumerable<LibraryHierarchyNode>>);
            selector = node =>
            {
                if (node.IsLeaf)
                {
                    return new[] { node };
                }
                node.LoadChildren();
                return node.Children.SelectMany(selector);
            };
            var leaves = nodes.SelectMany(selector);
            Assert.AreEqual(fileNames.Count(), leaves.Count());
        }
    }
}
