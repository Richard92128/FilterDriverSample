// FilterDynamicLib.cpp : Defines the exported functions for the DLL.
//

#include "pch.h"
#include "framework.h"
#include "FilterDynamicLib.h"
#include "DriverInfo.h"
#include <process.h>
#include <iostream>

FilterLog::FilterLog() :
    m_Thread(NULL),
    m_Port(INVALID_HANDLE_VALUE),
    m_cancel_flag(false)
{
    InitializeCriticalSection(&m_CritSec);
}

FilterLog::~FilterLog()
{
    SetCancelFlag(true);
    if (m_Port != INVALID_HANDLE_VALUE)
    {
        CloseHandle(m_Port);
    }

    if (m_Thread != NULL)
    {
        WaitForSingleObject(m_Thread, INFINITE);
    }
    DeleteCriticalSection(&m_CritSec);
}

bool FilterLog::GetCancelFlag()
{
    bool retVal = false;
    EnterCriticalSection(&m_CritSec);
    retVal = m_cancel_flag;
    LeaveCriticalSection(&m_CritSec);
    return retVal;
}

void FilterLog::SetCancelFlag(bool input)
{
    EnterCriticalSection(&m_CritSec);
    m_cancel_flag = input;
    LeaveCriticalSection(&m_CritSec);
}

bool FilterLog::StartLog()
{
    HRESULT hResult = S_OK;

    hResult = FilterConnectCommunicationPort(DRIVER_PORT_NAME,
        0,
        NULL,
        0,
        NULL,
        &m_Port);

    if (IS_ERROR(hResult)) {

        return false;
    }

    m_Thread = (HANDLE)_beginthreadex(NULL,
        0,
        RetrieveLogRecords,
        (LPVOID)this,
        0,
        NULL);

    return true;
}


unsigned  WINAPI FilterLog::RetrieveLogRecords(
    _In_ LPVOID lpParameter
)
/*++

Routine Description:

    This runs as a separate thread.  Its job is to retrieve log records
    from the filter and then output them

Arguments:

    lpParameter - Contains context structure for synchronizing with the
        main program thread.

Return Value:

    The thread successfully terminated

--*/
{
    FilterLog* context = (FilterLog*)lpParameter;
    HRESULT hResult = S_OK;
    TFR_DATA data;

    while (!context->GetCancelFlag())
    {
        hResult = FilterGetMessage(context->m_Port,
            &data.MessageHeader,
            sizeof(TFR_DATA),
            NULL);

        if (IS_ERROR(hResult)) {

            if (HRESULT_FROM_WIN32(ERROR_INVALID_HANDLE) == hResult) {

                break;
            }
            else {
                Sleep(REST_INTERVAL);
            }

            continue;
        }

        std::wcout << "File:" << data.FileName.Content << L" Deleted by " << data.ProcessId << std::endl;

    }

    return 0;
}

void FilterLog::EndLog()
{
    SetCancelFlag(true);
    if (m_Port != INVALID_HANDLE_VALUE)
    {
        CloseHandle(m_Port);
        m_Port = INVALID_HANDLE_VALUE;
    }

    if (m_Thread != NULL)
    {
        WaitForSingleObject(m_Thread, INFINITE);
        m_Thread = NULL;
    }
    SetCancelFlag(false);
}