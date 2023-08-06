// ExampleClient.cpp : This file contains the 'main' function. Program execution begins and ends there.
//

#include <atlbase.h>
#include <iostream>
#include <iomanip>
#include "ComInterface_i.h"
#include "ComInterface_i.c"

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

    hr = ::CoCreateInstance(CLSID_Server, nullptr, CLSCTX_LOCAL_SERVER, __uuidof(IDispatch), (void**)&server);
    if (FAILED(hr))
    {
        std::cout << "CoCreateInstance failure: " << std::hex << std::showbase << hr << std::endl;
        return EXIT_FAILURE;
    }


    DISPID id;
    LPOLESTR string = (LPOLESTR)L"ComputePi";
    if (FAILED(server->GetIDsOfNames(IID_NULL, &string, DISPATCH_METHOD, LOCALE_USER_DEFAULT, &id))) {
        return EXIT_FAILURE;
    }

    CComVariant result;
    DISPPARAMS p = { nullptr, nullptr, 0, 0 };
    VARIANT* args = new VARIANT[1];
    VariantInit(&args[0]);
    args[0].vt = VT_BSTR;
    args[0].bstrVal = SysAllocString(L"TungABC");
    p.rgvarg = args;
    p.cArgs = 1;

    hr = server->Invoke(id, IID_NULL, LOCALE_USER_DEFAULT, DISPATCH_PROPERTYGET | DISPATCH_METHOD, &p, &result, nullptr, nullptr);

    VariantClear(&args[0]);
    delete[] args;


    if (FAILED(hr))
    {
        std::cout << "Failure: " << std::hex << std::showbase << hr << std::endl;
        return EXIT_FAILURE;
    }

    std::cout << u8"\u03C0 = " << std::setprecision(16) << result.dblVal << std::endl;

    ::CoUninitialize();

    return EXIT_SUCCESS;
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
