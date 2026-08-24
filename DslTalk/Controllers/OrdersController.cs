using DslTalk.Models;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace DslTalk.Controllers;

public class OrdersController : ODataController
{
    private static readonly List<Order> Orders =
    [
        new(
            1001,
            "Open",
            "East",
            725.50m,
            new DateTime(2026, 8, 20),
            "US",
            "OH",
            true),

        new(
            1002,
            "Open",
            "West",
            1248.00m,
            new DateTime(2026, 8, 21),
            "CA",
            null,
            false),

        new(
            1003,
            "Closed",
            "East",
            1499.99m,
            new DateTime(2026, 8, 21),
            "US",
            "PA",
            true),

        new(
            1004,
            "Open",
            "South",
            125.00m,
            new DateTime(2026, 8, 22),
            "US",
            "GA",
            false),

        new(
            1005,
            "Open",
            "East",
            879.95m,
            new DateTime(2026, 8, 23),
            "DE",
            null,
            true)
    ];

    [EnableQuery(
        AllowedQueryOptions =
            AllowedQueryOptions.Filter)]
    public IQueryable<Order> Get()
    {
        return Orders.AsQueryable();
    }
}
