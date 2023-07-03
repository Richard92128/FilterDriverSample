/*++

Module Name:

    driver.h

Abstract:

    This file contains the driver definitions.

Environment:

    Kernel-mode Driver Framework

--*/

#include <fltKernel.h>
#include <dontuse.h>
#include <suppress.h>

#define DFDBG_TRACE_ERRORS              0x00000001
#define DFDBG_TRACE_ROUTINES            0x00000002
#define DFDBG_TRACE_OPERATION_STATUS    0x00000004

#define DF_VOLUME_GUID_NAME_SIZE        48

#define DF_INSTANCE_CONTEXT_POOL_TAG    'nIfD'
#define DF_STREAM_CONTEXT_POOL_TAG      'xSfD'
#define DF_TRANSACTION_CONTEXT_POOL_TAG 'xTfD'
#define DF_ERESOURCE_POOL_TAG           'sRfD'
#define DF_DELETE_NOTIFY_POOL_TAG       'nDfD'
#define DF_STRING_POOL_TAG              'rSfD'

#define DF_CONTEXT_POOL_TYPE            PagedPool

#define DF_NOTIFICATION_MASK            (TRANSACTION_NOTIFY_COMMIT_FINALIZE | \
                                         TRANSACTION_NOTIFY_ROLLBACK)

#define DRIVER_PORT_NAME    L"\\MicrosoftMiniFilterPort"
#define FLTTIMEOUT    (-10000)
#define MAX_FILE_PATH (261)

////////////////////////////////////////////////////////////////////////////////
////  Macros                                                                  //
////////////////////////////////////////////////////////////////////////////////
//
#define DF_PRINT( ... )                                                      \
    DbgPrintEx( DPFLTR_FLTMGR_ID, DPFLTR_ERROR_LEVEL, __VA_ARGS__ )

#define DF_DBG_PRINT( _dbgLevel, ... )                                       \
    (FlagOn( gTraceFlags, (_dbgLevel) ) ?                                    \
        DF_PRINT( __VA_ARGS__ ):                                             \
        (0))

#define FlagOnAll( F, T )                                                    \
    (FlagOn( F, T ) == T)

#ifdef __cplusplus
extern "C" {
#endif
    ////////////////////////////////////////////////////////////////////////////////
    ////  Data                                                                  //
    ////////////////////////////////////////////////////////////////////////////////
    //
    typedef struct _MINIFILTER_DATA {

        //
        //  The object that identifies this driver.
        //

        PDRIVER_OBJECT DriverObject;

        //
        //  The filter that results from a call to
        //  FltRegisterFilter.
        //

        PFLT_FILTER Filter;

        //
        //  Server port: user mode connects to this port
        //

        PFLT_PORT ServerPort;

        //
        //  Client connection port: only one connection is allowed at a time.,
        //

        PFLT_PORT ClientPort;
    } MNFLT_DATA;

    typedef struct _TFRSTRING
    {
        WCHAR  Content[MAX_FILE_PATH];
    } TFRSTR;

    typedef struct _TRANSFER_DATA {
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

