using System;
using System.Text;

namespace Contractor.CLI.Metamodell
{
    public enum AssociationEndType {
        From,
        To
    }
    public class AssociationEnd : MemberElement
    {
        private MultiplicityT multiplicity = MultiplicityT.One;

        private IAssociable reference;

        private AssociationEnd inverse;

        public MultiplicityT GetMultiplicity()
        {
            return this.multiplicity;
        }

        public void SetMultiplicity(MultiplicityT multiplicity)
        {
            this.multiplicity = multiplicity;
        }

        public String GetRoleName()
        {
            return base.GetName();
        }

        public AssociationEnd GetInverse()
        {
            return this.inverse;
        }

        public void SetInverse(AssociationEnd inverse)
        {
            this.inverse = inverse;
        }
        public void SetRoleName(String name)
        {
            SetName(name);
        }

        public IAssociable GetReference()
        {
            return this.reference;
        }

        public void SetReference(IAssociable reference)
        {
            this.reference = reference;
            reference.AddAssociation(this);
        }

        public override String ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(this.GetRoleName()).Append(" ");
            sb.Append(this.multiplicity);
            sb.Append(" [").Append(this.reference.GetName()).Append("]");
            return sb.ToString();
        }

        public void Generate()
        {
            //Todo, sobald Jonas Funktionalität für 1:n Associations hergestellt hat.
        }
    }
}