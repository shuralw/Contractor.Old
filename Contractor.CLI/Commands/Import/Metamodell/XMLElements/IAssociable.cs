using System;
using System.Collections.Generic;

namespace Contractor.CLI.Metamodell
{
    public interface IAssociable
    {

        void AddAssociation(AssociationEnd end);

        List<AssociationEnd> GetAssociations();

        String GetName();
    }
}
