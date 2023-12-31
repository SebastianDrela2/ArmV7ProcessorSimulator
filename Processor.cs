using ProcessorSim.Instructions;
using ProcessorSim.Variables;

namespace ProcessorSim
{
    internal class Processor
    {
        public bool ShouldStop;
        public int[] RamStack;
        public Register[] Registers;
        public List<Variable> Variables;

        public Processor(int amountOfRam, int amountOfRegisters)
        {
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
    }
}
