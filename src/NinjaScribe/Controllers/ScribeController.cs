using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using NinjaScribe.DataAccess;
using System;

namespace NinjaScribe.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ScribeController : ControllerBase
    {
        public ScribeController(IAzureMongoRepository repository)
        {
            _repository = repository;
        }

        [HttpPost]
        public IActionResult Post([FromQuery] string version, [FromBody] ScribeRequest request)
        {
            try
            {
                if (string.IsNullOrEmpty(version))
                    return BadRequest("Invalid version");

                if (version != "v1")
                    return BadRequest("Invalid version");

                if (request == null)
                    return BadRequest("Invalid request");

                if (string.IsNullOrEmpty(request.WebsiteId))
                    return Unauthorized();

                var remoteIpAddress = Request?.HttpContext?.Connection?.RemoteIpAddress?.ToString();
                var localIpAddress = Request?.HttpContext?.Connection?.LocalIpAddress?.ToString();
                var userAgent = Request?.Headers["User-Agent"].ToString();
                var location = request.Location;
                var visitTime = DateTimeOffset.Now;

                string collectionName;
                switch(request.WebsiteId)
                {
                    case "704MdV8W46xUuZaf769gU7zwm":
                        collectionName = "LincolnLutheranChoir";
                        break;
                    default:
                        return Unauthorized();
                }

                var visit = new Visit
                {
                    LocalIpAddress = localIpAddress,
                    Location = location,
                    RemoteIpAddress = remoteIpAddress,
                    UserAgent = userAgent,
                    VisitTime = visitTime,
                    VisitTimeString = visitTime.ToString("u")
                };

                _repository.InsertAsync(collectionName, visit);

                return new JsonResult(true);
            }
            catch (Exception e)
            {
                return new JsonResult(false);
            }
        }

        private readonly IAzureMongoRepository _repository;
    }

    public class ScribeRequest
    {
        public string WebsiteId { get; set; }

        public string Location { get; set; }
    }
}
