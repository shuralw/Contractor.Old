using System;
using System.Collections.Generic;
using System.Text;
using Contractor.Core;
using Contractor.Core.Jobs;

namespace Contractor.CLI.Metamodell
{
    public class Class : StructuredDataType, IAssociable
    {

        private List<AssociationEnd> Associations = new List<AssociationEnd>();

        public Class(String name, string plural) : base(name, plural)
        {
        }

        public void AddAssociation(AssociationEnd asso)
        {
            if (!this.Associations.Contains(asso))
            {
                this.Associations.Add(asso);
            }

        }

        public List<AssociationEnd> GetAssociations()
        {
            return this.Associations;
        }

        public override String ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(base.ToString());
            foreach (AssociationEnd asso in this.Associations)
            {
                sb.Append(" ").Append(asso).Append("\n");
            }

            return sb.ToString();
        }

        public void Generate(IContractorOptions contractorOptions, ContractorCoreApi contractorCoreApi, string domainName)
        {
            base.Generate(contractorOptions, contractorCoreApi, domainName);
            foreach (AssociationEnd asso in this.Associations)
            {
                asso.Generate();
            }
        }
    }
}
