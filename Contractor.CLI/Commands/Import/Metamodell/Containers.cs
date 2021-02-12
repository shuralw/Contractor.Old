namespace Contractor.CLI.Metamodell
{
    public class Containers
    {
        private readonly AssociationContainer associationContainer = new AssociationContainer();
        private readonly ClassContainer classContainer = new ClassContainer();
        public AssociationContainer AssociationContainer { get { return associationContainer; } }
        public ClassContainer ClassContainer { get { return classContainer; } }
    }
}