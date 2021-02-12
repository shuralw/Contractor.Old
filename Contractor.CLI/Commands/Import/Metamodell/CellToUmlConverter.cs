using System;

namespace Contractor.CLI.Metamodell
{
    public class CellToUmlConverter
    {
        private readonly StringHelper stringHelper = new StringHelper();

        public static Class CreateClass(MxCell cell)
        {
            string name = cell.Value.Split(':')[0];
            string plural = cell.Value.Split(':')[1];
            Class diagramClass = new Class(name, plural);
            return diagramClass;
        }

        public Association CreateAssociation(MxCell cell, ClassContainer classContainer)
        {
            string rolenameFrom = stringHelper.SubstringStartingFromFoundWord(cell.Value, ":");
            string rolenameTo = stringHelper.SubstringEndingAtFoundWord(cell.Value, ":");

            AssociationEnd diagramFromAssociationEnd = new AssociationEnd();
            diagramFromAssociationEnd.SetReference(classContainer.Classes[cell.SourceId]);
            diagramFromAssociationEnd.SetRoleName(rolenameFrom);

            AssociationEnd diagramToAssociationEnd = new AssociationEnd();
            diagramToAssociationEnd.SetReference(classContainer.Classes[cell.TargetId]);
            diagramToAssociationEnd.SetRoleName(rolenameTo);

            Association diagramAssociation = new Association(diagramFromAssociationEnd, diagramToAssociationEnd);
            return diagramAssociation;
        }

        public void AssignAttributeToClass(MxCell cell, ClassContainer classContainer)
        {
            string visibility = cell.Value.Split(' ')[0];
            string name = stringHelper.SubstringStartingFromFoundWordEndingAtFoundWord(cell.Value, " ", ":");
            string type = cell.Value.Split(' ')[2];
            Class correspondingClass = classContainer.Classes[cell.Parent];

            Attribute diagramAttribute = new Attribute(name, new DataType(type));
            correspondingClass.AddAttribute(diagramAttribute);
        }

        public void AssignMultiplicityToAssociation(MxCell cell, AssociationContainer associationContainer)
        {
            bool isFromEnd = cell.MxGeometry.x == "1";
            bool isToEnd = cell.MxGeometry.x == "-1";
            MultiplicityT multiplicity = GetMultiplicityType(cell.Value);
            Association correspondingAssociation = associationContainer.Associations[cell.Parent];

            if (isFromEnd)
            {
                correspondingAssociation.GetFrom().SetMultiplicity(multiplicity);
            }
            else if (isToEnd)
            {
                correspondingAssociation.GetTo().SetMultiplicity(multiplicity);
            }
        }
        private MultiplicityT GetMultiplicityType(string multiplicity)
        {
            switch (multiplicity)
            {
                case "0..n": return MultiplicityT.ZeroOrMany;
                case "1..n": return MultiplicityT.OneToMany;
                case "0..1": return MultiplicityT.ZeroOrOne;
                case "1": return MultiplicityT.One;
                default: throw new Exception("Ungültige Multiplizität.");
            }
        }

    }
}