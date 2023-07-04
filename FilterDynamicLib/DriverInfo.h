#pragma once
#include <Windows.h>
#include <fltUser.h>

#define DRIVER_PORT_NAME    L"\\MicrosoftMiniFilterPort"
#define REST_INTERVAL       (10)
#define MAX_FILE_PATH       (261)
#define MAX_VOLUME_NAME_LEN (100)

#ifdef __cplusplus
extern "C" {
#endif
    typedef struct _TFRSTRING
    {
        WCHAR VolumeName[MAX_VOLUME_NAME_LEN];
        WCHAR  Content[MAX_FILE_PATH];
    } TFRSTR;

    typedef struct _TRANSFER_DATA {

        //
        //  Header
        //
        FILTER_MESSAGE_HEADER MessageHeader;

        //
        //  Process.
        //
        HANDLE ProcessId;

        //
        //  File
        //
        TFRSTR FileName;
    } TFR_DATA;

#ifdef __cplusplus
};
#endif
