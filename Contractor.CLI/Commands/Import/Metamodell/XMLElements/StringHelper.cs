namespace Contractor.CLI.Metamodell
{
    public class StringHelper
    {
        public string SubstringStartingFromFoundWordEndingAtFoundWord(string fullTerm, string toBeSearchedFrom, string toBeSearchedTo)
        {
            // - Alter: Integer
            int length = fullTerm.IndexOf(toBeSearchedTo) - (fullTerm.IndexOf(toBeSearchedFrom) + toBeSearchedFrom.Length);
            return fullTerm.Substring(fullTerm.IndexOf(toBeSearchedFrom) + toBeSearchedFrom.Length, length);
        }

        public string SubstringEndingAtFoundWord(string fullTerm, string toBeSearchedTo)
        {
            int length = fullTerm.IndexOf(toBeSearchedTo);
            return fullTerm.Substring(0, length);
        }

        public string SubstringStartingFromFoundWord(string fullTerm, string toBeSearchedFrom)
        {
            int length = fullTerm.Length - fullTerm.IndexOf(toBeSearchedFrom);
            return fullTerm.Substring(fullTerm.IndexOf(toBeSearchedFrom) + 1, length - 1);
        }
    }
}