using ProcessorSim.PhysicalVirtualComponents;

namespace ProcessorSim.Instructions
{
    internal class InstructionsResolver
    {
        private readonly Processor _processor;
        private readonly InstructionRetriever _actionRetriever;

        public InstructionsResolver(Processor processor, InstructionRetriever actionRetriever)
        {
            _processor = processor;
            _actionRetriever = actionRetriever;

        }
        public Action ResolveInstruction(string line)
        {
            var instruction = ParseInstruction(line);           
            
            if (instruction.SecondParameter is not null &&
                _processor.Registers.Any(x => x.Key == instruction.FirstParameter) &&
				_processor.Registers.Any(x => x.Key == instruction.SecondParameter))
            {
                var register = ParseRegister(instruction.FirstParameter);
                var register2 = ParseRegister(instruction.SecondParameter);

                return _actionRetriever.GetLDRInstruction(register, register2);
            }

            if (_processor.Registers.Any(x => x.Key == instruction.FirstParameter))
            {
                return GetRegisterAction(instruction);
            }

            return _actionRetriever.GetNonRegisterInstruction(instruction);
        }

        private Action GetRegisterAction(Instruction instruction)
        {
            var parsedSecondValue = ParseParameterValue(instruction.SecondParameter);
            var parsedThirdValue = ParseParameterValue(instruction.ThirdParameter);
            var register = ParseRegister(instruction.FirstParameter);
        
            return _actionRetriever.GetArithemticRegisterInstruction(instruction.Operation, register, parsedSecondValue, parsedThirdValue);
        }

        private int ParseParameterValue(string parameter)
        {
            if (_processor.Registers.Any(x => x.Key == parameter))
            {
                return ParseRegister(parameter).Value;
            }

            if (int.TryParse(parameter, out var result))
            {
                return result;
            }

            return 0;
        }

        private Instruction ParseInstruction(string input)
        {
            var parts = input.Split(' ');           
            var instruction = new Instruction();

			Enum.TryParse(parts[0], out instruction.Operation);

            if (parts.Length > 1)
            {
                instruction.FirstParameter = parts[1];
            }

            if (parts.Length > 2)
            {
                instruction.SecondParameter = parts[2];
            }

            if (parts.Length > 3)
            {
                instruction.ThirdParameter = parts[3];
            }

            return instruction;
        }

        private Register ParseRegister(string registerLine)
        {
            return _processor.Registers[registerLine];
        }
    }
}
