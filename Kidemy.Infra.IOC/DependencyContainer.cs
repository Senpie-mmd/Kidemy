using Barnamenevisan.Localizing.IOC;
using Kidemy.Application.Mediator;
using Kidemy.Application.Services.Implementations;
using Kidemy.Application.Services.Interfaces;
using Kidemy.Domain.Interfaces.AboutUs;
using Kidemy.Domain.Interfaces.Account;
using Kidemy.Domain.Interfaces.Blog;
using Kidemy.Domain.Interfaces.ContactUs;
using Kidemy.Domain.Interfaces.Course;
using Kidemy.Domain.Interfaces.CourseRequest;
using Kidemy.Domain.Interfaces.Discount;
using Kidemy.Domain.Interfaces.DynamicText;
using Kidemy.Domain.Interfaces.FAQ;
using Kidemy.Domain.Interfaces.Identity;
using Kidemy.Domain.Interfaces.Link;
using Kidemy.Domain.Interfaces.Master;
using Kidemy.Domain.Interfaces.Newsletter;
using Kidemy.Domain.Interfaces.Notification;
using Kidemy.Domain.Interfaces.Page;
using Kidemy.Domain.Interfaces.SiteSetting;
using Kidemy.Domain.Interfaces.SmsProvider;
using Kidemy.Domain.Interfaces.SocialMedia;
using Kidemy.Domain.Interfaces.Survey;
using Kidemy.Domain.Interfaces.Ticket;
using Kidemy.Domain.Interfaces.User;
using Kidemy.Domain.Interfaces.VIPMembers;
using Kidemy.Domain.Interfaces.Wallet;
using Kidemy.Infra.Data.Context;
using Kidemy.Infra.Data.Repositories.AboutUs;
using Kidemy.Infra.Data.Repositories.Account;
using Kidemy.Infra.Data.Repositories.Blog;
using Kidemy.Infra.Data.Repositories.ContactUs;
using Kidemy.Infra.Data.Repositories.Course;
using Kidemy.Infra.Data.Repositories.Discount;
using Kidemy.Infra.Data.Repositories.CourseRequest;
using Kidemy.Infra.Data.Repositories.DynamicText;
using Kidemy.Infra.Data.Repositories.FAQ;
using Kidemy.Infra.Data.Repositories.Identity;
using Kidemy.Infra.Data.Repositories.Link;
using Kidemy.Infra.Data.Repositories.Master;
using Kidemy.Infra.Data.Repositories.Newsletter;
using Kidemy.Infra.Data.Repositories.Notification;
using Kidemy.Infra.Data.Repositories.Page;
using Kidemy.Infra.Data.Repositories.SiteSetting;
using Kidemy.Infra.Data.Repositories.SmsProvider;
using Kidemy.Infra.Data.Repositories.SocialMedia;
using Kidemy.Infra.Data.Repositories.Survey;
using Kidemy.Infra.Data.Repositories.Ticket;
using Kidemy.Infra.Data.Repositories.User;
using Kidemy.Infra.Data.Repositories.VIPMembers;
using Kidemy.Infra.Data.Repositories.Wallet;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Kidemy.Application.Services.Implementation;
using GoftemanMelal.Infra.Data.Repositories.BankAccountCard;
using Kidemy.Domain.Interfaces.BankAccountCard;
using Kidemy.Infra.Data.Repositories;
using Kidemy.Domain.Interfaces;
using Kidemy.Infra.Data.Repositories.WithdrawRequest;
using Kidemy.Domain.Interfaces.WithdrawRequest;
using GoftemanMelal.Application.Services.Implementations;
using Kidemy.Domain.Interfaces.Cart;
using Kidemy.Infra.Data.Repositories.Cart;
using Kidemy.Domain.Interfaces.Order;
using Kidemy.Infra.Data.Repositories.Order;
using System.Net.Security;
using Kidemy.Infra.Data.Repositories.Consultation;
using Kidemy.Domain.Interfaces.Consultation;
using Kidemy.Domain.Models.Consultation;
using Kidemy.Domain.Interfaces.Slider;
using Kidemy.Infra.Data.Repositories.Slider;
using Kidemy.Domain.Interfaces.Banner;
using Kidemy.Infra.Data.Repositories.Banner;
using Kidemy.Domain.Interfaces.MasterNotification;
using Kidemy.Infra.Data.Repositories.MasterNotification;

namespace Kidemy.Infra.IOC
{
    public class DependencyContainer
    {
        public static void RegisterDependencies(IServiceCollection services, string connectionString)
        {
            #region Localizing

            RegisterLocalizingDependencies.Register(services, typeof(KidemyContext));

            #endregion

            #region Database

            services.AddDbContext<KidemyContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });

            #endregion

            #region Repositories
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IPermissionRepository, PermissionRepository>();
            services.AddScoped<IPermissionRepository, PermissionRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IWalletTransactionRepository, WalletTransactionRepository>();
            services.AddScoped<IAccountCodeRepository, AccountCodeRepository>();
            services.AddScoped<ITicketRepository, TicketRepository>();
            services.AddScoped<IAboutUsRepository, AboutUsRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IWalletTransactionRepository, WalletTransactionRepository>();
            services.AddScoped<IAccountCodeRepository, AccountCodeRepository>();
            services.AddScoped<IContactUsRepository, ContactUsRepository>();
            services.AddScoped<IContactUsFormRepository, ContactUsFormRepository>();
            services.AddScoped<ISocialMediaRepository, SocialMediaRepository>();
            services.AddScoped<IPageRepository, PageRepository>();
            services.AddScoped<INewsletterRepository, NewsletterRepository>();
            services.AddScoped<ISurveyRepository, SurveyRepository>();
            services.AddScoped<ISmsProviderRepository, SmsProviderRepository>();
            services.AddScoped<ISurveyAnswerRepository, SurveyAnswerRepository>();
            services.AddScoped<ISurveyQuestionRepository, SurveyQuestionRepository>();
            services.AddScoped<ILinkRepository, LinkRepository>();
            services.AddScoped<ISmsProviderRepository, SmsProviderRepository>();
            services.AddScoped<IAboutUsProgressBarRepository, AboutUsProgressBarRepository>();
            services.AddScoped<ISiteSettingRepository, SiteSettingRepository>();
            services.AddScoped<IBlogRepository, BlogRepository>();
            services.AddScoped<IFAQRepository, FAQRepository>();
            services.AddScoped<ICourseRepository, CourseRepository>();
            services.AddScoped<IMasterRepository, MasterRepository>();
            services.AddScoped<ICourseCategoryRepository, CourseCategoryRepository>();
            services.AddScoped<INotificationRepository, NotificationRepository>();
            services.AddScoped<IVIPMemberReposirory, VIPMemberReposirory>();
            services.AddScoped<IVIPPlanRepository, VIPPlanRepository>();
            services.AddScoped<IDynamicTextRepository, DynamicTextRepository>();
            services.AddScoped<ICourseCommentRepository, CourseCommentRepository>();
            services.AddScoped<IDiscountRepository, DiscountRepository>();
            services.AddScoped<IDiscountLimitationRepository, DiscountLimitationRepository>();
            services.AddScoped<IDiscountUsageRepository, DiscountUsageRepository>();
            services.AddScoped<ICourseQuestionRepository, CourseQuestionRepository>();
            services.AddScoped<ICourseQuestionAnswerRepository, CourseQuestionAnswerRepository>();
            services.AddScoped<IMasterContractRepository, MasterContractRepository>();
            services.AddScoped<IUploadedMasterContractRepository, UploadedMasterContractRepository>();
            services.AddScoped<ICourseRequestRepository, CourseRequestRepository>();
            services.AddScoped<ICourseTagRepository, CourseTagRepository>();
            services.AddScoped<ICourseRequestVoteRepository, CourseRequestVoteRepository>();
            services.AddScoped<ICourseRequestMasterVolunteerRepository, CourseRequestMasterVolunteerRepository>();
            services.AddScoped<IBankAccountCardRepository, BankAccountCardRepository>();
            services.AddScoped<IWithdrawRequestRepository, WithdrawRequestRepository>();
            services.AddScoped<ISettlementTransactionRepository, SettlementTransactionRepository>();
            services.AddScoped<ICartRepository, CartRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<ICourseVideoRepository, CourseVideoRepository>();
            services.AddScoped<ICourseVideoCategoryRepository, CourseVideoCategoryRepository>();
            services.AddScoped<IAdviserRepository, AdviserRepository>();
            services.AddScoped<IAdviserConsultationTypeRespositry, AdviserConsultationTypeRespositry>();
            services.AddScoped<IAdviserAvailableDateRepository, AdviserAvailableDateRepository>();
            services.AddScoped<IConsultationRequestRepository, ConsultationRequestRepository>();
            services.AddScoped<ICourseCommentReportRepository, CourseCommentReportRepository>();
            services.AddScoped<ISliderRepository, SliderRepository>();
            services.AddScoped<IBannerRepository, BannerRepository>();
            services.AddScoped<ICourseViewRepository, CourseViewRepository>();
            services.AddScoped<IMasterNotificationRepository, MasterNotificationRepository>();
            services.AddScoped<IFavouriteCourseRepository, FavouriteCourseRepository>();
            services.AddScoped<ICourseNotificationRepository, CourseNotificationRepository>();
            services.AddScoped<IBlogCommentRepository, BlogCommentRepository>();
            services.AddScoped<IBlogTagRepository, BlogTagRepository>();
            #endregion

            #region Services
            services.AddSingleton<ICacheManager, CacheManager>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IViewRenderService, ViewRenderService>();
            services.AddScoped<IMediatorHandler, MediatorHandler>();
            services.AddScoped<IWalletService, WalletService>();
            services.AddScoped<ISmsSenderSevice, SmsSenderService>();
            services.AddScoped<IAccountCodeService, AccountCodeService>();
            services.AddScoped<IEmailSettingService, EmailSettingService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<ITicketService, TicketService>();
            services.AddScoped<IAboutUsService, AboutUsService>();
            services.AddScoped<ISocialMediaService, SocialMediaService>();
            services.AddSingleton<ICacheManager, CacheManager>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IViewRenderService, ViewRenderService>();
            services.AddScoped<IMediatorHandler, MediatorHandler>();
            services.AddScoped<IWalletService, WalletService>();
            services.AddScoped<ISmsSenderSevice, SmsSenderService>();
            services.AddScoped<IAccountCodeService, AccountCodeService>();
            services.AddScoped<IEmailSettingService, EmailSettingService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IContactUsService, ContactUsService>();
            services.AddScoped<IContactUsFormService, ContactUsFormService>();
            services.AddScoped<IPageService, PageService>();
            services.AddScoped<INewsletterService, NewsletterService>();
            services.AddScoped<ISmsProviderService, SmsProviderService>();
            services.AddScoped<IEncryptService, EncryptService>();
            services.AddScoped<ISurveyService, SurveyService>();
            services.AddScoped<ISmsProviderService, SmsProviderService>();
            services.AddScoped<ILinkService, LinkService>();
            services.AddScoped<ISiteSettingService, SiteSettingService>();
            services.AddScoped<IBlogService, BlogService>();
            services.AddScoped<IFAQService, FAQService>();
            services.AddScoped<IMasterService, MasterService>();
            services.AddScoped<ICourseService, CourseService>();
            services.AddScoped<INotificationService, NotificationService>();
            services.AddScoped<IVIPMembersService, VIPMembersService>();
            services.AddScoped<IVIPPlanService, VIPPlanService>();
            services.AddScoped<IDynamicTextService, DynamicTextService>();
            services.AddScoped<IDiscountService, DiscountService>();
            services.AddScoped<ICourseCategoryService, CourseCategoryService>();
            services.AddScoped<ICourseQuestionService, CourseQuestionService>();
            services.AddScoped<ICourseQuestionAnswerService, CourseQuestionAnswerService>();
            services.AddScoped<IMasterContractService, MasterContractService>();
            services.AddScoped<IUploadedMasterContractService, UploadedMasterContractService>();
            services.AddScoped<ICourseRequestService, CourseRequestService>();
            services.AddScoped<IDiscountVerificationService, DiscountNotUsedByUserVerficationService>();
            services.AddScoped<IDiscountVerificationService, DiscountCodeVerficationService>();
            services.AddScoped<IDiscountVerificationService, DiscountCourseVerficationService>();
            services.AddScoped<IDiscountVerificationService, DiscountTimeVerficationService>();
            services.AddScoped<IDiscountVerificationService, DiscountTypeVerficationService>();
            services.AddScoped<IDiscountVerificationService, DiscountUsageCountVerficationService>();
            services.AddScoped<IDiscountVerificationService, DiscountUserVerficationService>();
            services.AddScoped<IDiscountVerificationService, DiscountCourseCategoryVerficationService>();
            services.AddScoped<IBankAccountCardService, BankAccountCardService>();
            services.AddScoped<IWithdrawRequestService, WithdrawRequestService>();
            services.AddScoped<ISettlementTransactionService, SettlementTransactionService>();
            services.AddScoped<ICartService, CartService>();
            services.AddScoped<ICourseCommentService, CourseCommentService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<ICourseVideoService, CourseVideoService>();
            services.AddScoped<ISettlementTransactionService, SettlementTransactionService>();
            services.AddScoped<IAdviserSerivce, AdviserService>();
            services.AddScoped<IConsultationRequestService, ConsultationRequestService>();
            services.AddScoped<ISliderService, SliderService>();
            services.AddScoped<IBannerService, BannerService>();
            services.AddScoped<IMasterNotificationService, MasterNotificationService>();
            services.AddScoped<IBlogCommentService, BlogCommentService>();
            // services.AddScoped<IInPersonCourseService, InPersonCourseService>();
            #endregion

        }

    }
}
