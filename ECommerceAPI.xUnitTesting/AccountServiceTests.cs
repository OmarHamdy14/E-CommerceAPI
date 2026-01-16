using AutoMapper;
using AutoMapper.Configuration;
using ECommerceAPI.Data.Base;
using ECommerceAPI.Data.Services.Implementation;
using ECommerceAPI.Helpers;
using ECommerceAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.xUnitTesting
{
    public class AccountServiceTests
    {
        private readonly AccountService _accountService;
        private readonly Mock<UserManager<ApplicationUser>> _userManager;
        private readonly Mock<IMapper> _mapper;
        private readonly Mock<IOptions<JWT>> _jwt;
        private readonly Mock<IConfiguration> _confg;
        public AccountServiceTests()
        {
            _userManager = new Mock<UserManager<ApplicationUser>>();
            _mapper = new Mock<IMapper>();
            _jwt = new Mock<IOptions<JWT>>();
            _confg = new Mock<IConfiguration>();

            _accountService = new AccountService(_userManager.Object, _mapper.Object, _jwt.Object, _confg.Object);
        }
        [Fact]
        public async Task FindById_ShouldReturnUser()
        {
            var Id = "111";
            _userManager.Setup(r => r.FindByIdAsync(Id)).ReturnsAsync(new ApplicationUser { Id = "111" });

            var result = await _accountService.FindById(Id);
            Assert.NotNull(result);
            Assert.Equal(result.Id, Id);
        }
    }
}
