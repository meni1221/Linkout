
using Linkdout.Services;
using Linkout.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Linkdout.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]

    public class PostController : ControllerBase
    {
        private PostService postService;

        public PostController(PostService _postService) { postService = _postService; }

        //Get All Posts
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<PostListDTO>> getAllPosts()
        {
            return Ok(await postService.getAll());
        }

        //Get Single Post

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PostListDTO>> getSinglePost(int id)
        {
            return Ok(await postService.getPostById(id));
        }


        //Create New Post
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> CreatePost([FromBody] NewPostDTO req)
        {
            bool res = await postService.addNewPost(req);
            if (res)
            {
                return Created();
            }
            else
            {
                return BadRequest();
            }
        }

        //Edit Post

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> editPost ([FromBody] EditPostDTO req)
        {
            string oldBody = await postService.editPostBody(req.postId, req.newBody);
            return oldBody != String.Empty ? Ok(oldBody) : BadRequest();
        }

        //Delete Post
        [HttpDelete ("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<int>> deletePost(int id)
        {
            int res = await postService.deletePost(id);
            return  res != 1 ? Ok() : NotFound();
        }




    }
}
