using System;
using System.Collections.Generic;
using System.Text;
using Contractor.Core;
using Contractor.Core.Jobs;

namespace Contractor.CLI.Metamodell
{
    public class UMLClassDiagramm : Model
    {

        private List<INamespaceable> elements = new List<INamespaceable>();

        private List<Class> classes = new List<Class>();

        private List<Association> Associations = new List<Association>();

        public UMLClassDiagramm(String name) : base(name)
        {
        }

        public void Add(INamespaceable element)
        {

        }

        public void AddClass(Class @class)
        {
            this.classes.Add(@class);
        }

        public List<Class> GetClasses()
        {
            return this.classes;
        }

        public void AddAssociation(Association asso)
        {
            this.Associations.Add(asso);
        }

        public List<Association> GetAssociations()
        {
            return this.Associations;
        }

        public String ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Classes:").Append("\n");
            foreach (Class cls in this.classes)
            {
                sb.Append(cls).Append("\n");
            }

            sb.Append("Associations:").Append("\n");
            foreach (Association asso in this.Associations)
            {
                sb.Append(asso).Append("\n");
            }

            return sb.ToString();
        }

        public void GenerateRecursive(IContractorOptions contractorOptions, ContractorCoreApi contractorCoreApi, string domainName)
        {
            foreach (Class cls in this.classes)
            {
                cls.Generate(contractorOptions, contractorCoreApi, domainName);
            }

            foreach (Association asso in this.Associations)
            {
                asso.Generate(contractorOptions, contractorCoreApi, domainName);
            }
        }
    }
}
