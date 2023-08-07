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
    for (int i = 0; i < 100; ++i)
    {
		//HANDLE hPipe = NULL;
		//while (TRUE)
		//{
		//	hPipe = CreateFile(
		//		TEXT("\\\\.\\pipe\\WorkMornitorDeletedFileGate"),// Pipe name 
		//		GENERIC_READ |			// Read and write access 
		//		GENERIC_WRITE,
		//		0,						// No sharing 
		//		NULL,					// Default security attributes
		//		OPEN_EXISTING,			// Opens existing pipe 
		//		0,						// Default attributes 
		//		NULL);					// No template file 

		//	// Break if the pipe handle is valid. 
		//	if (hPipe != INVALID_HANDLE_VALUE)
		//		break;

		//	if (// Exit if an error other than ERROR_PIPE_BUSY occurs
		//		GetLastError() != ERROR_PIPE_BUSY
		//		||
		//		// All pipe instances are busy, so wait for 5 seconds
		//		!WaitNamedPipe(TEXT("\\\\.\\pipe\\WorkMornitorDeletedFileGate"), 5000))
		//	{
		//		return 1;
		//	}
		//}

		//// Set data to be read from the pipe as a stream of messages
		//DWORD dwMode = PIPE_READMODE_MESSAGE;
		//BOOL bResult = SetNamedPipeHandleState(hPipe, &dwMode, NULL, NULL);
		//if (!bResult)
		//{
		//	continue;
		//}

  //      std::wstring message = L"TEST";
  //      message += std::to_wstring(i);
  //      std::wstring retVal(1024, 0);
  //      DWORD byteRead = 0;
		//TransactNamedPipe(hPipe, (LPVOID)message.data(),
  //          message.length() * sizeof(wchar_t), (LPVOID)retVal.data(), retVal.length() * sizeof(wchar_t), &byteRead, NULL);

		//CloseHandle(hPipe);

		std::wstring message = L"TEST";
        message += std::to_wstring(i);
        std::wstring retVal(1024, 0);
        DWORD byteRead = 0;
		if (!CallNamedPipe(TEXT("\\\\.\\pipe\\WorkMornitorDeletedFileGate"), (LPVOID)message.data(),
			message.length() * sizeof(wchar_t), (LPVOID)retVal.data(), retVal.length() * sizeof(wchar_t), &byteRead, NULL))
		{
			// retry
			--i;
		}

    }
	
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
