using System;
using System.IO;
using Contractor.Core;
using Contractor.Core.Jobs;

namespace Contractor.CLI.Metamodell
{
    public class App
    {

        private readonly XmlDiagramReader xmlDiagramReader = new XmlDiagramReader();

        public void GenerateCodeFromDiagram(string path)
        {
            UMLClassDiagramm umlClassDiagramm = xmlDiagramReader.GetDiagramFromParsedXML(path);
            GenerateCode(umlClassDiagramm);
        }

        private void GenerateCode(UMLClassDiagramm umlClassDiagramm)
        {
            IContractorOptions contractorOptions = this.GetOptions(new string[] { "import" });
            ContractorCoreApi contractorCoreApi = new ContractorCoreApi();

            AddDomain(contractorCoreApi, contractorOptions);
            Console.WriteLine(umlClassDiagramm.ToString());
            
            umlClassDiagramm.GenerateRecursive(contractorOptions, contractorCoreApi, "Domain");
        }

        private static void AddDomain(ContractorCoreApi contractorCoreApi, IContractorOptions contractorOptions)
        {
            contractorCoreApi.AddDomain(new DomainAdditionOptions(contractorOptions)
            {
                Domain = "Domain"
            });
        }

        private IContractorOptions GetOptions(string[] args)
        {
            var options = ContractorOptionsLoader.Load(Directory.GetCurrentDirectory());
            return options;
        }



    }
}
