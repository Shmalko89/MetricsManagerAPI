using AutoMapper;
using MetricsManager.DAL.Models;
using MetricsManager.Response;

namespace MetricsManager.DAL.Handler
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<ManagerCpuMetrics, CpuMetricDto>();
            CreateMap<ManagerDotNetMetrics, DotNetMetricDto>();
            CreateMap<ManagerHddMetrics, HddMetricDto>();
            CreateMap<ManagerNetworkMetrics, NetworkMetricDto>();
            CreateMap<ManagerRamMetrics, RamMetricDto>();
        }
    }
}