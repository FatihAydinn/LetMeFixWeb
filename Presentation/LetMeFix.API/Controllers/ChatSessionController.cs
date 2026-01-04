using LetMeFix.Domain.Entities;
using LetMeFix.Persistence.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LetMeFix.Application.DTOs;
using AutoMapper;
using LetMeFix.Application.Interfaces;
using LetMeFix.Domain.Interfaces;

namespace LetMeFix.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatSessionController : ControllerBase
    {
        private readonly IChatSessionService _service;
        private readonly IGenericRepository<ChatSession> _genericRepository;
        private readonly IMapper _mapper;

        public ChatSessionController(IChatSessionService service, IGenericRepository<ChatSession> genericRepository, IMapper mapper)
        {
            _genericRepository = genericRepository;
            _service = service;
            _mapper = mapper;
        }

        [HttpPost("createChatRoom")]
        public async Task<IActionResult> CreateChatRoom([FromBody] ChatSessionDTO dto)
        {
            //MessageContent msgc = new MessageContent();
            //var chatSession = _mapper.Map<ChatSession>(dto);
            //chatSession.Id = Guid.NewGuid().ToString();
            //chatSession.MessageContent = dto.MessageContent;
            //chatSession.MessageContent[0].MessageId = Guid.NewGuid().ToString();
            //chatSession.MessageContent[0].ChatSessionId = chatSession.Id;
            //await _genericRepository.AddAsync(chatSession);
            var chatSession = await _service.CreateChatRoom(dto);
            return Ok(chatSession);
        }

        [HttpGet("getChatbyId")]
        public async Task<IActionResult> GetChatById(string id)
        {
            return Ok(await _genericRepository.GetByIdAsync(id));
        }

        [HttpPut("updateChat")]
        public async Task<IActionResult> UpdateChat([FromBody] ChatSession session)
        {
            await _genericRepository.UpdateAsync(session);
            return Ok(session);
        }

        [HttpDelete("deleteChat")]
        public async Task<IActionResult> DeleteChat(string id)
        {
            await _genericRepository.DeleteAsync(id);
            return Ok("success");
        }

        [HttpPut("pushNewMessage")]
        public async Task<IActionResult> PushNewMessage(MessageContent message)
        {
            message.MessageId = Guid.NewGuid().ToString();
            await _service.PushMessage(message.ChatSessionId, message);
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
