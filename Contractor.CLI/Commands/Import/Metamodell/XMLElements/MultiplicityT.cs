using System;
using System.Text;

namespace Contractor.CLI.Metamodell
{
    public class MultiplicityT
    {

        public static int MaxUpperBound = Int32.MaxValue;

        public static MultiplicityT One = new MultiplicityT(1, 1);

        public static MultiplicityT OneToMany = new MultiplicityT(1, MultiplicityT.MaxUpperBound);

        public static MultiplicityT Many = new MultiplicityT(0, MultiplicityT.MaxUpperBound);

        public static MultiplicityT ZeroOrMany = new MultiplicityT(0, MultiplicityT.MaxUpperBound);

        public static MultiplicityT ZeroOrOne = new MultiplicityT(0, 1);

        public int LowerBound = 0;

        public int UpperBound = 1;

        public MultiplicityT()
        {

        }

        public MultiplicityT(int lower, int upper)
        {
            this.LowerBound = lower;
            this.UpperBound = upper;
        }

        public override String ToString()
        {
            if (((this.UpperBound == 1)
                 && (this.LowerBound == 1)))
            {
                return "1";
            }

            StringBuilder sb = new StringBuilder();
            sb.Append(this.LowerBound).Append("..");
            if ((this.UpperBound == MultiplicityT.MaxUpperBound))
            {
                sb.Append("*");
            }
            else
            {
                sb.Append(this.UpperBound);
            }

            return sb.ToString();
        }
    }
}
