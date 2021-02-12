using System;
using System.Text;
using Contractor.Core;
using Contractor.Core.Jobs;

namespace Contractor.CLI.Metamodell
{
    public class Association : AnnotatedElement, INamespaceable
    {
        private AssociationEnd from = new AssociationEnd();
        private AssociationEnd to = new AssociationEnd();

        public Association()
        {
            this.from.SetInverse(this.to);
            this.to.SetInverse(this.from);
        }
        public Association(AssociationEnd diagramFromAssociationEnd, AssociationEnd diagramToAssociationEnd)
        {
            this.from.SetRoleName(diagramFromAssociationEnd.GetRoleName());
            this.from.SetReference(diagramFromAssociationEnd.GetReference());
            diagramFromAssociationEnd.GetReference().AddAssociation(this.from);

            this.to.SetRoleName(diagramToAssociationEnd.GetRoleName());
            this.to.SetReference(diagramToAssociationEnd.GetReference());
            diagramToAssociationEnd.GetReference().AddAssociation(this.to);
        }

        public Association(MultiplicityT fromMultiplicity, String fromRolename, Class fromClass, MultiplicityT toMultiplicity, String toRolename, Class toClass) : this()
        {
            this.from.SetMultiplicity(fromMultiplicity);
            this.from.SetRoleName(fromRolename);
            this.from.SetReference(fromClass);
            fromClass.AddAssociation(this.to);
            this.to.SetMultiplicity(toMultiplicity);
            this.to.SetRoleName(toRolename);
            this.to.SetReference(toClass);
            toClass.AddAssociation(this.from);
        }

      

        public AssociationEnd GetFrom()
        {
            return this.from;
        }

        public AssociationEnd GetTo()
        {
            return this.to;
        }

        public override String ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(this.from).Append(" ---");
            if ((GetName() != null))
            {
                sb.Append(" ").Append(base.ToString()).Append(" ");
            }

            sb.Append("--- ").Append(this.to);
            return sb.ToString();
        }

        public void Generate(IContractorOptions contractorOptions, ContractorCoreApi contractorCoreApi,
            string domainName)
        {
            //Todo, sobald Jonas Funktionalität für 1:n Associations hergestellt hat.
        }
    }
}