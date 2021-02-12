using System.Collections.Generic;

namespace Contractor.CLI.Metamodell
{
    public interface IBehavioral
    {

        void AddOperation(Operation operation);

        List<Operation> GetOperations();
    }
}
