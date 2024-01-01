using ProcessorSim.PhysicalVirtualComponents;

namespace ProcessorSim.Instructions
{
    internal class InstructionRetriever
    {
        private readonly InstructionExecutor _instructionExecutor;
        public InstructionRetriever(InstructionExecutor instructionExecutor)
        {
            _instructionExecutor = instructionExecutor;
        }

        public Action LoadMemoryIntoRegister(Register register, string variableName) =>
           () => _instructionExecutor.LoadMemoryIntoRegister(register, variableName);
        
        public Action GetArithemticRegisterInstruction(string operation, Register register, int value, int value2)
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

        public Action GetNonRegisterInstruction(string operation, string firstParameter, string secondParameter)
        {
            return operation switch
            {
                "EXIT" => () => _instructionExecutor.Exit(),
                "BLA" => () => _instructionExecutor.JumpBranch(firstParameter),
                "BGE" => () => _instructionExecutor.JumpBranchIfGreaterThanOrEquals(firstParameter),
                "BGT" => () => _instructionExecutor.JumpBranchIfGreaterThan(firstParameter),
                "BLE" => () => _instructionExecutor.JumpBranchIfLessThanEquals(firstParameter),
                "BLT" => () => _instructionExecutor.JumpBranchIfLessThan(firstParameter),
                "END" => () => _instructionExecutor.EndBranch(),
                "CMP" => () => _instructionExecutor.Compare(int.Parse(firstParameter), int.Parse(secondParameter)),
                "SWI" => () => _instructionExecutor.DoSystemInterupt(int.Parse(firstParameter)),
                _ => throw new NotImplementedException()
            };
        }
    }
}
