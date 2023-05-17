using ForumlaAirline.Api.Entities;
using ForumlaAirline.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace ForumlaAirline.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class BookingsController : ControllerBase
{
   
    private readonly ILogger<BookingsController> _logger;
    private readonly IMessageProducer _messageProducer;

    // In Memory Db     
    public static readonly List<Booking> _bookings = new();
    
    public BookingsController(ILogger<BookingsController> logger,
    IMessageProducer messageProducer)
    {
        _logger = logger;
        _messageProducer = messageProducer;
    }

    [HttpPost]

    public IActionResult CreateBooking(Booking newBooking)
    {
        
          if(!ModelState.IsValid)
            return BadRequest();
        
        // Adding The Booking To The DB
         _bookings.Add(newBooking);

         _messageProducer.SendingMessages<Booking>(newBooking);

        return Ok();



    }
   
   
}
