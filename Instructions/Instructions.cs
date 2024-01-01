using ProcessorSim.Variables;

namespace ProcessorSim.Instructions
{
    internal class InstructionExecutor
    {
        private readonly Processor _processor;
        public InstructionExecutor(Processor processor)
        {
            _processor = processor;
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

        public void LoadDataIntoRegister(Register register, string value)
        {
            var valueMemoryLocation = _processor.Variables.First(x => x.VariableName == value).MemoryLocation;
            register.Value = _processor.RamStack[valueMemoryLocation];
        }

        public void LoadValIntoStack(Variable variable)
        {
            if (variable.Value is string stringValue)
            {
                var ramPos = _processor.GetFreeRamPos(stringValue.Length);
                variable.MemoryLocation = ramPos;
                foreach (var c in stringValue)
                {
                    _processor.RamStack[ramPos] = Convert.ToInt32(c);
                    ramPos += 1;
                }
            }
            else if (variable.Value is char charValue)
            {
                var ramPos = _processor.GetFreeRamPos(1);
                variable.MemoryLocation = ramPos;
                _processor.RamStack[ramPos] = charValue;
            }
            else if (variable.Value is int intValue)
            {
                var ramPos = _processor.GetFreeRamPos(1);
                variable.MemoryLocation = ramPos;
                _processor.RamStack[ramPos] = intValue;
            }
            else if (variable.Value is List<string> list)
            {
                var ramPos = _processor.GetFreeRamPos(list.Count);
                variable.MemoryLocation = ramPos;

                foreach (var item in list)
                {
                    _processor.RamStack[ramPos] = int.Parse(item);
                    ramPos += 1;
                }
            }
        }

        public void JumpBranch(string branchName)
        {
            var branchLine = _processor.InstructionsToExecute.First(x => x.Contains($"{branchName}:"));
            var branchIndex = _processor.InstructionsToExecute.IndexOf(branchLine);
            _processor.Registers[5].Value = _processor.CurrentLine;
            _processor.CurrentLine = branchIndex;
            
        }

        public void EndBranch()
        {
            _processor.CurrentLine = _processor.Registers[5].Value;
        }

        public void Exit()
        {
            _processor.ShouldStop = true;
        }
    }
}
