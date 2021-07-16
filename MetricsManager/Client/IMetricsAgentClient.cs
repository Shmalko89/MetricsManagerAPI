using MetricsManager.Response;
using MetricsManager.Request;

namespace MetricsManager.Client
{
    public interface IMetricsAgentClient
    {
        AllCpuMetricsApiResponse GetAllCpuMetrics(AllCpuMetricsApiRequest request);

        AllDotNetMetricsApiResponse GetAllDotNetMetrics(AllDotNetMetricsApiRequest request);

        AllHddMetricsApiResponse GetAllHddMetrics(AllHddMetricsApiRequest request);

        AllNetworkMetricsApiResponse GetAllNetworkMetrics(AllNetworkMetricsApiRequest request);

        AllRamMetricsApiResponse GetAllRamMetrics(AllRamMetricsApiRequest request);
    }
}
