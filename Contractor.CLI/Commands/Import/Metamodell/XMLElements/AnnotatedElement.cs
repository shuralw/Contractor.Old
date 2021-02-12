using System;
using System.Collections.Generic;
using System.Text;

namespace Contractor.CLI.Metamodell
{
    public abstract class AnnotatedElement
    {

        private String name;

        private Dictionary<String, TaggedValue> taggedValues = new Dictionary<String, TaggedValue>();

        private List<Stereotype> stereotypes = new List<Stereotype>();

        public String GetName()
        {
            return this.name;
        }

        public void SetName(String name)
        {
            this.name = name;
        }

        public TaggedValue GetTaggedValue(String name)
        {
            if (this.taggedValues.ContainsKey(this.name))
            {
                return this.taggedValues[this.name];
            }

            return null;
        }

        public void SetTaggedValue(String name, String value)
        {
            TaggedValue tg = this.GetTaggedValue(this.name);
            if ((tg == null))
            {
                tg = new TaggedValue(this.name);
                this.taggedValues[this.name] = tg;
            }

            tg.SetValue(value);
        }

        public void AddStereotype(Stereotype stereotype)
        {
            if (!this.stereotypes.Contains(stereotype))
            {
                this.stereotypes.Add(stereotype);
            }

        }

        public List<Stereotype> GetStereotypes()
        {
            return this.stereotypes;
        }

        public override String ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(this.name);
            if ((this.stereotypes.Count> 0))
            {
                sb.Append(" <<");
                foreach (Stereotype st in this.stereotypes)
                {
                    sb.Append(st);
                }

                sb.Append(">>");
            }

            return sb.ToString();
        }
    }
}
