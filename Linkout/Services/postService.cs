using Linkout.DAL;
using Linkout.DTO;
using Linkout.Models;
using Microsoft.EntityFrameworkCore;

namespace Linkdout.Services
{
    public class PostService
    {
        private DataLayer db;
        public PostService(DataLayer _db) { db = _db; }
        public async Task<PostListDTO> getAll()
        {
            return db.Posts.Include(p => p.user).ToList() as PostListDTO;
        }

        public async Task<PostModel> getPostById(int id)
        {
            return db.Posts.Include(p => p.user).FirstOrDefault(p => p.Id == id);
        }
        public async Task<bool> addNewPost(NewPostDTO req)
        {
            try
            {
                UserModel user = db.Users.Find(req.userId);
                req.Post.user = user;
                db.Posts.Add(req.Post);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public async Task<string>editPostBody (int postid, string newBody)
        {
            PostModel post = db.Posts.Find(postid);
            string olBody = post.body;
            post.body = newBody;
            db.SaveChanges();
            return olBody;
        }

        public async Task<int> deletePost(int postid)
        {
            try
            {
                PostModel post = db.Posts.Find(postid);
                db.Posts.Remove(post);
                db.SaveChanges();
                return post.Id;
            }
            catch (Exception)
            {
                return-1;
            }
        }
    }
}