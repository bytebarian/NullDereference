// NullDereference.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include <windows.h>
#include <iostream>


int main()
{
	DWORD(WINAPI *NtAllocateVirtualMemory)(HANDLE ProcessHandle, PVOID *BaseAddress, ULONG ZeroBits, PULONG RegionSize, ULONG AllocationType, ULONG Protect);

	*(FARPROC *)&NtAllocateVirtualMemory = GetProcAddress(LoadLibrary(L"ntdll.dll"), "NtAllocateVirtualMemory");
	if (!NtAllocateVirtualMemory)
		return 0;

	PVOID pBaseAddr = (PVOID)1;
	ULONG uSize = 0x1000;

	DWORD result = NtAllocateVirtualMemory(GetCurrentProcess(), &pBaseAddr, 0, &uSize, MEM_COMMIT | MEM_RESERVE, PAGE_EXECUTE_READWRITE);

	memset(NULL, 0, 0x1000);
	printf("strlen(NULL): %i\n", strlen(NULL));
	getchar();
}

