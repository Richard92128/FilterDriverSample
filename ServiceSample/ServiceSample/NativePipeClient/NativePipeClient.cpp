// NativePipeClient.cpp : This file contains the 'main' function. Program execution begins and ends there.
//

//#include <iostream>
#include <windows.h>
#include <fileapi.h>
#include <namedpipeapi.h>
#include <string>
#include <handleapi.h>

int main()
{
  //  for (int i = 0; i < 100; ++i)
  //  {
		//std::wstring message = L"TEST";
  //      message += std::to_wstring(i);
  //      std::wstring retVal(1024, 0);
  //      DWORD byteRead = 0;
		//if (!CallNamedPipe(TEXT("\\\\.\\pipe\\WorkMornitorDeletedFileGate"), (LPVOID)message.data(),
		//	message.length() * sizeof(wchar_t), (LPVOID)retVal.data(), retVal.length() * sizeof(wchar_t), &byteRead, NULL))
		//{
		//	// retry
		//	--i;
		//}
  //  }

	std::wstring message = L"RegisterFolderPath>D:\\TestFolder";
	std::wstring retVal(1024, 0);
	DWORD byteRead = 0;
	while (!CallNamedPipe(TEXT("\\\\.\\pipe\\WorkMornitorDeletedFileGate"), (LPVOID)message.data(),
		message.length() * sizeof(wchar_t), (LPVOID)retVal.data(), retVal.length() * sizeof(wchar_t), &byteRead, NULL));
	
    return 0;
}

// Run program: Ctrl + F5 or Debug > Start Without Debugging menu
// Debug program: F5 or Debug > Start Debugging menu

// Tips for Getting Started: 
//   1. Use the Solution Explorer window to add/manage files
//   2. Use the Team Explorer window to connect to source control
//   3. Use the Output window to see build output and other messages
//   4. Use the Error List window to view errors
//   5. Go to Project > Add New Item to create new code files, or Project > Add Existing Item to add existing code files to the project
//   6. In the future, to open this project again, go to File > Open > Project and select the .sln file
