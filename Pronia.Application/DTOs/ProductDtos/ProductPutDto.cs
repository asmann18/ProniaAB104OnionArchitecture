﻿namespace Pronia.Application.DTOs;

public record ProductPutDto
{
    public int Id { get; init; }

    public string Name { get; init; }
    public decimal Price { get; init; }
    public string Description { get; init; }
    public byte Rating { get; init; }
    public string SKU { get; init; }
    public int CategoryId { get; init; }
    public int TagId { get; init; }

}
