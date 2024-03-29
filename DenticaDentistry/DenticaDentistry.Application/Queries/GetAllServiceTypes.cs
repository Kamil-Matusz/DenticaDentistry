﻿using DenticaDentistry.Application.Abstractions;
using DenticaDentistry.Application.DTO;

namespace DenticaDentistry.Application.Queries;

public class GetAllServiceTypes : IQuery<IEnumerable<ServiceTypeDto>>, ICommand
{
    public int PageIndex { get; set; }
    public int PageSize { get; set; }
}