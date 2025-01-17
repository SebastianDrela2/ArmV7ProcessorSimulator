using ProcessorSim.Instructions;
using ProcessorSim.PhysicalVirtualComponents;

namespace ProcessorSim
{
    class Program
    {
        static void Main()
        {
            var processor = new Processor(4096, 7);
            var system = new PhysicalVirtualComponents.System(processor);
            var instructionExecutor = new InstructionExecutor(processor, system);
            var instructionRetriever = new InstructionRetriever(instructionExecutor);
            var instructionsResolver = new InstructionsResolver(processor, instructionRetriever);
            
            Execute(processor, system, instructionsResolver);
        }

        private static void Execute(Processor processor, PhysicalVirtualComponents.System system, InstructionsResolver instructionsResolver)
        {
			processor.CurrentInstructionNum = 0;
            var lastInstructionIndex = processor.InstructionsToExecute.Keys.Last();

            while (processor.CurrentInstructionNum < lastInstructionIndex)
            {
                if (processor.ShouldStop)
                {
                    break;
                }

                var instruction = processor.GetInstruction();
                processor.ExecuteInstruction(instructionsResolver, instruction);

                processor.CurrentInstructionNum = processor.GetNextKey(processor.InstructionsToExecute, processor.CurrentInstructionNum);
            }

            Console.WriteLine();
            system.DisplayAllocatedStackMemory();
        }		
	}
}
