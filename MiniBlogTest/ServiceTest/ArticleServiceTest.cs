using System.Collections.Generic;
using System.Threading.Tasks;
using MiniBlog;
using MiniBlog.Model;
using MiniBlog.Repositories;
using MiniBlog.Services;
using MiniBlog.Stores;
using Moq;
using Xunit;

namespace MiniBlogTest.ServiceTest;

public class ArticleServiceTest{


    [Fact]
    public void Should_create_article_and_new_user_when_invoke_CreateArticle_given_article_user_not_exist()
    {
        // given
        Article newArticle = new Article("pocky","name","sss");
        var user = new Mock<IUserRepository>();
        var article = new Mock<IArticleRepository>();
        var articleService = new ArticleService(article.Object, user.Object);

        var addedArticle = articleService.CreateArticle(newArticle).Result;

        // then
       article.Verify(a=> a.CreateArticle(newArticle), Times.AtMostOnce());
       user.Verify(a => a.CreateUser(new User()), Times.AtMostOnce());

    }

    [Fact]
    public void Should_create_new_article_only_when_invoke_CreateArticle_given_user_exists()
    {
        // given
        Article newArticle = new Article("pocky", "name", "sss");
        var user = new Mock<IUserRepository>();
        var article = new Mock<IArticleRepository>();
        var articleService = new ArticleService(article.Object, user.Object);

        articleService.CreateArticle(newArticle);
        var addedArticle = articleService.CreateArticle(newArticle).Result;

        // then
        article.Verify(a => a.CreateArticle(newArticle), Times.AtLeastOnce());
        user.Verify(a => a.CreateUser(new User()), Times.AtMostOnce());

    }
}
