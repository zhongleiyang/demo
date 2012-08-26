namespace Aliyun.OpenServices.OpenTableService
{
    using Aliyun.OpenServices;
    using Aliyun.OpenServices.Common.Communication;
    using Aliyun.OpenServices.OpenTableService.Commands;
    using Aliyun.OpenServices.OpenTableService.Model;
    using Aliyun.OpenServices.OpenTableService.Utilities;
    using Aliyun.OpenServices.Properties;
    using System;
    using System.Collections.Generic;

    public class OtsClient
    {
        private readonly ClientConfiguration _configuration;
        private readonly ServiceCredentials _credentials;
        private readonly Uri _endpoint;

        public OtsClient(string accessId, string accessKey) : this("http://service.ots.aliyun.com", accessId, accessKey)
        {
        }

        public OtsClient(string endpoint, string accessId, string accessKey) : this(endpoint, accessId, accessKey, null)
        {
        }

        public OtsClient(string endpoint, string accessId, string accessKey, ClientConfiguration configuration)
        {
            if (string.IsNullOrEmpty(accessId))
            {
                throw new ArgumentException(Resources.ExceptionIfArgumentStringIsNullOrEmpty, "accessId");
            }
            if (string.IsNullOrEmpty(accessKey))
            {
                throw new ArgumentException(Resources.ExceptionIfArgumentStringIsNullOrEmpty, "accessKey");
            }
            if (string.IsNullOrEmpty(endpoint))
            {
                throw new ArgumentException(Resources.ExceptionIfArgumentStringIsNullOrEmpty, "endpoint");
            }
            if (!endpoint.StartsWith("http://", StringComparison.OrdinalIgnoreCase))
            {
                throw new ArgumentException(OtsExceptions.EndpointNotSupportedProtocal, "endpoint");
            }
            this._endpoint = new Uri(endpoint);
            this._credentials = new ServiceCredentials(accessId, accessKey);
            this._configuration = (configuration != null) ? ((ClientConfiguration) configuration.Clone()) : new ClientConfiguration();
        }

        public void AbortTransaction(string transactionId)
        {
            AbortTransactionCommand.Create(this.GetServiceClient(), this._endpoint, this.CreateContext("AbortTransaction"), transactionId).Execute();
        }

        public void BatchModifyData(string tableName, IEnumerable<RowChange> rowChanges, string transactionId)
        {
            BatchModifyDataCommand.Create(this.GetServiceClient(), this._endpoint, this.CreateContext("BatchModifyData"), tableName, rowChanges, transactionId).Execute();
        }

        public IAsyncResult BeginAbortTransaction(string transactionId, AsyncCallback callback, object state)
        {
            return AbortTransactionCommand.Create(this.GetServiceClient(), this._endpoint, this.CreateContext("AbortTransaction"), transactionId).BeginExecute(callback, state);
        }

        public IAsyncResult BeginBatchModifyData(string tableName, IEnumerable<RowChange> rowChanges, string transactionId, AsyncCallback callback, object state)
        {
            return BatchModifyDataCommand.Create(this.GetServiceClient(), this._endpoint, this.CreateContext("BatchModifyData"), tableName, rowChanges, transactionId).BeginExecute(callback, state);
        }

        public IAsyncResult BeginCommitTransaction(string transactionId, AsyncCallback callback, object state)
        {
            return CommitTransactionCommand.Create(this.GetServiceClient(), this._endpoint, this.CreateContext("CommitTransaction"), transactionId).BeginExecute(callback, state);
        }

        public IAsyncResult BeginCreateTable(TableMeta tableMeta, AsyncCallback callback, object state)
        {
            return CreateTableCommand.Create(this.GetServiceClient(), this._endpoint, this.CreateContext("CreateTable"), tableMeta).BeginExecute(callback, state);
        }

        public IAsyncResult BeginCreateTableGroup(string tableGroupName, PartitionKeyType partitionKeyType, AsyncCallback callback, object state)
        {
            return CreateTableGroupCommand.Create(this.GetServiceClient(), this._endpoint, this.CreateContext("CreateTableGroup"), tableGroupName, partitionKeyType).BeginExecute(callback, state);
        }

        public IAsyncResult BeginDeleteData(string tableName, RowDeleteChange rowChange, AsyncCallback callback, object state)
        {
            return this.BeginDeleteData(tableName, rowChange, null, callback, state);
        }

        public IAsyncResult BeginDeleteData(string tableName, RowDeleteChange rowChange, string transactionId, AsyncCallback callback, object state)
        {
            return DeleteDataCommand.Create(this.GetServiceClient(), this._endpoint, this.CreateContext("DeleteData"), tableName, rowChange, transactionId).BeginExecute(callback, state);
        }

        public IAsyncResult BeginDeleteTable(string tableName, AsyncCallback callback, object state)
        {
            return DeleteTableCommand.Create(this.GetServiceClient(), this._endpoint, this.CreateContext("DeleteTable"), tableName).BeginExecute(callback, state);
        }

        public IAsyncResult BeginDeleteTableGroup(string tableGroupName, AsyncCallback callback, object state)
        {
            return DeleteTableGroupCommand.Create(this.GetServiceClient(), this._endpoint, this.CreateContext("DeleteTableGroup"), tableGroupName).BeginExecute(callback, state);
        }

        public IAsyncResult BeginGetRow(SingleRowQueryCriteria criteria, AsyncCallback callback, object state)
        {
            return this.BeginGetRow(criteria, null, callback, state);
        }

        public IAsyncResult BeginGetRow(SingleRowQueryCriteria criteria, string transactionId, AsyncCallback callback, object state)
        {
            return GetRowCommand.Create(this.GetServiceClient(), this._endpoint, this.CreateContext("GetRow"), criteria, transactionId).BeginExecute(callback, state);
        }

        public IAsyncResult BeginGetRowsByOffset(OffsetRowQueryCriteria criteria, AsyncCallback callback, object state)
        {
            return this.BeginGetRowsByOffset(criteria, null, callback, state);
        }

        public IAsyncResult BeginGetRowsByOffset(OffsetRowQueryCriteria criteria, string transactionId, AsyncCallback callback, object state)
        {
            return GetRowsByOffsetCommand.Create(this.GetServiceClient(), this._endpoint, this.CreateContext("GetRowsByOffset"), criteria, transactionId).BeginExecute(callback, state);
        }

        public IAsyncResult BeginGetRowsByRange(RangeRowQueryCriteria criteria, AsyncCallback callback, object state)
        {
            return this.BeginGetRowsByRange(criteria, null, callback, state);
        }

        public IAsyncResult BeginGetRowsByRange(RangeRowQueryCriteria criteria, string transactionId, AsyncCallback callback, object state)
        {
            return GetRowsByRangeCommand.Create(this.GetServiceClient(), this._endpoint, this.CreateContext("GetRowsByRange"), criteria, transactionId).BeginExecute(callback, state);
        }

        public IAsyncResult BeginGetTableMeta(string tableName, AsyncCallback callback, object state)
        {
            if (string.IsNullOrEmpty(tableName) || !OtsUtility.IsEntityNameValid(tableName))
            {
                throw new ArgumentException(OtsExceptions.NameFormatIsInvalid, "tableName");
            }
            return GetTableMetaCommand.Create(this.GetServiceClient(), this._endpoint, this.CreateContext("GetTableMeta"), tableName).BeginExecute(callback, state);
        }

        public IAsyncResult BeginListTableGroups(AsyncCallback callback, object state)
        {
            return ListTableGroupCommand.Create(this.GetServiceClient(), this._endpoint, this.CreateContext("ListTableGroup")).BeginExecute(callback, state);
        }

        public IAsyncResult BeginListTables(AsyncCallback callback, object state)
        {
            return ListTableCommand.Create(this.GetServiceClient(), this._endpoint, this.CreateContext("ListTable")).BeginExecute(callback, state);
        }

        public IAsyncResult BeginPutData(string tableName, RowPutChange rowChange, AsyncCallback callback, object state)
        {
            return this.BeginPutData(tableName, rowChange, null, callback, state);
        }

        public IAsyncResult BeginPutData(string tableName, RowPutChange rowChange, string transactionId, AsyncCallback callback, object state)
        {
            return PutDataCommand.Create(this.GetServiceClient(), this._endpoint, this.CreateContext("PutData"), tableName, rowChange, transactionId).BeginExecute(callback, state);
        }

        public IAsyncResult BeginStartTransaction(string entityName, PartitionKeyValue partitionKeyValue, AsyncCallback callback, object state)
        {
            return StartTransactionCommand.Create(this.GetServiceClient(), this._endpoint, this.CreateContext("StartTransaction"), entityName, partitionKeyValue).BeginExecute(callback, state);
        }

        public void CommitTransaction(string transactionId)
        {
            CommitTransactionCommand.Create(this.GetServiceClient(), this._endpoint, this.CreateContext("CommitTransaction"), transactionId).Execute();
        }

        private ExecutionContext CreateContext(string action)
        {
            ExecutionContext context = new ExecutionContext {
                Signer = new OtsRequestSigner(),
                Credentials = this._credentials
            };
            context.ResponseHandlers.Add(new OtsExceptionHandler());
            context.ResponseHandlers.Add(new ValidationResponseHandler(this._credentials, action));
            return context;
        }

        public void CreateTable(TableMeta tableMeta)
        {
            CreateTableCommand.Create(this.GetServiceClient(), this._endpoint, this.CreateContext("CreateTable"), tableMeta).Execute();
        }

        public void CreateTableGroup(string tableGroupName, PartitionKeyType partitionKeyType)
        {
            CreateTableGroupCommand.Create(this.GetServiceClient(), this._endpoint, this.CreateContext("CreateTableGroup"), tableGroupName, partitionKeyType).Execute();
        }

        public void DeleteData(string tableName, RowDeleteChange rowChange)
        {
            this.DeleteData(tableName, rowChange, null);
        }

        public void DeleteData(string tableName, RowDeleteChange rowChange, string transactionId)
        {
            DeleteDataCommand.Create(this.GetServiceClient(), this._endpoint, this.CreateContext("DeleteData"), tableName, rowChange, transactionId).Execute();
        }

        public void DeleteTable(string tableName)
        {
            DeleteTableCommand.Create(this.GetServiceClient(), this._endpoint, this.CreateContext("DeleteTable"), tableName).Execute();
        }

        public void DeleteTableGroup(string tableGroupName)
        {
            DeleteTableGroupCommand.Create(this.GetServiceClient(), this._endpoint, this.CreateContext("DeleteTableGroup"), tableGroupName).Execute();
        }

        public void EndAbortTransaction(IAsyncResult asyncResult)
        {
            OtsCommand.EndExecute(this.GetServiceClient(), asyncResult);
        }

        public void EndBatchModifyData(IAsyncResult asyncResult)
        {
            OtsCommand.EndExecute(this.GetServiceClient(), asyncResult);
        }

        public void EndCommitTransaction(IAsyncResult asyncResult)
        {
            OtsCommand.EndExecute(this.GetServiceClient(), asyncResult);
        }

        public void EndCreateTable(IAsyncResult asyncResult)
        {
            if (asyncResult == null)
            {
                throw new ArgumentNullException("asyncResult");
            }
            OtsCommand.EndExecute(this.GetServiceClient(), asyncResult);
        }

        public void EndCreateTableGroup(IAsyncResult asyncResult)
        {
            if (asyncResult == null)
            {
                throw new ArgumentNullException("asyncResult");
            }
            OtsCommand.EndExecute(this.GetServiceClient(), asyncResult);
        }

        public void EndDeleteData(IAsyncResult asyncResult)
        {
            OtsCommand.EndExecute(this.GetServiceClient(), asyncResult);
        }

        public void EndDeleteTable(IAsyncResult asyncResult)
        {
            OtsCommand.EndExecute(this.GetServiceClient(), asyncResult);
        }

        public void EndDeleteTableGroup(IAsyncResult asyncResult)
        {
            OtsCommand.EndExecute(this.GetServiceClient(), asyncResult);
        }

        public Row EndGetRow(IAsyncResult asyncResult)
        {
            return OtsCommand<GetRowResult>.EndExecute(this.GetServiceClient(), asyncResult).GetSingleRow();
        }

        public IEnumerable<Row> EndGetRowsByOffset(IAsyncResult asyncResult)
        {
            return OtsCommand<GetRowsByOffsetResult>.EndExecute(this.GetServiceClient(), asyncResult).GetMultipleRows();
        }

        public IEnumerable<Row> EndGetRowsByRange(IAsyncResult asyncResult)
        {
            return OtsCommand<GetRowsByRangeResult>.EndExecute(this.GetServiceClient(), asyncResult).GetMultipleRows();
        }

        public TableMeta EndGetTableMeta(IAsyncResult asyncResult)
        {
            return OtsCommand<GetTableMetaResult>.EndExecute(this.GetServiceClient(), asyncResult).TableMeta.ToTableMeta();
        }

        public IEnumerable<string> EndListTableGroups(IAsyncResult asyncResult)
        {
            return OtsCommand<ListTableGroupResult>.EndExecute(this.GetServiceClient(), asyncResult).TableGroupNames;
        }

        public IEnumerable<string> EndListTables(IAsyncResult asyncResult)
        {
            return OtsCommand<ListTableResult>.EndExecute(this.GetServiceClient(), asyncResult).TableNames;
        }

        public void EndPutData(IAsyncResult asyncResult)
        {
            OtsCommand.EndExecute(this.GetServiceClient(), asyncResult);
        }

        public string EndStartTransaction(IAsyncResult asyncResult)
        {
            return OtsCommand<StartTransactionResult>.EndExecute(this.GetServiceClient(), asyncResult).TransactionId;
        }

        public Row GetRow(SingleRowQueryCriteria criteria)
        {
            return this.GetRow(criteria, null);
        }

        public Row GetRow(SingleRowQueryCriteria criteria, string transactionId)
        {
            return GetRowCommand.Create(this.GetServiceClient(), this._endpoint, this.CreateContext("GetRow"), criteria, transactionId).Execute().GetSingleRow();
        }

        public IEnumerable<Row> GetRowsByOffset(OffsetRowQueryCriteria criteria)
        {
            return this.GetRowsByOffset(criteria, null);
        }

        public IEnumerable<Row> GetRowsByOffset(OffsetRowQueryCriteria criteria, string transactionId)
        {
            return GetRowsByOffsetCommand.Create(this.GetServiceClient(), this._endpoint, this.CreateContext("GetRowsByOffset"), criteria, transactionId).Execute().GetMultipleRows();
        }

        public IEnumerable<Row> GetRowsByRange(RangeRowQueryCriteria criteria)
        {
            return this.GetRowsByRange(criteria, null);
        }

        public IEnumerable<Row> GetRowsByRange(RangeRowQueryCriteria criteria, string transactionId)
        {
            return GetRowsByRangeCommand.Create(this.GetServiceClient(), this._endpoint, this.CreateContext("GetRowsByRange"), criteria, transactionId).Execute().GetMultipleRows();
        }

        private IServiceClient GetServiceClient()
        {
            return ServiceClientFactory.CreateServiceClient(this._configuration);
        }

        public TableMeta GetTableMeta(string tableName)
        {
            return GetTableMetaCommand.Create(this.GetServiceClient(), this._endpoint, this.CreateContext("GetTableMeta"), tableName).Execute().TableMeta.ToTableMeta();
        }

        public IEnumerable<string> ListTableGroups()
        {
            return ListTableGroupCommand.Create(this.GetServiceClient(), this._endpoint, this.CreateContext("ListTableGroup")).Execute().TableGroupNames;
        }

        public IEnumerable<string> ListTables()
        {
            return ListTableCommand.Create(this.GetServiceClient(), this._endpoint, this.CreateContext("ListTable")).Execute().TableNames;
        }

        public void PutData(string tableName, RowPutChange rowChange)
        {
            this.PutData(tableName, rowChange, null);
        }

        public void PutData(string tableName, RowPutChange rowChange, string transactionId)
        {
            PutDataCommand.Create(this.GetServiceClient(), this._endpoint, this.CreateContext("PutData"), tableName, rowChange, transactionId).Execute();
        }

        public string StartTransaction(string entityName, PartitionKeyValue partitionKeyValue)
        {
            return StartTransactionCommand.Create(this.GetServiceClient(), this._endpoint, this.CreateContext("StartTransaction"), entityName, partitionKeyValue).Execute().TransactionId;
        }

        public string AccessId
        {
            get
            {
                return this._credentials.AccessId;
            }
        }

        [Obsolete("请使用OtsClient的构造函数传入ClientConfiguration的对象，而不要使用该属性进行配置。")]
        public ClientConfiguration Configuration
        {
            get
            {
                return this._configuration;
            }
        }

        public Uri Endpoint
        {
            get
            {
                return this._endpoint;
            }
        }
    }
}

