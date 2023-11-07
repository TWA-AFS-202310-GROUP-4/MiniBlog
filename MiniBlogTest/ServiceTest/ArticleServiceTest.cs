using System;
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


        article.Setup(r => r.CreateArticle(It.IsAny<Article>())).Callback<Article>(article => article.Id = Guid.NewGuid().ToString()).ReturnsAsync((Article article) => article);
        user.Setup(r => r.GetUserByName(It.IsAny<string>())).ReturnsAsync((User)null);
        user.Setup(r => r.CreateUser(It.IsAny<User>())).ReturnsAsync((User user) => user);
        // then
        var articleService = new ArticleService(article.Object, user.Object);
        articleService.CreateArticle(newArticle);

        article.Verify(a=> a.CreateArticle(newArticle), Times.Once());
        user.Verify(a => a.CreateUser(It.IsAny<User>()), Times.Once());

    }

    [Fact]
    public void Should_create_new_article_only_when_invoke_CreateArticle_given_user_exists()
    {
        // given
        Article newArticle = new Article("pocky", "name", "sss");
        var user = new Mock<IUserRepository>();
        var article = new Mock<IArticleRepository>();
        var articleService = new ArticleService(article.Object, user.Object);
        article.Setup(r => r.CreateArticle(It.IsAny<Article>())).Callback<Article>(article => article.Id = Guid.NewGuid().ToString()).ReturnsAsync((Article article) => article);
        user.Setup(r => r.GetUserByName(It.IsAny<string>())).ReturnsAsync(new User("pocky"));
        user.Setup(r => r.CreateUser(It.IsAny<User>())).ReturnsAsync((User user) => user);

        articleService.CreateArticle(newArticle);

        // then
        article.Verify(a => a.CreateArticle(newArticle), Times.Exactly(1));
        user.Verify(a => a.CreateUser(It.IsAny<User>()), Times.Never());

    }
}
