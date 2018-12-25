using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReportingServerManager.Logic;
using ReportingServerManager.Logic.Configuration;
using ReportingServerManager.Logic.Shared;

namespace ReportingServerManager.UnitTests
{
    [TestClass]
    public class FacadeTests
    {
        private IRSFacade facade;

        private readonly string testFolderName = String.Format("UnitTest at {0} ({1}", DateTime.Now.ToString("HH mm"),
                                                               Guid.NewGuid());
        private ReportItemDTO testFolder;

        [TestInitialize]
        public void Initialize()
        {
            var config2005 = new ServerSettingsConfigElement
            {
                SQLServerVersion = "2005",
                Url = "http://dhgwk281:81/ReportServer_sql2012/ReportService2005.asmx",
                IsSharePointMode = false,
                UseWindowsAuth = true,
            };

            var config2012 = new ServerSettingsConfigElement
            {
                SQLServerVersion = "2012",
                Url = "http://dhgwk281:81/ReportServer_sql2012/ReportService2010.asmx",
                IsSharePointMode = false,
                UseWindowsAuth = true,
            };

            facade = ReportingServicesFactory.CreateFacadeFromSettings(config2012);

            testFolder = CreateFolder(testFolderName, facade.BaseUrl);
        }

        [TestCleanup]
        public void Cleanup()
        {
            DeleteItem(testFolder.Path, ReportItemTypes.Folder, testFolderName);
        }

        [TestMethod]
        public void ShouldCreateDatasourceAsIntegrated()
        {
            var datasource = new Datasource
                                 {
                                     ConnectionString = @"Initial Catalog=ReportServer$SQL2012;Data Source=DHGWK281\SQL2012",
                                     CredentialRetrievalType = CredentialRetrievalTypes.Integrated,
                                     Enabled = true,
                                     Name = "Test Datasource",
                                     Extension = String.Empty
                                 };

            CreateDatasource(datasource);
        }

        [TestMethod]
        public void ShouldCreateDatasourceAsStored()
        {
            var datasource = new Datasource
            {
                ConnectionString = @"Initial Catalog=ReportServer$SQL2012;Data Source=DHGWK281\SQL2012",
                CredentialRetrievalType = CredentialRetrievalTypes.Store,
                Enabled = true,
                Name = "Test Datasource",
                Username = "test",
                Password = "test",
                Extension = String.Empty
            };

            CreateDatasource(datasource);
        }

        [TestMethod]
        public void ShouldCreateDatasourceAsPropmt()
        {
            var datasource = new Datasource
            {
                ConnectionString = @"Initial Catalog=ReportServer$SQL2012;Data Source=DHGWK281\SQL2012",
                CredentialRetrievalType = CredentialRetrievalTypes.Prompt,
                Enabled = true,
                Name = "Test Datasource",
                Prompt = "Enter your credentials",
                Extension = String.Empty
            };

            CreateDatasource(datasource);
        }

        [TestMethod]
        public void ShouldCreateDatasourceAsNone()
        {
            var datasource = new Datasource
            {
                ConnectionString = @"Initial Catalog=ReportServer$SQL2012;Data Source=DHGWK281\SQL2012",
                CredentialRetrievalType = CredentialRetrievalTypes.None,
                Enabled = true,
                Name = "Test Datasource",
                Extension = String.Empty
            };

            CreateDatasource(datasource);
        }

        [TestMethod]
        public void ShouldGetProperties()
        {
            var datasource = new Datasource
                                 {
                                     ConnectionString = @"Initial Catalog=ReportServer$SQL2012;Data Source=DHGWK281\SQL2012",
                                     CredentialRetrievalType = CredentialRetrievalTypes.Integrated,
                                     Enabled = true,
                                     Name = "Test Datasource",
                                     Extension = String.Empty
                                 };

            var item =  CreateDatasource(datasource);

            var properties = facade.GetItemProperties(item.Path);

            Assert.IsNotNull(properties);
            Assert.IsTrue(properties.Any(p => p[0] == "Name" && p[1] == "Test Datasource"));
        }

        [TestMethod, DeploymentItem("Assets//Catalog.rdl")]
        public void ShouldCreateThenDownloadReport()
        {
            string reportPath;

            var definition = CreateReport(out reportPath);

            var report = facade.GetReportDefinition(reportPath);

            Assert.AreEqual(definition.Length, report.Length);
            Assert.IsTrue(definition.SequenceEqual(report));
        }

        [TestMethod]
        public void ShouldSetSecurity()
        {
            var datasource = new Datasource
            {
                ConnectionString = @"Initial Catalog=ReportServer$SQL2012;Data Source=DHGWK281\SQL2012",
                CredentialRetrievalType = CredentialRetrievalTypes.Integrated,
                Enabled = true,
                Name = "Test Datasource",
                Extension = String.Empty
            };

            var item = CreateDatasource(datasource);

            var security = new Dictionary<string, string[]>
                               {
                                   { @"DHGWK281\Administrator", new[] {"Content Manager"} }
                               };

            facade.SetItemSecurity(item.Path, security);

            bool inherited;
            var actualSecurity = facade.GetItemSecurity(item.Path, out inherited);

            Assert.IsTrue(actualSecurity.Count == 1);
            Assert.IsTrue(actualSecurity.First().Key == @"DHGWK281\Administrator");
            Assert.IsTrue(actualSecurity.First().Value.Count() == 1);
            Assert.IsTrue(actualSecurity.First().Value[0] == "Content Manager");
            Assert.IsFalse(inherited);

            facade.SetItemSecurity(item.Path, null);
        }

        public byte[] CreateReport(out string reportPath)
        {
            var definition = Controller.GetBytesFromFile(@"Catalog.rdl");

            facade.CreateReport("Catalog", testFolder.Path, true, definition, null);

            var item = facade.ListChildren(testFolder.Path, false)
                .FirstOrDefault(i => i.Name == "Catalog" && i.Type == ReportItemTypes.Report);

            Assert.IsNotNull(item);

            reportPath = item.Path;

            return definition;
        }

        private ReportItemDTO CreateDatasource(Datasource datasource)
        {
            facade.CreateDataSource(datasource, testFolder.Path);

            var item = facade.ListChildren(testFolder.Path, false)
                .FirstOrDefault(i => i.Name == datasource.Name && i.Type == ReportItemTypes.Datasource);

            Assert.IsNotNull(item);

            var actualDatasource = facade.GetDatasource(item.Path);

            Assert.IsTrue(actualDatasource.CredentialRetrievalType == datasource.CredentialRetrievalType);

            return item;
        }

        private ReportItemDTO CreateFolder(string name, string parent)
        {
            facade.CreateFolder(name, parent, null);
            var folder = facade.ListChildren(facade.BaseUrl, false)
                .FirstOrDefault(i => i.Name == name && i.Type == ReportItemTypes.Folder);

            Assert.IsNotNull(folder);

            return folder;
        }

        private void DeleteItem(string path, ReportItemTypes checkType, string checkName)
        {
            facade.DeleteItem(path);

            var folder = facade.ListChildren(facade.BaseUrl, false)
                .FirstOrDefault(i => i.Name == checkName && i.Type == checkType);

            Assert.IsNull(folder);
        }
    }
}
