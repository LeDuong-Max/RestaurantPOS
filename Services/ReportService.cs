using BusinessObject;
using DataAccessLayer;
using System;
using System.Collections.Generic;

namespace Services
{
    public class ReportService : IReportService
    {
        public List<Order> GetRevenueReport(DateTime fromDate, DateTime toDate)
        {
            return ReportDAO.GetRevenueReport(fromDate, toDate);
        }

        public List<TopSellingFoodDTO> GetTopSellingFoods(DateTime fromDate, DateTime toDate)
        {
            return ReportDAO.GetTopSellingFoods(fromDate, toDate);
        }
    }
}
