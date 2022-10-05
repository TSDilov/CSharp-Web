namespace ReceptionApp.Web.Controllers
{
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using ReceptionApp.Services.Data;
    using ReceptionApp.Web.ViewModels.Votes;

    [ApiController]
    [Route("api/[controller]")]
    public class VotesController : BaseController
    {
        private readonly IVotesService voteservice;

        public VotesController(IVotesService voteservice)
        {
            this.voteservice = voteservice;
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<PostVoteResponceModel>> Post(PostVoteInputModel model)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            await this.voteservice.SetVoteAsync(model.RecipeId, userId, model.Value);
            var averageVotes = this.voteservice.GetAverageVotes(model.RecipeId);
            return new PostVoteResponceModel { AverageVote = averageVotes };
        }
    }
}
