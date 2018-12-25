using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace ReportingServerManager.Logic.ReportModel
{
    class ModelParser
    {
        readonly XmlNamespaceManager myNamespaceMgr = new XmlNamespaceManager(new NameTable());
        readonly Dictionary<string, string> guids = new Dictionary<string, string>();
        readonly XmlDocument doc = new XmlDocument();

        public bool LoadSMDL(string smdl)
        {
            myNamespaceMgr.AddNamespace("ds", "http://schemas.microsoft.com/sqlserver/2004/10/semanticmodeling");

            doc.LoadXml(smdl);

            AddEntityIDs();
            AddEntityAttributeIDs();
            AddEntityRoleIDs();
            AddIdentifyingAttributes();

            return true;
        }

        public bool ContainsGUID(string guid)
        {
            return guids.ContainsKey(guid);
        }

        private void AddEntityIDs()
        {
            AddIDValue("//ds:SemanticModel/ds:Entities/ds:Entity");
        }
        private void AddEntityAttributeIDs()
        {
            AddIDValue("//ds:SemanticModel/ds:Entities/ds:Entity/ds:Fields/ds:Attribute");
        }

        private void AddEntityRoleIDs()
        {
            AddIDValue("//ds:SemanticModel/ds:Entities/ds:Entity/ds:Fields/ds:Role");
        }

        private void AddIdentifyingAttributes()
        {
            var nodeList = doc.SelectNodes("//ds:Entity/ds:IdentifyingAttributes/ds:AttributeReference/ds:AttributeID", myNamespaceMgr);

            if (nodeList != null)
                foreach (var guid in nodeList.Cast<XmlNode>().Select(node => node.InnerText).Where(guid => guids.ContainsKey(guid) == false))
                {
                    guids.Add(guid, "Exists...");
                }
        }

        private void AddIDValue(string xPath)
        {
            var nodeList = doc.SelectNodes(xPath, myNamespaceMgr);

            if (nodeList != null)
                foreach (var guid in nodeList.Cast<XmlNode>().Select(node => node.Attributes != null ? node.Attributes["ID"].Value : null).Where(guid => guids.ContainsKey(guid) == false))
                {
                    guids.Add(guid, "Exists...");
                }
        }
    }
}

