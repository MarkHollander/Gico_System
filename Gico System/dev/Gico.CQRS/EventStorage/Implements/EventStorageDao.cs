using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using Gico.CQRS.EventStorage.Interfaces;
using Gico.CQRS.Model.Implements;

namespace Gico.CQRS.EventStorage.Implements
{
    public class EventStorageDao : StorageBaseDao, IEventStorageDao
    {
        public async Task<long> Add(Message message)
        {
            const string spName = ProcName.Event_Add;
            return await WithConnection(async p =>
             {
                 DynamicParameters parameters = CreateEventBusParameters(message);
                 return await p.ExecuteScalarAsync<long>(spName, parameters, commandType: CommandType.StoredProcedure);
             });
        }

        public async Task ChangsStatus(long stt, Event.StatusEnum status, string exception)
        {
            const string spName = ProcName.Event_ChangeStatus;
            await WithConnection(async p =>
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@STT", stt, DbType.Int64);
                parameters.Add("@Status", (int)status, DbType.Int32);
                parameters.Add("@Exception", exception, DbType.String);
                return await p.ExecuteAsync(spName, parameters, commandType: CommandType.StoredProcedure);
            });
        }

        public async Task Add(Message message, Message messagesResult)
        {
            const string spName = ProcName.Event_Add;
            await WithConnection(async (connection, transaction) =>
            {
                DynamicParameters parameters = CreateEventBusParameters(message);
                await connection.ExecuteScalarAsync<int>(spName, parameters, transaction, commandType: CommandType.StoredProcedure);

                DynamicParameters parametersResult = CreateEventBusParameters(messagesResult);
                await connection.ExecuteScalarAsync<int>(spName, parametersResult, transaction, commandType: CommandType.StoredProcedure);
                return true;
            });
        }

        public async Task Add(IEnumerable<Message> messages)
        {
            const string spName = ProcName.Event_Add;
            await WithConnection(async (connection, transaction) =>
            {
                foreach (var message in messages)
                {
                    DynamicParameters parameters = CreateEventBusParameters(message);
                    await connection.ExecuteScalarAsync<int>(spName, parameters, transaction, commandType: CommandType.StoredProcedure);
                }
                return true;
            });
        }

        private DynamicParameters CreateEventBusParameters(Message message)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@MessageId", message.MessageId, DbType.String);
            if (!string.IsNullOrEmpty(message.BodyDataJsonSerialize))
            {
                parameters.Add("@BodyString", message.BodyDataJsonSerialize, DbType.String);
            }
            else
            {
                parameters.Add("@BodyString", null, DbType.String);
            }
            if (message.BodyDataProtobufSerialize != null)
            {
                parameters.Add("@BodyByte", message.BodyDataProtobufSerialize, DbType.Binary);
            }
            else
            {
                parameters.Add("@BodyByte", null, DbType.Binary);
            }
            parameters.Add("@CorrelationId", message.CorrelationId, DbType.String);
            parameters.Add("@SerializeType", (int)message.SerializeType, DbType.Int32);
            parameters.Add("@BodyType", message.BodyType, DbType.String);
            parameters.Add("@CreatedDate", message.CreatedDate, DbType.DateTime);
            parameters.Add("@MessageType", (int)message.MessageType, DbType.Int32);
            parameters.Add("@ObjectId", message.ObjectId, DbType.String);
            parameters.Add("@Version", message.Version, DbType.Int32);
            return parameters;
        }


    }
}