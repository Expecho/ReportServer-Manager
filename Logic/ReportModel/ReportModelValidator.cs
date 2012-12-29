using System.Linq;

namespace ReportingServerManager.Logic.ReportModel
{
    class ReportModelValidator
    {
        private readonly ModelParser modelParser = new ModelParser();

        public ReportModelValidator(string modelSMDL)
        {
            modelParser.LoadSMDL(modelSMDL);
        }

        public bool ValidateModelForReport(string reportRDL)
        {
            var guids = SematicQueryParser.ExtractQueryReferenceGUIDs(reportRDL);

            return guids.All(guid => modelParser.ContainsGUID(guid));
        }
    }
}
