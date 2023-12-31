namespace ProcessorSim.Variables
{
    internal class VariablesRetriever
    {
        public List<Variable> GetVariables(List<string> instructionsToExecute)
        {
            var variablesList = new List<Variable>();

            foreach (var instruction in instructionsToExecute)
            {
                if (instruction.StartsWith("."))
                {
                    var parts = instruction.Split(' ');
                    var lineType = GetLineType(parts[0]);

                    var variable = new Variable(lineType, parts[1]);
                    variablesList.Add(variable);
                }
            }

            return variablesList;
        }

        private LineType GetLineType(string instruction)
        {
            return instruction switch
            {
                _ when instruction.Contains("list") => LineType.List,
                _ when instruction.Contains("int") => LineType.Integer,
                _ when instruction.Contains("string") => LineType.String,
                _ when instruction.Contains("char") => LineType.Char,
                _ => throw new NotImplementedException(),
            };
        }
    }
}
