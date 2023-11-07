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
    private readonly UserRepository userRepository = null!;
   
    public ArticleService(ArticleStore articleStore, UserStore userStore, IArticleRepository articleRepository, UserRepository userRepository, IArticleRepository articleRepo)
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

        // return articleStore.Articles.Find(articleExisted => articleExisted.Title == article.Title);

        if (article.UserName != null)
        {
            var result = userRepository.GetUserByName(article.UserName);
            if (result != null)
            {
                userRepository.CreateUser(new User(article.UserName));
            }
        }

        return await this.articleRepository.CreateArticle(article);
    }

    public async Task<List<Article>> GetAll()
    {
        return await articleRepository.GetArticles();
    }

    public Article? GetById(Guid id)
    {
        return articleStore.Articles.FirstOrDefault(article => article.Id == id.ToString());
    }
}
