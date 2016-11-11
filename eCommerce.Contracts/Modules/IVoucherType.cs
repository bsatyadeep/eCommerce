namespace eCommerce.Contracts.Modules
{
    public interface IVoucherType
    {
        string Description { get; set; }
        string Type { get; set; }
        string VoucherModule { get; set; }
        int VoucherTypeId { get; set; }
    }
}
