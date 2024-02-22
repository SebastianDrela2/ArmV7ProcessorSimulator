## ArmV7 Processor assembly executor simulator.

Example instruction:

```
﻿MOV r1 5
LDR r2 lista
CMP 3 6
BLT branch
BLA helloworld
EXIT

branch:
MOV r3 10
ADD r4 2 10
END

helloworld:
MOV r0 1
LDR r1 message
LDR r2 messagelength
MOV r7 4
SWI 0
END

.list lista "1","2","3"
.string message "helloworld"
.int messagelength 12
```
