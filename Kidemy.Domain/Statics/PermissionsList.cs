using Kidemy.Domain.Models.Identity;

namespace Kidemy.Domain.Statics
{
    public static class PermissionsList
    {
        private static readonly List<Permission> _permissions = new List<Permission>
        {
            #region User

            new Permission()
            {
                Id = 1,
                UniqueName = "AdminPanel",
                ParentId = null
            },
            new Permission()
            {
                Id = 2,
                UniqueName = "ManageUsers",
                ParentId = 1
            },
            new Permission()
            {
                Id = 3,
                UniqueName = "UserList",
                ParentId = 2
            },
            new Permission()
            {
                Id = 4,
                UniqueName = "CreateUser",
                ParentId = 2
            },
            new Permission()
            {
                Id = 5,
                UniqueName = "UpdateUser",
                ParentId = 2
            },
            new Permission()
            {
                Id = 6,
                UniqueName = "DeleteUser",
                ParentId = 2
            },
            new Permission
            {
                Id = 127,
                UniqueName = "UserProfile",
                ParentId =2
            },
            new Permission()
            {
                Id = 135,
                UniqueName = "UnableToWithdrawUser",
                ParentId = 2
            },
            new Permission()
            {
                Id = 136,
                UniqueName = "AbleToWithdrawUser",
                ParentId = 2
            },
            #endregion

            #region Roles
            new Permission()
            {
                Id = 7,
                UniqueName = "ManageRoles",
                ParentId = 1
            },
            new Permission()
            {
                Id = 8,
                UniqueName = "RolesList",
                ParentId = 7
            },
            new Permission()
            {
                Id = 9,
                UniqueName = "CreateRole",
                ParentId = 7
            },
            new Permission()
            {
                Id = 10,
                UniqueName = "UpdateRole",
                ParentId = 7
            },
            new Permission()
            {
                Id =11 ,
                UniqueName = "DeleteRole",
                ParentId = 7
            },
            #endregion

            #region Wallet
            new Permission()
            {
                Id =12 ,
                UniqueName = "ManageWallet",
                ParentId = 1
            },
            new Permission()
            {
                Id =13 ,
                UniqueName = "ChargeWallet",
                ParentId = 12
            },
            new Permission()
            {
                Id =14 ,
                UniqueName = "WithdrawFromWallet",
                ParentId = 12
            },
            #endregion

            #region SocialMedia

            new Permission()
            {
                Id =15 ,
                ParentId = 1,
                UniqueName = "ManageSocialMedia",
            },
            new Permission()
            {
                Id =16 ,
                ParentId = 15,
                UniqueName = "SocialMediaList",
            },
            new Permission()
            {
                Id =17 ,
                ParentId = 15,
                UniqueName = "CreateSocialMedia",
            },
            new Permission()
            {
                Id =18 ,
                ParentId = 15,
                UniqueName = "UpdateSocialMedia"
            },
            new Permission()
            {
                Id =19 ,
                ParentId = 15,
                UniqueName = "DeleteSocialMedia"
            },

	        #endregion

            #region Ticket

            new Permission
            {
                Id = 22,
                UniqueName = "ManageTicket",
                ParentId = 1
            },

            new Permission
            {
                Id = 23,
                UniqueName = "UpdateTicket",
                ParentId = 22
            },

            new Permission
            {
                Id = 24,
                UniqueName = "TicketDetails",
                ParentId = 22
            },

            new Permission
            {
                Id = 25,
                UniqueName = "TicketList",
                ParentId = 22
            },

            new Permission
            {
                Id = 26,
                UniqueName = "CreateTicket",
                ParentId = 22
            },

            #endregion

            #region ContactUs

            new Permission
            {
                Id=20,
                UniqueName="ManageContactUs",
                ParentId=1
            },

            new Permission
            {
                Id=21,
                UniqueName="UpdateContactUs",
                ParentId=20
            },


            #endregion

            #region ContactUsForm

            new Permission
            {
                Id=27,
                UniqueName="ContactUsFormList",
                ParentId=20
            },
            new Permission
            {
                Id=28,
                UniqueName="ContactUsFormReply",
                ParentId=20
            },
            new Permission
            {
                Id=29,
                UniqueName="DeleteContactUsForm",
                ParentId=20
            },

            #endregion

            #region AboutUs
            new Permission
            {
                Id = 56,
                ParentId = 1,
                UniqueName = "ManageAboutUs"
            },
            new Permission()
            {
                Id =30 ,
                ParentId = 56,
                UniqueName = "UpdateAboutUs",
            },
            new Permission
            {
                Id = 57,
                ParentId = 56,
                UniqueName = "CreateProgressBar"
            },
            new Permission
            {
                Id = 58,
                ParentId = 56,
                UniqueName = "ProgressBarList"
            },
            new Permission
            {
                Id = 59,
                ParentId = 56,
                UniqueName = "DeleteProgressBar"
            },
            new Permission
            {
                Id = 60,
                ParentId = 56,
                UniqueName = "UpdateProgresBar"
            },

	        #endregion 

            #region Page

            new Permission
            {
                Id = 31,
                UniqueName = "ManagePages",
                ParentId = 1
            },

            new Permission
            {
                Id = 32,
                UniqueName = "CreatePage",
                ParentId = 31
            },

            new Permission
            {
                Id = 33,
                UniqueName = "UpdatePage",
                ParentId = 31
            },

            new Permission
            {
                Id = 34,
                UniqueName = "PagesList",
                ParentId = 31
            },

            new Permission
            {
                Id = 35,
                UniqueName = "DeletePage",
                ParentId = 31
            },

            #endregion

            #region Newsletters

            new Permission
            {
                Id = 36,
                UniqueName = "ManageNewsletter",
                ParentId = 1
            },
            new Permission
            {
                Id = 37,
                ParentId = 36,
                UniqueName = "MembersList_Newsletter"
            },
            new Permission
            {
                Id = 38,
                ParentId = 36,
                UniqueName = "DeleteMember_Newsletter"
            },
       

            #endregion 

            #region Survey

            new Permission()
            {
                Id =39 ,
                ParentId = 1,
                UniqueName = "ManageSurvey",
            },
            new Permission()
            {
                Id = 40,
                ParentId = 39,
                UniqueName = "SurveyList",
            },
            new Permission()
            {
                Id = 41,
                ParentId = 39,
                UniqueName = "CreateSurvey",
            },
            new Permission()
            {
                Id = 42,
                ParentId = 39,
                UniqueName = "UpdateSurvey",
            },
            new Permission()
            {
                Id = 43,
                ParentId = 39,
                UniqueName = "DeleteSurvey",
            },
            new Permission()
            {
                Id = 44,
                ParentId = 39,
                UniqueName = "SurveyQuestionsList",
            },
            new Permission()
            {
                Id = 45,
                ParentId = 39,
                UniqueName = "CreateSurveyQuestion",
            },
            new Permission()
            {
                Id = 46,
                ParentId = 39,
                UniqueName = "UpdateSurveyQuestion",
            },
            new Permission()
            {
                Id = 47,
                ParentId = 39,
                UniqueName = "DeleteSurveyQuestion",
            },
            new Permission()
            {
                Id = 48,
                ParentId = 39,
                UniqueName = "SurveyAnswersList",
            },
            new Permission()
            {
                Id = 49,
                ParentId = 39,
                UniqueName = "SurveyAnswerDetails",
            },

            #endregion 

            #region Link

            new Permission
            {
                Id = 50,
                UniqueName = "ManageLinks",
                ParentId = 1
            },

            new Permission
            {
                Id = 51,
                UniqueName = "CreateLink",
                ParentId = 50
            },

            new Permission
            {
                Id = 52,
                UniqueName = "UpdateLink",
                ParentId = 50
            },

            new Permission
            {
                Id = 53,
                UniqueName = "LinksList",
                ParentId = 50
            },

            new Permission
            {
                Id = 54,
                UniqueName = "DeleteLink",
                ParentId = 50
            },

            #endregion 

            #region SiteSetting

            new Permission()
            {
                Id =55 ,
                ParentId = 1,
                UniqueName = "UpdateSiteSetting",
            },

            #endregion 

            #region Blog
            new Permission
            {
                Id = 64,
                UniqueName = "ManageBlogs",
                ParentId = 1
            },

            new Permission
            {
                Id = 65,
                UniqueName = "CreateBlog",
                ParentId = 64
            },

            new Permission
            {
                Id = 66,
                UniqueName = "UpdateBlog",
                ParentId = 64
            },

            new Permission
            {
                Id = 67,
                UniqueName = "BlogsList",
                ParentId = 64
            },

            new Permission
            {
                Id = 68,
                UniqueName = "DeleteBlog",
                ParentId = 64
            },
            #endregion

            #region SmsProvider

            new Permission()
            {
                Id =69 ,
                ParentId = 1,
                UniqueName = "ManageSmsProvide",
            },
               new Permission()
            {
                Id =70 ,
                ParentId = 69,
                UniqueName = "SmsProviderList",
            },
                  new Permission()
            {
                Id =71 ,
                ParentId = 69,
                UniqueName = "UpdateSmsProvider",
            },
	        #endregion 

            #region FAQ

            new Permission
            {
                Id = 72,
                UniqueName = "ManageFAQs",
                ParentId = 1
            },

            new Permission
            {
                Id = 73,
                UniqueName = "CreateFAQ",
                ParentId = 72
            },

            new Permission
            {
                Id = 74,
                UniqueName = "UpdateFAQ",
                ParentId = 72
            },

            new Permission
            {
                Id = 75,
                UniqueName = "FAQsList",
                ParentId = 72
            },

            new Permission
            {
                Id = 76,
                UniqueName = "DeleteFAQ",
                ParentId = 72
            },

            #endregion  

            #region SendNotification
            new Permission
            {
                Id = 77,
                ParentId = 1,
                UniqueName = "ManageSendNotification"
            },
            new Permission
            {
                Id = 78,
                ParentId = 77,
                UniqueName = "SendNotification"
            },
              new Permission
            {
                Id = 79,
                ParentId = 77,
                UniqueName = "FilterUsersForSendNotification"
            },
                   new Permission
            {
                Id =190 ,
                ParentId = 77,
                UniqueName = "NotificationList",
            },
                new Permission
            {
                Id =191 ,
                ParentId = 77,
                UniqueName = "DeleteNotification",
            },
                 new Permission
            {
                Id =192 ,
                ParentId = 77,
                UniqueName = "UpdateNotification",
            },

            #endregion 

            #region Discount

            new Permission
            {
                Id = 80,
                ParentId = 1,
                UniqueName = "ManageDiscount"
            },
            new Permission
            {
                Id = 81,
                ParentId = 80,
                UniqueName = "DiscountList"
            },
            new Permission
            {
                Id = 82,
                ParentId = 80,
                UniqueName = "CreateDiscount"
            },
            new Permission
            {
                Id = 83,
                ParentId = 80,
                UniqueName = "UpdateDiscount"
            },
            new Permission
            {
                Id = 84,
                ParentId = 80,
                UniqueName = "DeleteDiscount"
            },
            new Permission
            {
                Id = 85,
                ParentId = 80,
                UniqueName = "DiscountLimitationList"
            },
            new Permission
            {
                Id = 86,
                ParentId = 80,
                UniqueName = "CreateDiscountLimitation"
            },
            new Permission
            {
                Id = 87,
                ParentId = 80,
                UniqueName = "UpdateDiscountLimitation"
            },
            new Permission
            {
                Id = 88,
                ParentId = 80,
                UniqueName = "DeleteDiscountLimitation"
            },
	        #endregion

            #region Master

            new Permission
            {
                Id = 89,
                ParentId = 1,
                UniqueName = "ManageMasters"
            },
            new Permission
            {
                Id = 90,
                ParentId = 89,
                UniqueName = "MasterList"
            },
              new Permission
            {
                Id = 99,
                ParentId = 89,
                UniqueName = "MasterDetails"
            },
                 new Permission
            {
                Id = 153,
                ParentId = 89,
                UniqueName = "SetBlockedAmount"
            },

            #endregion
           
            #region DynamicText

            new Permission
            {
                Id = 91,
                ParentId = 1,
                UniqueName = "ManageDynamicTexts"
            },
            new Permission
            {
                Id = 92,
                ParentId = 91,
                UniqueName = "DynamicTextsList"
            },
            new Permission
            {
                Id = 93,
                ParentId = 91,
                UniqueName = "UpdateDynamicText"
            },

            #endregion

            #region VIPService

            new Permission
            {
                Id = 94,
                ParentId = 1,
                UniqueName = "ManageVIPServices"
            },
            new Permission
            {
                Id = 95,
                ParentId = 94,
                UniqueName = "VIPPlansList"
            },
            new Permission
            {
                Id = 96,
                ParentId = 94,
                UniqueName = "CreateVIPPlan"
            },
            new Permission
            {
                Id = 97,
                ParentId = 94,
                UniqueName = "UpdateVIPPlan"
            },
              new Permission
            {
                Id = 98,
                ParentId = 94,
                UniqueName = "DeleteVIPPlan"
            },

            #endregion
              
            #region MasterContract

            new Permission
            {
                Id = 100,
                ParentId = 1,
                UniqueName = "ManageMastersContracts"
            },
            new Permission
            {
                Id = 101,
                ParentId = 100,
                UniqueName = "MasterContractsList"
            },
             new Permission
            {
                Id = 102,
                ParentId = 100,
                UniqueName = "CreateMasterContract"
            },
             new Permission
            {
                Id = 103,
                ParentId = 100,
                UniqueName = "UpdateMasterContract"
            },
             new Permission
            {
                Id = 104,
                ParentId = 100,
                UniqueName = "MasterContractsDetails"
            },
             new Permission
            {
                Id = 105,
                ParentId = 100,
                UniqueName = "PendingContractsList"
            },


            #endregion

            #region CourseQuestion

                 new Permission
            {
                Id = 106,
                ParentId = 1,
                UniqueName = "ManageCourseQuestion"
            },
            new Permission
            {
                Id = 107,
                ParentId = 106,
                UniqueName = "CourseQuestionList"
            },
            new Permission
            {
                Id = 108,
                ParentId = 106,
                UniqueName = "CourseQuestionMessagesList"
            },
            new Permission
            {
                Id = 109,
                ParentId = 106,
                UniqueName = "CloseCourseQuestion"
            },
              new Permission
            {
                Id = 110,
                ParentId = 106,
                UniqueName = "OpenCourseQuestion"
            },
              new Permission
            {
                Id = 111,
                ParentId = 106,
                UniqueName = "ConfirmCourseQuestion"
            },
              new Permission
            {
                Id = 112,
                ParentId = 106,
                UniqueName = "DeleteCourseQuestion"
            },

                 new Permission
            {
                Id = 113,
                ParentId = 106,
                UniqueName = "DeleteCourseQuestionAnswer"
            },
                       new Permission
            {
                Id = 114,
                ParentId = 106,
                UniqueName = "ConfirmCourseQuestionAnswer"
            },

             new Permission
            {
                Id = 115,
                ParentId = 106,
                UniqueName = "NotConfirmedCourseQuestionAndAnswersList"
            },
               new Permission
            {
                Id = 116,
                ParentId = 106,
                UniqueName = "CourseQuestionsDoesNotAnsweredByTeacherAfter48HoursList"
            },
            #endregion
 
            #region CourseCategory
            new Permission
            {
                Id = 117,
                ParentId = 1,
                UniqueName = "ManageCourseCategories"
            },
            new Permission
            {
                Id = 118,
                ParentId = 117,
                UniqueName = "CourseCategories"
            },
            new Permission
            {
                Id = 119,
                ParentId = 117,
                UniqueName = "CreateNewCourseCategory"
            },
            new Permission
            {
                Id = 120,
                ParentId = 117,
                UniqueName = "DeleteCourseCategory"
            },
            new Permission
            {
                 Id = 121,
                 ParentId = 118,
                 UniqueName = "UpdateCourseCategory"
            },
            #endregion

            #region Course
            new Permission
            {
                Id = 122,
                ParentId =1,
                UniqueName = "ManageCourses"
            },
            new Permission
            {
                Id = 123,
                ParentId = 122,
                UniqueName = "CoursesList"
            },
            new Permission
            {
                Id = 124,
                ParentId = 122,
                UniqueName = "CreateNewCourse"
            },
            new Permission
            {
                Id = 125,
                ParentId = 122,
                UniqueName = "DeleteCourse"
            },
            new Permission
            {
                Id = 126,
                ParentId = 122,
                UniqueName ="UpdateCourse"
            },
            new Permission
            {
                Id = 181,
                ParentId = 122,
                UniqueName = "CourseVideoList"
            },
            new Permission
            {
                Id = 182,
                ParentId = 122,
                UniqueName = "DeleteCourseVideo"
            },
            new Permission
            {
                Id = 183,
                ParentId = 122,
                UniqueName = "CreateNewCourseVideo"
            },
            new Permission
            {
                Id = 184,
                ParentId = 122,
                UniqueName = "UpdateCourseVideo"
            },
            new Permission
            {
                Id = 185,
                ParentId = 122,
                UniqueName = "DownloadCourseVideo"
            },   
            new Permission
            {
                Id = 193,
                ParentId = 122,
                UniqueName = "PublishCourseVideo"
            },
            #endregion

            #region BankAccountCard

             new Permission
            {
                Id=128,
                UniqueName="ManageAccountBankCard",
                ParentId=1
            },
            new Permission
            {
                Id=129,
                UniqueName="BankAccountCardList",
                ParentId=128
            },
            new Permission
            {
                Id=130,
                UniqueName="ConfirmBankAccountCard",
                ParentId=128
            },
             new Permission
            {
                Id=131,
                UniqueName="RejectBankAccountCard",
                ParentId=128
            },
          
	      #endregion

            #region SettlementTransaction

            new Permission
            {
                Id=132,
                UniqueName="ManageSettlementTransaction",
                ParentId=1
            },
            new Permission
            {
                Id=133,
                UniqueName="AddSettlementTransaction",
                ParentId=132
            },
            new Permission
            {
                Id=134,
                UniqueName="EditSettlementTransaction",
                ParentId=132
            },
            #endregion

            #region WithdrawRequests

            new Permission
            {
                Id=144,
                UniqueName="ManageWithdrawRequests",
                ParentId=1
            },
            new Permission
            {
                Id=145,
                UniqueName="ListOfWithdrawRequests",
                ParentId=144
            },
            new Permission
            {
                Id=146,
                UniqueName="AcceptWithdrawRequest",
                ParentId=144
            },
               new Permission
            {
                Id=147,
                UniqueName="RejectWithdrawRequest",
                ParentId=144
            },
            #endregion

            #region Adviser
                new Permission
            {
                Id=148,
                UniqueName="ManageAdviser",
                ParentId=1
            },
            new Permission
            {
                Id=149,
                UniqueName="AdviserList",
                ParentId=148
            },
            new Permission
            {
                Id=150,
                UniqueName="AddAdviser",
                ParentId=148
            },
               new Permission
            {
                Id=151,
                UniqueName="EditAdviser",
                ParentId=148
            },
            new Permission
            {
                Id=152,
                UniqueName="DeleteAdviser",
                ParentId=148
            },
	#endregion

            #region Order

            new Permission
            {
                Id = 140,
                ParentId = 1,
                UniqueName = "ManageOrders"
            },
            new Permission
            {
                Id = 141,
                ParentId = 140,
                UniqueName = "OrdersList"
            },
            new Permission
            {
                Id = 142,
                ParentId = 140,
                UniqueName = "OrderDetails"
            },
              #endregion

            #region Slider

            new Permission
            {
                Id =155 ,
                ParentId = 1,
                UniqueName = "ManageSliders",
            },
            new Permission
            {
                Id =156 ,
                ParentId = 155,
                UniqueName = "SlidersList",
            },
            new Permission
            {
                Id =157 ,
                ParentId = 155,
                UniqueName = "CreateSlider",
            },
            new Permission
            {
                Id =158 ,
                ParentId = 155,
                UniqueName = "UpdateSlider"
            },
            new Permission
            {
                Id =159 ,
                ParentId = 155,
                UniqueName = "DeleteSlider"
            },

            #endregion

            #region Banner

            new Permission
            {
                Id =160 ,
                ParentId = 1,
                UniqueName = "ManageBanners",
            },
            new Permission
            {
                Id =161 ,
                ParentId = 160,
                UniqueName = "BannersList",
            },
            new Permission
            {
                Id =162 ,
                ParentId = 160,
                UniqueName = "CreateBanner",
            },
            new Permission
            {
                Id =163 ,
                ParentId = 160,
                UniqueName = "UpdateBanner"
            },
            new Permission
            {
                Id =164 ,
                ParentId = 160,
                UniqueName = "DeleteBanner"
            },

            #endregion

            #region CourseRequest

            new Permission
            {
                Id =165 ,
                ParentId = 1,
                UniqueName = "ManageCourseRequests",
            },
            new Permission
            {
                Id =166 ,
                ParentId = 165,
                UniqueName = "CourseRequestsList",
            },
                     new Permission
            {
                Id = 167,
                ParentId = 165,
                UniqueName = "ConfirmCourseRequest"
            },
               new Permission
            {
                Id = 168,
                ParentId = 165,
                UniqueName = "NotConfirmCourseRequest"
            },
                  new Permission
            {
                Id = 169,
                ParentId = 165,
                UniqueName = "CourseRequestDetails"
            },

            #endregion
                  
            #region MasterNotification

            new Permission
            {
                Id =170 ,
                ParentId = 1,
                UniqueName = "ManageMastersNotifications",
            },
            new Permission
            {
                Id =171 ,
                ParentId = 170,
                UniqueName = "SendMastersNotifications",
            },
                 new Permission
            {
                Id =177 ,
                ParentId = 170,
                UniqueName = "MastersNotificationsList",
            },
                new Permission
            {
                Id =178 ,
                ParentId = 170,
                UniqueName = "DeleteMasterNotification",
            },
                 new Permission
            {
                Id =179 ,
                ParentId = 170,
                UniqueName = "UpdateMasterNotification",
            },


            #endregion

            #region MasterPermission
            new Permission
            {
                Id = 172,
                ParentId = 1,
                UniqueName = "Master"
            },
            new Permission
            {
                Id = 173,
                ParentId = 1,
                UniqueName = "ManageConsultationRequest"
            },
            new Permission
            {
                Id = 174,
                ParentId = 173,
                UniqueName = "ConsultationRequestList"
            },
            new Permission
            {
                Id = 175,
                ParentId = 173,
                UniqueName = "SetTimeConsultationRequest"
            },
             new Permission
            {
                Id = 176,
                ParentId = 173,
                UniqueName = "ConsultationRequestCancele"
            },
            #endregion

            #region ConsultationRequest

            #endregion

            #region CourseComment
             new Permission
             {
                 Id = 186,
                 ParentId = 1,
                 UniqueName = "ManageCourseComments"
             },
             new Permission
             {
                 Id = 187,
                 ParentId = 186,
                 UniqueName = "CourseCommentsList"
             },
             new Permission
             {
                 Id = 188,
                 ParentId = 186,
                 UniqueName = "ReportedCommentsList"
             },
             new Permission
             {
                 Id = 189,
                 ParentId = 186,
                 UniqueName = "ConfirmOrDenyComment"
             },
            #endregion
             
            //last id used = 193
        };

        public static List<Permission> Permissions => _permissions;
    }
}
