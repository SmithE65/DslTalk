namespace DslTalk.Models;

public sealed record Order(
    int OrderNumber,
    string Status,
    string Region,
    decimal Total,
    DateTime SubmittedOn,
    string ShippingCountry,
    string? ShippingState,
    bool HasManagerApproval);
