﻿using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace InventoryService.Models;

public class Order
{
    public Guid OrderId { get; set; }
    public string Details { get; set; }
    public decimal Price { get; set; }
    public int UserId { get; set; }
}