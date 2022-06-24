using AutoMapper;
using BrowserChat.Backend.Core.Data;
using BrowserChat.Entity.DTO;
using Microsoft.AspNetCore.Mvc;

namespace BrowserChat.Backend.Controllers
{
    [Route("/api/chat/[controller]")]
    [ApiController]
    public class RoomsController : ControllerBase
    {
        private readonly IBrowserChatRepository _repo;
        private readonly IMapper _mapper;

        public RoomsController(IBrowserChatRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<RoomReadDTO>> GetAllRooms()
        {
            return Ok(_mapper.Map<IEnumerable<RoomReadDTO>>(_repo.GetAllRooms()));
        }
    }
}
