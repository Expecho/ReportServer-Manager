using System;
using System.Collections.Generic;
using System.Text;

namespace RSS_Report_Retrievers.Classes.ReportModel
{
    class ReportModelValidator
    {
        private ModelParser modelParser = new ModelParser();

        public ReportModelValidator(string modelSMDL)
        {
            modelParser.LoadSMDL(modelSMDL);
        }

        public bool ValidateModelForReport(string reportRDL)
        {
            ICollection<string> guids= SematicQueryParser.ExtractQueryReferenceGUIDs(reportRDL);

            foreach (string guid in guids)
            {
                if(modelParser.ContainsGUID(guid) == false)
                    return false;
            }

            return true;
        }
    }
}
