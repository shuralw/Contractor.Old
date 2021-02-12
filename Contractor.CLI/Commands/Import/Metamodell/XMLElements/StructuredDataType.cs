using System;
using System.Collections.Generic;
using System.Text;
using Contractor.Core;
using Contractor.Core.Jobs;

namespace Contractor.CLI.Metamodell
{
    public class StructuredDataType : DataType, IBehavioral, INamespaceable
    {

        private VisibilityEnum visibility = VisibilityEnum.Private;

        private List<StructuredDataType> superClasses = new List<StructuredDataType>();

        private List<StructuredDataType> subClasses = new List<StructuredDataType>();

        private List<Attribute> attributes = new List<Attribute>();

        private List<Operation> operations = new List<Operation>();

        public string Plural { get; set; }

        public void AddOperation(Operation operation)
        {
            this.operations.Add(operation);
        }

        public List<Operation> GetOperations()
        {
            return this.operations;
        }

        public StructuredDataType(String name, string plural) : base(name)
        {
            this.Plural = plural;
        }

        public VisibilityEnum GetVisibility()
        {
            return this.visibility;
        }

        public void SetVisibility(VisibilityEnum visibility)
        {
            this.visibility = visibility;
        }

        public void AddSuperClasses(StructuredDataType superClass)
        {
            if (!this.superClasses.Contains(superClass))
            {
                this.superClasses.Add(superClass);
                superClass.subClasses.Add(this);
            }

        }

        public List<StructuredDataType> GetSuperClasses()
        {
            return this.superClasses;
        }

        public List<StructuredDataType> GetSubClasses()
        {
            return this.subClasses;
        }

        public void AddAttribute(Attribute attribute)
        {
            if (!this.attributes.Contains(attribute))
            {
                this.attributes.Add(attribute);
            }

        }

        public List<Attribute> GetAttributes()
        {
            return this.attributes;
        }

        public override String ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(base.ToString()).Append("\n");
            foreach (Attribute attr in this.attributes)
            {
                sb.Append(" ").Append(attr).Append("\n");
            }

            return sb.ToString();
        }

        protected void Generate(IContractorOptions contractorOptions, ContractorCoreApi contractorCoreApi, string domainName)
        {
            contractorCoreApi.AddEntity(new EntityAdditionOptions(contractorOptions)
            {
                Domain = domainName, 
                EntityName = GetName(), 
                EntityNamePlural = Plural
            });

            foreach (Attribute attr in this.attributes)
            {
                attr.Generate(contractorOptions, contractorCoreApi, domainName, GetName(), Plural);
            }
        }
    }
}
