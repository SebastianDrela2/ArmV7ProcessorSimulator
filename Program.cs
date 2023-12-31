using System.Reflection;
using ProcessorSim.Instructions;
using ProcessorSim.Variables;

namespace ProcessorSim
{
    class Program
    {
        static void Main()
        {
            var processor = new Processor(100, 7);
            var instructionExecutor = new InstructionExecutor(processor);
            var actionRetriever = new InstructionRetriever(instructionExecutor);
            var instructionsResolver = new InstructionsResolver(processor, actionRetriever);
            var variablesRetriever = new VariablesRetriever();

            var instructionsToExecute = ReadResource("ProcessorSim.Instructions.InstructionsToExecute.txt");
            var variables = variablesRetriever.GetVariables(instructionsToExecute);

            foreach (var variable in variables)
            {
                processor.LoadValIntoStack(instructionExecutor, variable);
            }

            foreach (var instruction in instructionsToExecute)
            {
                if (!processor.ShouldStop)
                {
                    processor.ExecuteInstruction(instructionsResolver, instruction);
                }
            }
        }

        private static List<string>? ReadResource(string resourceName)
        {
            var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName);

            if (stream == null)
            {
                return null;
            }

            var returnList = new List<string>();

            using var reader = new StreamReader(stream);

            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();

                if (line != null)
                {
                    returnList.Add(line);
                }
            }

            return returnList;

        }
    }
}
