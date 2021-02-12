using System.Collections.Generic;

namespace Contractor.CLI.Metamodell
{
    public class ClassContainer
    {
        public Dictionary<string, Class> Classes { get; set; } = new Dictionary<string, Class>();

        public void Add(string key, Class value)
        {
            Classes.Add(key, value);
        }
    }
}