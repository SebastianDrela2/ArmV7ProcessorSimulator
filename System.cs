namespace ProcessorSim
{
    internal class System
    {
        private readonly Processor _processor;

        public System(Processor processor)
        {
            _processor = processor;
        }

        public void DoSystemInterupt()
        {
            var descriptorValue = _processor.Registers["r0"].Value;
            var messageRamPos = _processor.Registers["r1"].Value;
            var messageLength = _processor.Registers["r2"].Value;
            var operation = _processor.Registers["r7"].Value;

            if (descriptorValue == 1 && operation == 4)
            {
                WriteToConsole(messageRamPos, messageLength);
            }
        }

        public void WriteToConsole(int messageRamPos, int messageLength)
        {
            for (var i = 0; i < messageLength ; i++)
            {
                var c = Convert.ToChar(_processor.RamStack[messageRamPos]);
                Console.Write(c);

                messageRamPos++;
            }
        }
        
    }
}
