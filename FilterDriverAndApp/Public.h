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

DEFINE_GUID (GUID_DEVINTERFACE_FilterDriverAndApp,
    0xbd5cb75d,0xe8b1,0x42d2,0xa2,0x45,0x83,0x67,0x9d,0xc1,0xc1,0x69);
// {bd5cb75d-e8b1-42d2-a245-83679dc1c169}
