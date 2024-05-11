namespace Domain.Enums
{
    public enum PaymentMethod
    {
        PaywithCard = 1,
        PaywithBank = 2,
        PaywithUSSD = 3,
        PaywithTransfer = 4,
        InternationalPayment = 5
    }

    public enum CampaignStatus
    {
        Active = 1,
        Inactive = 2
    }

    public enum Gender
    {
        Male = 1,
        Female = 2
    }

    public enum DonorType
    {
        Individual = 1,
        RegisteredBody = 2
    }

    public enum GiveNetworkPaymentOption
    {
        PaywithCard = 1,
        InternationalPayment = 2
    }
}
