namespace eCommerce.Contracts.Modules
{
    public interface IVoucher
    {
        decimal Value { get; set; }
        string VoucherCode { get; set; }
        string VoucherDescription { get; set; }
        int VoucherId { get; set; }
        decimal MinSpend { get; set; }
        int VoucherTypeId { get; set; }
        int AppliesToProductId { get; set; }
        string AssignedTo { get; set; }
        bool multipleUse { get; set; }
    }
}
