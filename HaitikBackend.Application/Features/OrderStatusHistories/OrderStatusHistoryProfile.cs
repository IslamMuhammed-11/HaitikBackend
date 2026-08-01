using AutoMapper;
using HaitikBackend.Application.Features.OrderStatusHistories.Queries.Responses;
using HaitikBackend.Domain.Entities;

namespace HaitikBackend.Application.Features.OrderStatusHistories;

public class OrderStatusHistoryProfile : Profile
{
    public OrderStatusHistoryProfile()
    {
        CreateMap<OrderStatusHistory, OrderStatusHistoryResponse>();
    }
}
