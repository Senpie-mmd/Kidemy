using Kidemy.Domain.Enums.Link;
using Kidemy.Domain.Enums.Sms;
using Kidemy.Domain.Models.AboutUs;
using Kidemy.Domain.Models.ContactUs;
using Kidemy.Domain.Models.DynamicText;
using Kidemy.Domain.Models.Identity;
using Kidemy.Domain.Models.Link;
using Kidemy.Domain.Models.SiteSetting;
using Kidemy.Domain.Models.SmsProvider;
using Kidemy.Domain.Models.User;
using Kidemy.Domain.Models.VIPMembers;
using Kidemy.Domain.Statics;
using Microsoft.EntityFrameworkCore;
using Kidemy.Domain.Models.Page;

namespace Kidemy.Infra.Data.Context
{
    public static class DataSeeding
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            #region Identity

            var adminRole = new Role
            {
                Id = 1,
                Title = "ادمین",
                UniqueName = "Admin",
            };

            var masterRole = new Role
            {
                Id = 2,
                Title = "استاد",
                UniqueName = "Master",
            };

            modelBuilder.Entity<Role>().HasData(adminRole, masterRole);

            // rolePermission
            modelBuilder.Entity<RolePermissionMapping>().HasData(PermissionsList.Permissions.Select(permission => new RolePermissionMapping
            {
                PermissionId = permission.Id,
                RoleId = adminRole.Id
            }));

            modelBuilder.Entity<RolePermissionMapping>().HasData(new RolePermissionMapping
            {
                PermissionId = 172,
                RoleId = 2
            });

            #endregion

            #region User

            // user
            modelBuilder.Entity<User>().HasData(new User
            {
                Id = 1,
                Email = "admin@gmail.com",
                FirstName = "Admin",
                AvatarName = "default.png",
                Mobile = "09122233322",
                IsEmailActive = true,
                IsMobileActive = true
            });

            // userRole
            modelBuilder.Entity<UserRoleMapping>().HasData(new UserRoleMapping
            {
                RoleId = adminRole.Id,
                UserId = 1
            });

            #endregion

            #region Permission

            modelBuilder.Entity<Permission>().HasData(PermissionsList.Permissions);

            #endregion

            #region ContactUs

            modelBuilder.Entity<ContactUs>().HasData(new ContactUs
            {
                Id = 1,
                Address = "تهران",
                Email = "Kidemy.sample@gmail.com",
                Mobile = "09121111111",
                IsDeleted = false,
                CreatedDateOnUtc = DateTime.Now,
                UpdatedDateOnUtc = DateTime.Now
            });

            #endregion

            #region AboutUs

            modelBuilder.Entity<AboutUs>().HasData(new AboutUs
            {
                Id = 1,
                Title = "عنوان درباره ما",
                Description = "توضیحات درباره ما",
                IsDeleted = false,
                ImageNumber1 = "/assets/images/about/05.jpg",
                ImageNumber2 = "/assets/images/about/03.jpg",
                ImageNumber3 = "/assets/images/about/09.jpg",
                ImageNumber4 = "/assets/images/about/10.jpg",
                ImageNumber5 = "/assets/images/about/06.jpg"
            });

            #endregion

            #region SiteSetting

            modelBuilder.Entity<SiteSetting>().HasData(new SiteSetting
            {
                Id = 1,
                Email = "jafarabbaspour1367@gmail.com",
                Mobile = "09125554478",
                Mobile2 = "09125554478",
                Address = "خیابان ملک",
                LogoName = "لوگو",
                CollectionManagement = "مدیر مجموعه",
                CopyrightDescription = "حق کپی رایت",
                FooterDescription = "توضیحات فوتر",
                NewsletterDescription = "توضیحات خبرنامه",
                IsDeleted = false,
            });

            #endregion

            #region SmsProvider

            modelBuilder.Entity<SmsProvider>().HasData(new SmsProvider
            {
                Id = 1,
                ApiKey = "6AC5E403-C244-49B9-A497-E0436649FBBB",
                SmsProviderType = SmsProviderType.ParsGreen,
                IsDefault = true,
                IsDeleted = false
            });

            modelBuilder.Entity<SmsProvider>().HasData(new SmsProvider
            {
                Id = 2,
                ApiKey = "59693856665334494F3241446C5959517557796D6B78412F334F68496D44784E6C7667544972744E314A493D",
                SmsProviderType = SmsProviderType.KaveNegar,
                IsDefault = false,
                IsDeleted = false
            });

            #endregion

            #region DynamicText

            modelBuilder.Entity<DynamicText>().HasData(new DynamicText
            {
                Id = 1,
                Position = Domain.Enums.DynamicText.DynamicTextPosition.VIPPageText,
                Text = "  شما کاربر گرامی می‌توانید برای دسترسی به امکانات ویژه سایت، اشتراک ویژه را فراهم نمایید.",
                IsDeleted = false
            });

            modelBuilder.Entity<DynamicText>().HasData(new DynamicText
            {
                Id = 2,
                Position = Domain.Enums.DynamicText.DynamicTextPosition.VIPInvoiceText,
                Text = "برای نهایی کردن فاکتور باید کیف پول خود را به میزان مبلغ قابل پرداخت فاکتور شارژ نمایید.برای ادامه بر روی دکمه زیر کلیک نمایید .",
                IsDeleted = false
            });

            modelBuilder.Entity<DynamicText>().HasData(new DynamicText
            {
                Id = 3,
                Position = Domain.Enums.DynamicText.DynamicTextPosition.VIPDashBoardTextBeforeJoin,
                Text = "درحال حاضر شما عضو ویژه سایت نمی باشید ! درصورت تمایل برای استفاده از امکانات اعضای ویژه باید اشتراک خریداری نمایید",
                IsDeleted = false
            });

            modelBuilder.Entity<DynamicText>().HasData(new DynamicText
            {
                Id = 4,
                Position = Domain.Enums.DynamicText.DynamicTextPosition.VIPDashBoardTextAfterJoin,
                Text = "شما عضو ویژه سایت می باشید تا تاریخ ",
                IsDeleted = false
            });

            modelBuilder.Entity<DynamicText>().HasData(new DynamicText
            {
                Id = 5,
                Position = Domain.Enums.DynamicText.DynamicTextPosition.TitleOfTopOfHomePage,
                Text = "چرا آموزش برنامه نویسی به\r\nکودکان و نوجوانان !؟",
                IsDeleted = false
            });

            modelBuilder.Entity<DynamicText>().HasData(new DynamicText
            {
                Id = 6,
                Position = Domain.Enums.DynamicText.DynamicTextPosition.DescriptionOfTopOfHomePage,
                Text = "آموزش برنامه نویسی به کودکان و نوجوانان به دلیل اینکه در این سنین ذهنیت کودکان و نوجوانان برای یادگیری برنامه نویسی بسیار مناسب است و این موضوع باعث می شود که کودکان و نوجوانان بتوانند به راحتی و با سرعت بالا برنامه نویسی را یاد بگیرند.",
                IsDeleted = false
            });

            modelBuilder.Entity<DynamicText>().HasData(new DynamicText
            {
                Id = 7,
                Position = Domain.Enums.DynamicText.DynamicTextPosition.TheFirstFeature,
                Text = "مدرس مجرب",
                IsDeleted = false
            });

            modelBuilder.Entity<DynamicText>().HasData(new DynamicText
            {
                Id = 8,
                Position = Domain.Enums.DynamicText.DynamicTextPosition.TheSecondFeature,
                Text = "ارائه مدرک",
                IsDeleted = false
            });

            modelBuilder.Entity<DynamicText>().HasData(new DynamicText
            {
                Id = 9,
                Position = Domain.Enums.DynamicText.DynamicTextPosition.TheThirdFeature,
                Text = "جذب مدرس",
                IsDeleted = false
            });

            modelBuilder.Entity<DynamicText>().HasData(new DynamicText
            {
                Id = 10,
                Position = Domain.Enums.DynamicText.DynamicTextPosition.TextOfTheMostPopularCoursesOfHomePage,
                Text = "هر موضوعی را در هر زمان مطالعه کنید. هزاران دوره آموزشی را با کمترین قیمت جستجو کنید!",
                IsDeleted = false
            });

            modelBuilder.Entity<DynamicText>().HasData(new DynamicText
            {
                Id = 11,
                Position = Domain.Enums.DynamicText.DynamicTextPosition.TextOfTheProposedCoursesOfHomePage,
                Text = "مشاهده دوره های جدید و 🔥 در جشنواره",
                IsDeleted = false
            });

            modelBuilder.Entity<DynamicText>().HasData(new DynamicText
            {
                Id = 12,
                Position = Domain.Enums.DynamicText.DynamicTextPosition.UserCount,
                Text = "60",
                IsDeleted = false
            });
            modelBuilder.Entity<DynamicText>().HasData(new DynamicText
            {
                Id = 13,
                Position = Domain.Enums.DynamicText.DynamicTextPosition.FirstPageBannerText,
                Text = "ارائه راهکارها و شرایط سخت تایپ به پایان رسد وزمان مورد نیاز شامل حروفچینی دستاوردهای اصلی و جوابگوی سوالات پیوسته اهل دنیای موجود طراحی اساسا مورد استفاده قرار گیرد.",
                IsDeleted = false
            });

            #endregion

            #region SmsProvider

            modelBuilder.Entity<VIPPlan>().HasData(new VIPPlan
            {
                Id = 1,
                Title = "یک ماهه",
                DurationDay = 30,
                Price = 30000,
                IsDeleted = false,

            });

            modelBuilder.Entity<VIPPlan>().HasData(new VIPPlan
            {
                Id = 2,
                Title = "دو ماهه",
                DurationDay = 60,
                Price = 60000,
                IsDeleted = false,
            });

            #endregion

            #region Link

            modelBuilder.Entity<Link>().HasData(new Link
            {
                Id = 1,
                Title = "سوالات متداول",
                Address = "/FAQs",
                LinkType = LinkType.Footer,
                CreatedDateOnUtc = DateTime.UtcNow,
                IsDeleted = false

            });

            #endregion

            #region Link

            modelBuilder.Entity<Page>().HasData(new Page
            {
                Id = 1,
                Title = "همکاری با کیدمی",
                Slug = "همکاری-با-کیدمی",
                Body = "<h2 style=\"margin-right: 0\"><span style=\"color: rgba(76, 230, 230, 1)\"><strong>مراحل همکاری در کیدمی</strong></span></h2><p style=\"margin-right: 0; text-align: justify\">کیدمی یک پلتفرم آموزشی در بستر تجارت الکترونیک می باشد که از اساتید با تجربه در این حوزه دعوت به همکاری می نماید.</p><p style=\"margin-right: 0; text-align: justify\">اگر تمایل دارید در کیدمی در زمینه تولید محتوای آموزشی فعالیت نمایید، می توانید از طریق&nbsp;تیکت (در پنل کاربری) &nbsp;با کارشناسان ما در ارتباط باشید.</p><p style=\"margin-right: 0; text-align: justify\">جهت شروع همکاری به این نکات توجه داشته باشید:</p><p style=\"margin-right: 0; text-align: justify\">. &nbsp;حداقل سن برای تدریس 18 سال می باشد.</p><p style=\"margin-right: 0; text-align: justify\">. &nbsp;برای شروع تدریس در کیدمی باید یک دوره رایگان که در قسمت اعضای ویژه (حداقل 3 ساعت) منتشر می شود را ارائه دهید. در صورتی که زمان دوره کمتر باشد می توانید دو دوره ارائه دهید.</p><p style=\"margin-right: 0; text-align: justify\">. &nbsp;ویدیوی معرفی دوره باید شامل معرفی خود و معرفی دوره و آنچه که می خواهید در دوره تدریس نمایید باشد.</p><p style=\"margin-right: 0; text-align: justify\">. &nbsp;کیفیت صدا و تصویر ویدیوها باید مناسب باشد.</p><p style=\"margin-right: 0; text-align: justify\">توجه داشته باشید، برای دوره های نقدی تعدادی از قسمت های هر دوره به صورت رندم رایگان منتشر می شوند، که تعداد این بخش های رایگان با توجه به زمان دوره تعیین می شود.</p><p style=\"margin-right: 0; text-align: justify\">در صورت برگزاری دوره رایگان، دوره رایگان شما نباید به صورت نقدی در سایت وجود داشته باشد.</p><p style=\"margin-right: 0; text-align: justify\">لطفا در هنگام ضبط ویدیو به این سه نکته توجه نمایید:</p><p style=\"margin-right: 0; text-align: justify\"><strong>. &nbsp;ابتدای هر جلسه موشن وجود دارد.</strong></p><p style=\"margin-right: 0; text-align: justify\"><strong>. &nbsp;بعد از موشن، ویدیو باید از صفحه اول سایت کیدمی یا صفحه دوره شروع شود.</strong></p><p style=\"margin-right: 0; text-align: justify\"><strong>. &nbsp;از ابتدای ویدیو (بعد از موشن) تا انتهای ویدیو لوگوی کیدمی باید در پایین گوشه تصویر قرار گیرد.</strong></p><p style=\"margin-right: 0; text-align: justify\">برای دانلود موشن، لوگو واتر مارک و فایل مربوط به مشخصات دوره از این لینک استفاده نمایید.<span style=\"color: rgba(230, 77, 77, 1)\"><strong>(</strong></span><a href= \"/files/Cooperation/KidemyFiles.rar\"><span style=\"color: rgba(230, 77, 77, 1)\"><strong>لینک دانلود</strong></span></a><span style=\"color: rgba(230, 77, 77, 1)\"><strong>)</strong></span></p><p style=\"margin-right: 0; text-align: justify\">ابتدا عنوان دوره خود را در <a href=\"/userpanel/tickets\"><span style=\"color: rgba(77, 230, 153, 1)\">تیکت &nbsp;</span></a>ارسال نمایید.</p><p style=\"margin-right: 0; text-align: justify\">بعد از تایید عنوان دوره، <strong>ویدیوی معرفی، کارت ملی و فایل مشخصات دوره</strong> خود را به صورت یک فایل rar در <a href=\"https://mega.nz/\"><span style=\"color: rgba(230, 77, 77, 1)\"><strong>این لینک</strong></span></a> آپلود نموده و در تیکت، لینک را ارسال نمایید.</p><p style=\"margin-right: 0; text-align: justify\">پس از طی مراحل فوق و تایید کارشناسان، سمت استاد به شما داده شده و بقیه مراحل کار در اختیار شما قرار خواهد گرفت.</p><p style=\"margin-right: 0; text-align: justify\">در رابطه با دوره های نقدی، مدرسین قیمت پیشنهادی دوره خود را ارائه داده و پس از بررسی قیمت گذاری نهایی می گردد.</p><p style=\"margin-right: 0; text-align: justify\">رزومه شما به عنوان مدرس در وب سایت درج خواهد شد .</p><p style=\"margin-right: 0; text-align: justify\">دقت داشته باشید دوره ای که در سایت کیدمی درج شود متعلق به کیدمی خواهد بود و تحت هیچ شرایطی از سایت حذف نخواهد شد و اگر مدرس دوره را در سایت و یا جای دیگری منتشر کند دوره در کیدمی رایگان خواهد شد.</p>",
                LinkPlace = Domain.Enums.Page.LinkPlace.Header,
                IsPublished = true,
                CreatedDateOnUtc = DateTime.UtcNow,
                IsDeleted = false

            });

            #endregion

        }
    }
}