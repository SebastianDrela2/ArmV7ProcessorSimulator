﻿namespace ProcessorSim.Variables
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
                    var typeDefinition = parts[0];
                    var variableName = parts[1];
                    var value = parts[2];

                    var lineType = GetLineType(typeDefinition);

                    var variable = new Variable(lineType, value, variableName);
                    variablesList.Add(variable);
                }
            }

            return variablesList;
        }

        private VariableLineType GetLineType(string instruction)
        {
            return instruction switch
            {
                _ when instruction.Contains("list") => VariableLineType.List,
                _ when instruction.Contains("int") => VariableLineType.Integer,
                _ when instruction.Contains("string") => VariableLineType.String,
                _ when instruction.Contains("char") => VariableLineType.Char,
                _ => throw new NotImplementedException(),
            };
        }
    }
}
