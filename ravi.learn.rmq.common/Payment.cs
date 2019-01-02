using System;

namespace ravi.learn.rmq.common
{
    [Serializable]
    public class Payment
    {
        public string CardNumber { get; set; }
        public string Name { get; set; }
        public decimal AmountToPay { get; set; }

        public override string ToString()
        {
            return $"Name: {Name} Card#: {CardNumber} Amount: {AmountToPay:C}";
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode() | CardNumber.GetHashCode() | AmountToPay.GetHashCode();
        }
    }
}
