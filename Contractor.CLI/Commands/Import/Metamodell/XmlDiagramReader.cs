using System.Collections.Generic;

namespace Contractor.CLI.Metamodell
{
    public class XmlDiagramReader
    {
        UMLClassDiagramm diagram = new UMLClassDiagramm("Klassendiagramm");
        public readonly Containers Containers = new Containers();
        public readonly CellToUmlConverter CellToUmlConverter = new CellToUmlConverter();

        public UMLClassDiagramm GetDiagramFromParsedXML(string path)
        {
            XmlClassDiagramReader xmlClassDiagramReader = new XmlClassDiagramReader();
            Mxfile mxfile = xmlClassDiagramReader.Read(path);
            InsertXmlCellsIntoCorrespondingContainers(mxfile);
            return diagram;
        }

        private void InsertXmlCellsIntoCorrespondingContainers(Mxfile mxfile)
        {
            ReadAllXmlCellsIntoDiagram(mxfile);
            AddAllFoundClassesToContainer();
            AddAllFoundAssociationsToContainer();
        }

        private void AddAllFoundAssociationsToContainer()
        {
            foreach (KeyValuePair<string, Association> element in Containers.AssociationContainer.Associations)
            {
                diagram.AddAssociation(element.Value);
            }
        }

        private void AddAllFoundClassesToContainer()
        {
            foreach (KeyValuePair<string, Class> element in Containers.ClassContainer.Classes)
            {
                diagram.AddClass(element.Value);
            }
        }

        private void ReadAllXmlCellsIntoDiagram(Mxfile mxfile)
        {
            foreach (MxCell cell in mxfile.Diagram.MxGraphModel.Root.MxCells)
            {
                cell.DetermineType();
                InsertIntoCorrespondingContainer(cell);
            }
        }
        public void InsertIntoCorrespondingContainer(MxCell cell)
        {
            if (cell.CellType == CellType.Class)
            {
                AddToClassContainer(cell);
            }

            if (cell.CellType == CellType.Attribute)
            {
                CellToUmlConverter.AssignAttributeToClass(cell, Containers.ClassContainer);
            }

            if (cell.CellType == CellType.Association)
            {
                AddToAssociationContainer(cell);
            }

            if (cell.CellType == CellType.Multiplicity)
            {
                CellToUmlConverter.AssignMultiplicityToAssociation(cell, Containers.AssociationContainer);
            }
        }

        public void AddToAssociationContainer(MxCell cell)
        {
            Association diagramAssociation = CellToUmlConverter.CreateAssociation(cell, Containers.ClassContainer);
            InsertIntoCorrespondingContainer(cell, diagramAssociation);
        }

        public void AddToClassContainer(MxCell cell)
        {
            Class diagramClass = CellToUmlConverter.CreateClass(cell);
            Containers.ClassContainer.Add(cell.Id, diagramClass);
        }

        private void InsertIntoCorrespondingContainer(MxCell cell, Association diagramAssociation)
        {
            Containers.AssociationContainer.Associations.Add(cell.Id, diagramAssociation);
        }
    }
}