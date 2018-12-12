namespace ravi.learn.rmq.common
{
    public class PurchaseOrder
    {
        public decimal AmountToPay { get; set; }
        public string PurchaseOrderNumer { get; set; }
        public string CompanyName { get; set; }
        public int PaymentDayTerms { get; set; }
    }
}
