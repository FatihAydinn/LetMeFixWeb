using LetMeFix.Domain.Entities;
using LetMeFix.Persistence.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LetMeFix.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatSessionController : ControllerBase
    {
        private readonly ChatSessionService _service;

        public ChatSessionController(ChatSessionService service)
        {
            _service = service;
        }

        [HttpPost("createChatRoom")]
        public async Task<IActionResult> CreateChatRoom([FromBody] ChatSession chatSession)
        {
            try
            {
                chatSession.Id = Guid.NewGuid().ToString();
                chatSession.MessageContent ??= new List<MessageContent>();
                await _service.AddAsync(chatSession);
                return Ok(chatSession);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("getChatbyId")]
        public async Task<IActionResult> GetChatById(string id)
        {
            try
            {
                return Ok(await _service.GetByChatIdAsync(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
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
            try
            {
                await _service.UpdateAsync(session);
                return Ok(session);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("deleteChat")]
        public async Task<IActionResult> DeleteChat(string id)
        {
            try
            {
                await _service.DeleteAsync(id);
                return Ok("success");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("pushNewMessage")]
        public async Task<IActionResult> PushNewMessage(string id, MessageContent message)
        {
            try
            {
                message.MessageId = Guid.NewGuid().ToString();
                await _service.PushMessage(id, message);
                return Ok(message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("deleteChat")]
        public async Task<IActionResult> DeleteChat(string chatId, string messageId)
        {
            try
            {
                await _service.DeleteMessage(chatId, messageId);
                return Ok("success");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("editMessage")]
        public async Task<IActionResult> EditMessage(string editedmsg, string chatid, string messageid)
        {
            try
            {
                await _service.EditMessage(editedmsg, chatid, messageid);
                return Ok(editedmsg);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("getMessage")]
        public async Task<IActionResult> GetMessageById(string chatId, string messageId)
        {
            try
            {
                var content = await _service.GetMessageById(chatId, messageId);
                return Ok(content);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
