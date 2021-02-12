using System.Collections.Generic;

namespace Contractor.CLI.Metamodell
{
    public class Interface : AnnotatedElement, IBehavioral, IAssociable, INamespaceable
    {

        private List<AssociationEnd> Associations = new List<AssociationEnd>();

        private List<Operation> operations = new List<Operation>();

        public void AddOperation(Operation operation)
        {
            this.operations.Add(operation);
        }

        public List<Operation> GetOperations()
        {
            return this.operations;
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
    }
}
