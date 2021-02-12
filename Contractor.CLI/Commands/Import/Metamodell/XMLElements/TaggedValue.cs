using System;

namespace Contractor.CLI.Metamodell
{
    public class TaggedValue
    {

        private String name;

        private String value;

        public TaggedValue(String name)
        {
            this.name = value;
        }

        public String GetName()
        {
            return this.name;
        }

        public void SetName(String name)
        {
            this.name = name;
        }

        public String GetValue()
        {
            return this.value;
        }

        public void SetValue(String value)
        {
            this.value = value;
        }
    }

}
