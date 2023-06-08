using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Projekat.Application.Emails;
using Projekat.Application.UseCases;
using Projekat.Application.UseCases.Commands.Category;
using Projekat.Application.UseCases.Commands.Comment;
using Projekat.Application.UseCases.Commands.Picture;
using Projekat.Application.UseCases.Commands.Post;
using Projekat.Application.UseCases.Commands.Rate;
using Projekat.Application.UseCases.Commands.Register;
using Projekat.Application.UseCases.Commands.User;
using Projekat.Application.UseCases.Queries;
using Projekat.EfDataAccess;
using Projekat.Implementation.Email;
using Projekat.Implementation.UseCases.Commands.EfCategory;
using Projekat.Implementation.UseCases.Commands.EfComment;
using Projekat.Implementation.UseCases.Commands.EfPicture;
using Projekat.Implementation.UseCases.Commands.EfPost;
using Projekat.Implementation.UseCases.Commands.EfRate;
using Projekat.Implementation.UseCases.Commands.EfRegister;
using Projekat.Implementation.UseCases.Commands.EfUser;
using Projekat.Implementation.UseCases.Queries;
using Projekat.Implementation.Validators;
using System;
using System.Text;

namespace Projekat.Api.Core
{
    public static class ContainerExtensions
    {
        public static void AddUsesCases(this IServiceCollection services)
        {
            #region Validators
            //Validators
            services.AddTransient<CreatePostValidator>();
            services.AddTransient<ModifyPostValidator>();
            services.AddTransient<CreateUserValidator>();
            services.AddTransient<ModifyUserValidator>();
            services.AddTransient<CreateCategoryValidator>();
            services.AddTransient<ModifyCategoryValidator>();
            services.AddTransient<CreateCommentValidator>();
            services.AddTransient<ModifyCommentValidator>();
            services.AddTransient<RegisterUserValidator>();
            #endregion

            services.AddTransient<UseCaseExecutor>();

            //Post
            services.AddTransient<IGetPostsQuery, EfGetPostsQuery>();
            services.AddTransient<IGetPostQuery, EfGetPostQuery>();
            services.AddTransient<ICreatePostCommand, EfCreatePostCommand>();
            services.AddTransient<IModifyPostCommand, EfModifyPostCommand>();
            services.AddTransient<IDeletePostCommand, EfDeletePostCommand>();

            //User
            services.AddTransient<IGetUserQuery, EfGetUserQuery>();
            services.AddTransient<ICreateUserCommand, EfCreateUserCommand>();
            services.AddTransient<IModifyUserCommand, EfModifyUserCommand>();
            services.AddTransient<IDeleteUserCommand, EfDeleteUserCommand>();

            //Category
            services.AddTransient<IGetCategoryQuery, EfGetCategoryQuery>();
            services.AddTransient<ICreateCategoryCommand, EfCreateCategoryCommand>();
            services.AddTransient<IModifyCategoryCommand, EfModifyCategoryCommand>();
            services.AddTransient<IDeleteCategoryCommand, EfDeleteCategoryCommand>();

            //Rate
            services.AddTransient<IRatePostCommand, EfCreateRateCommand>();

            //Picture
            services.AddTransient<ICreatePictureCommand, EfCreatePictureCommand>();

            //Comment
            services.AddTransient<ICreateCommentCommand, EfCreateCommentCommand>();
            services.AddTransient<IModifyCommentCommand, EfModifyCommentCommand>();
            services.AddTransient<IDeleteCommentCommand, EfDeleteCommentCommand>();

            //Register
            services.AddTransient<IRegisterUserCommand, EfRegisterUserCommand>();

        }

        public static void AddApplicationUser(this IServiceCollection services)
        {
            services.AddTransient<IApplicationUser>(x =>
            {
                var accessor = x.GetService<IHttpContextAccessor>();
                var user = accessor.HttpContext.User;

                if (user.FindFirst("UserData") == null)
                {
                    return new AnonymousUser();
                }

                var userString = user.FindFirst("UserData").Value;
                var actor = JsonConvert.DeserializeObject<JwtUser>(userString);
                return actor;
            });
        }

        public static void AddJwt(this IServiceCollection services, AppSettings settings)
        {
            services.AddTransient(x =>
            {
                var context = x.GetService<ProjekatContext>();
                var settings = x.GetService<AppSettings>();

                return new JwtManager(context, settings.JwtSettings);
            });

            services.AddAuthentication(options =>
            {
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(cfg =>
            {
                cfg.RequireHttpsMetadata = false;
                cfg.SaveToken = true;
                cfg.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = settings.JwtSettings.Issuer,
                    ValidateIssuer = true,
                    ValidAudience = "Any",
                    ValidateAudience = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings.JwtSettings.SecretKey)),
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });
        }
    }
}
