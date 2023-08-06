﻿![Vanara](https://raw.githubusercontent.com/dahall/Vanara/master/docs/icons/VanaraHeading.png)
### **Vanara.PInvoke.Cryptography NuGet Package**
[![Version](https://img.shields.io/nuget/v/Vanara.PInvoke.Cryptography?label=NuGet&style=flat-square)](https://github.com/dahall/Vanara/releases)
[![Build status](https://img.shields.io/appveyor/build/dahall/vanara?label=AppVeyor%20build&style=flat-square)](https://ci.appveyor.com/project/dahall/vanara)

PInvoke API (methods, structures and constants) imported from Windows crypt32.dll, bcrypt.dll, ncrypt.dll, tokenbinding.dll, cryptnet.dll, cryptdlg.dll and cryptui.dll.

### **What is Vanara?**

[Vanara](https://github.com/dahall/Vanara) is a community project that contains various .NET assemblies which have P/Invoke functions, interfaces, enums and structures from Windows libraries. Each assembly is associated with one or a few tightly related libraries.

### **Issues?**

First check if it's already fixed by trying the [AppVeyor build](https://ci.appveyor.com/nuget/vanara-prerelease).
If you're still running into problems, file an [issue](https://github.com/dahall/Vanara/issues).

### **Included in Vanara.PInvoke.Cryptography**

Functions | Enumerations | Structures
--- | --- | ---
BCryptAddContextFunction BCryptCloseAlgorithmProvider BCryptConfigureContext BCryptConfigureContextFunction BCryptCreateContext BCryptCreateHash BCryptCreateMultiHash BCryptDecrypt BCryptDeleteContext BCryptDeriveKey BCryptDeriveKeyCapi BCryptDeriveKeyPBKDF2 BCryptDestroyHash BCryptDestroyKey BCryptDestroySecret BCryptDuplicateHash BCryptDuplicateKey BCryptEncrypt BCryptEnumAlgorithms BCryptEnumContextFunctionProviders BCryptEnumContextFunctions BCryptEnumContexts BCryptEnumProviders BCryptEnumRegisteredProviders BCryptExportKey BCryptFinalizeKeyPair BCryptFinishHash BCryptFreeBuffer BCryptGenerateKeyPair BCryptGenerateSymmetricKey BCryptGenRandom BCryptGetFipsAlgorithmMode BCryptGetProperty BCryptHash BCryptHashData BCryptImportKey BCryptImportKeyPair BCryptKeyDerivation BCryptOpenAlgorithmProvider BCryptProcessMultiOperations BCryptQueryContextConfiguration BCryptQueryContextFunctionConfiguration BCryptQueryContextFunctionProperty BCryptQueryProviderRegistration BCryptRegisterConfigChangeNotify BCryptRemoveContextFunction BCryptResolveProviders BCryptSecretAgreement BCryptSetContextFunctionProperty BCryptSetProperty BCryptSignHash BCryptUnregisterConfigChangeNotify BCryptVerifySignature CertAddCertificateContextToStore CertAddCertificateLinkToStore CertAddCRLContextToStore CertAddCRLLinkToStore CertAddCTLContextToStore CertAddCTLLinkToStore CertAddEncodedCertificateToStore CertAddEncodedCertificateToSystemStore CertAddEncodedCRLToStore CertAddEncodedCTLToStore CertAddEnhancedKeyUsageIdentifier CertAddRefServerOcspResponse CertAddRefServerOcspResponseContext CertAddSerializedElementToStore CertAddStoreToCollection CertAlgIdToOID CertCloseServerOcspResponse CertCloseStore CertCompareCertificate CertCompareCertificateName CertCompareIntegerBlob CertComparePublicKeyInfo CertControlStore CertCreateCertificateChainEngine CertCreateCertificateContext CertCreateContext CertCreateCRLContext CertCreateCTLContext CertCreateCTLEntryFromCertificateContextProperties CertCreateSelfSignCertificate CertDeleteCertificateFromStore CertDeleteCRLFromStore CertDeleteCTLFromStore CertDuplicateCertificateChain CertDuplicateCertificateContext CertDuplicateCRLContext CertDuplicateCTLContext CertDuplicateStore CertEnumCertificateContextProperties CertEnumCertificatesInStore CertEnumCRLContextProperties CertEnumCRLsInStore CertEnumCTLContextProperties CertEnumCTLsInStore CertEnumPhysicalStore CertEnumSubjectInSortedCTL CertEnumSystemStore CertEnumSystemStoreLocation CertFindAttribute CertFindCertificateInCRL CertFindCertificateInStore CertFindChainInStore CertFindCRLInStore CertFindCTLInStore CertFindExtension CertFindRDNAttr CertFindSubjectInCTL CertFindSubjectInSortedCTL CertFreeCertificateChain CertFreeCertificateChainEngine CertFreeCertificateChainList CertFreeCertificateContext CertFreeCRLContext CertFreeCTLContext CertFreeServerOcspResponseContext CertGetCertificateChain CertGetCertificateContextProperty CertGetCRLContextProperty CertGetCRLFromStore CertGetCTLContextProperty CertGetEnhancedKeyUsage CertGetIntendedKeyUsage CertGetIssuerCertificateFromStore CertGetNameString CertGetPublicKeyLength CertGetServerOcspResponseContext CertGetStoreProperty CertGetSubjectCertificateFromStore CertGetValidUsages CertIsRDNAttrsInCertificateName CertIsStrongHashToSign CertIsValidCRLForCertificate CertModifyCertificatesToTrust CertNameToStr CertOIDToAlgId CertOpenServerOcspResponse CertOpenStore CertOpenSystemStore CertRDNValueToStr CertRegisterPhysicalStore CertRegisterSystemStore CertRemoveEnhancedKeyUsageIdentifier CertRemoveStoreFromCollection CertResyncCertificateChainEngine CertRetrieveLogoOrBiometricInfo CertSaveStore CertSelectCertificate CertSelectCertificateChains CertSelectionGetSerializedBlob CertSerializeCertificateStoreElement CertSerializeCRLStoreElement CertSerializeCTLStoreElement CertSetCertificateContextPropertiesFromCTLEntry CertSetCertificateContextProperty CertSetCRLContextProperty CertSetCTLContextProperty CertSetEnhancedKeyUsage CertSetStoreProperty CertStrToName CertUnregisterPhysicalStore CertUnregisterSystemStore CertVerifyCertificateChainPolicy CertVerifyCRLRevocation CertVerifyCRLTimeValidity CertVerifyCTLUsage CertVerifyRevocation CertVerifySubjectCertificateContext CertVerifyTimeValidity CertVerifyValidityNesting CertViewProperties CryptAcquireCertificatePrivateKey CryptBinaryToString CryptCreateKeyIdentifierFromCSP CryptDecodeMessage CryptDecodeObject CryptDecodeObjectEx CryptDecryptAndVerifyMessageSignature CryptDecryptMessage CryptEncodeObject CryptEncodeObjectEx CryptEncryptMessage CryptEnumKeyIdentifierProperties CryptEnumOIDFunction CryptEnumOIDInfo CryptExportPKCS8 CryptExportPKCS8Ex CryptExportPublicKeyInfo CryptExportPublicKeyInfoEx CryptExportPublicKeyInfoFromBCryptKeyHandle CryptFindCertificateKeyProvInfo CryptFindLocalizedName CryptFindOIDInfo CryptFormatObject CryptFreeOIDFunctionAddress CryptGetDefaultOIDDllList CryptGetDefaultOIDFunctionAddress CryptGetKeyIdentifierProperty CryptGetMessageCertificates CryptGetMessageSignerCount CryptGetObjectUrl CryptGetOIDFunctionAddress CryptGetOIDFunctionValue CryptGetTimeValidObject CryptHashCertificate CryptHashCertificate2 CryptHashMessage CryptHashPublicKeyInfo CryptHashToBeSigned CryptImportPKCS8 CryptImportPublicKeyInfo CryptImportPublicKeyInfoEx CryptImportPublicKeyInfoEx2 CryptInitOIDFunctionSet CryptInstallDefaultContext CryptInstallOIDFunctionAddress CryptMemAlloc CryptMemFree CryptMemRealloc CryptMsgCalculateEncodedLength CryptMsgClose CryptMsgControl CryptMsgCountersign CryptMsgCountersignEncoded CryptMsgDuplicate CryptMsgEncodeAndSignCTL CryptMsgGetAndVerifySigner CryptMsgGetParam CryptMsgOpenToDecode CryptMsgOpenToEncode CryptMsgSignCTL CryptMsgUpdate CryptMsgVerifyCountersignatureEncoded CryptMsgVerifyCountersignatureEncodedEx CryptProtectData CryptProtectMemory CryptQueryObject CryptRegisterDefaultOIDFunction CryptRegisterOIDFunction CryptRegisterOIDInfo CryptRetrieveObjectByUrl CryptRetrieveTimeStamp CryptSetKeyIdentifierProperty CryptSetOIDFunctionValue CryptSignAndEncodeCertificate CryptSignAndEncryptMessage CryptSignCertificate CryptSignMessage CryptSignMessageWithKey CryptSIPAddProvider CryptSIPCreateIndirectData CryptSIPGetCaps CryptSIPGetSignedDataMsg CryptSIPLoad CryptSIPPutSignedDataMsg CryptSIPRemoveProvider CryptSIPRemoveSignedDataMsg CryptSIPRetrieveSubjectGuid CryptSIPRetrieveSubjectGuidForCatalogFile CryptSIPVerifyIndirectData CryptStringToBinary CryptUIDlgCertMgr CryptUIDlgSelectCertificateFromStore CryptUIDlgViewCertificate CryptUIDlgViewContext CryptUIWizDigitalSign CryptUIWizExport CryptUIWizFreeDigitalSignContext CryptUIWizImport CryptUninstallDefaultContext CryptUnprotectData CryptUnprotectMemory CryptUnregisterDefaultOIDFunction CryptUnregisterOIDFunction CryptUnregisterOIDInfo CryptUpdateProtectedState CryptVerifyCertificateSignature CryptVerifyCertificateSignatureEx CryptVerifyDetachedMessageHash CryptVerifyDetachedMessageSignature CryptVerifyMessageHash CryptVerifyMessageSignature CryptVerifyMessageSignatureWithKey CryptVerifyTimeStampSignature GetFriendlyNameOfCert NCryptCloseProtectionDescriptor NCryptCreateClaim NCryptCreatePersistedKey NCryptCreateProtectionDescriptor NCryptDecrypt NCryptDeleteKey NCryptDeriveKey NCryptEncrypt NCryptEnumAlgorithms NCryptEnumKeys NCryptEnumStorageProviders NCryptExportKey NCryptFinalizeKey NCryptFreeBuffer NCryptFreeObject NCryptGetProperty NCryptGetProtectionDescriptorInfo NCryptImportKey NCryptIsAlgSupported NCryptIsKeyHandle NCryptKeyDerivation NCryptNotifyChangeKey NCryptOpenKey NCryptOpenStorageProvider NCryptProtectSecret NCryptQueryProtectionDescriptorName NCryptRegisterProtectionDescriptorName NCryptSecretAgreement NCryptSetProperty NCryptSignHash NCryptStreamClose NCryptStreamOpenToProtect NCryptStreamOpenToUnprotect NCryptStreamOpenToUnprotectEx NCryptStreamUpdate NCryptTranslateHandle NCryptUnprotectSecret NCryptVerifyClaim NCryptVerifySignature PFXExportCertStore PFXExportCertStoreEx PFXImportCertStore PFXIsPFXBlob PFXVerifyPassword SslComputeClientAuthHash SslComputeEapKeyBlock SslComputeFinishedHash SslCreateClientAuthHash SslCreateEphemeralKey SslCreateHandshakeHash SslDecrementProviderReferenceCount SslDecryptPacket SslEncryptPacket SslEnumCipherSuites SslEnumProtocolProviders SslExportKey SslExportKeyingMaterial SslFreeBuffer SslFreeObject SslGenerateMasterKey SslGenerateSessionKeys SslGetCipherSuitePRFHashAlgorithm SslGetKeyProperty SslGetProviderProperty SslHashHandshake SslImportKey SslImportMasterKey SslIncrementProviderReferenceCount SslLookupCipherLengths SslLookupCipherSuiteInfo SslOpenPrivateKey SslOpenProvider SslSignHash SslVerifySignature TokenBindingDeleteAllBindings TokenBindingDeleteBinding TokenBindingGenerateBinding TokenBindingGenerateID TokenBindingGenerateMessage TokenBindingGetKeyTypesClient TokenBindingGetKeyTypesServer TokenBindingVerifyMessage  | AlgOperations AlgProviderFlags BCRYPT_HASH_OPERATION_TYPE BCRYPT_MULTI_OPERATION_TYPE BlobMagicNumber ContextConfigFlags ContextConfigTable CryptPriority DeriveKeyFlags EncryptFlags GenRandomFlags ImportFlags InterfaceId KeyDerivationFlags PaddingScheme ProviderInfoType ResolveProviderFlags CryptProtectFlags CryptProtectMemoryFlags CryptProtectPrompt MSSIP SPC CertCreateSelfSignFlags CertSelectBy CertSelection CryptRetrievalFlags CertNameFlags CertNameStringFormat CertNameType CertRDNType CryptFormatStr CryptStringFormat ALG_CLASS ALG_ID ALG_TYPE CERT_INFO_CHOICE CertCompareFunction CertEncodingType CertFindType CertInfoFlags CertKeySpec CryptAcquireFlags CryptDefaultContextFlags CryptDefaultContextType PrivateKeyType TimeStampRetrivalFlags CertKeyUsage CertQueryContentFlags CertQueryContentType CertQueryFormatFlags CertQueryFormatType CertQueryObjectType CertRDNAttrsFlag CertRevocationType CertVerifyFlags CRL_REASON CryptFindFlags CryptOIDInfoFlags CryptVerifyCertSignFlags CryptVerifyCertSignIssuer CryptVerifyCertSignSubject BlobType CertFindUsageFlags CryptKeyIdFlags CryptMsgActionFlags CryptMsgControlType CryptMsgFlags CryptMsgKeyOriginator CryptMsgParamType CryptMsgSignerType CryptMsgType CryptMsgVerifyCounterFlags CryptDecodeFlags CryptEncodeFlags CryptInstallOIDFuncFlags OIDFuncFlags OIDGroupId PFXExportFlags PFXImportFlags CertPropId CertStoreVerification CrlFindFlags CrlFindType CertCloseStoreFlags CertCreateContextFlags CertPhysicalStoreFlags CertStoreAdd CertStoreContextFlags CertStoreContextType CertStoreControlFlags CertStoreControlType CertStoreFlags CertStoreSaveAs CertStoreSaveTo CertSystemStore CertSystemStoreId CtlCertSubject CertChainEngineExclusiveFlags CertChainEngineFlags CertChainFlags CertChainPolicyFlags CertChainStrongSignFlags CertCreateCTLEntryFlags CertVerifyCTLFlags CryptMsgEncodeFlags CryptMsgSignerFlags CryptMsgSignFlags CtlVerifyUsageStatusFlags UsageMatchType CertDisplayWell CertModifyCertificatesOp CertSelectFlags ViewPropertiesFlags CryptGetUrlFlags CryptGetUrlFromFlags TimeValidObjectFlags CryptUISelect CryptUIViewCertificateFlags CryptUIWizAddChoice CryptUIWizExportType CryptUIWizFlags CryptUIWizImportType CryptUIWizPVKChoice CryptUIWizSignLoc CryptUIWizSigType CryptUIWizToSign CreatePersistedFlags ExportPolicy FinalizeKeyFlags GetPropertyFlags ImplType KeyDerivationBufferType KeyDerivationFlags KeyUsage NCryptDecryptFlag NCryptUIFlags NotifyFlags OpenKeyFlags SetPropFlags UIPolicy CreateProtectionDescriptorFlags ProtectFlags ProtectionDescriptorInfoType ProtectionDescriptorNameFlags UnprotectSecretFlags PacketContentType SslHost SslProviderCipherSuiteId SslProviderKeyTypeId SslProviderProtocolId TOKENBINDING_EXTENSION_FORMAT TOKENBINDING_KEY_PARAMETERS_TYPE TOKENBINDING_TYPE                                                                                                                                                                                                                          | BCRYPT_ALG_HANDLE BCRYPT_ALGORITHM_IDENTIFIER BCRYPT_HANDLE BCRYPT_HASH_HANDLE BCRYPT_KEY_HANDLE BCRYPT_KEY_LENGTHS_STRUCT BCRYPT_MULTI_HASH_OPERATION BCRYPT_MULTI_OBJECT_LENGTH_STRUCT BCRYPT_OAEP_PADDING_INFO BCRYPT_OID_LIST BCRYPT_PKCS1_PADDING_INFO BCRYPT_PROVIDER_NAME BCRYPT_PSS_PADDING_INFO BCRYPT_SECRET_HANDLE CRYPT_CONTEXT_CONFIG CRYPT_CONTEXT_FUNCTION_CONFIG CRYPT_CONTEXT_FUNCTION_PROVIDERS CRYPT_CONTEXT_FUNCTIONS CRYPT_CONTEXTS CRYPT_PROVIDERS CRYPTPROTECT_PROMPTSTRUCT CRYPTCATMEMBER CRYPTCATSTORE MS_ADDINFO_BLOB MS_ADDINFO_CATALOGMEMBER MS_ADDINFO_FLAT SIP_ADD_NEWPROVIDER SIP_CAP_SET_V2 SIP_CAP_SET_V3 SIP_DISPATCH_INFO SIP_INDIRECT_DATA SIP_SUBJECTINFO CERT_CHAIN_CONTEXT CERT_CHAIN_ELEMENT CERT_REVOCATION_CRL_INFO CERT_REVOCATION_INFO CERT_SELECT_CHAIN_PARA CERT_SELECT_CRITERIA CERT_SIMPLE_CHAIN CERT_TRUST_LIST_INFO HCERT_SERVER_OCSP_RESPONSE HCERTCHAINENGINE PCCERT_SERVER_OCSP_RESPONSE_CONTEXT CERT_CONTEXT CERT_EXTENSION CERT_EXTENSIONS CERT_ID CERT_INFO CERT_ISSUER_SERIAL_NUMBER CERT_KEY_CONTEXT CERT_PUBLIC_KEY_INFO CERT_RDN CERT_RDN_ATTR CERT_STRONG_SIGN_PARA CERT_TRUST_STATUS CRL_CONTEXT CRL_ENTRY CRL_INFO CRYPT_ALGORITHM_IDENTIFIER CRYPT_ATTRIBUTE CRYPT_ATTRIBUTE_TYPE_VALUE CRYPT_BIT_BLOB CRYPT_KEY_PROV_INFO CRYPT_TIMESTAMP_ACCURACY CRYPT_TIMESTAMP_CONTEXT CRYPT_TIMESTAMP_INFO CRYPT_TIMESTAMP_PARA CRYPTOAPI_BLOB CTL_CONTEXT CTL_ENTRY CTL_INFO CTL_USAGE HCRYPTDEFAULTCONTEXT HCRYPTHASH HCRYPTKEY HCRYPTPROV PCCERT_CONTEXT PCCRL_CONTEXT PCCTL_CONTEXT SafeCRYPTOAPI_BLOB CERT_NAME_INFO CERT_REVOCATION_PARA CERT_REVOCATION_STATUS CRYPT_ATTRIBUTES CRYPT_PKCS8_EXPORT_PARAMS CRYPT_PKCS8_IMPORT_PARAMS CRYPT_PRIVATE_KEY_INFO PUBLICKEYSTRUC CMSG_CMS_SIGNER_INFO CMSG_CTRL_ADD_SIGNER_UNAUTH_ATTR_PARA CMSG_CTRL_DECRYPT_PARA CMSG_CTRL_DEL_SIGNER_UNAUTH_ATTR_PARA CMSG_CTRL_KEY_AGREE_DECRYPT_PARA CMSG_CTRL_KEY_TRANS_DECRYPT_PARA CMSG_CTRL_MAIL_LIST_DECRYPT_PARA CMSG_CTRL_VERIFY_SIGNATURE_EX_PARA CMSG_KEY_AGREE_RECIPIENT_INFO CMSG_KEY_TRANS_RECIPIENT_INFO CMSG_MAIL_LIST_RECIPIENT_INFO CMSG_RECIPIENT_ENCRYPTED_KEY_INFO CMSG_SIGNER_ENCODE_INFO CMSG_SIGNER_INFO CMSG_STREAM_INFO CRYPT_DECRYPT_MESSAGE_PARA CRYPT_ENCRYPT_MESSAGE_PARA CRYPT_HASH_MESSAGE_PARA CRYPT_KEY_SIGN_MESSAGE_PARA CRYPT_KEY_VERIFY_MESSAGE_PARA CRYPT_SIGN_MESSAGE_PARA CRYPT_VERIFY_MESSAGE_PARA CRYPT_DECODE_PARA CRYPT_ENCODE_PARA CRYPT_OID_FUNC_ENTRY CRYPT_OID_INFO HCRYPTOIDFUNCADDR HCRYPTOIDFUNCSET PCCRYPT_OID_INFO CERT_CREATE_CONTEXT_PARA CERT_PHYSICAL_STORE_INFO CERT_SYSTEM_STORE_INFO CERT_SYSTEM_STORE_RELOCATE_PARA HCERTSTORE HCRYPTMSG CERT_CHAIN_ENGINE_CONFIG CERT_CHAIN_PARA CERT_CHAIN_POLICY_PARA CERT_CHAIN_POLICY_STATUS CERT_USAGE_MATCH CMSG_SIGNED_ENCODE_INFO CTL_VERIFY_USAGE_PARA CTL_VERIFY_USAGE_STATUS PCCERT_CHAIN_CONTEXT CERT_SELECT_STRUCT CERT_VIEWPROPERTIES_STRUCT CTL_MODIFY_REQUEST CERT_REVOCATION_CHAIN_PARA CRYPT_CREDENTIALS CRYPT_GET_TIME_VALID_OBJECT_EXTRA_INFO CRYPT_RETRIEVE_AUX_INFO CRYPT_URL_ARRAY CRYPT_URL_INFO CERT_SELECTUI_INPUT CRYPTUI_CERT_MGR_STRUCT CRYPTUI_INITDIALOG_STRUCT CRYPTUI_VIEWCERTIFICATE_STRUCT CRYPTUI_WIZ_DIGITAL_SIGN_BLOB_INFO CRYPTUI_WIZ_DIGITAL_SIGN_CERT_PVK_INFO CRYPTUI_WIZ_DIGITAL_SIGN_CONTEXT CRYPTUI_WIZ_DIGITAL_SIGN_EXTENDED_INFO CRYPTUI_WIZ_DIGITAL_SIGN_INFO CRYPTUI_WIZ_DIGITAL_SIGN_PVK_FILE_INFO CRYPTUI_WIZ_DIGITAL_SIGN_STORE_INFO CRYPTUI_WIZ_EXPORT_CERTCONTEXT_INFO CRYPTUI_WIZ_EXPORT_INFO CRYPTUI_WIZ_IMPORT_SRC_INFO PCCRYPTUI_WIZ_DIGITAL_SIGN_CONTEXT NCRYPT_ALLOC_PARA NCRYPT_HASH_HANDLE NCRYPT_HANDLE NCRYPT_KEY_HANDLE NCRYPT_PROV_HANDLE NCRYPT_SECRET_HANDLE NCryptAlgorithmName NCryptBuffer NCryptBufferDesc NCRYPT_DESCRIPTOR_HANDLE NCRYPT_PROTECT_STREAM_INFO NCRYPT_PROTECT_STREAM_INFO_EX NCRYPT_STREAM_HANDLE NCRYPT_SSL_CIPHER_LENGTHS NCRYPT_SSL_CIPHER_SUITE TOKENBINDING_IDENTIFIER TOKENBINDING_KEY_TYPES TOKENBINDING_RESULT_DATA TOKENBINDING_RESULT_LIST CMSG_CTRL_DECRYPT_PARA_HANDLES CMSG_CTRL_KEY_AGREE_DECRYPT_PARA_HANDLES CMSG_CTRL_KEY_TRANS_DECRYPT_PARA_HANDLES CMSG_KEY_AGREE_RECIPIENT_INFO_UNION CMSG_SIGNER_ENCODE_INFO_HANDLES CRYPT_KEY_SIGN_MESSAGE_PARA_HANDLE CRYPT_OID_INFO_UNION CRYPTUI_WIZ_DIGITAL_SIGN_INFO_UNION CRYPTUI_WIZ_EXPORT_INFO_UNION CRYPTUI_WIZ_IMPORT_SRC_INFO_UNION                                                                                                                                                                                         