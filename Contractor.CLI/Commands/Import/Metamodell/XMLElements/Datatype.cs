using System;

namespace Contractor.CLI.Metamodell
{
    public class DataType : AnnotatedElement
    {

        public static DataType String = new DataType("String");

        public static DataType Boolean = new DataType("Boolean");

        public static DataType Integer = new DataType("Integer");

        public static DataType Float = new DataType("Float");

        public DataType(String name)
        {
            SetName(name);
        }
    }
}
