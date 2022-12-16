using BackendEngineerAssignment.Models.Domain;
using BackendEngineerAssignment.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using static BackendEngineerAssignment.Repositories.PostRepository;

namespace BackendEngineerAssignment.Repositories.IRepositories
{
    public interface IPostRepository
    {
        Task<GetPostsDTO> GetAllPosts();
        Task<GetSinglePostDTO> GetPostBySlug(string slug);
        Task<Post> AddPost(AddPostDTO addPostDTO);
        Task<Post> UpdatePost(string slug, UpdatePostDTO postDTO);
        Task<Post> DeletePost(string slug);        
    }
}
