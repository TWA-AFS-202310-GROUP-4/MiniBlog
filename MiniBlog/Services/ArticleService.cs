using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MiniBlog.Model;
using MiniBlog.Repositories;
using MiniBlog.Stores;

namespace MiniBlog.Services;

public class ArticleService : IArticleService
{
    private readonly IArticleRepository articleRepository = null!;
    private readonly IUserRepository userRepository = null!;

    public ArticleService(IArticleRepository articleRepository, IUserRepository userRepository)
    {
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

        // return articleStore.Articles.Find(articleExisted => articleExisted.Title == article.Title);

        if (article.UserName != null)
        {
            var articleUser = await userRepository.FindUserByName(article.UserName);

            if (articleUser == null)
            {
                await userRepository.AddUser(new User(article.UserName));
            }

            await articleRepository.CreateArticle(article);
        }

        return await articleRepository.FindArticleByTitle(article.Title);
    }

    public async Task<List<Article>> GetAll()
    {
        return await articleRepository.GetArticles();
    }

    public async Task<Article> GetById(Guid id)
    {
        return await articleRepository.FindArticleById(id);
    }
}
