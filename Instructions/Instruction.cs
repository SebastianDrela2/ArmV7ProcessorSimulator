namespace ProcessorSim.Instructions
{
    public class Instruction
    {
        public OpCode Operation;
        public string FirstParameter { get; set; } = string.Empty;
        public string SecondParameter { get; set; } = string.Empty;
        public string ThirdParameter { get; set; } = string.Empty;

        public override string ToString()
        {
            return $"Operation: {Operation}, FirstParameter: {FirstParameter}, SecondParameter: {SecondParameter}";
        }
    }
}
