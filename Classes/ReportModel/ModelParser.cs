using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Xml;

namespace RSS_Report_Retrievers.Classes.ReportModel
{
    class ModelParser
    {
        XmlNamespaceManager myNamespaceMgr = new XmlNamespaceManager(new NameTable());
        Dictionary<string, string> guids = new Dictionary<string, string>();

        XmlDocument doc = new XmlDocument();

        public bool LoadSMDL(string smdl)
        {
            myNamespaceMgr.AddNamespace("ds", "http://schemas.microsoft.com/sqlserver/2004/10/semanticmodeling");

            doc.LoadXml(smdl);

            Dictionary<string, string> guids = new Dictionary<string, string>();

            AddEntityIDs();
            AddEntityAttributeIDs();
            AddEntityRoleIDs();
            AddIdentifyingAttributes();

            return true;
        }

        public bool ContainsGUID(string guid)
        {
            return this.guids.ContainsKey(guid);
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
            XmlNodeList nodeList = doc.SelectNodes("//ds:Entity/ds:IdentifyingAttributes/ds:AttributeReference/ds:AttributeID", myNamespaceMgr);

            foreach (XmlNode node in nodeList)
            {
                string guid = node.InnerText;

                if (guids.ContainsKey(guid) == false)
                    guids.Add(guid, "Exists...");
            }
        }

        private void AddIDValue(string xPath)
        {
            XmlNodeList nodeList = doc.SelectNodes(xPath, myNamespaceMgr);

            foreach (XmlNode node in nodeList)
            {
                string guid = node.Attributes["ID"].Value;

                if (guids.ContainsKey(guid) == false)
                    guids.Add(guid, "Exists...");
            }
        }

        
    }
}
/*<Entity ID="G7ebf822b-1dbf-471f-a147-094509208970">
<Name>Grunddata - Arbetsställen</Name>
<CollectionName>Grunddata - Arbetsställen</CollectionName>
<Description>tEmployer. OBS! Inget behörighetsskydd!</Description>
<IdentifyingAttributes>
<AttributeReference>
<AttributeID>*/
