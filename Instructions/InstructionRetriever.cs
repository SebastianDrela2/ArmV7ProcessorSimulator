namespace ProcessorSim.Instructions
{
    internal class InstructionRetriever
    {
        private readonly InstructionExecutor _instructionExecutor;
        public InstructionRetriever(InstructionExecutor instructionExecutor)
        {
            _instructionExecutor = instructionExecutor;
        }

        public Action GetRegisterInstruction(string operation, Register register, int value, int value2)
        {
            return operation switch
            {
                "MOV" => () => _instructionExecutor.MoveIntoRegister(register, value),
                "ADD" => () => _instructionExecutor.AddIntoRegister(register, value, value2),
                "SUB" => () => _instructionExecutor.SubIntoRegister(register, value, value2),
                "MUL" => () => _instructionExecutor.MulIntoRegister(register, value, value2),
                "LS" => () => _instructionExecutor.LeftShiftIntoRegister(register, value, value2),
                "RS" => () => _instructionExecutor.RightShiftIntoRegister(register, value, value2),
                _ => throw new NotImplementedException()
            };
        }

        public Action GetNonRegisterInstruction(string operation)
        {
            return operation switch
            {
                "EXIT" => () => _instructionExecutor.Exit(),
                _ => throw new NotImplementedException()
            };
        }
    }
}
