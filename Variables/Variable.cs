namespace ProcessorSim.Variables
{
    public class Variable
    {
        public LineType LineType { get; set; }
        public object? Value { get; set; }
        public string VariableName { get; set; } // i am not making a linker.

        public int MemoryLocation = -1;

        public Variable(LineType lineType, object value, string variableName)
        {
            LineType = lineType;
            VariableName = variableName;
            Value = lineType switch
            {
                LineType.Integer => Convert.ToInt32(value),
                LineType.Char => Convert.ToChar(value),
                LineType.String => Convert.ToString(value),
                LineType.List => CreateList(value),
                _ => throw new ArgumentException("Unsupported LineType")
            };
        }

        private List<string> CreateList(object value)
        {
            var list = ConvertStringToList(value.ToString());
            return list;
        }

        private List<string> ConvertStringToList(string inputString)
        {
            var resultList = new List<string>();

            var startIndex = 0;
            var currentIndex = 0;
            var inQuotes = false;

            while (currentIndex < inputString.Length)
            {
                var currentChar = inputString[currentIndex];

                if (currentChar == '"')
                {
                    inQuotes = !inQuotes;
                }
                else if (currentChar == ',' && !inQuotes)
                {
                    var element = inputString.Substring(startIndex, currentIndex - startIndex).Trim('"');
                    resultList.Add(element);
                    startIndex = currentIndex + 1;
                }

                currentIndex++;
            }

            if (startIndex < currentIndex)
            {
                var lastElement = inputString.Substring(startIndex, currentIndex - startIndex).Trim('"');
                resultList.Add(lastElement);
            }

            return resultList;
        }
    }

    public enum LineType
    {
        Integer,
        List,
        String,
        Char
    }
}
