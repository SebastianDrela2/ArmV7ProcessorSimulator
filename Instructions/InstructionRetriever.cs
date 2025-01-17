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
        public Action GetArithemticRegisterInstruction(OpCode operation, Register register, int value, int value2)
        {
            return operation switch
            {
                OpCode.MOV => () => _instructionExecutor.MoveIntoRegister(register, value),
                OpCode.ADD => () => _instructionExecutor.AddIntoRegister(register, value, value2),
                OpCode.SUB => () => _instructionExecutor.SubIntoRegister(register, value, value2),
                OpCode.MUL => () => _instructionExecutor.MulIntoRegister(register, value, value2),
                OpCode.LS => () => _instructionExecutor.LeftShiftIntoRegister(register, value, value2),
                OpCode.RS => () => _instructionExecutor.RightShiftIntoRegister(register, value, value2),
                _ => throw new NotImplementedException()
            };
        }

        public Action GetNonRegisterInstruction(Instruction instruction)
        {
            return instruction.Operation switch
            {
                OpCode.EXIT => () => _instructionExecutor.Exit(),
                OpCode.BLA => () => _instructionExecutor.JumpBranch(instruction.FirstParameter),
                OpCode.BGE => () => _instructionExecutor.JumpBranchIfGreaterThanOrEquals(instruction.FirstParameter),
                OpCode.BGT => () => _instructionExecutor.JumpBranchIfGreaterThan(instruction.FirstParameter),
                OpCode.BLE => () => _instructionExecutor.JumpBranchIfLessThanEquals(instruction.FirstParameter),
                OpCode.BLT => () => _instructionExecutor.JumpBranchIfLessThan(instruction.FirstParameter),
                OpCode.END => () => _instructionExecutor.EndBranch(),
                OpCode.CMP => () => _instructionExecutor.Compare(int.Parse(instruction.FirstParameter), int.Parse(instruction.SecondParameter)),
                OpCode.SWI => () => _instructionExecutor.DoSystemInterupt(int.Parse(instruction.FirstParameter)),
                _ => throw new NotImplementedException()
            };
        }

        public Action GetLDRInstruction(Register register, Register register2)
        {
            return () => _instructionExecutor.LoadDataFromRegister(register, register2);
        }
    }
}
