namespace ProcessorSim.Instructions;

public enum OpCode : byte
{
	MOV, 
	ADD, 
	SUB, 
	MUL, 
	LS,
	LDR,
	RS,
	EXIT,
	BLA,
	BGE,
	BGT,
	BLE,
	BLT,
	END,
	CMP,
	SWI
}
