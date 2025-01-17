using ProcessorSim.PhysicalVirtualComponents;

namespace ProcessorSim.Instructions
{
    internal class InstructionExecutor
    {
        private readonly Processor _processor;
        private readonly PhysicalVirtualComponents.System _system;

        public InstructionExecutor(Processor processor, PhysicalVirtualComponents.System system)
        {
            _processor = processor;
            _system = system;
        }

        public void MoveIntoRegister(Register register, int value)
        {
            register.Value = value;
        }

        public void AddIntoRegister(Register register, int value, int value2)
        {
            register.Value = value + value2;
        }

        public void SubIntoRegister(Register register, int value, int value2)
        {
            register.Value = value - value2;
        }

        public void MulIntoRegister(Register register, int value, int value2)
        {
            register.Value = value * value2;
        }

        public void LeftShiftIntoRegister(Register register, int value, int value2)
        {
            for (var i = 0; i < value2; i++)
            {
                value *= 2;
            }

            register.Value = value;
        }

        public void RightShiftIntoRegister(Register register, int value, int value2)
        {
            for (var i = 0; i < value2; i++)
            {
                value /= 2;
            }

            register.Value = value;
        }
        
        public void JumpBranch(string branchName)
        {           
            var branchIndex = _processor.InstructionsToExecute.First(x => x.Value.Contains($"{branchName}:")).Key;
            var nextIndexFromBranch = _processor.GetNextKey(_processor.InstructionsToExecute, branchIndex);
			
			_processor.Registers["lr"].Value = _processor.CurrentInstructionNum;
			_processor.CurrentInstructionNum = nextIndexFromBranch;
		}

        public void JumpBranchIfGreaterThanOrEquals(string branchName)
        {
            if (_processor.Registers["cspr"].Value >= 0)
            {
                JumpBranch(branchName);
            }
        }

        public void JumpBranchIfGreaterThan(string branchName)
        {
            if (_processor.Registers["cspr"].Value > 0)
            {
                JumpBranch(branchName);
            }
        }

        public void JumpBranchIfLessThan(string branchName)
        {
            if (_processor.Registers["cspr"].Value < 0)
            {
                JumpBranch(branchName);
            }
        }

        public void JumpBranchIfLessThanEquals(string branchName)
        {
            if (_processor.Registers["cspr"].Value <= 0)
            {
                JumpBranch(branchName);
            }
        }

        public void LoadDataFromRegister(Register register, Register register2)
        {
            register.Value = register2.Value;
        }

        public void EndBranch()
        {
            _processor.CurrentInstructionNum = _processor.Registers["lr"].Value;
        }

        public void Compare(int value1, int value2)
        {
            var comparisonValue = value1 - value2;
            _processor.Registers["cspr"].Value = comparisonValue;
        }

        public void DoSystemInterupt(int value)
        {
            if (value == 0)
            {
                _system.DoSystemInterupt();
            }
        }

        public void Exit()
        {
            _processor.ShouldStop = true;
        }
    }
}
