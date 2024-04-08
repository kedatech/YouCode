using Microsoft.AspNetCore.Mvc;
using YouCode.BE;
using YouCode.BL;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace YouCode.GUI.Controllers.Api
{
    [ApiController]
    [Route("api/follower")]
    public class FollowerController : ControllerBase
    {
        private readonly FollowerBL _followerBL = new FollowerBL();

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetAll(int id)
        {
            var followers = await _followerBL.GetAllAsync();
            var filteredFollowers = followers.FindAll(f => f.IdFollower == id);
            return Ok(filteredFollowers);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Follower follower)
        {
            Console.WriteLine(JsonConvert.SerializeObject(follower));
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            int result = await _followerBL.CreateAsync(follower);
            return CreatedAtAction("Get", new { id = follower.Id }, follower);
        }


        [HttpDelete("{followerId}/{followId}")]
        public async Task<IActionResult> Delete(int followerId, int followId)
        {
            var result = await _followerBL.DeleteAsync(new Follower { IdFollower = followerId, IdFollow = followId });
            return NoContent();
        }
    }
}
