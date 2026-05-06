using Domain;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Webhooks
{
    public class RegisterWebhookHandler
    {
        private readonly IWebhookRepository _repository;

        public RegisterWebhookHandler(IWebhookRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(WebhookEntity entity)
        {
            await _repository.AddAsync(entity);
        }
    }
}
