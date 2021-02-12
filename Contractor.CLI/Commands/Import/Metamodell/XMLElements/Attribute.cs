using System;
using System.Text;
using Contractor.Core;
using Contractor.Core.Jobs;

namespace Contractor.CLI.Metamodell
{
    public class Attribute : MemberElement
    {

        private DataType type;
        public string ParentId { get; set; }

        public Attribute(String name, DataType type)
        {
            SetName(name);
            this.type = type;
        }

        public Attribute(String name, DataType type, Stereotype st) : this(name, type)
        {
            AddStereotype(st);
        }

        public DataType GetType()
        {
            return this.type;
        }

        public void SetType(DataType type)
        {
            this.type = type;
        }

        public override String ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(GetName());
            if ((this.type != null))
            {
                sb.Append(" : ").Append(this.type.ToString());
            }

            return sb.ToString();
        }

        public void Generate(IContractorOptions contractorOptions, ContractorCoreApi coreApi, string domainName, string entityName, string entitiNamePlural)
        {
            coreApi.AddProperty(new PropertyAdditionOptions(contractorOptions)
            {
                Domain = domainName,
                EntityName = entityName,
                EntityNamePlural = entitiNamePlural, // From Metamodell,
                PropertyName = GetName(), // From Metamodell,
                PropertyType = GetType().ToString(), // From Metamodell,
            });
        }
    }
}
