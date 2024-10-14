using Business_Layer.Services;
using Data_Access_Layer.Interfaces;
using Data_Access_Layer.Repositry;
using Microsoft.AspNetCore.Http.Json;

namespace Shippping_Managment.RepositoyContainer
{
    public static class RepositoryCollection
    { 
        public static IServiceCollection AddRepository(this IServiceCollection Services)
        {
            Services.AddScoped<IGovern, GovernRepository>();
            Services.AddScoped<IBranch, BranchRepository>();
            Services.AddScoped<IFieldJob, FieldJobRepository>();
            Services.AddScoped<IFieldPrivilege, FiledPrivilegeRepository>();
            Services.AddScoped<IPrivilege, PrivilegeRepository>();
            Services.AddScoped<IUser, User>();
            Services.AddScoped<IProduct, ProductRepository>();
            Services.AddScoped<ITypeOfPayment, TypeOfPaymentRepository>();
            Services.AddScoped<ITypeOfCharge, TypeOfChargeRepository>();
            Services.AddScoped<IOrderStatus, OrderStatusRepository>();
            Services.AddScoped<IOrder, OrderRepository>();
            Services.AddScoped<ISpecialCharge, SpecialChargeRepo>();
            Services.AddScoped<ISeller, SellerRepository>();
            Services.AddScoped<IWeight, WeightRepository>();
            Services.AddScoped<ITypeOfReceipt, TypeOfReceiptRepository>();
            Services.AddScoped<ITypeOfOffer, TypeOfOfferRepository>();
            Services.AddScoped<IAgent, AgentRepository>();
            Services.AddScoped<InvoiceService>();
            return Services; 

        }
   
    }
}
