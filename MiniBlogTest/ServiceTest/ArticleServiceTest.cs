using MiniBlog.Model;
using MiniBlog.Services;
using MiniBlog.Stores;
using MiniBlog.Repositories;
using Moq;
using Xunit;
using System;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;

namespace MiniBlogTest.ServiceTest;

public class ArticleServiceTest
{
    [Fact]
    public async Task Should_create_article_when_author_does_not_exist_given_a_not_added_user_with_article()
    {
        //Given
        var mockArticleRepository = new Mock<IArticleRepository>();
        var mockUserRepository = new Mock<IUserRepository>();
        var articleService = new ArticleService(mockArticleRepository.Object, mockUserRepository.Object);
        var article = new Article() { UserName = "Jack", Title = "Test", Content = "test test" };
        User user = new User(article.UserName);

        //When
        await articleService.CreateArticle(article);

        //Then
        mockArticleRepository.Verify(method => method.CreateArticle(article), Times.Once());
        mockUserRepository.Verify(method => method.AddUser(It.IsAny<User>()), Times.Once());
    }

    [Fact]
    public async Task Should_add_article_only_when_author_exist_given_an_article()
    {
        //Given
        var mockArticleRepository = new Mock<IArticleRepository>();
        var mockUserRepository = new Mock<IUserRepository>();
        var articleService = new ArticleService(mockArticleRepository.Object, mockUserRepository.Object);
        mockUserRepository.Setup(repository => repository.FindUserByName("Jack")).Returns(Task.FromResult(new User("Jack")));
        var article = new Article() { UserName = "Jack", Title = "Test", Content = "test test" };

        //When
        await articleService.CreateArticle(article);
        mockArticleRepository.Verify(method => method.CreateArticle(article), Times.Once());
        mockUserRepository.Verify(method => method.AddUser(It.IsAny<User>()), Times.Never());
    }
}
