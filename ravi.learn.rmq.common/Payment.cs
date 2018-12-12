using System;

namespace ravi.learn.rmq.common
{
    [Serializable]
    public class Payment
    {
        public string CardNumber { get; set; }
        public string Name { get; set; }
        public decimal AmountToPay { get; set; }
    }
}
