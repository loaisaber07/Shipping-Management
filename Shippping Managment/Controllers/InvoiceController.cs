using Business_Layer.Services;
using Data_Access_Layer.Entity;
using Data_Access_Layer.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PdfSharpCore;
using PdfSharpCore.Pdf;
using TheArtOfDev.HtmlRenderer.PdfSharp;

namespace Shippping_Managment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly InvoiceService invoiceServ;
        private readonly IOrder orderRepo;
        private readonly IProduct productRepo;

        public InvoiceController(InvoiceService invoiceServ ,IOrder orderRepo,IProduct productRepo)
        {
            this.invoiceServ = invoiceServ;
            this.orderRepo = orderRepo;
            this.productRepo = productRepo;
        }
        [HttpGet]
        public async Task<ActionResult> GenerateInvoice(int orderId)
        {

            Order? order =await orderRepo.GetById(orderId);
            if (order == null)
            {
                return NotFound();
            }
          List<Product> productList = await productRepo.getProductsByOrderId(orderId);

            var document = new PdfDocument();
            string HtmlContent = "<div class='body' style='font-family: Arial, sans-serif; margin: 20px'>"
        + "<div class='container' style='max-width: 600px; margin: auto; border: 1px solid #ccc; padding: 20px;'>"
        + "<div class='header' style='text-align: center; margin-bottom: 20px'>"
        + "<img src='https://pioneers-solutions.com/uploads/logo.logo.png' width='214px' height='65px' alt='logo' />"
        + $"<p>Order No:{order.ID} </p>"
        + "<p>Date: 15th September 2024</p>"
        + "</div>"

        + "<div class='section' style='margin-bottom: 20px'>"
        + "<div class='section-title' style='font-weight: bold; border-bottom: 1px solid #ccc; padding-bottom: 5px; margin-bottom: 10px;'>"
        + "Merchant Information"
        + "</div>"
        + "<table class='info-table' style='width: 100%; border-collapse: collapse;'>"
        + "<tr>"
        + "<td style='border: 1px solid #ddd; padding: 8px'>Name:</td>"
        + $"<td style='border: 1px solid #ddd; padding: 8px'>{order.Seller.UserName}</td>"
        + "</tr>"
        + "<tr>"
        + "<td style='border: 1px solid #ddd; padding: 8px'>Store Name:</td>"
        + $"<td style='border: 1px solid #ddd; padding: 8px'>{order.Seller.StoreName}</td>"
        + "</tr>"
        + "<tr>"
        + "<td style='border: 1px solid #ddd; padding: 8px'>Address:</td>"
        + $"<td style='border: 1px solid #ddd; padding: 8px'>{order.Seller.Govern}+,+{order.Seller.City}</td>"
        + "</tr>"
        + "<tr>"
        + "<td style='border: 1px solid #ddd; padding: 8px'>Phone:</td>"
        + $"<td style='border: 1px solid #ddd; padding: 8px'>{order.Seller.PhoneNumber}</td>"
        + "</tr>"
        + "</table>"
        + "</div>"

        + "<div class='section' style='margin-bottom: 20px'>"
        + "<div class='section-title' style='font-weight: bold; border-bottom: 1px solid #ccc; padding-bottom: 5px; margin-bottom: 10px;'>"
        + "Client Information"
        + "</div>"
        + "<table class='info-table' style='width: 100%; border-collapse: collapse;'>"
        + "<tr>"
        + "<td>Name:</td>"
        + "<td>Jane Smith</td>"
        + "</tr>"
        + "<tr>"
        + "<td>Address:</td>"
        + "<td>456 Elm St, Springfield, IL</td>"
        + "</tr>"
        + "<tr>"
        + "<td>Phone:</td>"
        + "<td>(987) 654-3210</td>"
        + "</tr>"
        + "</table>"
        + "</div>"

        + "<div class='section' style='margin-bottom: 20px'>"
        + "<div class='section-title' style='font-weight: bold; border-bottom: 1px solid #ccc; padding-bottom: 5px; margin-bottom: 10px;'>"
        + "Item Description"
        + "</div>"
        + "<table class='item-table numbers' style='width: 100%; border-collapse: collapse; text-align: center;'>"
        + "<thead>"
        + "<tr>"
        + "<th style='border: 1px solid #ddd; padding: 8px; background-color: #f2f2f2;'>Product</th>"
        + "<th style='border: 1px solid #ddd; padding: 8px; background-color: #f2f2f2;'>Quantity</th>"
        + "<th style='border: 1px solid #ddd; padding: 8px; background-color: #f2f2f2;'>Price</th>"
        + "<th style='border: 1px solid #ddd; padding: 8px; background-color: #f2f2f2;'>Weight</th>"
        + "<th style='border: 1px solid #ddd; padding: 8px; background-color: #f2f2f2;'>Total Price</th>"
        + "<th style='border: 1px solid #ddd; padding: 8px; background-color: #f2f2f2;'>Total Weight</th>"
        + "</tr>"
        + "</thead>"
        + "<tbody>"
        + "<tr>"
        + "<td style='border: 1px solid #ddd; padding: 8px'>Widget A</td>"
        + "<td style='border: 1px solid #ddd; padding: 8px'>2</td>"
        + "<td style='border: 1px solid #ddd; padding: 8px'>$10.00</td>"
        + "<td style='border: 1px solid #ddd; padding: 8px'>2 Kg</td>"
        + "<td style='border: 1px solid #ddd; padding: 8px'>$20.00</td>"
        + "<td style='border: 1px solid #ddd; padding: 8px'>4 Kg</td>"
        + "</tr>"
        + "<tr>"
        + "<td style='border: 1px solid #ddd; padding: 8px'>Gadget B</td>"
        + "<td style='border: 1px solid #ddd; padding: 8px'>1</td>"
        + "<td style='border: 1px solid #ddd; padding: 8px'>$15.00</td>"
        + "<td style='border: 1px solid #ddd; padding: 8px'>3 Kg</td>"
        + "<td style='border: 1px solid #ddd; padding: 8px'>$15.00</td>"
        + "<td style='border: 1px solid #ddd; padding: 8px'>3 Kg</td>"
        + "</tr>"
        + "</tbody>"
        + "</table>"
        + "</div>"

        + "<div class='section' style='margin-bottom: 20px'>"
        + "<div class='section-title' style='font-weight: bold; border-bottom: 1px solid #ccc; padding-bottom: 5px; margin-bottom: 10px;'>"
        + "Total Charges"
        + "</div>"
        + "<table class='info-table' style='width: 100%; border-collapse: collapse;'>"
        + "<tr>"
        + "<td>Subtotal:</td>"
        + "<td>$35.00</td>"
        + "</tr>"
        + "<tr>"
        + "<td>Shipping:</td>"
        + "<td>$5.00</td>"
        + "</tr>"
        + "<tr class='total' style='font-weight: bold'>"
        + "<td>Total:</td>"
        + "<td>$40.00</td>"
        + "</tr>"
        + "</table>"
        + "</div>"
        + "</div>"
        + "</div>";


            PdfGenerator.AddPdfPages(document, HtmlContent, PageSize.A4);
            byte[]? response = null;
            using (MemoryStream ms = new MemoryStream())
            {
                document.Save(ms);
                response= ms.ToArray();
            }
            string FileName = "Invoice.pdf";
            return File(response, "application/pdf",FileName);


        }
    }
}
