namespace Aliyun.OpenServices.OpenTableService
{
    using System;

    public static class OtsErrorCode
    {
        public const string AuthorizationFailure = "OTSAuthFailed";
        public const string InternalServerError = "OTSInternalServerError";
        public const string QuotaExhausted = "OTSQuotaExhausted";
        public const string StorageDataOutOfRange = "OTSStorageDataOutOfRange";
        public const string StorageInternalError = "OTSStorageInternalError";
        public const string StorageInvalidPrimaryKey = "OTSStorageInvalidPK";
        public const string StorageObjectAlreadyExist = "OTSStorageObjectAlreadyExist";
        public const string StorageObjectNotExist = "OTSStorageObjectNotExist";
        public const string StorageParameterInvalid = "OTSStorageParameterInvalid";
        public const string StoragePartitionNotReady = "OTSStoragePartitionNotReady";
        public const string StoragePrimaryKeyAlreadyExist = "OTSStoragePrimaryKeyAlreadyExist";
        public const string StoragePrimaryKeyNotExist = "OTSStoragePrimaryKeyNotExist";
        public const string StorageServerBusy = "OTSStorageServerBusy";
        public const string StorageSessionNotExist = "OTSStorageSessionNotExist";
        public const string StorageTimeout = "OTSStorageTimeout";
        public const string StorageTransactionLockKeyFail = "OTSStorageTxnLockKeyFail";
        public const string StorageUnknownError = "OTSStorageUnknownError";
        public const string StorageViewIncompletePrimaryKey = "OTSStorageViewIncompletePK";
        public const string UnmatchedMeta = "OTSMetaNotMatch";
    }
}

