// The following ifdef block is the standard way of creating macros which make exporting
// from a DLL simpler. All files within this DLL are compiled with the FILTERDYNAMICLIB_EXPORTS
// symbol defined on the command line. This symbol should not be defined on any project
// that uses this DLL. This way any other project whose source files include this file see
// FILTERDYNAMICLIB_API functions as being imported from a DLL, whereas this DLL sees symbols
// defined with this macro as being exported.

#include <Windows.h>

#ifdef FILTERDYNAMICLIB_EXPORTS
#define FILTERDYNAMICLIB_API __declspec(dllexport)
#else
#define FILTERDYNAMICLIB_API __declspec(dllimport)
#endif

class FILTERDYNAMICLIB_API FilterLog
{
public:
	FilterLog();
	~FilterLog();
	bool StartLog();
	void EndLog();
private:
	static unsigned WINAPI RetrieveLogRecords(_In_ LPVOID lpParameter);
	bool GetCancelFlag();
	void SetCancelFlag(bool input);


	HANDLE m_Thread;
	HANDLE m_Port;
	CRITICAL_SECTION m_CritSec;
	volatile bool m_cancel_flag;

};
