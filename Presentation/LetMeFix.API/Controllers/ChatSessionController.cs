using LetMeFix.Domain.Entities;
using LetMeFix.Persistence.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LetMeFix.Application.DTOs;
using AutoMapper;

namespace LetMeFix.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatSessionController : ControllerBase
    {
        private readonly ChatSessionService _service;
        private readonly IMapper _mapper;

        public ChatSessionController(ChatSessionService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpPost("createChatRoom")]
        public async Task<IActionResult> CreateChatRoom([FromBody] ChatSessionDTO dto)
        {
            var chatSession = _mapper.Map<ChatSession>(dto);
            chatSession.Id = Guid.NewGuid().ToString();
            await _service.AddAsync(chatSession);
            return Ok(chatSession);
        }

        [HttpGet("getChatbyId")]
        public async Task<IActionResult> GetChatById(string id)
        {
            return Ok(await _service.GetByChatIdAsync(id));
        }

        //[HttpGet("getChatsByUserId")]
        //public async Task<IActionResult> GetChatsByUserId(string userId)
        //{
        //    try
        //    {
        //        return Ok(await _service.GetByUserId(userId));
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}

        [HttpPut("updateChat")]
        public async Task<IActionResult> UpdateChat([FromBody] ChatSession session)
        {
            await _service.UpdateAsync(session);
            return Ok(session);
        }

        [HttpDelete("deleteChat")]
        public async Task<IActionResult> DeleteChat(string id)
        {
            await _service.DeleteAsync(id);
            return Ok("success");
        }

        [HttpPut("pushNewMessage")]
        public async Task<IActionResult> PushNewMessage(string id, MessageContent message)
        {
            message.MessageId = Guid.NewGuid().ToString();
            await _service.PushMessage(id, message);
            return Ok(message);
        }

        [HttpPut("deleteChat")]
        public async Task<IActionResult> DeleteChat(string chatId, string messageId)
        {
            await _service.DeleteMessage(chatId, messageId);
            return Ok("success");
        }

        [HttpPut("editMessage")]
        public async Task<IActionResult> EditMessage(string editedmsg, string chatid, string messageid)
        {
            await _service.EditMessage(editedmsg, chatid, messageid);
            return Ok(editedmsg);
        }

        [HttpGet("getMessage")]
        public async Task<IActionResult> GetMessageById(string chatId, string messageId)
        {
            var content = await _service.GetMessageById(chatId, messageId);
            return Ok(content);
        }
    }
}
