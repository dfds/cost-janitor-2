using CloudEngineering.CodeOps.Abstractions.Aggregates;
using CloudEngineering.CodeOps.Abstractions.Commands;
using CloudEngineering.CodeOps.Abstractions.Events;
using CostJanitor.Application.Mapping.Converters;
using CostJanitor.Domain.Aggregates;

namespace CostJanitor.Application.Mapping.Profiles
{
    public sealed class DefaultProfile : AutoMapper.Profile
    {
        public DefaultProfile()
        {
            CreateMap<IIntegrationEvent, IAggregateRoot>()
            .ConvertUsing<IIntegrationEventToAggregateRootConverter>();

            CreateMap<IIntegrationEvent, ReportRoot>()
            .ConvertUsing<AwsContextAccountCreatedEventToReportRootConverter>();

            CreateMap<IAggregateRoot, ICommand<IAggregateRoot>>()
            .ConvertUsing<AggregateRootToCommandConverter>();
        }
    }
}
