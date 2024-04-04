using ProcessorSim.Instructions;
using ProcessorSim.Variables;
using System.Reflection;

namespace ProcessorSim.PhysicalVirtualComponents
{
    internal class Processor
    {
        public List<string> InstructionsToExecute;
        public Dictionary<string, Register> Registers;
        public List<Variable> Variables;
        public int CurrentInstructionNum;
        public bool ShouldStop;
        public byte[] RamStack;       

        public Processor(int amountOfRam, int amountOfRegisters)
        {
            Registers = new Dictionary<string, Register>();
            InstructionsToExecute = ReadResource("ProcessorSim.Instructions.InstructionsToExecute.txt")!;           
            RamStack = new byte[amountOfRam];
            ShouldStop = false;

            for (var i = 0; i <= amountOfRegisters; i++)
            {
                Registers.Add($"r{i}", new Register());
            }

            Registers.Add("lr", new Register());
            Registers.Add("cspr", new Register());
        }

        public int GetFreeRamPos(int neededSize)
        {
            for (var i = 0; i <= RamStack.Length - neededSize; i++)
            {
                if (RamStack.Skip(i).Take(neededSize).All(x => x == 0))
                {
                    return i;
                }
            }

            return -1;
        }

        public void LoadValIntoStack(InstructionExecutor instructionExecutor, Variable variable)
        {
            instructionExecutor.LoadValIntoStack(variable);
        }

        public void SetVariables(List<Variable> variables)
        {
            Variables = variables;
        }

        public void ExecuteInstruction(InstructionsResolver instructionsResolver, string instruction)
        {
            var action = instructionsResolver.ResolveInstruction(instruction);
            action.Invoke();
        }

        public string GetInstruction()
        {
            return InstructionsToExecute[CurrentInstructionNum];
        }

        public int GetMemoryLocationForVariable(string variableName)
        {
            return Variables.First(x => x.VariableName == variableName).MemoryLocation;
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
