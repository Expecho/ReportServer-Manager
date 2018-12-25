using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace ReportingServerManager.Logic.ReportModel
{
    public class SematicQueryParser
    {
        public static ICollection<string> ExtractQueryReferenceGUIDs(string rdlFile)
        {
            var doc = ExtractCommandXML(rdlFile);

            var nsMgr = GetNSNameSpaceMgr("http://schemas.microsoft.com/sqlserver/2004/10/semanticmodeling");

            var nodes = doc.SelectNodes("//ns:EntityID|//ns:AttributeID|//ns:RoleID", nsMgr);

            var guids = new Dictionary<string, string>();

            if (nodes != null)
                foreach (var guid in nodes.Cast<XmlNode>().Select(node => node.InnerXml).Where(guid => guids.ContainsKey(guid) == false))
                {
                    guids.Add(guid, "exists...");
                }

            return guids.Keys;
        }

        private static XmlNamespaceManager GetNSNameSpaceMgr(string nameSpace)
        {
            var nt = new NameTable();
            var nsMgr = new XmlNamespaceManager(nt);
            nsMgr.AddNamespace("ns", nameSpace);
            return nsMgr;
        }

        private static XmlDocument ExtractCommandXML(string rdlFile)
        {
            var nsMgr = GetNSNameSpaceMgr("http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition");

            var doc = new XmlDocument();
            doc.LoadXml(rdlFile);

            var commandText = doc.SelectSingleNode("/ns:Report/ns:DataSets/ns:DataSet/ns:Query/ns:CommandText", nsMgr);

            return ConstructXMLFromEscapedString(commandText.InnerText);
        }

        private static XmlDocument ConstructXMLFromEscapedString(string escapedXML)
        {
            var serializer = new System.Xml.Serialization.XmlSerializer(typeof(XmlDocument));

            var doc = (XmlDocument)serializer.Deserialize(new System.IO.StringReader(escapedXML));

            return doc;
        }

    }
}
