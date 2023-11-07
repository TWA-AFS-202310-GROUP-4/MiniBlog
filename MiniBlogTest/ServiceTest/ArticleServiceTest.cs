using MiniBlog.Model;
using MiniBlog.Repositories.Interface;
using MiniBlog.Services;
using MiniBlog.Stores;
using Moq;
using System;
using Xunit;

namespace MiniBlogTest.ServiceTest;

public class ArticleServiceTest
{
    [Fact]
    public async void Should_create_article_when_invoke_CreateArticle_given_input_article()
    {
        // given
        Article newArticle = new Article("pocky", "name", "sss");
        var userMock = new Mock<IUserRepository>();
        var articleMock = new Mock<IArticleRepository>();

        articleMock.Setup(
            repository => repository
            .CreateArticle(It.IsAny<Article>()))
            .Callback<Article>(article => article.Id = Guid.NewGuid().ToString())
            .ReturnsAsync((Article article) => article);
        userMock.Setup(
            repository => repository
            .GetByName(It.IsAny<string>()))
            .ReturnsAsync((User)null);
        userMock.Setup(
            repository => repository
            .Add(It.IsAny<User>()))
            .ReturnsAsync((User user) => user);
        // then
        var articleService = new ArticleService(articleMock.Object, userMock.Object);
        var articleCreated = await articleService.CreateArticle(newArticle);

        articleMock.Verify(repository => repository.CreateArticle(newArticle), Times.Once());
        userMock.Verify(repository => repository.Add(It.IsAny<User>()), Times.Once());
    }

    [Fact]
    public async void Should_create_new_article_only_when_invoke_CreateArticle_given_user_exists()
    {
        // given
        Article newArticle = new Article("pocky", "name", "sss");
        var userMock = new Mock<IUserRepository>();
        var articleMock = new Mock<IArticleRepository>();
        var articleService = new ArticleService(articleMock.Object, userMock.Object);
        articleMock.Setup(
            repository => repository.
            CreateArticle(It.IsAny<Article>())).
            Callback<Article>(article => article.Id = Guid.NewGuid().ToString())
            .ReturnsAsync((Article article) => article);
        userMock.Setup(
            repository => repository.
            GetByName(It.IsAny<string>())).
            ReturnsAsync(new User("pocky"));
        userMock.Setup(
            repository => repository
            .Add(It.IsAny<User>()))
            .ReturnsAsync((User user) => user);

        var articleCreated = await articleService.CreateArticle(newArticle);

        // then
        articleMock.Verify(repository => repository.CreateArticle(newArticle), Times.Exactly(1));
        userMock.Verify(repository => repository.Add(It.IsAny<User>()), Times.Never());
    }
}
