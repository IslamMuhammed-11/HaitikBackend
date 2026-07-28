using System.Collections.Generic;

namespace HaitikBackend.Application.Features.Drivers.Queries.Responses;

public class DriversPageResponse
{
    public DriversPageResponse(List<DriverDetails> drivers, int pageSize, int pageNumber, int totalCount)
    {
        Drivers = drivers;
        PageSize = pageSize;
        PageNumber = pageNumber;
        TotalCount = totalCount;
    }

    public List<DriverDetails> Drivers { get; set; }

    public int PageSize { get; set; }

    public int PageNumber { get; set; }

    public int TotalCount { get; set; }
}
