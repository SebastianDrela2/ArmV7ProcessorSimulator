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

        public Action GetNonRegisterInstruction(Instruction instruction)
        {
            return instruction.Operation switch
            {
                "EXIT" => () => _instructionExecutor.Exit(),
                "BLA" => () => _instructionExecutor.JumpBranch(instruction.FirstParameter),
                "BGE" => () => _instructionExecutor.JumpBranchIfGreaterThanOrEquals(instruction.FirstParameter),
                "BGT" => () => _instructionExecutor.JumpBranchIfGreaterThan(instruction.FirstParameter),
                "BLE" => () => _instructionExecutor.JumpBranchIfLessThanEquals(instruction.FirstParameter),
                "BLT" => () => _instructionExecutor.JumpBranchIfLessThan(instruction.FirstParameter),
                "END" => () => _instructionExecutor.EndBranch(),
                "CMP" => () => _instructionExecutor.Compare(int.Parse(instruction.FirstParameter), int.Parse(instruction.SecondParameter)),
                "SWI" => () => _instructionExecutor.DoSystemInterupt(int.Parse(instruction.FirstParameter)),
                _ => throw new NotImplementedException()
            };
        }
    }
}
