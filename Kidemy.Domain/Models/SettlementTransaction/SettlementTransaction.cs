using Barnamenevisan.Localizing.Shared;

namespace Kidemy.Domain.Models.SettlementTransaction;

public class SettlementTransaction : AuditBaseEntity<int>
{
    public DateTime TransactionDate { get; set; }

    public decimal Price { get; set; }

    public string CardNumber { get; set; }

    public string AccountNumber { get; set; }

    public string TrackingCode { get; set; }

    public int UserId { get; set; }

    public bool IsDelete { get; set; }
    
}