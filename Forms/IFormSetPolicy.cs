using System;
namespace RSS_Report_Retrievers.Forms
{
    interface IFormSetPolicy
    {
        void Init(System.Collections.Generic.IEnumerable<string> availableRoles, string itemName, System.Collections.Generic.Dictionary<string, string> existingPolicies);
        bool Recursive { get; }
        string UserName { get; }
    }
}
