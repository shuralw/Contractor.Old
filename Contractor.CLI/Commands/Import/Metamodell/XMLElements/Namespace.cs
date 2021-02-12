using System.Collections.Generic;

namespace Contractor.CLI.Metamodell
{
    public class Package : AnnotatedElement, INamespaceable
    {

        private List<INamespaceable> elements = new List<INamespaceable>();

        public List<INamespaceable> GetElements()
        {
            return this.elements;
        }

        public void SetElements(List<INamespaceable> elements)
        {
            this.elements = elements;
        }

        public void AddElement(INamespaceable element)
        {
            this.elements.Add(element);
        }
    }
}
