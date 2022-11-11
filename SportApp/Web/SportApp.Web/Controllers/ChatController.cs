namespace SportApp.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using SportApp.Services.Data;
    using SportApp.Web.ViewModels;

    public class ChatController : BaseController
    {
        private readonly IMessageService messageService;

        public ChatController(IMessageService messageService)
        {
            this.messageService = messageService;
        }

        [Authorize]
        public IActionResult Chat()
        {
            var model = new ChatViewModel
            {
                Messages = this.messageService.GetAll(),
            };

            return this.View(model);
        }
    }
}
