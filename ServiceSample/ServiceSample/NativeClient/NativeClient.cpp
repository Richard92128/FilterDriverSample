// NativeClient.cpp : This file contains the 'main' function. Program execution begins and ends there.
//

#include <atlbase.h>
#include <iostream>
#include <iomanip>
#include "rpc.h"
#include "rpcndr.h"

#define MIDL_DEFINE_GUID(type,name,l,w1,w2,b1,b2,b3,b4,b5,b6,b7,b8) \
        DEFINE_GUID(name,l,w1,w2,b1,b2,b3,b4,b5,b6,b7,b8)

class DECLSPEC_UUID("7c462b89-9733-4dd6-a939-b9bc090d7651")
    ComServer;
HRESULT IDisPatchCall(CComPtr<IDispatch>& server, LPOLESTR funcName, LPOLESTR arg);

int main()
{
    ::SetConsoleOutputCP(CP_UTF8);

    HRESULT hr;
    hr = ::CoInitializeEx(0, COINITBASE_MULTITHREADED);
    if (FAILED(hr))
    {
        std::cout << "CoInitializeEx failure: " << std::hex << std::showbase << hr << std::endl;
        return EXIT_FAILURE;
    }

    CComPtr<IDispatch> server;

    hr = ::CoCreateInstance(__uuidof(ComServer), nullptr, CLSCTX_LOCAL_SERVER, __uuidof(IDispatch), (void**)&server);
    if (FAILED(hr))
    {
        std::cout << "CoCreateInstance failure: " << std::hex << std::showbase << hr << std::endl;
        return hr;
    }

    IDisPatchCall(server, (LPOLESTR)L"RegisterPath", (LPOLESTR)L"TungAbc");
    IDisPatchCall(server, (LPOLESTR)L"ExportFile", (LPOLESTR)L"TungEfg");
    IDisPatchCall(server, (LPOLESTR)L"UnregisterPath", (LPOLESTR)L"TungHik");

    ::CoUninitialize();

    return hr;
}

HRESULT IDisPatchCall(CComPtr<IDispatch>& server, LPOLESTR funcName, LPOLESTR arg)
{
    DISPID id;
    LPOLESTR string = funcName;
    if (FAILED(server->GetIDsOfNames(IID_NULL, &string, DISPATCH_METHOD, LOCALE_USER_DEFAULT, &id))) {
        return EXIT_FAILURE;
    }

    CComVariant result;
    DISPPARAMS p = { nullptr, nullptr, 0, 0 };
    VARIANT* args = new VARIANT[1];
    VariantInit(&args[0]);
    args[0].vt = VT_BSTR;
    args[0].bstrVal = SysAllocString(arg);
    p.rgvarg = args;
    p.cArgs = 1;

    HRESULT hr = server->Invoke(id, IID_NULL, LOCALE_USER_DEFAULT, DISPATCH_PROPERTYGET | DISPATCH_METHOD, &p, &result, nullptr, nullptr);

    if (FAILED(hr))
    {
        std::cout << "Failure: " << std::hex << std::showbase << hr << std::endl;
        return hr;
    }

    std::cout << u8"\u03C0 = " << std::setprecision(16) << result.dblVal << std::endl;

    VariantClear(&args[0]);
    delete[] args;

    return hr;
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
