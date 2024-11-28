using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Kidemy.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class addAllTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AboutUs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PageTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true),
                    OurGoal = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    OurGoalTitle = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    OurGoalDescription = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: true),
                    OurGoalFeatures = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageNumber1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageNumber2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageNumber3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageNumber4 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageNumber5 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AboutUs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AboutUsProgressBar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Percent = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AboutUsProgressBar", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AccountCode",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Receiver = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    ExpiryDateTimeUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountCode", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Adviser",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ConsultationPercentage = table.Column<int>(type: "int", nullable: false),
                    Priority = table.Column<int>(type: "int", nullable: false),
                    IsPublished = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDateOnUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDateOnUtc = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Adviser", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BankAccountCard",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    BankName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OwnerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CardNumber = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false),
                    ShabaNumber = table.Column<string>(type: "nvarchar(24)", maxLength: 24, nullable: false),
                    AccountNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDefault = table.Column<bool>(type: "bit", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDateOnUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDateOnUtc = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankAccountCard", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Banner",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Place = table.Column<int>(type: "int", nullable: false),
                    URL = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    IsPublished = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Banner", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Blog",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Body = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShortDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Slug = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsPublished = table.Column<bool>(type: "bit", nullable: false),
                    ImageName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AuthorId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDateOnUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDateOnUtc = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blog", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BlogTag",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogTag", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cart",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDateOnUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDateOnUtc = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cart", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ContactUs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Address = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Mobile = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreatedDateOnUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDateOnUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactUs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ContactUsForm",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    State = table.Column<int>(type: "int", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AnswererId = table.Column<int>(type: "int", nullable: true),
                    ParentId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactUsForm", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContactUsForm_ContactUsForm_ParentId",
                        column: x => x.ParentId,
                        principalTable: "ContactUsForm",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Course",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShortDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Slug = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Level = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    IsOffer = table.Column<bool>(type: "bit", nullable: false),
                    MasterId = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    VideosTotalTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    HasCertificate = table.Column<bool>(type: "bit", nullable: false),
                    AllowComenting = table.Column<bool>(type: "bit", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    IsPublished = table.Column<bool>(type: "bit", nullable: false),
                    MasterCommissionPercentage = table.Column<int>(type: "int", nullable: false),
                    FileSuffix = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DemoVideoFileName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDateOnUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDateOnUtc = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Course", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CourseCategory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LogoFileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ParentCourseCategoryId = table.Column<int>(type: "int", nullable: true),
                    IsPopular = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDateOnUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDateOnUtc = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseCategory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CourseCategory_CourseCategory_ParentCourseCategoryId",
                        column: x => x.ParentCourseCategoryId,
                        principalTable: "CourseCategory",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CourseNotification",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourseId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    CourseNotificationType = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDateOnUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDateOnUtc = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseNotification", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CourseRequest",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PreferedMasterId = table.Column<int>(type: "int", nullable: true),
                    RequestedById = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SelectedTags = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsConfirm = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDateOnUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDateOnUtc = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseRequest", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CourseTag",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseTag", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CourseVideoCategory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CourseId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseVideoCategory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Discount",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Value = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsPercentage = table.Column<bool>(type: "bit", nullable: false),
                    StartDateTimeOnUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndDateTimeOnUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    AutoUse = table.Column<bool>(type: "bit", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDateOnUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDateOnUtc = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Discount", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DynamicText",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Position = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DynamicText", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FAQ",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Answer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDateOnUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDateOnUtc = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FAQ", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FavouriteCourse",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourseId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDateOnUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDateOnUtc = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FavouriteCourse", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InPersonCourse",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    Slug = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShortDescription = table.Column<string>(type: "nvarchar(750)", maxLength: 750, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageName = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    CourseRequirements = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CourseTopics = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CourseGoals = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CourseAudience = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AttachmentFileName = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true),
                    MetaTag = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CertificateAvailable = table.Column<bool>(type: "bit", nullable: false),
                    IsPublished = table.Column<bool>(type: "bit", nullable: false),
                    PrivateClassAvailable = table.Column<bool>(type: "bit", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Priority = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDateOnUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDateOnUtc = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InPersonCourse", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InPersonCourseTag",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDateOnUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDateOnUtc = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InPersonCourseTag", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InPersonCourseVisit",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourseId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    IP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDateOnUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDateOnUtc = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InPersonCourseVisit", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Link",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    LinkType = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDateOnUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDateOnUtc = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Link", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LocalizedProperty",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EntityId = table.Column<int>(type: "int", nullable: false),
                    EntityName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CultureName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Key = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocalizedProperty", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MasterContract",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    IsPublished = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDateOnUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDateOnUtc = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MasterContract", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MasterNotification",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SenderId = table.Column<int>(type: "int", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DemoVideoFileName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDateOnUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDateOnUtc = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MasterNotification", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Newsletter",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(350)", maxLength: 350, nullable: false),
                    Ip = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDateOnUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDateOnUtc = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Newsletter", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Notification",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SenderId = table.Column<int>(type: "int", nullable: false),
                    Subject = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDateOnUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDateOnUtc = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notification", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DiscountedTotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    IsPaid = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDateOnUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDateOnUtc = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Page",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Body = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Slug = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsPublished = table.Column<bool>(type: "bit", nullable: false),
                    LinkPlace = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDateOnUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDateOnUtc = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Page", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Permission",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UniqueName = table.Column<string>(type: "nvarchar(350)", maxLength: 350, nullable: false),
                    ParentId = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permission", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    UniqueName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDateOnUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDateOnUtc = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SettlementTransaction",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TransactionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CardNumber = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false),
                    AccountNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TrackingCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDateOnUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDateOnUtc = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SettlementTransaction", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SiteSetting",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(350)", maxLength: 350, nullable: true),
                    Mobile = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    Mobile2 = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(800)", maxLength: 800, nullable: true),
                    LogoName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CollectionManagement = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FooterDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CopyrightDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DefaultPaymentMethod = table.Column<int>(type: "int", nullable: false),
                    NewsletterDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WithdrawRequestMinimumAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SiteSetting", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Slider",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(900)", maxLength: 900, nullable: true),
                    ImageName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Priority = table.Column<int>(type: "int", nullable: false),
                    SliderPlace = table.Column<int>(type: "int", nullable: false),
                    URL = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    IsPublished = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDateOnUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDateOnUtc = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Slider", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SmsProvider",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApiKey = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SmsProviderType = table.Column<int>(type: "int", nullable: false),
                    IsDefault = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SmsProvider", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SocialMedia",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Priority = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Link = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    ImageName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDateOnUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDateOnUtc = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SocialMedia", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Survey",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    IsPublished = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDateOnUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDateOnUtc = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Survey", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ticket",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    OwnerUserId = table.Column<int>(type: "int", nullable: false),
                    IsClosed = table.Column<bool>(type: "bit", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Priority = table.Column<int>(type: "int", nullable: false),
                    Section = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDateOnUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDateOnUtc = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ticket", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(350)", maxLength: 350, nullable: true),
                    Mobile = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Password = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    IsEmailActive = table.Column<bool>(type: "bit", nullable: false),
                    IsMobileActive = table.Column<bool>(type: "bit", nullable: false),
                    AvatarName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IsBan = table.Column<bool>(type: "bit", nullable: false),
                    UnableToWithdraw = table.Column<bool>(type: "bit", nullable: false),
                    BirthDateOnUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDateOnUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDateOnUtc = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VIPPlan",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DurationDay = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsPublished = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDateOnUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDateOnUtc = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VIPPlan", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WalletTransaction",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: true),
                    CourseId = table.Column<int>(type: "int", nullable: true),
                    IP = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TransactionType = table.Column<int>(type: "int", nullable: false),
                    TransactionCase = table.Column<int>(type: "int", nullable: false),
                    TransactionWay = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Balance = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsSuccess = table.Column<bool>(type: "bit", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(700)", maxLength: 700, nullable: true),
                    RefId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PlanId = table.Column<int>(type: "int", nullable: true),
                    ConsulationRequestId = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDateOnUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDateOnUtc = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WalletTransaction", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WithdrawRequest",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RefId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    WalletTransactionId = table.Column<int>(type: "int", nullable: false),
                    DestinationBankAccountCardId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDateOnUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDateOnUtc = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WithdrawRequest", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AdviserAvailableDate",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AdviserId = table.Column<int>(type: "int", nullable: false),
                    DayOfWeek = table.Column<int>(type: "int", nullable: false),
                    StartTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    EndTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdviserAvailableDate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AdviserAvailableDate_Adviser_AdviserId",
                        column: x => x.AdviserId,
                        principalTable: "Adviser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AdviserConsultationType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AdviserId = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsPublished = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDateOnUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDateOnUtc = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdviserConsultationType", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AdviserConsultationType_Adviser_AdviserId",
                        column: x => x.AdviserId,
                        principalTable: "Adviser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BlogComment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Message = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    IsConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    CommentedById = table.Column<int>(type: "int", nullable: false),
                    BlogId = table.Column<int>(type: "int", nullable: false),
                    ReplyCommentId = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDateOnUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDateOnUtc = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogComment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BlogComment_BlogComment_ReplyCommentId",
                        column: x => x.ReplyCommentId,
                        principalTable: "BlogComment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BlogComment_Blog_BlogId",
                        column: x => x.BlogId,
                        principalTable: "Blog",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BlogTagMapping",
                columns: table => new
                {
                    BlogId = table.Column<int>(type: "int", nullable: false),
                    TagId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogTagMapping", x => new { x.BlogId, x.TagId });
                    table.ForeignKey(
                        name: "FK_BlogTagMapping_BlogTag_TagId",
                        column: x => x.TagId,
                        principalTable: "BlogTag",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BlogTagMapping_Blog_BlogId",
                        column: x => x.BlogId,
                        principalTable: "Blog",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CartItem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CartId = table.Column<int>(type: "int", nullable: false),
                    CourseId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDateOnUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDateOnUtc = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CartItem_Cart_CartId",
                        column: x => x.CartId,
                        principalTable: "Cart",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CourseComment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Message = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Score = table.Column<int>(type: "int", maxLength: 15, nullable: false),
                    IsConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    CommentedById = table.Column<int>(type: "int", nullable: false),
                    CourseId = table.Column<int>(type: "int", nullable: false),
                    ReplyCommnetId = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDateOnUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDateOnUtc = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseComment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CourseComment_CourseComment_ReplyCommnetId",
                        column: x => x.ReplyCommnetId,
                        principalTable: "CourseComment",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CourseComment_Course_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Course",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CourseQuestion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AskedById = table.Column<int>(type: "int", nullable: false),
                    CourseId = table.Column<int>(type: "int", nullable: false),
                    IsClosed = table.Column<bool>(type: "bit", nullable: false),
                    IsConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    UserReaction = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDateOnUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDateOnUtc = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseQuestion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CourseQuestion_Course_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Course",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CourseUserMapping",
                columns: table => new
                {
                    CourseId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseUserMapping", x => new { x.CourseId, x.UserId });
                    table.ForeignKey(
                        name: "FK_CourseUserMapping_Course_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Course",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CourseView",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourseId = table.Column<int>(type: "int", nullable: false),
                    UserIp = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseView", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CourseView_Course_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Course",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CourseCategoryMapping",
                columns: table => new
                {
                    CourseId = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseCategoryMapping", x => new { x.CourseId, x.CategoryId });
                    table.ForeignKey(
                        name: "FK_CourseCategoryMapping_CourseCategory_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "CourseCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseCategoryMapping_Course_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Course",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CourseRequestMasterVolunteer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourseRequestId = table.Column<int>(type: "int", nullable: false),
                    MasterId = table.Column<int>(type: "int", nullable: false),
                    MasterDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AdminDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDateOnUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDateOnUtc = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseRequestMasterVolunteer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CourseRequestMasterVolunteer_CourseRequest_CourseRequestId",
                        column: x => x.CourseRequestId,
                        principalTable: "CourseRequest",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CourseRequestVote",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourseRequestId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    IsAgree = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDateOnUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDateOnUtc = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseRequestVote", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CourseRequestVote_CourseRequest_CourseRequestId",
                        column: x => x.CourseRequestId,
                        principalTable: "CourseRequest",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CourseTagMapping",
                columns: table => new
                {
                    CourseId = table.Column<int>(type: "int", nullable: false),
                    TagId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseTagMapping", x => new { x.CourseId, x.TagId });
                    table.ForeignKey(
                        name: "FK_CourseTagMapping_CourseTag_TagId",
                        column: x => x.TagId,
                        principalTable: "CourseTag",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseTagMapping_Course_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Course",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CourseVideo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ThumbnailImageName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    VideoFileName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    VideoLength = table.Column<TimeSpan>(type: "time", maxLength: 10, nullable: false),
                    Priority = table.Column<int>(type: "int", nullable: false),
                    VideoCategoryId = table.Column<int>(type: "int", nullable: false),
                    CourseId = table.Column<int>(type: "int", nullable: false),
                    IsFree = table.Column<bool>(type: "bit", nullable: false),
                    IsPublished = table.Column<bool>(type: "bit", nullable: false),
                    AttachFileName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDateOnUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDateOnUtc = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseVideo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CourseVideo_CourseVideoCategory_VideoCategoryId",
                        column: x => x.VideoCategoryId,
                        principalTable: "CourseVideoCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseVideo_Course_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Course",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DiscountLimitation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DiscountId = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDateOnUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDateOnUtc = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiscountLimitation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DiscountLimitation_Discount_DiscountId",
                        column: x => x.DiscountId,
                        principalTable: "Discount",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InPersonCourseCategoryMapping",
                columns: table => new
                {
                    CourseId = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InPersonCourseCategoryMapping", x => new { x.CourseId, x.CategoryId });
                    table.ForeignKey(
                        name: "FK_InPersonCourseCategoryMapping_CourseCategory_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "CourseCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InPersonCourseCategoryMapping_InPersonCourse_CourseId",
                        column: x => x.CourseId,
                        principalTable: "InPersonCourse",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InPersonCourseTagMapping",
                columns: table => new
                {
                    CourseId = table.Column<int>(type: "int", nullable: false),
                    TagId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InPersonCourseTagMapping", x => new { x.CourseId, x.TagId });
                    table.ForeignKey(
                        name: "FK_InPersonCourseTagMapping_InPersonCourseTag_TagId",
                        column: x => x.TagId,
                        principalTable: "InPersonCourseTag",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InPersonCourseTagMapping_InPersonCourse_CourseId",
                        column: x => x.CourseId,
                        principalTable: "InPersonCourse",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NotificationMasterMapping",
                columns: table => new
                {
                    MasterNotificationId = table.Column<int>(type: "int", nullable: false),
                    MasterId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDateOnUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDateOnUtc = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationMasterMapping", x => new { x.MasterNotificationId, x.MasterId });
                    table.ForeignKey(
                        name: "FK_NotificationMasterMapping_MasterNotification_MasterNotificationId",
                        column: x => x.MasterNotificationId,
                        principalTable: "MasterNotification",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NotificationUserMapping",
                columns: table => new
                {
                    NotificationId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationUserMapping", x => new { x.NotificationId, x.UserId });
                    table.ForeignKey(
                        name: "FK_NotificationUserMapping_Notification_NotificationId",
                        column: x => x.NotificationId,
                        principalTable: "Notification",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderItem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    CourseId = table.Column<int>(type: "int", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DiscountedUnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDateOnUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDateOnUtc = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItem_Order_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Order",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RolePermissionMapping",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    PermissionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolePermissionMapping", x => new { x.RoleId, x.PermissionId });
                    table.ForeignKey(
                        name: "FK_RolePermissionMapping_Permission_PermissionId",
                        column: x => x.PermissionId,
                        principalTable: "Permission",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RolePermissionMapping_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SurveyQuestion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    SurveyId = table.Column<int>(type: "int", nullable: false),
                    Options = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Priority = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDateOnUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDateOnUtc = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SurveyQuestion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SurveyQuestion_Survey_SurveyId",
                        column: x => x.SurveyId,
                        principalTable: "Survey",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TicketMessage",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TicketId = table.Column<int>(type: "int", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SenderId = table.Column<int>(type: "int", nullable: false),
                    SeenByClient = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDateOnUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDateOnUtc = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketMessage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TicketMessage_Ticket_TicketId",
                        column: x => x.TicketId,
                        principalTable: "Ticket",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Master",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Bio = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FatherName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    NationalIDNumber = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    IdentificationNumber = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    PlaceOfIssuance = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    PostalCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdentificationFileName = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    BlockedAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDateOnUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDateOnUtc = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Master", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Master_User_Id",
                        column: x => x.Id,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRoleMapping",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoleMapping", x => new { x.RoleId, x.UserId });
                    table.ForeignKey(
                        name: "FK_UserRoleMapping_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VIPMember",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    VIPPlanId = table.Column<int>(type: "int", nullable: false),
                    MembershipStartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MembershipEndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDateOnUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDateOnUtc = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VIPMember", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VIPMember_VIPPlan_VIPPlanId",
                        column: x => x.VIPPlanId,
                        principalTable: "VIPPlan",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ConsultationRequest",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequestedByUserId = table.Column<int>(type: "int", nullable: false),
                    AdviserId = table.Column<int>(type: "int", nullable: false),
                    SelectedDateId = table.Column<int>(type: "int", nullable: false),
                    AdviserConsultationTypeId = table.Column<int>(type: "int", nullable: false),
                    FixedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Topic = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    State = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDateOnUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDateOnUtc = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConsultationRequest", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConsultationRequest_AdviserAvailableDate_SelectedDateId",
                        column: x => x.SelectedDateId,
                        principalTable: "AdviserAvailableDate",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ConsultationRequest_AdviserConsultationType_AdviserConsultationTypeId",
                        column: x => x.AdviserConsultationTypeId,
                        principalTable: "AdviserConsultationType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ConsultationRequest_Adviser_AdviserId",
                        column: x => x.AdviserId,
                        principalTable: "Adviser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CourseCommentReport",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CommentId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsAdminChecked = table.Column<bool>(type: "bit", nullable: false),
                    CheckedById = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDateOnUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDateOnUtc = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseCommentReport", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CourseCommentReport_CourseComment_CommentId",
                        column: x => x.CommentId,
                        principalTable: "CourseComment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CourseQuestionAnswer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuestionId = table.Column<int>(type: "int", nullable: false),
                    AnsweredById = table.Column<int>(type: "int", nullable: false),
                    Answer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDateOnUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDateOnUtc = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseQuestionAnswer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CourseQuestionAnswer_CourseQuestion_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "CourseQuestion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DiscountLimitationCategoryMapping",
                columns: table => new
                {
                    DiscountLimitationId = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiscountLimitationCategoryMapping", x => new { x.DiscountLimitationId, x.CategoryId });
                    table.ForeignKey(
                        name: "FK_DiscountLimitationCategoryMapping_DiscountLimitation_DiscountLimitationId",
                        column: x => x.DiscountLimitationId,
                        principalTable: "DiscountLimitation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DiscountLimitationCourseMapping",
                columns: table => new
                {
                    DiscountLimitationId = table.Column<int>(type: "int", nullable: false),
                    CourseId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiscountLimitationCourseMapping", x => new { x.DiscountLimitationId, x.CourseId });
                    table.ForeignKey(
                        name: "FK_DiscountLimitationCourseMapping_DiscountLimitation_DiscountLimitationId",
                        column: x => x.DiscountLimitationId,
                        principalTable: "DiscountLimitation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DiscountLimitationUsageCountMapping",
                columns: table => new
                {
                    DiscountLimitationId = table.Column<int>(type: "int", nullable: false),
                    Count = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiscountLimitationUsageCountMapping", x => x.DiscountLimitationId);
                    table.ForeignKey(
                        name: "FK_DiscountLimitationUsageCountMapping_DiscountLimitation_DiscountLimitationId",
                        column: x => x.DiscountLimitationId,
                        principalTable: "DiscountLimitation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DiscountLimitationUserMapping",
                columns: table => new
                {
                    DiscountLimitationId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiscountLimitationUserMapping", x => new { x.DiscountLimitationId, x.UserId });
                    table.ForeignKey(
                        name: "FK_DiscountLimitationUserMapping_DiscountLimitation_DiscountLimitationId",
                        column: x => x.DiscountLimitationId,
                        principalTable: "DiscountLimitation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DiscountUsage",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UsedByUserId = table.Column<int>(type: "int", nullable: false),
                    DiscountId = table.Column<int>(type: "int", nullable: false),
                    OrderItemId = table.Column<int>(type: "int", nullable: true),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    ReducedAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsFinally = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDateOnUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDateOnUtc = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiscountUsage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DiscountUsage_Discount_DiscountId",
                        column: x => x.DiscountId,
                        principalTable: "Discount",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DiscountUsage_OrderItem_OrderItemId",
                        column: x => x.OrderItemId,
                        principalTable: "OrderItem",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DiscountUsage_Order_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Order",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SurveyAnswer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AnswerText = table.Column<string>(type: "nvarchar(900)", maxLength: 900, nullable: false),
                    AnswererId = table.Column<int>(type: "int", nullable: false),
                    QuestionId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDateOnUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDateOnUtc = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SurveyAnswer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SurveyAnswer_SurveyQuestion_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "SurveyQuestion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UploadedMasterContract",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MasterId = table.Column<int>(type: "int", nullable: false),
                    MasterContractId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDateOnUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDateOnUtc = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UploadedMasterContract", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UploadedMasterContract_MasterContract_MasterContractId",
                        column: x => x.MasterContractId,
                        principalTable: "MasterContract",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UploadedMasterContract_Master_MasterId",
                        column: x => x.MasterId,
                        principalTable: "Master",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AboutUs",
                columns: new[] { "Id", "Description", "ImageNumber1", "ImageNumber2", "ImageNumber3", "ImageNumber4", "ImageNumber5", "IsDeleted", "OurGoal", "OurGoalDescription", "OurGoalFeatures", "OurGoalTitle", "PageTitle", "Title" },
                values: new object[] { 1, "توضیحات درباره ما", "/assets/images/about/05.jpg", "/assets/images/about/03.jpg", "/assets/images/about/09.jpg", "/assets/images/about/10.jpg", "/assets/images/about/06.jpg", false, null, null, null, null, null, "عنوان درباره ما" });

            migrationBuilder.InsertData(
                table: "ContactUs",
                columns: new[] { "Id", "Address", "CreatedDateOnUtc", "Email", "IsDeleted", "Mobile", "UpdatedDateOnUtc" },
                values: new object[] { 1, "تهران", new DateTime(2024, 5, 5, 11, 40, 48, 944, DateTimeKind.Local).AddTicks(4967), "Kidemy.sample@gmail.com", false, "09121111111", new DateTime(2024, 5, 5, 11, 40, 48, 944, DateTimeKind.Local).AddTicks(4974) });

            migrationBuilder.InsertData(
                table: "DynamicText",
                columns: new[] { "Id", "IsDeleted", "Position", "Text" },
                values: new object[,]
                {
                    { 1, false, 1, "  شما کاربر گرامی می‌توانید برای دسترسی به امکانات ویژه سایت، اشتراک ویژه را فراهم نمایید." },
                    { 2, false, 2, "برای نهایی کردن فاکتور باید کیف پول خود را به میزان مبلغ قابل پرداخت فاکتور شارژ نمایید.برای ادامه بر روی دکمه زیر کلیک نمایید ." },
                    { 3, false, 3, "درحال حاضر شما عضو ویژه سایت نمی باشید ! درصورت تمایل برای استفاده از امکانات اعضای ویژه باید اشتراک خریداری نمایید" },
                    { 4, false, 4, "شما عضو ویژه سایت می باشید تا تاریخ " },
                    { 5, false, 5, "چرا آموزش برنامه نویسی به\r\nکودکان و نوجوانان !؟" },
                    { 6, false, 6, "آموزش برنامه نویسی به کودکان و نوجوانان به دلیل اینکه در این سنین ذهنیت کودکان و نوجوانان برای یادگیری برنامه نویسی بسیار مناسب است و این موضوع باعث می شود که کودکان و نوجوانان بتوانند به راحتی و با سرعت بالا برنامه نویسی را یاد بگیرند." },
                    { 7, false, 7, "مدرس مجرب" },
                    { 8, false, 8, "ارائه مدرک" },
                    { 9, false, 9, "جذب مدرس" },
                    { 10, false, 10, "هر موضوعی را در هر زمان مطالعه کنید. هزاران دوره آموزشی را با کمترین قیمت جستجو کنید!" },
                    { 11, false, 11, "مشاهده دوره های جدید و 🔥 در جشنواره" },
                    { 12, false, 12, "60" },
                    { 13, false, 13, "ارائه راهکارها و شرایط سخت تایپ به پایان رسد وزمان مورد نیاز شامل حروفچینی دستاوردهای اصلی و جوابگوی سوالات پیوسته اهل دنیای موجود طراحی اساسا مورد استفاده قرار گیرد." }
                });

            migrationBuilder.InsertData(
                table: "Link",
                columns: new[] { "Id", "Address", "CreatedDateOnUtc", "IsDeleted", "LinkType", "Title", "UpdatedDateOnUtc" },
                values: new object[] { 1, "/FAQs", new DateTime(2024, 5, 5, 8, 10, 48, 944, DateTimeKind.Utc).AddTicks(5214), false, 2, "سوالات متداول", new DateTime(2024, 5, 5, 8, 10, 48, 944, DateTimeKind.Utc).AddTicks(5213) });

            migrationBuilder.InsertData(
                table: "Page",
                columns: new[] { "Id", "Body", "CreatedDateOnUtc", "IsDeleted", "IsPublished", "LinkPlace", "Slug", "Title", "UpdatedDateOnUtc" },
                values: new object[] { 1, "<h2 style=\"margin-right: 0\"><span style=\"color: rgba(76, 230, 230, 1)\"><strong>مراحل همکاری در کیدمی</strong></span></h2><p style=\"margin-right: 0; text-align: justify\">کیدمی یک پلتفرم آموزشی در بستر تجارت الکترونیک می باشد که از اساتید با تجربه در این حوزه دعوت به همکاری می نماید.</p><p style=\"margin-right: 0; text-align: justify\">اگر تمایل دارید در کیدمی در زمینه تولید محتوای آموزشی فعالیت نمایید، می توانید از طریق&nbsp;تیکت (در پنل کاربری) &nbsp;با کارشناسان ما در ارتباط باشید.</p><p style=\"margin-right: 0; text-align: justify\">جهت شروع همکاری به این نکات توجه داشته باشید:</p><p style=\"margin-right: 0; text-align: justify\">. &nbsp;حداقل سن برای تدریس 18 سال می باشد.</p><p style=\"margin-right: 0; text-align: justify\">. &nbsp;برای شروع تدریس در کیدمی باید یک دوره رایگان که در قسمت اعضای ویژه (حداقل 3 ساعت) منتشر می شود را ارائه دهید. در صورتی که زمان دوره کمتر باشد می توانید دو دوره ارائه دهید.</p><p style=\"margin-right: 0; text-align: justify\">. &nbsp;ویدیوی معرفی دوره باید شامل معرفی خود و معرفی دوره و آنچه که می خواهید در دوره تدریس نمایید باشد.</p><p style=\"margin-right: 0; text-align: justify\">. &nbsp;کیفیت صدا و تصویر ویدیوها باید مناسب باشد.</p><p style=\"margin-right: 0; text-align: justify\">توجه داشته باشید، برای دوره های نقدی تعدادی از قسمت های هر دوره به صورت رندم رایگان منتشر می شوند، که تعداد این بخش های رایگان با توجه به زمان دوره تعیین می شود.</p><p style=\"margin-right: 0; text-align: justify\">در صورت برگزاری دوره رایگان، دوره رایگان شما نباید به صورت نقدی در سایت وجود داشته باشد.</p><p style=\"margin-right: 0; text-align: justify\">لطفا در هنگام ضبط ویدیو به این سه نکته توجه نمایید:</p><p style=\"margin-right: 0; text-align: justify\"><strong>. &nbsp;ابتدای هر جلسه موشن وجود دارد.</strong></p><p style=\"margin-right: 0; text-align: justify\"><strong>. &nbsp;بعد از موشن، ویدیو باید از صفحه اول سایت کیدمی یا صفحه دوره شروع شود.</strong></p><p style=\"margin-right: 0; text-align: justify\"><strong>. &nbsp;از ابتدای ویدیو (بعد از موشن) تا انتهای ویدیو لوگوی کیدمی باید در پایین گوشه تصویر قرار گیرد.</strong></p><p style=\"margin-right: 0; text-align: justify\">برای دانلود موشن، لوگو واتر مارک و فایل مربوط به مشخصات دوره از این لینک استفاده نمایید.<span style=\"color: rgba(230, 77, 77, 1)\"><strong>(</strong></span><a href= \"/files/Cooperation/KidemyFiles.rar\"><span style=\"color: rgba(230, 77, 77, 1)\"><strong>لینک دانلود</strong></span></a><span style=\"color: rgba(230, 77, 77, 1)\"><strong>)</strong></span></p><p style=\"margin-right: 0; text-align: justify\">ابتدا عنوان دوره خود را در <a href=\"/userpanel/tickets\"><span style=\"color: rgba(77, 230, 153, 1)\">تیکت &nbsp;</span></a>ارسال نمایید.</p><p style=\"margin-right: 0; text-align: justify\">بعد از تایید عنوان دوره، <strong>ویدیوی معرفی، کارت ملی و فایل مشخصات دوره</strong> خود را به صورت یک فایل rar در <a href=\"https://mega.nz/\"><span style=\"color: rgba(230, 77, 77, 1)\"><strong>این لینک</strong></span></a> آپلود نموده و در تیکت، لینک را ارسال نمایید.</p><p style=\"margin-right: 0; text-align: justify\">پس از طی مراحل فوق و تایید کارشناسان، سمت استاد به شما داده شده و بقیه مراحل کار در اختیار شما قرار خواهد گرفت.</p><p style=\"margin-right: 0; text-align: justify\">در رابطه با دوره های نقدی، مدرسین قیمت پیشنهادی دوره خود را ارائه داده و پس از بررسی قیمت گذاری نهایی می گردد.</p><p style=\"margin-right: 0; text-align: justify\">رزومه شما به عنوان مدرس در وب سایت درج خواهد شد .</p><p style=\"margin-right: 0; text-align: justify\">دقت داشته باشید دوره ای که در سایت کیدمی درج شود متعلق به کیدمی خواهد بود و تحت هیچ شرایطی از سایت حذف نخواهد شد و اگر مدرس دوره را در سایت و یا جای دیگری منتشر کند دوره در کیدمی رایگان خواهد شد.</p>", new DateTime(2024, 5, 5, 8, 10, 48, 944, DateTimeKind.Utc).AddTicks(5235), false, true, 0, "همکاری-با-کیدمی", "همکاری با کیدمی", new DateTime(2024, 5, 5, 8, 10, 48, 944, DateTimeKind.Utc).AddTicks(5232) });

            migrationBuilder.InsertData(
                table: "Permission",
                columns: new[] { "Id", "IsDeleted", "ParentId", "UniqueName" },
                values: new object[,]
                {
                    { 1, false, null, "AdminPanel" },
                    { 2, false, 1, "ManageUsers" },
                    { 3, false, 2, "UserList" },
                    { 4, false, 2, "CreateUser" },
                    { 5, false, 2, "UpdateUser" },
                    { 6, false, 2, "DeleteUser" },
                    { 7, false, 1, "ManageRoles" },
                    { 8, false, 7, "RolesList" },
                    { 9, false, 7, "CreateRole" },
                    { 10, false, 7, "UpdateRole" },
                    { 11, false, 7, "DeleteRole" },
                    { 12, false, 1, "ManageWallet" },
                    { 13, false, 12, "ChargeWallet" },
                    { 14, false, 12, "WithdrawFromWallet" },
                    { 15, false, 1, "ManageSocialMedia" },
                    { 16, false, 15, "SocialMediaList" },
                    { 17, false, 15, "CreateSocialMedia" },
                    { 18, false, 15, "UpdateSocialMedia" },
                    { 19, false, 15, "DeleteSocialMedia" },
                    { 20, false, 1, "ManageContactUs" },
                    { 21, false, 20, "UpdateContactUs" },
                    { 22, false, 1, "ManageTicket" },
                    { 23, false, 22, "UpdateTicket" },
                    { 24, false, 22, "TicketDetails" },
                    { 25, false, 22, "TicketList" },
                    { 26, false, 22, "CreateTicket" },
                    { 27, false, 20, "ContactUsFormList" },
                    { 28, false, 20, "ContactUsFormReply" },
                    { 29, false, 20, "DeleteContactUsForm" },
                    { 30, false, 56, "UpdateAboutUs" },
                    { 31, false, 1, "ManagePages" },
                    { 32, false, 31, "CreatePage" },
                    { 33, false, 31, "UpdatePage" },
                    { 34, false, 31, "PagesList" },
                    { 35, false, 31, "DeletePage" },
                    { 36, false, 1, "ManageNewsletter" },
                    { 37, false, 36, "MembersList_Newsletter" },
                    { 38, false, 36, "DeleteMember_Newsletter" },
                    { 39, false, 1, "ManageSurvey" },
                    { 40, false, 39, "SurveyList" },
                    { 41, false, 39, "CreateSurvey" },
                    { 42, false, 39, "UpdateSurvey" },
                    { 43, false, 39, "DeleteSurvey" },
                    { 44, false, 39, "SurveyQuestionsList" },
                    { 45, false, 39, "CreateSurveyQuestion" },
                    { 46, false, 39, "UpdateSurveyQuestion" },
                    { 47, false, 39, "DeleteSurveyQuestion" },
                    { 48, false, 39, "SurveyAnswersList" },
                    { 49, false, 39, "SurveyAnswerDetails" },
                    { 50, false, 1, "ManageLinks" },
                    { 51, false, 50, "CreateLink" },
                    { 52, false, 50, "UpdateLink" },
                    { 53, false, 50, "LinksList" },
                    { 54, false, 50, "DeleteLink" },
                    { 55, false, 1, "UpdateSiteSetting" },
                    { 56, false, 1, "ManageAboutUs" },
                    { 57, false, 56, "CreateProgressBar" },
                    { 58, false, 56, "ProgressBarList" },
                    { 59, false, 56, "DeleteProgressBar" },
                    { 60, false, 56, "UpdateProgresBar" },
                    { 64, false, 1, "ManageBlogs" },
                    { 65, false, 64, "CreateBlog" },
                    { 66, false, 64, "UpdateBlog" },
                    { 67, false, 64, "BlogsList" },
                    { 68, false, 64, "DeleteBlog" },
                    { 69, false, 1, "ManageSmsProvide" },
                    { 70, false, 69, "SmsProviderList" },
                    { 71, false, 69, "UpdateSmsProvider" },
                    { 72, false, 1, "ManageFAQs" },
                    { 73, false, 72, "CreateFAQ" },
                    { 74, false, 72, "UpdateFAQ" },
                    { 75, false, 72, "FAQsList" },
                    { 76, false, 72, "DeleteFAQ" },
                    { 77, false, 1, "ManageSendNotification" },
                    { 78, false, 77, "SendNotification" },
                    { 79, false, 77, "FilterUsersForSendNotification" },
                    { 80, false, 1, "ManageDiscount" },
                    { 81, false, 80, "DiscountList" },
                    { 82, false, 80, "CreateDiscount" },
                    { 83, false, 80, "UpdateDiscount" },
                    { 84, false, 80, "DeleteDiscount" },
                    { 85, false, 80, "DiscountLimitationList" },
                    { 86, false, 80, "CreateDiscountLimitation" },
                    { 87, false, 80, "UpdateDiscountLimitation" },
                    { 88, false, 80, "DeleteDiscountLimitation" },
                    { 89, false, 1, "ManageMasters" },
                    { 90, false, 89, "MasterList" },
                    { 91, false, 1, "ManageDynamicTexts" },
                    { 92, false, 91, "DynamicTextsList" },
                    { 93, false, 91, "UpdateDynamicText" },
                    { 94, false, 1, "ManageVIPServices" },
                    { 95, false, 94, "VIPPlansList" },
                    { 96, false, 94, "CreateVIPPlan" },
                    { 97, false, 94, "UpdateVIPPlan" },
                    { 98, false, 94, "DeleteVIPPlan" },
                    { 99, false, 89, "MasterDetails" },
                    { 100, false, 1, "ManageMastersContracts" },
                    { 101, false, 100, "MasterContractsList" },
                    { 102, false, 100, "CreateMasterContract" },
                    { 103, false, 100, "UpdateMasterContract" },
                    { 104, false, 100, "MasterContractsDetails" },
                    { 105, false, 100, "PendingContractsList" },
                    { 106, false, 1, "ManageCourseQuestion" },
                    { 107, false, 106, "CourseQuestionList" },
                    { 108, false, 106, "CourseQuestionMessagesList" },
                    { 109, false, 106, "CloseCourseQuestion" },
                    { 110, false, 106, "OpenCourseQuestion" },
                    { 111, false, 106, "ConfirmCourseQuestion" },
                    { 112, false, 106, "DeleteCourseQuestion" },
                    { 113, false, 106, "DeleteCourseQuestionAnswer" },
                    { 114, false, 106, "ConfirmCourseQuestionAnswer" },
                    { 115, false, 106, "NotConfirmedCourseQuestionAndAnswersList" },
                    { 116, false, 106, "CourseQuestionsDoesNotAnsweredByTeacherAfter48HoursList" },
                    { 117, false, 1, "ManageCourseCategories" },
                    { 118, false, 117, "CourseCategories" },
                    { 119, false, 117, "CreateNewCourseCategory" },
                    { 120, false, 117, "DeleteCourseCategory" },
                    { 121, false, 118, "UpdateCourseCategory" },
                    { 122, false, 1, "ManageCourses" },
                    { 123, false, 122, "CoursesList" },
                    { 124, false, 122, "CreateNewCourse" },
                    { 125, false, 122, "DeleteCourse" },
                    { 126, false, 122, "UpdateCourse" },
                    { 127, false, 2, "UserProfile" },
                    { 128, false, 1, "ManageAccountBankCard" },
                    { 129, false, 128, "BankAccountCardList" },
                    { 130, false, 128, "ConfirmBankAccountCard" },
                    { 131, false, 128, "RejectBankAccountCard" },
                    { 132, false, 1, "ManageSettlementTransaction" },
                    { 133, false, 132, "AddSettlementTransaction" },
                    { 134, false, 132, "EditSettlementTransaction" },
                    { 135, false, 2, "UnableToWithdrawUser" },
                    { 136, false, 2, "AbleToWithdrawUser" },
                    { 140, false, 1, "ManageOrders" },
                    { 141, false, 140, "OrdersList" },
                    { 142, false, 140, "OrderDetails" },
                    { 144, false, 1, "ManageWithdrawRequests" },
                    { 145, false, 144, "ListOfWithdrawRequests" },
                    { 146, false, 144, "AcceptWithdrawRequest" },
                    { 147, false, 144, "RejectWithdrawRequest" },
                    { 148, false, 1, "ManageAdviser" },
                    { 149, false, 148, "AdviserList" },
                    { 150, false, 148, "AddAdviser" },
                    { 151, false, 148, "EditAdviser" },
                    { 152, false, 148, "DeleteAdviser" },
                    { 153, false, 89, "SetBlockedAmount" },
                    { 155, false, 1, "ManageSliders" },
                    { 156, false, 155, "SlidersList" },
                    { 157, false, 155, "CreateSlider" },
                    { 158, false, 155, "UpdateSlider" },
                    { 159, false, 155, "DeleteSlider" },
                    { 160, false, 1, "ManageBanners" },
                    { 161, false, 160, "BannersList" },
                    { 162, false, 160, "CreateBanner" },
                    { 163, false, 160, "UpdateBanner" },
                    { 164, false, 160, "DeleteBanner" },
                    { 165, false, 1, "ManageCourseRequests" },
                    { 166, false, 165, "CourseRequestsList" },
                    { 167, false, 165, "ConfirmCourseRequest" },
                    { 168, false, 165, "NotConfirmCourseRequest" },
                    { 169, false, 165, "CourseRequestDetails" },
                    { 170, false, 1, "ManageMastersNotifications" },
                    { 171, false, 170, "SendMastersNotifications" },
                    { 172, false, 1, "Master" },
                    { 173, false, 1, "ManageConsultationRequest" },
                    { 174, false, 173, "ConsultationRequestList" },
                    { 175, false, 173, "SetTimeConsultationRequest" },
                    { 176, false, 173, "ConsultationRequestCancele" },
                    { 177, false, 170, "MastersNotificationsList" },
                    { 178, false, 170, "DeleteMasterNotification" },
                    { 179, false, 170, "UpdateMasterNotification" },
                    { 181, false, 122, "CourseVideoList" },
                    { 182, false, 122, "DeleteCourseVideo" },
                    { 183, false, 122, "CreateNewCourseVideo" },
                    { 184, false, 122, "UpdateCourseVideo" },
                    { 185, false, 122, "DownloadCourseVideo" },
                    { 186, false, 1, "ManageCourseComments" },
                    { 187, false, 186, "CourseCommentsList" },
                    { 188, false, 186, "ReportedCommentsList" },
                    { 189, false, 186, "ConfirmOrDenyComment" },
                    { 190, false, 77, "NotificationList" },
                    { 191, false, 77, "DeleteNotification" },
                    { 192, false, 77, "UpdateNotification" },
                    { 193, false, 122, "PublishCourseVideo" }
                });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "CreatedDateOnUtc", "IsDeleted", "Title", "UniqueName", "UpdatedDateOnUtc" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 5, 5, 8, 10, 48, 944, DateTimeKind.Utc).AddTicks(4552), false, "ادمین", "Admin", new DateTime(2024, 5, 5, 8, 10, 48, 944, DateTimeKind.Utc).AddTicks(4554) },
                    { 2, new DateTime(2024, 5, 5, 8, 10, 48, 944, DateTimeKind.Utc).AddTicks(4555), false, "استاد", "Master", new DateTime(2024, 5, 5, 8, 10, 48, 944, DateTimeKind.Utc).AddTicks(4556) }
                });

            migrationBuilder.InsertData(
                table: "SiteSetting",
                columns: new[] { "Id", "Address", "CollectionManagement", "CopyrightDescription", "DefaultPaymentMethod", "Email", "FooterDescription", "IsDeleted", "LogoName", "Mobile", "Mobile2", "NewsletterDescription", "WithdrawRequestMinimumAmount" },
                values: new object[] { 1, "خیابان ملک", "مدیر مجموعه", "حق کپی رایت", 0, "jafarabbaspour1367@gmail.com", "توضیحات فوتر", false, "لوگو", "09125554478", "09125554478", "توضیحات خبرنامه", 0m });

            migrationBuilder.InsertData(
                table: "SmsProvider",
                columns: new[] { "Id", "ApiKey", "IsDefault", "IsDeleted", "SmsProviderType" },
                values: new object[,]
                {
                    { 1, "6AC5E403-C244-49B9-A497-E0436649FBBB", true, false, 0 },
                    { 2, "59693856665334494F3241446C5959517557796D6B78412F334F68496D44784E6C7667544972744E314A493D", false, false, 1 }
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "AvatarName", "BirthDateOnUtc", "CreatedDateOnUtc", "Email", "FirstName", "Gender", "IsBan", "IsDeleted", "IsEmailActive", "IsMobileActive", "LastName", "Mobile", "Password", "UnableToWithdraw", "UpdatedDateOnUtc" },
                values: new object[] { 1, "default.png", null, new DateTime(2024, 5, 5, 8, 10, 48, 944, DateTimeKind.Utc).AddTicks(4851), "admin@gmail.com", "Admin", 0, false, false, true, true, null, "09122233322", null, false, new DateTime(2024, 5, 5, 8, 10, 48, 944, DateTimeKind.Utc).AddTicks(4852) });

            migrationBuilder.InsertData(
                table: "VIPPlan",
                columns: new[] { "Id", "CreatedDateOnUtc", "DurationDay", "IsDeleted", "IsPublished", "Price", "Title", "UpdatedDateOnUtc" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 5, 5, 8, 10, 48, 944, DateTimeKind.Utc).AddTicks(5181), 30, false, false, 30000m, "یک ماهه", new DateTime(2024, 5, 5, 8, 10, 48, 944, DateTimeKind.Utc).AddTicks(5181) },
                    { 2, new DateTime(2024, 5, 5, 8, 10, 48, 944, DateTimeKind.Utc).AddTicks(5194), 60, false, false, 60000m, "دو ماهه", new DateTime(2024, 5, 5, 8, 10, 48, 944, DateTimeKind.Utc).AddTicks(5194) }
                });

            migrationBuilder.InsertData(
                table: "RolePermissionMapping",
                columns: new[] { "PermissionId", "RoleId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 1 },
                    { 3, 1 },
                    { 4, 1 },
                    { 5, 1 },
                    { 6, 1 },
                    { 7, 1 },
                    { 8, 1 },
                    { 9, 1 },
                    { 10, 1 },
                    { 11, 1 },
                    { 12, 1 },
                    { 13, 1 },
                    { 14, 1 },
                    { 15, 1 },
                    { 16, 1 },
                    { 17, 1 },
                    { 18, 1 },
                    { 19, 1 },
                    { 20, 1 },
                    { 21, 1 },
                    { 22, 1 },
                    { 23, 1 },
                    { 24, 1 },
                    { 25, 1 },
                    { 26, 1 },
                    { 27, 1 },
                    { 28, 1 },
                    { 29, 1 },
                    { 30, 1 },
                    { 31, 1 },
                    { 32, 1 },
                    { 33, 1 },
                    { 34, 1 },
                    { 35, 1 },
                    { 36, 1 },
                    { 37, 1 },
                    { 38, 1 },
                    { 39, 1 },
                    { 40, 1 },
                    { 41, 1 },
                    { 42, 1 },
                    { 43, 1 },
                    { 44, 1 },
                    { 45, 1 },
                    { 46, 1 },
                    { 47, 1 },
                    { 48, 1 },
                    { 49, 1 },
                    { 50, 1 },
                    { 51, 1 },
                    { 52, 1 },
                    { 53, 1 },
                    { 54, 1 },
                    { 55, 1 },
                    { 56, 1 },
                    { 57, 1 },
                    { 58, 1 },
                    { 59, 1 },
                    { 60, 1 },
                    { 64, 1 },
                    { 65, 1 },
                    { 66, 1 },
                    { 67, 1 },
                    { 68, 1 },
                    { 69, 1 },
                    { 70, 1 },
                    { 71, 1 },
                    { 72, 1 },
                    { 73, 1 },
                    { 74, 1 },
                    { 75, 1 },
                    { 76, 1 },
                    { 77, 1 },
                    { 78, 1 },
                    { 79, 1 },
                    { 80, 1 },
                    { 81, 1 },
                    { 82, 1 },
                    { 83, 1 },
                    { 84, 1 },
                    { 85, 1 },
                    { 86, 1 },
                    { 87, 1 },
                    { 88, 1 },
                    { 89, 1 },
                    { 90, 1 },
                    { 91, 1 },
                    { 92, 1 },
                    { 93, 1 },
                    { 94, 1 },
                    { 95, 1 },
                    { 96, 1 },
                    { 97, 1 },
                    { 98, 1 },
                    { 99, 1 },
                    { 100, 1 },
                    { 101, 1 },
                    { 102, 1 },
                    { 103, 1 },
                    { 104, 1 },
                    { 105, 1 },
                    { 106, 1 },
                    { 107, 1 },
                    { 108, 1 },
                    { 109, 1 },
                    { 110, 1 },
                    { 111, 1 },
                    { 112, 1 },
                    { 113, 1 },
                    { 114, 1 },
                    { 115, 1 },
                    { 116, 1 },
                    { 117, 1 },
                    { 118, 1 },
                    { 119, 1 },
                    { 120, 1 },
                    { 121, 1 },
                    { 122, 1 },
                    { 123, 1 },
                    { 124, 1 },
                    { 125, 1 },
                    { 126, 1 },
                    { 127, 1 },
                    { 128, 1 },
                    { 129, 1 },
                    { 130, 1 },
                    { 131, 1 },
                    { 132, 1 },
                    { 133, 1 },
                    { 134, 1 },
                    { 135, 1 },
                    { 136, 1 },
                    { 140, 1 },
                    { 141, 1 },
                    { 142, 1 },
                    { 144, 1 },
                    { 145, 1 },
                    { 146, 1 },
                    { 147, 1 },
                    { 148, 1 },
                    { 149, 1 },
                    { 150, 1 },
                    { 151, 1 },
                    { 152, 1 },
                    { 153, 1 },
                    { 155, 1 },
                    { 156, 1 },
                    { 157, 1 },
                    { 158, 1 },
                    { 159, 1 },
                    { 160, 1 },
                    { 161, 1 },
                    { 162, 1 },
                    { 163, 1 },
                    { 164, 1 },
                    { 165, 1 },
                    { 166, 1 },
                    { 167, 1 },
                    { 168, 1 },
                    { 169, 1 },
                    { 170, 1 },
                    { 171, 1 },
                    { 172, 1 },
                    { 173, 1 },
                    { 174, 1 },
                    { 175, 1 },
                    { 176, 1 },
                    { 177, 1 },
                    { 178, 1 },
                    { 179, 1 },
                    { 181, 1 },
                    { 182, 1 },
                    { 183, 1 },
                    { 184, 1 },
                    { 185, 1 },
                    { 186, 1 },
                    { 187, 1 },
                    { 188, 1 },
                    { 189, 1 },
                    { 190, 1 },
                    { 191, 1 },
                    { 192, 1 },
                    { 193, 1 },
                    { 172, 2 }
                });

            migrationBuilder.InsertData(
                table: "UserRoleMapping",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { 1, 1 });

            migrationBuilder.CreateIndex(
                name: "IX_AdviserAvailableDate_AdviserId",
                table: "AdviserAvailableDate",
                column: "AdviserId");

            migrationBuilder.CreateIndex(
                name: "IX_AdviserConsultationType_AdviserId",
                table: "AdviserConsultationType",
                column: "AdviserId");

            migrationBuilder.CreateIndex(
                name: "IX_BlogComment_BlogId",
                table: "BlogComment",
                column: "BlogId");

            migrationBuilder.CreateIndex(
                name: "IX_BlogComment_ReplyCommentId",
                table: "BlogComment",
                column: "ReplyCommentId");

            migrationBuilder.CreateIndex(
                name: "IX_BlogTagMapping_TagId",
                table: "BlogTagMapping",
                column: "TagId");

            migrationBuilder.CreateIndex(
                name: "IX_CartItem_CartId",
                table: "CartItem",
                column: "CartId");

            migrationBuilder.CreateIndex(
                name: "IX_ConsultationRequest_AdviserConsultationTypeId",
                table: "ConsultationRequest",
                column: "AdviserConsultationTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ConsultationRequest_AdviserId",
                table: "ConsultationRequest",
                column: "AdviserId");

            migrationBuilder.CreateIndex(
                name: "IX_ConsultationRequest_SelectedDateId",
                table: "ConsultationRequest",
                column: "SelectedDateId");

            migrationBuilder.CreateIndex(
                name: "IX_ContactUsForm_ParentId",
                table: "ContactUsForm",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseCategory_ParentCourseCategoryId",
                table: "CourseCategory",
                column: "ParentCourseCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseCategoryMapping_CategoryId",
                table: "CourseCategoryMapping",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseComment_CourseId",
                table: "CourseComment",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseComment_ReplyCommnetId",
                table: "CourseComment",
                column: "ReplyCommnetId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseCommentReport_CommentId",
                table: "CourseCommentReport",
                column: "CommentId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseQuestion_CourseId",
                table: "CourseQuestion",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseQuestionAnswer_QuestionId",
                table: "CourseQuestionAnswer",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseRequestMasterVolunteer_CourseRequestId",
                table: "CourseRequestMasterVolunteer",
                column: "CourseRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseRequestVote_CourseRequestId",
                table: "CourseRequestVote",
                column: "CourseRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseTagMapping_TagId",
                table: "CourseTagMapping",
                column: "TagId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseVideo_CourseId",
                table: "CourseVideo",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseVideo_VideoCategoryId",
                table: "CourseVideo",
                column: "VideoCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseView_CourseId",
                table: "CourseView",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_DiscountLimitation_DiscountId",
                table: "DiscountLimitation",
                column: "DiscountId");

            migrationBuilder.CreateIndex(
                name: "IX_DiscountUsage_DiscountId",
                table: "DiscountUsage",
                column: "DiscountId");

            migrationBuilder.CreateIndex(
                name: "IX_DiscountUsage_OrderId",
                table: "DiscountUsage",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_DiscountUsage_OrderItemId",
                table: "DiscountUsage",
                column: "OrderItemId",
                unique: true,
                filter: "[OrderItemId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_InPersonCourseCategoryMapping_CategoryId",
                table: "InPersonCourseCategoryMapping",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_InPersonCourseTagMapping_TagId",
                table: "InPersonCourseTagMapping",
                column: "TagId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_OrderId",
                table: "OrderItem",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_RolePermissionMapping_PermissionId",
                table: "RolePermissionMapping",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_SurveyAnswer_QuestionId",
                table: "SurveyAnswer",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_SurveyQuestion_SurveyId",
                table: "SurveyQuestion",
                column: "SurveyId");

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_OwnerUserId",
                table: "Ticket",
                column: "OwnerUserId")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.CreateIndex(
                name: "IX_TicketMessage_TicketId",
                table: "TicketMessage",
                column: "TicketId");

            migrationBuilder.CreateIndex(
                name: "IX_UploadedMasterContract_MasterContractId",
                table: "UploadedMasterContract",
                column: "MasterContractId");

            migrationBuilder.CreateIndex(
                name: "IX_UploadedMasterContract_MasterId",
                table: "UploadedMasterContract",
                column: "MasterId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoleMapping_UserId",
                table: "UserRoleMapping",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_VIPMember_VIPPlanId",
                table: "VIPMember",
                column: "VIPPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_WalletTransaction_UserId",
                table: "WalletTransaction",
                column: "UserId")
                .Annotation("SqlServer:Clustered", false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AboutUs");

            migrationBuilder.DropTable(
                name: "AboutUsProgressBar");

            migrationBuilder.DropTable(
                name: "AccountCode");

            migrationBuilder.DropTable(
                name: "BankAccountCard");

            migrationBuilder.DropTable(
                name: "Banner");

            migrationBuilder.DropTable(
                name: "BlogComment");

            migrationBuilder.DropTable(
                name: "BlogTagMapping");

            migrationBuilder.DropTable(
                name: "CartItem");

            migrationBuilder.DropTable(
                name: "ConsultationRequest");

            migrationBuilder.DropTable(
                name: "ContactUs");

            migrationBuilder.DropTable(
                name: "ContactUsForm");

            migrationBuilder.DropTable(
                name: "CourseCategoryMapping");

            migrationBuilder.DropTable(
                name: "CourseCommentReport");

            migrationBuilder.DropTable(
                name: "CourseNotification");

            migrationBuilder.DropTable(
                name: "CourseQuestionAnswer");

            migrationBuilder.DropTable(
                name: "CourseRequestMasterVolunteer");

            migrationBuilder.DropTable(
                name: "CourseRequestVote");

            migrationBuilder.DropTable(
                name: "CourseTagMapping");

            migrationBuilder.DropTable(
                name: "CourseUserMapping");

            migrationBuilder.DropTable(
                name: "CourseVideo");

            migrationBuilder.DropTable(
                name: "CourseView");

            migrationBuilder.DropTable(
                name: "DiscountLimitationCategoryMapping");

            migrationBuilder.DropTable(
                name: "DiscountLimitationCourseMapping");

            migrationBuilder.DropTable(
                name: "DiscountLimitationUsageCountMapping");

            migrationBuilder.DropTable(
                name: "DiscountLimitationUserMapping");

            migrationBuilder.DropTable(
                name: "DiscountUsage");

            migrationBuilder.DropTable(
                name: "DynamicText");

            migrationBuilder.DropTable(
                name: "FAQ");

            migrationBuilder.DropTable(
                name: "FavouriteCourse");

            migrationBuilder.DropTable(
                name: "InPersonCourseCategoryMapping");

            migrationBuilder.DropTable(
                name: "InPersonCourseTagMapping");

            migrationBuilder.DropTable(
                name: "InPersonCourseVisit");

            migrationBuilder.DropTable(
                name: "Link");

            migrationBuilder.DropTable(
                name: "LocalizedProperty");

            migrationBuilder.DropTable(
                name: "Newsletter");

            migrationBuilder.DropTable(
                name: "NotificationMasterMapping");

            migrationBuilder.DropTable(
                name: "NotificationUserMapping");

            migrationBuilder.DropTable(
                name: "Page");

            migrationBuilder.DropTable(
                name: "RolePermissionMapping");

            migrationBuilder.DropTable(
                name: "SettlementTransaction");

            migrationBuilder.DropTable(
                name: "SiteSetting");

            migrationBuilder.DropTable(
                name: "Slider");

            migrationBuilder.DropTable(
                name: "SmsProvider");

            migrationBuilder.DropTable(
                name: "SocialMedia");

            migrationBuilder.DropTable(
                name: "SurveyAnswer");

            migrationBuilder.DropTable(
                name: "TicketMessage");

            migrationBuilder.DropTable(
                name: "UploadedMasterContract");

            migrationBuilder.DropTable(
                name: "UserRoleMapping");

            migrationBuilder.DropTable(
                name: "VIPMember");

            migrationBuilder.DropTable(
                name: "WalletTransaction");

            migrationBuilder.DropTable(
                name: "WithdrawRequest");

            migrationBuilder.DropTable(
                name: "BlogTag");

            migrationBuilder.DropTable(
                name: "Blog");

            migrationBuilder.DropTable(
                name: "Cart");

            migrationBuilder.DropTable(
                name: "AdviserAvailableDate");

            migrationBuilder.DropTable(
                name: "AdviserConsultationType");

            migrationBuilder.DropTable(
                name: "CourseComment");

            migrationBuilder.DropTable(
                name: "CourseQuestion");

            migrationBuilder.DropTable(
                name: "CourseRequest");

            migrationBuilder.DropTable(
                name: "CourseTag");

            migrationBuilder.DropTable(
                name: "CourseVideoCategory");

            migrationBuilder.DropTable(
                name: "DiscountLimitation");

            migrationBuilder.DropTable(
                name: "OrderItem");

            migrationBuilder.DropTable(
                name: "CourseCategory");

            migrationBuilder.DropTable(
                name: "InPersonCourseTag");

            migrationBuilder.DropTable(
                name: "InPersonCourse");

            migrationBuilder.DropTable(
                name: "MasterNotification");

            migrationBuilder.DropTable(
                name: "Notification");

            migrationBuilder.DropTable(
                name: "Permission");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "SurveyQuestion");

            migrationBuilder.DropTable(
                name: "Ticket");

            migrationBuilder.DropTable(
                name: "MasterContract");

            migrationBuilder.DropTable(
                name: "Master");

            migrationBuilder.DropTable(
                name: "VIPPlan");

            migrationBuilder.DropTable(
                name: "Adviser");

            migrationBuilder.DropTable(
                name: "Course");

            migrationBuilder.DropTable(
                name: "Discount");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "Survey");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
