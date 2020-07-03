# NullDereference
## What exactly is NULL?
In the majority of object-oriented languages, NULL exists as a default value of reference variables. It indicates that a given variable doesn’t have a specific value. NULL itself also has to have its own representation in a program’s memory.

If we dig deeper, we can notice that NULL is a pointer pointing to address zero in the virtual memory of a given process. More specifically, it leads to the first page of virtual memory, which is reserved by a system for NULL representation.

In the case of Windows systems, this even includes the first 64kb of memory, to which a program doesn’t have authorisation and which are unavailable to it. Because of this, a NULL reference causes exceptions.

## Overwrite null
As I have mentioned before, NULL represents the first page in the virtual memory of a given process, and the operating system prevents programs from accessing that memory fragment. But is true, without a doubt, that there is no way to modify this part of the virtual memory? In one of my presentations, I show how to overwrite NULL.

It appears that there is an unreported NtAllocateVirtualMemory method in the Windows system, which can allocate this memory block if given properly prepared parameters. Next, we can write anything we want there, i.e. a fragment of a procedure that will execute itself in the event of a NULL reference.

This gap may be even more dangerous, because, inside the virtual memory of a process, there is also a part dedicated to processes working in kernel mode. They use the same virtual memory and the same value of NULL.

The skillful exploitation of these facts may result in a privilege escalation attack. If you are interested in this topic, check this repo. I put the code fragments here, tested on Windows 7 32 bit.
