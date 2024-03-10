# ArmV7 Processor assembly executor simulator.

## Loading Memory Into Register

- Operation `MOV` generates an action to load the content of a memory variable into the specified register.

## Loading Data into the Register

- Operation `LDR` loads linked data ex `.string message "helloworld"` into desired register.

## Arithmetic Register Instructions

- Supported operations include `ADD` `SUB` `MUL` `LS` _(Left Shift)_, and `RS` _(Right Shift)_. 
- The chosen action corresponds to the execution of the respective arithmetic operation involving registers and immediate values.

## Non-Register Instructions

- Supported operations include `EXIT` 
- `BLA` _(Branch and Link Always)_, 
- `BGE` _(Branch Greater Than or Equal)_ 
- `BGT` _(Branch Greater Than)_, 
- `BLE` _(Branch Less Than or Equal)_
- `BLT` _(Branch Less Than)_
- `END` _(End Branch)_
- `CMP` _(Compare)_
- `SWI` _(System Interrupt)_. 

The chosen action corresponds to the execution of the respective non-register operation, taking into account any required parameters.

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
