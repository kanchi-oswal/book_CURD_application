using Book_app.Data.Services;
using Book_app.Data.ViewModel;
using Book_app.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Book_app.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublishersController : ControllerBase
    {
        private PublishersService _publishersService;
        public PublishersController(PublishersService publisherService)
        {
            this._publishersService = publisherService;
        }
        [HttpPost("add-publisher")]
        public IActionResult AddPublisherDetails([FromBody]PublisherVM publisher)
        {
            try
            {
                var _newPublisher = _publishersService.AddPublisher(publisher);
                return Created(nameof(AddPublisherDetails), _newPublisher);
            }
            catch(PublisherNameException ex)
            {
                return BadRequest($"{ex.Message},Publisher Name : {ex.PublisherName}");
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
           
        }
        [HttpGet("get-publisher-with-book-with-Authors/{id}")]
        public IActionResult GetPublisherDetails(int id)
        {
            var publisherDetail = _publishersService.GetPublisherData(id);
            return Ok(publisherDetail);

        
        }
        [HttpGet("get-publisher-by-id/{id}")]
        public IActionResult GetPublisherById(int id)
        {
            var publisherDetail = _publishersService.GetPublisherById(id);
            if(publisherDetail!=null)
            {
                return Ok(publisherDetail);
            }
            else
            {
                return NotFound();
            }


        }
        [HttpDelete("delete-publisher-by-id/{id}")]
        public IActionResult DeletePublisherById(int id)
        {
            try
            {
                _publishersService.DeletedPublisherById(id);
                return Ok();
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
          
        }
    }
}
