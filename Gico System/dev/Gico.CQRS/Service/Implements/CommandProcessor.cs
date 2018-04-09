using System;
using System.Threading.Tasks;
using Gico.CQRS.Bus.Interfaces;
using Gico.CQRS.EventStorage.Interfaces;
using Gico.CQRS.Model.Implements;
using Microsoft.Extensions.DependencyInjection;

namespace Gico.CQRS.Service.Implements
{
    public class CommandProcessor : MessageProcessor
    {
        private readonly ICommandBus _bus;
        // private readonly IEventStorageDao _eventStorageDao;
        public CommandProcessor(ICommandBus bus)
        {
            _bus = bus;
            // _eventStorageDao = eventStorageDao;
        }

        public override void Start()
        {
            _bus.CreateConsumerChannel(ProcessMessage);
        }

        public override bool IsCommonProcess => true;

        public async Task<bool> ProcessMessage(Message message)
        {
            var messageProcess = message.Clone();
            await Task.Run(async () =>
            {
                try
                {
                    ICommandStorageDao commandStorageDao = this.ServiceProvider.GetService<ICommandStorageDao>();

                    await commandStorageDao.Add(messageProcess);
                    var result = await Handle(messageProcess.Body);
                    if(result==null)
                    {
                        return;
                    }
                    CommandResult commandResult = (CommandResult)result;
                    commandResult.MessageId = messageProcess.MessageId;
                    commandResult.ObjectId = messageProcess.ObjectId;

                    var messageResult = new Message(commandResult)
                    {
                        ResultBrokerName = messageProcess.ResultBrokerName,
                        ResultKey = messageProcess.ResultKey,
                        ResultQueueName = messageProcess.ResultQueueName,
                        ProcessDate = DateTime.Now
                    };
                    if (messageProcess.IsSendResult)
                    {
                        await _bus.SendResult(messageResult);
                    }
                    await commandStorageDao.Add(messageResult);


                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            });

            return await Task.FromResult(true);
        }
    }
}