using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DDM.MultiTenancy.HostDashboard.Dto;

namespace DDM.MultiTenancy.HostDashboard
{
    public interface IIncomeStatisticsService
    {
        Task<List<IncomeStastistic>> GetIncomeStatisticsData(DateTime startDate, DateTime endDate,
            ChartDateInterval dateInterval);
    }
}