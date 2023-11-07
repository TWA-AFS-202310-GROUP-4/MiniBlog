using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MiniBlog.Model;
using MiniBlog.Repositories;
using MiniBlog.Stores;

namespace MiniBlog.Services;

public class ArticleService
{
    private readonly ArticleStore articleStore = null!;
    private readonly UserStore userStore = null!;
    private readonly IArticleRepository articleRepository = null!;
    private readonly IUserRepository userRepository = null!;

    public ArticleService(ArticleStore articleStore, UserStore userStore, IArticleRepository articleRepository, IUserRepository userRepository)
    {
        this.articleStore = articleStore;
        this.userStore = userStore;
        this.articleRepository = articleRepository;
        this.userRepository = userRepository;
    }

    public async Task<Article?> CreateArticle(Article article)
    {
        // if (article.UserName != null)
        // {
        //     if (!userStore.Users.Exists(_ => article.UserName == _.Name))
        //     {
        //         userStore.Users.Add(new User(article.UserName));
        //     }

        //     articleStore.Articles.Add(article);
        // }
        if (article.UserName != null)
        {
            if (!await userRepository.IsUserExsit(article.UserName))
            {
                await userRepository.CreateUser(article.UserName);
            }
        }

        article.Id = string.Empty;
        return await articleRepository.CreateArticle(article);
    }

    public async Task<List<Article>> GetAll()
    {
        return await articleRepository.GetArticles();
    }

    public async Task<Article?> GetByIdAsync(string id)
    {
        //     return articleStore.Articles.FirstOrDefault(article => article.Id == id.ToString());
        return await articleRepository.GetArticleById(id);
    }
}
