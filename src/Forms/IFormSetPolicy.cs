using System.Collections.Generic;
using System.Windows.Forms;

namespace ReportingServerManager.Forms
{
    public interface IFormSetPolicy
    {
        void Init(IEnumerable<string> availableRoles, string itemName);
        string UserName { get; }
        DialogResult ShowDialog();
        List<string> SelectedRoles { get;}
    }
}