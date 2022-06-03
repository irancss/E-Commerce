﻿namespace ECommerce_Website.Models;

public class OrderResponseModel
{
    public string UserName { get; set; }
    public decimal TotalPrice { get; set; }


    #region billing Address

    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? EmailAddress { get; set; }
    public string? AddressLine { get; set; }
    public string? Country { get; set; }
    public string? State { get; set; }
    public string? ZipCode { get; set; }

    #endregion


    #region Payment
    public string CardName { get; set; }
    public string CardNumber { get; set; }
    public string Expiration { get; set; }
    public string CW { get; set; }
    public string PaymentMethod { get; set; }

    #endregion
}