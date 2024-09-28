using System.Drawing;
using System.Drawing.Text;
using System.Drawing.Imaging;
using Microsoft.AspNetCore.Mvc;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using Data_Access_Layer.Entity;
using Data_Access_Layer.Interfaces;
using QRCoder;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Drawing.Printing;

namespace Shipping_Management.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly IOrder _orderRepo;
        private readonly IProduct _productRepo;

        public InvoiceController(IOrder orderRepo, IProduct productRepo)
        {
            _orderRepo = orderRepo;
            _productRepo = productRepo;
        }

        [HttpGet("GenerateOrderPdfWithQRCode")]
        public async Task<ActionResult> GenerateOrderPdfWithQRCode(int orderId)
        {
            Order? order = await _orderRepo.GetById(orderId);
            if (order == null)
            {
                return NotFound(new { Message = "Order Not Found !!" });
            }

            IEnumerable<Product> products = await _productRepo.getProductsByOrderId(orderId);

           
            var orderDetails = $"Invoice for Order ID: {order.ID}\n" +
                 $"Merchant Name: {order.Seller.UserName}\n" +
                 $"Merchant Store Name: {order.Seller.StoreName}\n" +
                 $"Customer Name: {order.ClientName}\n" +
                 $"Customer address: {order.Govern.Name + "," + order.City.Name + "," + order.VillageOrStreet}\n" +
                 $"Date: {order.DateAdding}\n"
                 + "                         \n";
            foreach (var item in products)
            {
                orderDetails += $"Product Name: {item.Name}\n" +
                    $"Quantity: {item.Quantity}\n" +
                    $"Product Weight: {item.Weight} Kg\n";
            }
            orderDetails += "                         \n" + 
                $"Total  Weight: {order.Weight} Kg\n";
            orderDetails += $"Order Cost: {order.Cost} LE \n" +
              $"Shipping Cost: {order.chargeCost} LE \n" +
                $"Total Cost: {order.Cost + order.chargeCost}  LE ";
           
            var qrCodeBytes = GenerateQRCode(orderDetails);
            XImage qrXImage;
            using (MemoryStream ms = new MemoryStream(qrCodeBytes))
            {
                
                using (Bitmap qrBitmap = new Bitmap(ms))
                {
                    
                    using (MemoryStream imageStream = new MemoryStream())
                    {
                        qrBitmap.Save(imageStream, ImageFormat.Png); 
                        imageStream.Position = 0;  
                        qrXImage = XImage.FromStream(imageStream); 
                    }
                }
            }
            var textImage = ConvertTextToImage(orderDetails, "Arial", 15, Color.Black, Color.White);
            var pdfDocument = new PdfDocument();
            var pdfPage = pdfDocument.AddPage();
            var graphics = XGraphics.FromPdfPage(pdfPage);

               MemoryStream imgStream = new MemoryStream();
            textImage.Save(imgStream, ImageFormat.Png);
                imgStream.Position = 0;
                var xImage = XImage.FromStream(imgStream);

            double verticalMargin = 20; 
            double verticalImageWidth = pdfPage.Width - 2 * 20; 
            double verticalImageHeight = (pdfPage.Height - 3 * verticalMargin) / 2; 

            graphics.DrawImage(xImage, verticalMargin, verticalMargin, verticalImageWidth, verticalImageHeight);
            graphics.DrawImage(qrXImage, (pdfPage.Width/2)-250, pdfPage.Height/2, 100, 100);

            using (MemoryStream stream = new MemoryStream())
            {
                
                pdfDocument.Save(stream, false);
                byte[] pdfBytes = stream.ToArray();
                return File(pdfBytes, "application/pdf", "OrderInvoice.pdf");
            }
        }

        private Bitmap ConvertTextToImage(string text, string fontName, int fontSize, Color textColor, Color backColor)
        {
          
            Font font = new Font(fontName, fontSize);
            SizeF textSize;

           
            using (Bitmap bmp = new Bitmap(1, 1))
            using (Graphics g = Graphics.FromImage(bmp))
            {
                textSize = g.MeasureString(text, font);
            }

           
            Bitmap textImage = new Bitmap((int)textSize.Width + 10, (int)textSize.Height + 10);
            using (Graphics graphics = Graphics.FromImage(textImage))
            {
                graphics.Clear(backColor);

                
                using (Brush textBrush = new SolidBrush(textColor))
                {
                    graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
                    graphics.DrawString(text, font, textBrush, new PointF(5, 5));
                }
            }

            return textImage;
        }

        private byte[] GenerateQRCode(string qrText)
        {
            using (QRCodeGenerator qrGenerator = new QRCodeGenerator())
            {
                QRCodeData qrCodeData = qrGenerator.CreateQrCode(qrText, QRCodeGenerator.ECCLevel.Q);
                using (BitmapByteQRCode qrCode = new BitmapByteQRCode(qrCodeData))
                {
                    return qrCode.GetGraphic(20);
                }
            }
        }
    }
}


