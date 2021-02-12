using System;

namespace Contractor.CLI.Metamodell
{
    public class Stereotype
    {

        private String name;

        public Stereotype(String name)
        {
            this.name = name;
        }

        public String GetName()
        {
            return this.name;
        }

        public void SetName(String name)
        {
            this.name = name;
        }

        public String ToString()
        {
            return this.name;
        }
    }
}
