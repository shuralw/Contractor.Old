using Contractor.Core;
using Contractor.Core.Jobs;
using Contractor.Core.Jobs.DomainAddition;
using Contractor.Core.Jobs.EntityAddition;
using System;
using System.IO;

namespace Contractor.CLI
{
    public class ImportHandler
    {
        public void PerformImport(string[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("Bitte geben sie einen Domain Name an: contractor import <file-path>");
                return;
            }

            IContractorOptions contractorOptions = this.GetOptions(args);
            this.Import(contractorOptions);
        }

        private IContractorOptions GetOptions(string[] args)
        {
            var options = ContractorOptionsLoader.Load(Directory.GetCurrentDirectory());
            return options;
        }

        private void Import(IContractorOptions contractorOptions)
        {
            ContractorCoreApi contractorCoreApi = new ContractorCoreApi();

            // Load File into Metamodell
            // For Each ( Domain in Metamodell)
            contractorCoreApi.AddDomain(new DomainOptions(contractorOptions)
            {
                Domain = "Bankwesen" // From Metamodell
            });

            // Load File into Metamodell
            // For Each ( entity in Domain)
            contractorCoreApi.AddEntity(new EntityOptions(contractorOptions)
            {
                Domain = "Bankwesen", // From Metamodell,
                EntityName = "Bank", // From Metamodell,
                EntityNamePlural = "Banken" // From Metamodell,
            });

            // Load File into Metamodell
            // For Each (Property in Enitity in Metamodell)
            contractorCoreApi.AddProperty(new PropertyOptions(contractorOptions)
            {
                Domain = "Bankwesen", // From Metamodell
                EntityName = "Bank", // From Metamodell,
                EntityNamePlural = "Banken", // From Metamodell,
                PropertyName = "Name", // From Metamodell,
                PropertyType = "string", // From Metamodell,
                PropertyTypeExtra = "256", // Optional (z.B. bei string -> 256 als maximale Länge)
            });
        }
    }
}