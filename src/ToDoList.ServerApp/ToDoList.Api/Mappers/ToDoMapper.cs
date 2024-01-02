using AutoMapper;
using ToDoList.Api.Models.Dtos;
using ToDoList.Domain.Entities;

namespace ToDoList.Api.Mappers;

public class ToDoMapper : Profile
{
    public ToDoMapper()
    {
        CreateMap<ToDoEntity, ToDoDto>().ReverseMap();
    }
}