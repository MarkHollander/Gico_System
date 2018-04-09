using System;
using System.Threading.Tasks;
using Gico.Common;
using Gico.CQRS.Bus.Interfaces;
using Gico.CQRS.EventStorage.Interfaces;
using Gico.CQRS.Model.Implements;
using Microsoft.Extensions.DependencyInjection;

namespace Gico.CQRS.Service.Implements
{
    public class EventProcessor : MessageProcessor
    {
        private readonly IEventBus _bus;
        public EventProcessor(IEventBus bus)
        {
            _bus = bus;
        }

        public override void Start()
        {
            _bus.CreateConsumerChannel(ProcessMessage);
        }

        public override bool IsCommonProcess => false;

        public async Task<bool> ProcessMessage(Message message)
        {
            var messageProcess = message.Clone();
            await Task.Run(async () =>
            {
                try
                {
                    IEventStorageDao eventStorageDao = this.ServiceProvider.GetService<IEventStorageDao>();
                    long eventId = await eventStorageDao.Add(messageProcess);
                    try
                    {
                        var result = await Handle(messageProcess.Body);
                        await eventStorageDao.ChangsStatus(eventId, Event.StatusEnum.Success, null);
                    }
                    catch (Exception e)
                    {
                        await eventStorageDao.ChangsStatus(eventId, Event.StatusEnum.Fail, e.ToJson());
                    }

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