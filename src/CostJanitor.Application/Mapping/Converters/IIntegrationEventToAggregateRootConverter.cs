using AutoMapper;
using CloudEngineering.CodeOps.Abstractions.Aggregates;
using CloudEngineering.CodeOps.Abstractions.Events;
using CostJanitor.Domain.Aggregates;

namespace CostJanitor.Application.Mapping.Converters
{
    public class IIntegrationEventToAggregateRootConverter : ITypeConverter<IIntegrationEvent, IAggregateRoot>
    {
        public readonly IMapper _mapper;

        public IIntegrationEventToAggregateRootConverter(IMapper mapper)
        {
            _mapper = mapper;
        }

        public IAggregateRoot Convert(IIntegrationEvent source, IAggregateRoot destination, ResolutionContext context)
        {
            return source.Type switch
            {
                "aws_context_account_created" => _mapper.Map<ReportRoot>(source),
                _ => null
            };
        }
    }
}
