using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using NuGet.DependencyResolver;
using PrimaryConnect.Data;
using PrimaryConnect.Dto;
using PrimaryConnect.Models;
using PrimaryConnect.notificationHub;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class NotificationsController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IHubContext<NotificationHub> _hubContext;

    public NotificationsController(AppDbContext context, IHubContext<NotificationHub> hubContext)
    {
        _context = context;
        _hubContext = hubContext;
    }
    [HttpPost("SendToTeacher")]
    public async Task<IActionResult> SendToSpecificTeacher([FromBody] SendToUserDto dto)
    {
        var teacher = await _context.Teachers.FindAsync(dto.UserId);
        if (teacher == null) return NotFound("Teacher not found");

        var notification = new NotificationRequest
        {
            UserId = dto.UserId,
            UserType = UserType.Teacher,
            Message = dto.Message,
            Date = DateTime.UtcNow,
            IsRead = false
        };

        _context.Notifications.Add(notification);
        await _context.SaveChangesAsync();

        await _hubContext.Clients.Group($"Teacher_{dto.UserId}").SendAsync("ReceiveNotification", notification);
        return Ok();
    }

    [HttpPost("SendToAllTeachers")]
    public async Task<IActionResult> SendToAllTeachers([FromBody] SendToAllDto dto)
    {
        var teachers = await _context.Teachers.ToListAsync();

        foreach (var teacher in teachers)
        {
            var notification = new NotificationRequest
            {
                UserId = teacher.Id,
                UserType = UserType.Teacher,
                Message = dto.Message,
                Date = DateTime.UtcNow,
                IsRead = false
            };

            _context.Notifications.Add(notification);
        }

        await _context.SaveChangesAsync();

        await _hubContext.Clients.Group("Teachers").SendAsync("ReceiveNotification", new { Message = dto.Message });
        return Ok();
    }
    [HttpPost("sendtoLevel")]
    public async Task<IActionResult> Sendtolevl(  int levelId, [FromBody] SendToAllDto dto )
    {
        var students = _context.Students.Where(s=> s.Degree == levelId).ToList();
        foreach(var student in students)
        {
            var notification = new NotificationRequest
            {
                UserId = student.ParentId,
                UserType = UserType.Parent,
                Message = dto.Message,
                Date = DateTime.UtcNow,
                IsRead = false
            };
            _context.Notifications.Add(notification);
        }
        await _context.SaveChangesAsync();
        await _hubContext.Clients.Group("Parents").SendAsync("ReceiveNotification", new { Message = dto.Message });

        return Ok();
    }
    [HttpPost("SendtoClass")]
  public async Task<IActionResult> SendtoClass(  string classname, [FromBody] SendToAllDto dto )
 {
     Class? _class =await _context.Classes.FirstOrDefaultAsync(c => c.name == classname);
     var students = _context.Students.Where(s=> s.ClassId == _class.id).ToList();
     foreach(var student in students)
     {
         var notification = new NotificationRequest
         {
             UserId = student.ParentId,
             UserType = UserType.Parent,
             Message = dto.Message,
             Date = DateTime.UtcNow,
             IsRead = false
         };
         _context.Notifications.Add(notification);
     }
     await _context.SaveChangesAsync();
     await _hubContext.Clients.Group("Parents").SendAsync("ReceiveNotification", new { Message = dto.Message });

     return Ok();
 }
    [HttpPost("SendToParent")]
    public async Task<IActionResult> SendToSpecificParent([FromBody] SendToUserDto dto)
    {
        var parent = await _context.Parents.FindAsync(dto.UserId);
        if (parent == null) return NotFound("Parent not found");

        var notification = new NotificationRequest
        {
            UserId = dto.UserId,
            UserType = UserType.Parent,
            Message = dto.Message,
            Date = DateTime.UtcNow,
            IsRead = false
        };

        _context.Notifications.Add(notification);
        await _context.SaveChangesAsync();

        await _hubContext.Clients.Group($"Parent_{dto.UserId}").SendAsync("ReceiveNotification", notification);
        return Ok();
    }
    [HttpPost("SendToAllParents")]
    public async Task<IActionResult> SendToAllParents([FromBody] SendToAllDto dto)
    {
        var parents = await _context.Parents.ToListAsync();

        foreach (var parent in parents)
        {
            var notification = new NotificationRequest
            {
                UserId = parent.Id,
                UserType = UserType.Parent,
                Message = dto.Message,
                Title=dto.Title ,
                Date = DateTime.UtcNow,
                IsRead = false
            };

            _context.Notifications.Add(notification);
        }

        await _context.SaveChangesAsync();

        await _hubContext.Clients.Group("Parents").SendAsync("ReceiveNotification", new { Message = dto.Message });
        return Ok();
    }
    [HttpGet("getall")]
    public async Task<IActionResult> getall()
    {
    return Ok( await _context.notifications.ToListAsync());

    }
    [HttpGet("GetByTeacherId")]
    public  IActionResult GetByTeacherId(int teacherId)
    {
        IEnumerable<NotificationRequest> notificationRequests=  _context.Notifications.Where(n=>(n.UserType==UserType.Teacher)&&(n.UserId==teacherId));
        return Ok( notificationRequests);
    }
    [HttpGet("GetByParentId")]
    public  IActionResult GetByParentId(int ParentId)
    {
        IEnumerable<NotificationRequest> notificationRequests=  _context.Notifications.Where(n=>(n.UserType==UserType.Parent)

        &&(n.UserId==ParentId));
        return Ok( notificationRequests);
    }


}