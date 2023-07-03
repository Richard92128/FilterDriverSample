/*++

Copyright (c) 2011  Microsoft Corporation

Module Name:

    communication.c

Abstract:

    Communication module implementation.
    This module contains the routines that involves the communication
    between kernel mode and user mode.

Environment:

    Kernel mode

--*/

#include "Driver.h"

extern MNFLT_DATA gData;

NTSTATUS
MnFltConnectNotifyCallback(
    _In_ PFLT_PORT ClientPort,
    _In_ PVOID ServerPortCookie,
    _In_reads_bytes_(SizeOfContext) PVOID ConnectionContext,
    _In_ ULONG SizeOfContext,
    _Outptr_result_maybenull_ PVOID* ConnectionCookie
);

VOID
MnFltDisconnectNotifyCallback(
    _In_opt_ PVOID ConnectionCookie
);

NTSTATUS
MnFltMessageNotifyCallback(
    _In_ PVOID ConnectionCookie,
    _In_reads_bytes_opt_(InputBufferSize) PVOID InputBuffer,
    _In_ ULONG InputBufferSize,
    _Out_writes_bytes_to_opt_(OutputBufferSize, *ReturnOutputBufferLength) PVOID OutputBuffer,
    _In_ ULONG OutputBufferSize,
    _Out_ PULONG ReturnOutputBufferLength
);

NTSTATUS
MnFltPrepareServerPort(
    _In_  PSECURITY_DESCRIPTOR SecurityDescriptor
);

#ifdef ALLOC_PRAGMA
#pragma alloc_text(PAGE, MnFltMessageNotifyCallback)
#pragma alloc_text(PAGE, MnFltConnectNotifyCallback)
#pragma alloc_text(PAGE, MnFltDisconnectNotifyCallback)
#pragma alloc_text(PAGE, MnFltPrepareServerPort)
#endif

NTSTATUS
MnFltConnectNotifyCallback(
    _In_ PFLT_PORT ClientPort,
    _In_ PVOID ServerPortCookie,
    _In_reads_bytes_(SizeOfContext) PVOID ConnectionContext,
    _In_ ULONG SizeOfContext,
    _Outptr_result_maybenull_ PVOID* ConnectionCookie
)
/*++

Routine Description

    Communication connection callback routine.
    This is called when user-mode connects to the server port.

Arguments

    ClientPort - This is the client connection port that will be used to send messages from the filter

    ServerPortCookie - Unused

    ConnectionContext - The connection context passed from the user. This is to recognize which type
            connection the user is trying to connect.

    SizeofContext   - The size of the connection context.

    ConnectionCookie - Propagation of the connection context to disconnection callback.

Return Value

    STATUS_SUCCESS - to accept the connection
    STATUS_INSUFFICIENT_RESOURCES - if memory is not enough
    STATUS_INVALID_PARAMETER_3 - Connection context is not valid.
--*/
{
    PAGED_CODE();

    UNREFERENCED_PARAMETER(ServerPortCookie);
    UNREFERENCED_PARAMETER(ConnectionContext);
    UNREFERENCED_PARAMETER(SizeOfContext);
    ConnectionCookie = NULL;

    FLT_ASSERT(gData.ClientPort == NULL);
    gData.ClientPort = ClientPort;
    return STATUS_SUCCESS;
}

VOID
MnFltDisconnectNotifyCallback(
    _In_opt_ PVOID ConnectionCookie
)
/*++

Routine Description

    Communication disconnection callback routine.
    This is called when user-mode disconnects the server port.

Arguments

    ConnectionCookie - The cookie set in AvConnectNotifyCallback(...). It is connection context.

Return Value

    None
--*/
{
    PAGED_CODE();

    UNREFERENCED_PARAMETER(ConnectionCookie);

    //
    //  Close our handle
    //

    FltCloseClientPort(gData.Filter, &gData.ClientPort);

}

NTSTATUS
MnFltMessageNotifyCallback(
    _In_ PVOID ConnectionCookie,
    _In_reads_bytes_opt_(InputBufferSize) PVOID InputBuffer,
    _In_ ULONG InputBufferSize,
    _Out_writes_bytes_to_opt_(OutputBufferSize, *ReturnOutputBufferLength) PVOID OutputBuffer,
    _In_ ULONG OutputBufferSize,
    _Out_ PULONG ReturnOutputBufferLength
)
/*++

Routine Description:

    This routine is called whenever the user program sends message to
    filter via FilterSendMessage(...).

    The user space scanner sends message to

    1) Create the section for data scan
    2) Close the section for data scan
    3) Set a certain file to be infected
    4) Query the file state of a file

Arguments:

    InputBuffer - A buffer containing input data, can be NULL if there
        is no input data.

    InputBufferSize - The size in bytes of the InputBuffer.

    OutputBuffer - A buffer provided by the application that originated
        the communication in which to store data to be returned to the
        application.

    OutputBufferSize - The size in bytes of the OutputBuffer.

    ReturnOutputBufferSize - The size in bytes of meaningful data
        returned in the OutputBuffer.

Return Value:

    Returns the status of processing the message.

--*/
{
    NTSTATUS status = STATUS_SUCCESS;

    PAGED_CODE();

    UNREFERENCED_PARAMETER(ConnectionCookie);
    UNREFERENCED_PARAMETER(InputBuffer);
    UNREFERENCED_PARAMETER(InputBufferSize);
    UNREFERENCED_PARAMETER(OutputBuffer);
    UNREFERENCED_PARAMETER(OutputBufferSize);
    *ReturnOutputBufferLength = 0;

    return status;

}

NTSTATUS
MnFltPrepareServerPort(
    _In_  PSECURITY_DESCRIPTOR SecurityDescriptor
)
/*++

Routine Description:

    A wrapper function that prepare the communicate port.

Arguments:

    SecurityDescriptor - Specifies a security descriptor to InitializeObjectAttributes(...).

    ConnectionType - The type of connection: AvConnectForScan, AvConnectForAbort, AvConnectForQuery

Return Value:

    Returns the status of the prepartion.

--*/
{
    NTSTATUS status = STATUS_SUCCESS;
    OBJECT_ATTRIBUTES oa;
    UNICODE_STRING uniString;
    LONG maxConnections = 1;
    PCWSTR portName = DRIVER_PORT_NAME;

    RtlInitUnicodeString(&uniString, portName);

    InitializeObjectAttributes(&oa,
        &uniString,
        OBJ_KERNEL_HANDLE | OBJ_CASE_INSENSITIVE,
        NULL,
        SecurityDescriptor);

    status = FltCreateCommunicationPort(gData.Filter,
        &gData.ServerPort,  // this is the output to server port.
        &oa,
        NULL,
        MnFltConnectNotifyCallback,
        MnFltDisconnectNotifyCallback,
        MnFltMessageNotifyCallback,
        maxConnections);

    return status;
}


