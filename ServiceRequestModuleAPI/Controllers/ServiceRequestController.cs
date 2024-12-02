using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class ServiceRequestController : ControllerBase
{
    private readonly DataContext _context;

    public ServiceRequestController(DataContext context)
    {
        _context = context;
    }

    // GET: api/servicerequest
    [HttpGet]
    public IActionResult GetAll()
    {
        var requests = _context.ServiceRequests.ToList();
        if (!requests.Any())
        {
            return StatusCode(204, "empty content");
        }
        StatusCode(200, "list of service requests");
        return Ok(requests);
    }

    // GET: api/servicerequest/{id}
    [HttpGet("{id}")]
    public IActionResult GetById(Guid id)
    {
        var request = _context.ServiceRequests.Find(id);
        if (request == null)
        {
            return NotFound();
        }
        StatusCode(200, "single service request");
        return Ok(request);
    }

    // POST: api/servicerequest
    [HttpPost]
    public IActionResult Create(ServiceRequest request)
    {
        request.Id = Guid.NewGuid();
        request.CreatedDate = DateTime.UtcNow;
        request.LastModifiedDate = DateTime.UtcNow;

        _context.ServiceRequests.Add(request);
        _context.SaveChanges();

        StatusCode(201, "Creared service request with id " + request.Id);
        return CreatedAtAction(nameof(GetById), new { id = request.Id }, request);
    }

    // PUT: api/servicerequest/{id}
    [HttpPut("{id}")]
    public IActionResult Update(Guid id, ServiceRequest updatedRequest)
    {
        var existingRequest = _context.ServiceRequests.Find(id);
        if (existingRequest == null)
        {
            return NotFound("bad service request");
        }

        existingRequest.BuildingCode = updatedRequest.BuildingCode;
        existingRequest.Description = updatedRequest.Description;
        existingRequest.currentStatus = updatedRequest.currentStatus;
        existingRequest.LastModifiedBy = updatedRequest.LastModifiedBy;
        existingRequest.LastModifiedDate = DateTime.UtcNow;

        _context.SaveChanges();
        StatusCode(200, "updated service request");
        return Ok(existingRequest);
    }

    // DELETE: api/servicerequest/{id}
    [HttpDelete("{id}")]
    public IActionResult Delete(Guid id)
    {
        var request = _context.ServiceRequests.Find(id);
        if (request == null)
        {
            return NotFound("not found");
        }

        _context.ServiceRequests.Remove(request);
        _context.SaveChanges();
        return StatusCode(201, "successful");
    }
}