using ProcessorSim.Instructions;
using ProcessorSim.Variables;
using System.Reflection;

namespace ProcessorSim
{
    internal class Processor
    {
        public List<string> InstructionsToExecute;
        public int CurrentInstructionNum;
        public bool ShouldStop;
        public int[] RamStack;
        public Register[] Registers;
        public List<Variable> Variables;

        public Processor(int amountOfRam, int amountOfRegisters)
        {
            InstructionsToExecute = ReadResource("ProcessorSim.Instructions.InstructionsToExecute.txt")!;
            CurrentInstructionNum = 0;
            RamStack = new int[amountOfRam];
            Registers = new Register[amountOfRegisters];
            ShouldStop = false;

            for (var i = 0; i < amountOfRegisters; i++)
            {
                Registers[i] = new Register(); 
            }
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

            return -1 ;
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
