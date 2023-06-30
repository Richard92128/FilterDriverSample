/*++

Module Name:

    public.h

Abstract:

    This module contains the common declarations shared by driver
    and user applications.

Environment:

    user and kernel

--*/

//
// Define an Interface Guid so that apps can find the device and talk to it.
//

DEFINE_GUID (GUID_DEVINTERFACE_FilterDriverSample,
    0x947feb59,0xa293,0x41c4,0xb3,0xde,0x32,0xea,0x3c,0xec,0xac,0xc0);
// {947feb59-a293-41c4-b3de-32ea3cecacc0}
