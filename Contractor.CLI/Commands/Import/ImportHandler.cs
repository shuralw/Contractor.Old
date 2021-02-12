using Contractor.Core;
using Contractor.Core.Jobs;
using System;
using System.IO;
using Contractor.CLI.Metamodell;

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
            App app = new App();
            app.GenerateCodeFromDiagram(@"C:\Entwicklung\Contractor\Contractor.CLI\ExtendedExampleDiagram.xml");
        }

        private void ImportJonas(IContractorOptions contractorOptions)
        {
            ContractorCoreApi contractorCoreApi = new ContractorCoreApi();

            // Load File into Metamodell
            // For Each ( Domain in Metamodell)
            contractorCoreApi.AddDomain(new DomainAdditionOptions(contractorOptions)
            {
                Domain = "Domain" // From Metamodell
            });

            // Load File into Metamodell
            // For Each ( entity in Domain)
            contractorCoreApi.AddEntity(new EntityAdditionOptions(contractorOptions)
            {
                Domain = "Domain", // From Metamodell,
                EntityName = "Kind", // From Metamodell,
                EntityNamePlural = "Kinder" // From Metamodell,
            });

            // Load File into Metamodell
            // For Each (Property in Enitity in Metamodell)
            contractorCoreApi.AddProperty(new PropertyAdditionOptions(contractorOptions)
            {
                Domain = "Domain", // From Metamodell
                EntityName = "Kind", // From Metamodell,
                EntityNamePlural = "Kinder", // From Metamodell,
                PropertyName = "Name", // From Metamodell,
                PropertyType = "string", // From Metamodell,
                PropertyTypeExtra = "256", // Optional (z.B. bei string -> 256 als maximale Länge)
            });

        }
    }
}