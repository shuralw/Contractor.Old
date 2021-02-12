using System.Collections.Generic;

namespace Contractor.CLI.Metamodell
{
    public class AssociationContainer
    {
        public Dictionary<string, Association> Associations { get; set; } = new Dictionary<string, Association>();
        public void Add(string key, Association value)
        {
            Associations.Add(key, value);
        }
    }
}