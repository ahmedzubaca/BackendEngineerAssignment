using BackendEngineerAssignment.Data;
using BackendEngineerAssignment.Models.Domain;
using BackendEngineerAssignment.Repositories.IRepositories;
using Microsoft.Data.SqlClient;

namespace BackendEngineerAssignment.Repositories
{
    public class PostTagRepository : IPostTagRepository
    {        
        private readonly BlogDbContext _db;

        public PostTagRepository(BlogDbContext db)
        {
            _db = db; 
        }
        public void AddPostTag(int postId, List<int> tagsIds)
        {
            var postsTags = new List<PostTag>();
            
                foreach (int tagId in tagsIds)
                {
                    var postTag = new PostTag()
                    {
                        PostId = postId,
                        TagId = tagId
                    };
                    
                    postsTags.Add(postTag);
                }
            _db.PostsTags.AddRange(postsTags);            
            _db.SaveChanges();
        }

        public void DeletePostTag(int postId) 
        {              
             var postTagsToDelete = _db.PostsTags.Where(pt => pt.PostId == postId).ToList();
            if(postTagsToDelete.Count > 0)
            {
                _db.PostsTags.RemoveRange(postTagsToDelete);
            }               
        }
    }
}
