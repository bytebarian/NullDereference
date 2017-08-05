// NullRefEx.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include <iostream>
using namespace std;


int main()
{
	char *s = NULL;

	for (int i = 0; i < strlen(s); i++) {
		cout << s[i];
	}

	getchar();

    return 0;
}

