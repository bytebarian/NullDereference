// NullDereference.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include <windows.h>
#include <iostream>
using namespace std;

void my_null_func() 
{
	printf("Hello NULL World");
}

int main()
{
	DWORD(WINAPI *NtAllocateVirtualMemory)(HANDLE ProcessHandle, PVOID *BaseAddress, ULONG ZeroBits, PULONG RegionSize, ULONG AllocationType, ULONG Protect);

	*(FARPROC *)&NtAllocateVirtualMemory = GetProcAddress(LoadLibrary(L"ntdll.dll"), "NtAllocateVirtualMemory");
	if (!NtAllocateVirtualMemory)
		return 0;

	PVOID pBaseAddr = (PVOID)1;
	ULONG uSize = 0x1000;

	DWORD result = NtAllocateVirtualMemory(GetCurrentProcess(), &pBaseAddr, 0, &uSize, MEM_COMMIT | MEM_RESERVE | MEM_TOP_DOWN, PAGE_EXECUTE_READWRITE);

	/*char source[] = "Hello it's NULL";
	memcpy(NULL, source, sizeof(source));

	printf("strlen(NULL): %i\n", strlen(NULL));*/

	void(*func)(void);
	func = my_null_func;
	memcpy(NULL, &func, sizeof(&func));

	void(*nullFunc)(void) = NULL;
	nullFunc();

	getchar();
}

