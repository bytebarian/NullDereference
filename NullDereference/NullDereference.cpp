// NullDereference.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include <windows.h>
#include <iostream>
#include <conio.h>
#include <string>
using namespace std;

#pragma region 
void init()
{
	DWORD(WINAPI *NtAllocateVirtualMemory)
		(HANDLE ProcessHandle, 
			PVOID *BaseAddress, 
			ULONG ZeroBits, 
			PULONG RegionSize, 
			ULONG AllocationType, 
			ULONG Protect);

	*(FARPROC *)&NtAllocateVirtualMemory 
		= GetProcAddress(LoadLibrary(L"ntdll.dll"), "NtAllocateVirtualMemory");
	if (!NtAllocateVirtualMemory)
		return;

	PVOID pBaseAddr = (PVOID)1;
	ULONG uSize = 0x1000;

	DWORD result = NtAllocateVirtualMemory
		(GetCurrentProcess(), 
			&pBaseAddr, 
			0, 
			&uSize, 
			MEM_COMMIT | MEM_RESERVE | MEM_TOP_DOWN, 
			PAGE_EXECUTE_READWRITE);

	char source[] = "8====D karny kutas za odwolanie do nulla";
	memcpy(NULL, source, sizeof(source));
}
#pragma endregion

int main()
{
	init();

	char *s = NULL;

	for (int i = 0; i < strlen(s); i++) {
		cout << s[i];
	}

	getchar();
}



