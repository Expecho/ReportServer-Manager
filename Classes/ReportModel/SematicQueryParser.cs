using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Xml;

namespace RSS_Report_Retrievers.Classes.ReportModel
{
    public class SematicQueryParser
    {
        public static ICollection<string> ExtractQueryReferenceGUIDs(string rdlFile)
        {
            XmlDocument doc = ExtractCommandXML(rdlFile);

            XmlNamespaceManager nsMgr = GetNSNameSpaceMgr("http://schemas.microsoft.com/sqlserver/2004/10/semanticmodeling");

            XmlNodeList nodes = doc.SelectNodes("//ns:EntityID|//ns:AttributeID|//ns:RoleID", nsMgr);

            Dictionary<string,string> guids = new Dictionary<string,string>();

            foreach (XmlNode node in nodes)
            {
                string guid = node.InnerXml;

                if (guids.ContainsKey(guid) == false)
                    guids.Add(guid, "exists...");
            }

            return guids.Keys;
        }

        private static XmlNamespaceManager GetNSNameSpaceMgr(string nameSpace)
        {
            NameTable nt = new NameTable();
            XmlNamespaceManager nsMgr = new XmlNamespaceManager(nt);
            nsMgr.AddNamespace("ns", nameSpace);
            return nsMgr;
        }

        private static XmlDocument ExtractCommandXML(string rdlFile)
        {
            XmlNamespaceManager nsMgr = GetNSNameSpaceMgr("http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition");

            XmlDocument doc = new XmlDocument();
            doc.LoadXml(rdlFile);

            XmlNode commandText = doc.SelectSingleNode("/ns:Report/ns:DataSets/ns:DataSet/ns:Query/ns:CommandText",nsMgr);

            return ConstructXMLFromEscapedString(commandText.InnerText);
        }

        private static XmlDocument ConstructXMLFromEscapedString(string escapedXML)
        {
            System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(typeof(XmlDocument));

            XmlDocument doc = (XmlDocument) serializer.Deserialize(new System.IO.StringReader(escapedXML));

            return doc;
        }

    }
}
