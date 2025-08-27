using Booking.Application.Common.DTOs.BookingDTOs;
using Booking.Application.Common.Services;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Previewer;

namespace Booking.Infrastructure.Services
{
    public class PdfGeneratorService : IPdfGeneratorService
    {
        public byte[] GenerateBookingDetailsPdf(BookingDto bookingDto)
        {
            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(2f * 28.3465f);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(12));

                    page.Header()
                        .Text("Here are your booking details!")
                        .SemiBold().FontSize(20).FontColor(Colors.Black);

                    page.Content()
                        .PaddingVertical(1, QuestPDF.Infrastructure.Unit.Centimetre)
                        .Column(column =>
                        {
                            column.Spacing(5);
                            column.Item().Text(text =>
                            {
                                text.Span("Apartment booked: ").SemiBold();
                                text.Span(bookingDto.Apartment.ToString());
                            });

                            column.Item().Text(text =>
                            {
                                text.Span("Booking period: ").SemiBold();
                                text.Span($"{bookingDto.Start:dd/MM/yyyy} - {bookingDto.End:dd/MM/yyyy}");
                            });

                            column.Item().Text(text =>
                            {
                                text.Span("Total price: ").SemiBold();
                                text.Span($"{bookingDto.TotalPrice:C}");
                            });
                        });

                    page.Footer()
                        .AlignCenter()
                        .Text(x =>
                        {
                            x.Span("treat people with kindness :)");
                        });
                });
            }).GeneratePdf();

            return document;
        }
    }
}
