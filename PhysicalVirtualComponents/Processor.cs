using ProcessorSim.Instructions;
using System.Reflection;

namespace ProcessorSim.PhysicalVirtualComponents
{
    internal class Processor
    {
		private InstructionsSizeCalculator _instructionsSizeCalculator;

		public SortedDictionary<int, string> InstructionsToExecute;
        public Dictionary<string, Register> Registers;
        public int CurrentInstructionNum;
        public bool ShouldStop;
        public byte[] Memory;            

        public Processor(int amountOfRam, int amountOfRegisters)
        {
            _instructionsSizeCalculator = new InstructionsSizeCalculator();

            Registers = new Dictionary<string, Register>();
            InstructionsToExecute = ReadResource("ProcessorSim.Instructions.InstructionsToExecute.txt")!;            
            Memory = new byte[amountOfRam];
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
            for (var i = 0; i <= Memory.Length - neededSize; i++)
            {
                if (Memory.Skip(i).Take(neededSize).All(x => x == 0))
                {
                    return i;
                }
            }

            return -1;
        }

        public void ExecuteInstruction(InstructionsResolver instructionsResolver, string instruction)
        {
            var action = instructionsResolver.ResolveInstruction(instruction);
            action.Invoke();
        }

        public string GetInstruction()
        {
           InstructionsToExecute.TryGetValue(CurrentInstructionNum, out var instruction);
           return instruction!;
        }

		public int GetNextKey(SortedDictionary<int, string> dictionary, int currentKey)
		{
			bool foundCurrent = false;

			foreach (var key in dictionary.Keys)
			{
				if (foundCurrent)
				{
					return key;
				}

				if (key == currentKey)
				{
					foundCurrent = true;
				}
			}

			return -1;
		}

		private SortedDictionary<int, string>? ReadResource(string resourceName)
        {
            var assemblyInstructions = new SortedDictionary<int, string>();
            var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName);

            if (stream == null)
            {
                return null;
            }
            
            var totalInstructionsLength = 0;

            using var reader = new StreamReader(stream);

            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine()!;               
                var size = _instructionsSizeCalculator.CalculateInstructionSize(line);

                if (size == 0)
                {
                    continue;
                }

                assemblyInstructions.Add(totalInstructionsLength, line);

                totalInstructionsLength += size;
            }

            return assemblyInstructions;
        }		
	}
}
