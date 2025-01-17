namespace ProcessorSim.Instructions;

internal class InstructionsSizeCalculator
{
	private readonly List<OpCode> _nonRegisterInstructions =
	[
		OpCode.EXIT,
		OpCode.BLA,
		OpCode.BGE,
		OpCode.BGT,
		OpCode.BLE,
		OpCode.BLT,
		OpCode.END,
		OpCode.CMP,
		OpCode.SWI
	];

	private readonly List<OpCode> _registerInstructions =
	[
		OpCode.MOV,
		OpCode.ADD,
		OpCode.SUB,
		OpCode.MUL,
		OpCode.LS,
		OpCode.RS
	];
	public int CalculateInstructionSize(string lines)
	{
		var totalSize = 0;
		var splitLines = lines.Split(' ');
		
		if (Enum.TryParse<OpCode>(splitLines[0], out var opCode))
		{
			var intOpCode = (int)opCode;
			totalSize += intOpCode;
		}

		var instructionType = GetInstructionType(opCode, splitLines);

		if (!string.IsNullOrEmpty(lines))
		{
			totalSize += CalculateRestOfInstructionBasedOnInstructionAndOpCode(instructionType, opCode, splitLines);
		}

		return totalSize;
	}
	
	private int CalculateRestOfInstructionBasedOnInstructionAndOpCode(InstructionType instructionType, OpCode opCode, string[] splitLines)
	{		
		if (instructionType is InstructionType.RegisterInstruction)
		{
			var register = splitLines[1].ToUpper();
			var value = int.Parse(splitLines[2]);

			Enum.TryParse<RegisterCode>(register, out var registerCode);

			var intRegisterCode = (int)registerCode;
			var result = intRegisterCode + value;

			if (opCode is not OpCode.MOV)
			{
				var value2 = int.Parse(splitLines[3]);
				result += value2;
			}

			return result;
		}

		if (instructionType is InstructionType.NonRegisterInstruction)
		{
			if (opCode == OpCode.EXIT || opCode == OpCode.END)
			{
				return 0;
			}

			var result = 0;

			if (splitLines.Length > 2)
			{
				result += int.Parse(splitLines[2]);				
			}

			if (splitLines.Length > 3)
			{
				var value2 = int.Parse(splitLines[3]);
				result += value2;
			}

			return result;
		}

		if (instructionType is InstructionType.Label)
		{
			return 4;
		}

		return 1;
	}

	private InstructionType GetInstructionType(OpCode opCode, string[] splitLines)
	{
		if (splitLines.Any(line => line.Contains(":")))
		{
			return InstructionType.Label;
		}

		if (_nonRegisterInstructions.Contains(opCode))
		{
			return InstructionType.NonRegisterInstruction;
		}

		if (_registerInstructions.Contains(opCode))
		{
			return InstructionType.RegisterInstruction;
		}

		return InstructionType.LdrInstruction;
	}
}
