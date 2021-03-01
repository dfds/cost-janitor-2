using AutoMapper;
using CloudEngineering.CodeOps.Abstractions.Events;
using CostJanitor.Domain.Aggregates;
using System;

namespace CostJanitor.Application.Mapping.Converters
{
    public class AwsContextAccountCreatedEventToReportRootConverter : ITypeConverter<IIntegrationEvent, ReportRoot>
    {
        public readonly IMapper _mapper;

        public AwsContextAccountCreatedEventToReportRootConverter(IMapper mapper)
        {
            _mapper = mapper;
        }

        public ReportRoot Convert(IIntegrationEvent source, ReportRoot destination, ResolutionContext context)
        {
            //TODO: Finish this logic
            return new ReportRoot();
        }
    }
}
