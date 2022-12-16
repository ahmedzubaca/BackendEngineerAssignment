using BackendEngineerAssignment.Data;
using BackendEngineerAssignment.Models.Domain;
using BackendEngineerAssignment.Repositories.IRepositories;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;
using System.Security;
using System.Text.RegularExpressions;

namespace BackendEngineerAssignment.Repositories
{
    public class TagRepository : ITagRepository
    {
        private readonly BlogDbContext _db;

        public TagRepository(BlogDbContext db)
        {
            _db = db;

        }

        public async Task<List<Tag>> GetAllTags()
        {
            var tags = await _db.Tags.ToListAsync();
            if(tags == null)
            {
                return null!;
            }
            return tags;
        }
        public async Task<List<int>> AddTags(List<Tag> requestTags)
        {
            var existingTags = _db.Tags.AsEnumerable().Join(requestTags,
                                                      exTag => exTag.Name,
                                                      rqTag => rqTag.Name,
                                                      (exTag, rqTag) => exTag).ToList();

            var newTags = requestTags.Where(tag => existingTags.All(x => x.Name != tag.Name)).ToList();
            if (newTags.Count > 0)
            {
                await _db.Tags.AddRangeAsync(newTags);
                _db.SaveChanges();
            }

            var tagIds = existingTags.Select(x => x.Id).ToList();
            tagIds.AddRange(newTags.Select(x => x.Id));

            return tagIds;
        }        

        public List<Tag> GetTagsByPost()
        {
            var postsTagsList = _db.PostsTags.ToList();
            var tagsList = _db.Tags.ToList();
            var postList = _db.Posts.ToList();

           var tagNamesByPostIds = postsTagsList.AsEnumerable().Join(tagsList,
                pt => pt.TagId,
                t => t.Id,                
                (pt, t) => new
                {                    
                    PostId = pt.PostId,
                    Name = t.Name,
                }
            ).ToList();            

            postList.ForEach(post =>
            {
                tagNamesByPostIds.ForEach(tbn =>
                {
                    if(post.Id == tbn.PostId)
                    {
                        post.Tags.Add(tbn.Name.ToString());
                    }
                });
            });

            var list = postList;

            return tagsList;
        }
    }
}
