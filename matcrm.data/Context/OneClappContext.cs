using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using matcrm.data.Models.Tables;
using static matcrm.data.Helpers.DataUtility;

namespace matcrm.data.Context
{
    public class OneClappContext : DbContext
    {
        public static string ConnectionString { get; set; }
        public static string CurrentURL { get; set; }
        public static string ProjectName { get; set; }
        public static string SecretKey { get; set; }
        public static string AppURL { get; set; }
        public static string TokenExpireMinute { get; set; }
        public static string GoogleClientId { get; set; }
        public static string GoogleSecretKey { get; set; }

        public static string GoogleCalendarClientId { get; set; }
        public static string GoogleCalendarSecretKey { get; set; }
        public static string GoogleCalendarApiKey { get; set; }
        public static string GoogleCalendarId { get; set; }
        public static string ValidIssuer { get; set; }
        public static string ValidAudience { get; set; }
        public static string SubmitUrl { get; set; }

        public static string MollieClientId { get; set; }
        public static string MollieSecretKey { get; set; }
        public static string MollieApiKey { get; set; }
        public static string MollieDefaultRedirectUrl { get; set; }

        public static string HcaptchaSiteKey { get; set; }
        public static string HcaptchaSiteSecret { get; set; }
        public static string HcaptchaVerifyUrl { get; set; }

        public static string MicroSoftClientId { get; set; }
        public static string MicroSecretKey { get; set; }
        public static string MicroSoftTenantId { get; set; }
        public static string MicroSoftRedirectUrl { get; set; }
        public static string OriginalUserProfileDirPath { get; set; }
        public static string ReSizedUserProfileDirPath { get; set; }
        public static string CustomerFileUploadDirPath { get; set; }
        public static string DiscussionCommentUploadDirPath { get; set; }
        public static string EmployeeChildTaskUploadDirPath { get; set; }
        public static string EmployeeSubTaskUploadDirPath { get; set; }
        public static string SubTaskUploadDirPath { get; set; }
        public static string ChildTaskUploadDirPath { get; set; }
        public static string EmployeeTaskUploadDirPath { get; set; }
        public static string DynamicFormHeaderDirPath { get; set; }
        public static string ImportContactDirPath { get; set; }
        public static string DynamicFormLayoutDirPath { get; set; }
        public static string DefaultLayoutDirPath { get; set; }
        public static string MailCommentUploadDirPath { get; set; }
        public static string FormsJSUploadDirPath { get; set; }
        public static string ModalFormsJSUploadDirPath { get; set; }
        public static string SlidingFormJSUploadDirPath { get; set; }
        public static string OrganizationUploadDirPath { get; set; }
        public static string ProjectLogoDirPath { get; set; }
        public static string TaskUploadDirPath { get; set; }
        public static string BgImageDirPath { get; set; }
        public static string LogoImageDirPath { get; set; }
        public static string ClamAVServerURL { get; set; }
        public static string ClamAVServerPort { get; set; }
        public static bool ClamAVServerIsLive { get; set; }

        public OneClappContext() { }

        public OneClappContext(DbContextOptions<OneClappContext> options) : base(options)
        {
            Database.SetCommandTimeout(150000);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //  optionsBuilder.UseSqlServer ("Data Source=tcp:113.212.87.157\\TA4,1438;Initial Catalog=OneClappDB;User Id=sa;Password=Admin@123;");
            // optionsBuilder.UseSqlServer("Data Source=tcp:TA4\\TA4,1438,1438;Initial Catalog=OneClappDB_Shakti;User Id=sa;Password=Admin@123;MultipleActiveResultSets=True;");
            // optionsBuilder.UseSqlServer ("Data Source=tcp:113.212.87.157\\TA4,1438;Initial Catalog=OneClappDB_Theme;User Id=sa;Password=Admin@123;");
            // optionsBuilder.UseSqlServer("Server=localhost\\MSSQLSERVER01;Database=beMate_DB;Trusted_Connection=True;MultipleActiveResultSets=True;");
            // optionsBuilder.UseSqlServer(ConnectionString);
            optionsBuilder.UseNpgsql(ConnectionString);
            //optionsBuilder.UseNpgsql("User ID=postgres;Password=postgres;Host=localhost;Port=5432;Database=Matefinity;Pooling=true;");
        }

        public DbSet<Tenant> Tenants { get; set; }
        public DbSet<User> Users { get; set; }
        // public DbSet<Phrases> Phrases { get; set; }
        public DbSet<ErrorLog> ErrorLog { get; set; }
        public DbSet<VerificationCode> VerificationCode { get; set; }
        public DbSet<EmailTemplate> EmailTemplate { get; set; }
        public DbSet<Language> Language { get; set; }
        public DbSet<Role> Role { get; set; }

        // Start OneClapp theme tables
        public DbSet<OneClappLatestTheme> OneClappLatestTheme { get; set; }
        public DbSet<OneClappLatestThemeScheme> OneClappLatestThemeScheme { get; set; }
        public DbSet<OneClappLatestThemeLayout> OneClappLatestThemeLayout { get; set; }
        public DbSet<OneClappLatestThemeConfig> OneClappLatestThemeConfig { get; set; }
        // End OneClapp theme tables

        // Start OneClapp task tables
        public DbSet<OneClappTask> OneClappTask { get; set; }
        public DbSet<OneClappSubTask> OneClappSubTask { get; set; }
        public DbSet<Models.Tables.TaskStatus> TaskStatus { get; set; }
        public DbSet<OneClappChildTask> OneClappChildTask { get; set; }
        public DbSet<OneClappTaskUser> OneClappTaskUser { get; set; }
        public DbSet<TaskWeclappUser> TaskWeclappUser { get; set; }
        public DbSet<OneClappSubTaskUser> OneClappSubTaskUser { get; set; }
        public DbSet<OneClappChildTaskUser> OneClappChildTaskUser { get; set; }
        public DbSet<TaskTimeRecord> TaskTimeRecord { get; set; }
        public DbSet<SubTaskTimeRecord> SubTaskTimeRecord { get; set; }
        public DbSet<ChildTaskTimeRecord> ChildTaskTimeRecord { get; set; }
        public DbSet<TaskAttachment> TaskAttachment { get; set; }
        public DbSet<SubTaskAttachment> SubTaskAttachment { get; set; }
        public DbSet<ChildTaskAttachment> ChildTaskAttachment { get; set; }
        public DbSet<TaskComment> TaskComment { get; set; }
        public DbSet<SubTaskComment> SubTaskComment { get; set; }
        public DbSet<ChildTaskComment> ChildTaskComment { get; set; }
        public DbSet<TenantConfig> TenantConfig { get; set; }
        public DbSet<TaskActivity> TaskActivity { get; set; }
        public DbSet<SubTaskActivity> SubTaskActivity { get; set; }
        public DbSet<ChildTaskActivity> ChildTaskActivity { get; set; }
        public DbSet<TenantActivity> TenantActivity { get; set; }
        public DbSet<Section> Section { get; set; }
        public DbSet<SectionActivity> SectionActivity { get; set; }
        public DbSet<PdfTemplate> PdfTemplate { get; set; }

        // End Oneclapp task

        // Start Custom field tables

        public DbSet<CustomControl> CustomControl { get; set; }
        public DbSet<CustomControlOption> CustomControlOption { get; set; }
        public DbSet<CustomField> CustomField { get; set; }
        public DbSet<CustomModule> CustomModule { get; set; }
        public DbSet<ModuleField> ModuleField { get; set; }
        public DbSet<TenantModule> TenantModule { get; set; }
        public DbSet<CustomTenantField> CustomTenantField { get; set; }
        public DbSet<CustomTable> CustomTable { get; set; }
        public DbSet<CustomFieldValue> CustomFieldValue { get; set; }
        public DbSet<CustomTableColumn> CustomTableColumn { get; set; }
        public DbSet<TableColumnUser> TableColumnUser { get; set; }
        // End Custom field tables

        //Start Customers Tables

        public DbSet<LabelCategory> LabelCategory { get; set; }
        public DbSet<Label> Label { get; set; }
        public DbSet<Status> Status { get; set; }
        public DbSet<ActivityType> ActivityType { get; set; }
        public DbSet<ActivityAvailability> ActivityAvailability { get; set; }


        public DbSet<Organization> Organization { get; set; }
        public DbSet<OrganizationNote> OrganizationNote { get; set; }
        public DbSet<OrganizationAttachment> OrganizationAttachment { get; set; }
        public DbSet<OrganizationNotesComment> OrganizationNotesComment { get; set; }
        public DbSet<OrganizationActivity> OrganizationActivity { get; set; }
        public DbSet<OrganizationActivityMember> OrganizationActivityMember { get; set; }
        public DbSet<OrganizationLabel> OrganizationLabel { get; set; }

        // public DbSet<CustomerTopic> CustomerTopic { get; set; }
        public DbSet<CustomerType> CustomerType { get; set; }
        public DbSet<TicketPriority> TicketPriority { get; set; }
        public DbSet<TicketStatus> TicketStatus { get; set; }
        public DbSet<TicketChannel> TicketChannel { get; set; }
        public DbSet<Tag> Tag { get; set; }
        public DbSet<Ticket> Ticket { get; set; }
        public DbSet<TicketTag> TicketTag { get; set; }
        public DbSet<TicketType> TicketType { get; set; }
        public DbSet<Salutation> Salutation { get; set; }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<TicketCategory> TicketCategory { get; set; }
        public DbSet<EmailPhoneNoType> EmailPhoneNoType { get; set; }
        public DbSet<CustomerEmail> CustomerEmail { get; set; }
        public DbSet<CustomerPhone> CustomerPhone { get; set; }
        public DbSet<CustomerNote> CustomerNote { get; set; }
        public DbSet<CustomerAttachment> CustomerAttachment { get; set; }
        public DbSet<CustomerNotesComment> CustomerNotesComment { get; set; }
        public DbSet<CustomerLabel> CustomerLabel { get; set; }

        public DbSet<CustomerActivity> CustomerActivity { get; set; }
        public DbSet<CustomerActivityMember> CustomerActivityMember { get; set; }


        public DbSet<Lead> Lead { get; set; }
        public DbSet<LeadNote> LeadNote { get; set; }
        public DbSet<LeadLabel> LeadLabel { get; set; }

        public DbSet<LeadActivity> LeadActivity { get; set; }
        public DbSet<LeadActivityMember> LeadActivityMember { get; set; }

        public DbSet<WeClappUser> WeClappUser { get; set; }
        public DbSet<ModuleRecordCustomField> ModuleRecordCustomField { get; set; }

        // End Customer Tables

        // start email tables
        public DbSet<EmailProvider> EmailProvider { get; set; }
        public DbSet<EmailConfig> EmailConfig { get; set; }
        public DbSet<EmailLog> EmailLog { get; set; }
        // end email tables

        // Start ERP system tables
        public DbSet<ERPSystem> ERPSystem { get; set; }
        public DbSet<UserERPSystem> UserERPSystem { get; set; }
        public DbSet<ERPSystemColumnMap> ERPSystemColumnMap { get; set; }
        // End ERP system tables

        // // Start Employee Task Tables
        public DbSet<EmployeeTaskStatus> EmployeeTaskStatus { get; set; }
        public DbSet<EmployeeProject> EmployeeProject { get; set; }
        public DbSet<EmployeeProjectUser> EmployeeProjectUser { get; set; }
        public DbSet<EmployeeProjectStatus> EmployeeProjectStatus { get; set; }
        public DbSet<EmployeeTask> EmployeeTask { get; set; }
        public DbSet<EmployeeTaskAttachment> EmployeeTaskAttachment { get; set; }
        public DbSet<EmployeeTaskComment> EmployeeTaskComment { get; set; }
        public DbSet<EmployeeTaskUser> EmployeeTaskUser { get; set; }
        public DbSet<EmployeeTaskTimeRecord> EmployeeTaskTimeRecord { get; set; }
        public DbSet<EmployeeTaskActivity> EmployeeTaskActivity { get; set; }
        public DbSet<EmployeeProjectActivity> EmployeeProjectActivity { get; set; }

        public DbSet<EmployeeSubTask> EmployeeSubTask { get; set; }
        public DbSet<EmployeeSubTaskAttachment> EmployeeSubTaskAttachment { get; set; }
        public DbSet<EmployeeSubTaskComment> EmployeeSubTaskComment { get; set; }
        public DbSet<EmployeeSubTaskUser> EmployeeSubTaskUser { get; set; }
        public DbSet<EmployeeSubTaskTimeRecord> EmployeeSubTaskTimeRecord { get; set; }
        public DbSet<EmployeeSubTaskActivity> EmployeeSubTaskActivity { get; set; }

        public DbSet<EmployeeChildTask> EmployeeChildTask { get; set; }
        public DbSet<EmployeeChildTaskAttachment> EmployeeChildTaskAttachment { get; set; }
        public DbSet<EmployeeChildTaskComment> EmployeeChildTaskComment { get; set; }
        public DbSet<EmployeeChildTaskUser> EmployeeChildTaskUser { get; set; }
        public DbSet<EmployeeChildTaskTimeRecord> EmployeeChildTaskTimeRecord { get; set; }
        public DbSet<EmployeeChildTaskActivity> EmployeeChildTaskActivity { get; set; }

        // // End Employee Task Tables


        // Start Calendar task tables
        public DbSet<CalendarRepeatType> CalendarRepeatType { get; set; }
        public DbSet<CalendarList> CalendarList { get; set; }

        public DbSet<CalendarTask> CalendarTask { get; set; }
        public DbSet<CalendarSubTask> CalendarSubTask { get; set; }
        // End Calendar task tables

        // Start IntProvider tables
        public DbSet<IntProvider> IntProvider { get; set; }
        public DbSet<IntProviderApp> IntProviderApp { get; set; }
        public DbSet<IntProviderAppSecret> IntProviderAppSecret { get; set; }
        public DbSet<CustomDomainEmailConfig> CustomDomainEmailConfig { get; set; }
        public DbSet<CalendarSyncActivity> CalendarSyncActivity { get; set; }
        // End IntProvider tables

        // Start checklist tables
        public DbSet<OneClappModule> OneClappModule { get; set; }
        public DbSet<CheckList> CheckList { get; set; }
        public DbSet<CheckListUser> CheckListUser { get; set; }
        public DbSet<CheckListAssignUser> CheckListAssignUser { get; set; }
        // End checklist tables

        // Start Form builder module tables
        public DbSet<OneClappFormType> OneClappFormType { get; set; }
        public DbSet<OneClappForm> OneClappForm { get; set; }
        public DbSet<OneClappFormField> OneClappFormField { get; set; }
        public DbSet<OneClappFormStatus> OneClappFormStatus { get; set; }
        public DbSet<OneClappRequestForm> OneClappRequestForm { get; set; }
        public DbSet<OneClappFormFieldValue> OneClappFormFieldValue { get; set; }
        public DbSet<OneClappFormAction> OneClappFormAction { get; set; }

        public DbSet<BorderStyle> BorderStyle { get; set; }
        // public DbSet<Border> Border { get; set; }
        // public DbSet<Typography> Typography { get; set; }
        // public DbSet<OneClappFormFieldStyle> OneClappFormFieldStyle { get; set; }
        // public DbSet<OneClappFormStyle> OneClappFormStyle { get; set; }
        public DbSet<OneClappFormHeader> OneClappFormHeader { get; set; }
        public DbSet<OneClappFormLayout> OneClappFormLayout { get; set; }
        public DbSet<OneClappFormLayoutBackground> OneClappFormLayoutBackground { get; set; }
        // public DbSet<BoxShadow> BoxShadow { get; set; }

        // End Form builder module

        // Start Subscription tables
        public DbSet<SubscriptionPlan> SubscriptionPlan { get; set; }
        public DbSet<SubscriptionType> SubscriptionType { get; set; }
        public DbSet<SubscriptionPlanDetail> SubscriptionPlanDetail { get; set; }
        public DbSet<UserSubscription> UserSubscription { get; set; }
        public DbSet<MollieCustomer> MollieCustomer { get; set; }
        public DbSet<MollieSubscription> MollieSubscription { get; set; }

        // End Subscription tables

        public DbSet<ImportContactAttachment> ImportContactAttachment { get; set; }

        public DbSet<ExternalUser> ExternalUser { get; set; }

        // Start mailbox module tables

        public DbSet<MailBoxTeam> MailBoxTeam { get; set; }
        public DbSet<MailComment> MailBoxComment { get; set; }
        public DbSet<MailAssignUser> MailAssignUser { get; set; }
        public DbSet<MailRead> MailRead { get; set; }
        public DbSet<Discussion> Discussion { get; set; }
        public DbSet<DiscussionRead> DiscussionRead { get; set; }
        public DbSet<DiscussionCommentAttachment> DiscussionCommentAttachment { get; set; }
        public DbSet<DiscussionParticipant> DiscussionParticipant { get; set; }
        public DbSet<DiscussionComment> DiscussionComment { get; set; }
        public DbSet<TeamInbox> TeamInbox { get; set; }
        public DbSet<TeamInboxAccess> TeamInboxAccess { get; set; }
        public DbSet<MailCommentAttachment> MailCommentAttachment { get; set; }
        public DbSet<MailParticipant> MailParticipant { get; set; }
        public DbSet<MailAssignCustomer> MailAssignCustomer { get; set; }
        public DbSet<Tax> Tax { get; set; }
        public DbSet<TaxRate> TaxRate { get; set; }
        public DbSet<Currency> Currency { get; set; }
        public DbSet<ServiceArticleCategory> ServiceArticleCategory { get; set; }
        public DbSet<ServiceArticle> ServiceArticle { get; set; }
        public DbSet<ServiceArticleHour> ServiceArticleHour { get; set; }


        // End mailbox module tables

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            // AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);

            foreach (var property in modelBuilder.Model.GetEntityTypes()
                 .SelectMany(t => t.GetProperties())
                 .Where
                 (p
                   => p.ClrType == typeof(DateTime)
                      || p.ClrType == typeof(DateTime?)
                 )
        )
            {
                property.SetColumnType("timestamp without time zone");
            }
        }
    }

    public static class DataSeeder
    {
        public static bool AllMigrationsApplied(this OneClappContext context)
        {
            var applied = context.GetService<IHistoryRepository>()
                .GetAppliedMigrations()
                .Select(m => m.MigrationId);

            var total = context.GetService<IMigrationsAssembly>()
                .Migrations
                .Select(m => m.Key);

            return !total.Except(applied).Any();
        }

        public static void Seed(OneClappContext _context)
        {
            try
            {
                SeedRoles(_context);
                SeedDefaultTenant(_context);
                SeedSuperAdminUser(_context);
                SeedLanguages(_context);
                // SeedThemeColor(_context);
                // SeedLayoutStyle(_context);
                // SeedLayoutWidth(_context);
                // SeedLayoutNavBarPosition(_context);
                // SeedLayoutNavBarVariant(_context);
                // SeedLayoutSidePanelPosition(_context);
                // SeedToolBarFooterPosition(_context);
                SeedCustomControl(_context);
                SeedCustomTable(_context);
                SeedCustomModule(_context);
                SeedCustomTableColumn(_context);
                SeedCustomerType(_context);
                SeedActivityType(_context);
                // SeedOrganizationLabel (_context);
                SeedEmailPhoneNoType(_context);
                SeedLabelCateGory(_context);
                SeedLabel(_context);
                SeedActivityAvailability(_context);
                SeedCalendarRepeatTypes(_context);
                SeedCalendarList(_context);
                SeedOneClappLatestThemeLayout(_context);
                SeedOneClappLatestThemeScheme(_context);
                SeedOneClappLatestTheme(_context);
                SeedIntProviders(_context);
                SeedIntProviderApps(_context);
                SeedOneClappModules(_context);
                SeedOneClappFormTypes(_context);
                SeedOneClappFormStatus(_context);
                SeedOneClappFormAction(_context);
                SeedSubscriptionPlan(_context);
                SeedSubscriptionPlanDetail(_context);
                SeedSubscriptionType(_context);
                SeedSalutation(_context);
                SeedBorderStyle(_context);
                // SeedCustomerLabel (_context);
                // SeedLeadLabel(_context);
                // // _context.SaveChanges ();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static void SeedDefaultTenant(OneClappContext _context)
        {
            if (_context.Tenants != null && !_context.Tenants.Any())
            {
                _context.Tenants.Add(
                    new Tenant()
                    {
                        Username = "Admin",
                        TenantName = "Admin",
                        CreatedOn = DateTime.UtcNow,
                        BlockedOn = null,
                        IsBlocked = false,
                        Token = "Admin@123",
                        IsAdmin = true
                    }
                );

                _context.Tenants.Add(
                    new Tenant()
                    {
                        Username = "Testit",
                        TenantName = "testit",
                        CreatedOn = DateTime.UtcNow,
                        BlockedOn = null,
                        IsBlocked = false,
                        Token = "1beaa048-43c0-47b4-a6d7-2263c6953b5b",
                        IsAdmin = true
                    }
                );

                _context.SaveChanges();
            }
        }

        public static void SeedSuperAdminUser(OneClappContext _context)
        {
            var password = "Admin@123";
            byte[] passwordSalt;
            byte[] passwordHash;
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }

            if (_context.Users != null && !_context.Users.Any())
            {

                // Super admin
                _context.Users.Add(
                    new User()
                    {
                        // TenantId = (int) OneclappDefaultTenants.Admin,
                        TenantId = _context.Tenants.Where(t => t.TenantName == "Admin").FirstOrDefault().TenantId,
                        FirstName = "Super",
                        LastName = "Admin",
                        UserName = "Super Admin",
                        Email = "super.admin@oneclapp.com",
                        WeClappToken = "1beaa048-43c0-47b4-a6d7-2263c6953b5b",
                        PasswordHash = passwordHash,
                        PasswordSalt = passwordSalt,
                        IsEmailVerified = true,
                        VerifiedOn = DateTime.UtcNow,
                        IsBlocked = false,
                        IsDeleted = false,
                        // RoleId = (int) OneclappRoles.Admin,
                        RoleId = _context.Role.Where(t => t.RoleName == "Admin").FirstOrDefault().RoleId,
                        TempGuid = Convert.ToString(Guid.NewGuid()),
                        CreatedOn = DateTime.UtcNow
                    }
                );

                // Tenant admin
                _context.Users.Add(
                    new User()
                    {
                        // TenantId = (int) OneclappDefaultTenants.TestIT,
                        TenantId = _context.Tenants.Where(t => t.TenantName == "testit").FirstOrDefault().TenantId,
                        FirstName = "Tenant",
                        LastName = "Admin",
                        UserName = "TenantAdmin",
                        Email = "tenant.admin@oneclapp.com",
                        PasswordHash = passwordHash,
                        PasswordSalt = passwordSalt,
                        IsEmailVerified = true,
                        VerifiedOn = DateTime.UtcNow,
                        IsBlocked = false,
                        IsDeleted = false,
                        // RoleId = (int) OneclappRoles.TenantAdmin,
                        RoleId = _context.Role.Where(t => t.RoleName == "TenantAdmin").FirstOrDefault().RoleId,
                        TempGuid = Convert.ToString(Guid.NewGuid()),
                        CreatedOn = DateTime.UtcNow
                    }
                );

                // Tenant Manager
                _context.Users.Add(
                    new User()
                    {
                        // TenantId = (int) OneclappDefaultTenants.TestIT,
                        TenantId = _context.Tenants.Where(t => t.TenantName == "testit").FirstOrDefault().TenantId,
                        FirstName = "Tenant",
                        LastName = "Manager",
                        UserName = "TenantManager",
                        Email = "tenant.manager@oneclapp.com",
                        PasswordHash = passwordHash,
                        PasswordSalt = passwordSalt,
                        IsEmailVerified = true,
                        VerifiedOn = DateTime.UtcNow,
                        IsBlocked = false,
                        IsDeleted = false,
                        // RoleId = (int) OneclappRoles.TenantManager,
                        RoleId = _context.Role.Where(t => t.RoleName == "TenantManager").FirstOrDefault().RoleId,
                        TempGuid = Convert.ToString(Guid.NewGuid()),
                        CreatedOn = DateTime.UtcNow
                    }
                );

                // Tenant User
                _context.Users.Add(
                    new User()
                    {
                        // TenantId = (int) OneclappDefaultTenants.TestIT,
                        TenantId = _context.Tenants.Where(t => t.TenantName == "testit").FirstOrDefault().TenantId,
                        FirstName = "Tenant",
                        LastName = "User",
                        UserName = "TenantUser",
                        Email = "tenant.user@oneclapp.com",
                        PasswordHash = passwordHash,
                        PasswordSalt = passwordSalt,
                        IsEmailVerified = true,
                        VerifiedOn = DateTime.UtcNow,
                        IsBlocked = false,
                        IsDeleted = false,
                        // RoleId = (int) OneclappRoles.TenantUser,
                        RoleId = _context.Role.Where(t => t.RoleName == "TenantUser").FirstOrDefault().RoleId,
                        TempGuid = Convert.ToString(Guid.NewGuid()),
                        CreatedOn = DateTime.UtcNow
                    }
                );

                // External User
                _context.Users.Add(
                    new User()
                    {
                        // TenantId = (int) OneclappDefaultTenants.TestIT,
                        TenantId = _context.Tenants.Where(t => t.TenantName == "testit").FirstOrDefault().TenantId,
                        FirstName = "Tenant",
                        LastName = "External User",
                        UserName = "TenantExternalUser",
                        Email = "tenant.externaluser@oneclapp.com",
                        PasswordHash = passwordHash,
                        PasswordSalt = passwordSalt,
                        IsEmailVerified = true,
                        VerifiedOn = DateTime.UtcNow,
                        IsBlocked = false,
                        IsDeleted = false,
                        // RoleId = (int) OneclappRoles.TenantExternalUser,
                        RoleId = _context.Role.Where(t => t.RoleName == "ExternalUser").FirstOrDefault().RoleId,
                        TempGuid = Convert.ToString(Guid.NewGuid()),
                        CreatedOn = DateTime.UtcNow
                    }
                );

                _context.SaveChanges();

            }
        }

        public static void SeedLanguages(OneClappContext _context)
        {

            if (_context.Language != null && !_context.Language.Any())
            {
                List<Language> languageList = new List<Language>() {
                new Language () { LanguageCode = "en-US", LanguageName = "English", IsActive = true, CreatedOn = DateTime.UtcNow, IsDeleted = false, DeletedOn = null },
                new Language () { LanguageCode = "de-DE", LanguageName = "German", IsActive = true, CreatedOn = DateTime.UtcNow, IsDeleted = false, DeletedOn = null },
                new Language () { LanguageCode = "gu-IN", LanguageName = "Gujarati", IsActive = true, CreatedOn = DateTime.UtcNow, IsDeleted = false, DeletedOn = null }
                };
                languageList.AddRange(languageList);
                _context.Language.AddRange(languageList);
                _context.SaveChanges();
            }
        }

        public static void SeedRoles(OneClappContext _context)
        {

            if (_context.Role != null && !_context.Role.Any())
            {
                List<Role> roleList = new List<Role>() {
                new Role () { RoleName = "Admin", IsActive = true, TenantId = null, CreatedOn = DateTime.UtcNow, UpdatedOn = null, IsDeleted = false, DeletedOn = null },
                new Role () { RoleName = "TenantAdmin", IsActive = true, TenantId = null, CreatedOn = DateTime.UtcNow, UpdatedOn = null, IsDeleted = false, DeletedOn = null },
                new Role () { RoleName = "TenantManager", IsActive = true, TenantId = null, CreatedOn = DateTime.UtcNow, UpdatedOn = null, IsDeleted = false, DeletedOn = null },
                new Role () { RoleName = "TenantUser", IsActive = true, TenantId = null, CreatedOn = DateTime.UtcNow, UpdatedOn = null, IsDeleted = false, DeletedOn = null },
                new Role () { RoleName = "ExternalUser", IsActive = true, TenantId = null, CreatedOn = DateTime.UtcNow, UpdatedOn = null, IsDeleted = false, DeletedOn = null }
                };
                roleList.AddRange(roleList);
                _context.Role.AddRange(roleList);
                _context.SaveChanges();
            }
        }

        public static void SeedCustomControl(OneClappContext _context)
        {

            if (_context.CustomControl != null && !_context.CustomControl.Any())
            {
                List<CustomControl> customControlList = new List<CustomControl>() {
                new CustomControl () { Name = "TextBox", CreatedBy = null, CreatedOn = DateTime.UtcNow, IsDeleted = false, DeletedOn = null },
                new CustomControl () { Name = "TextArea", CreatedBy = null, CreatedOn = DateTime.UtcNow, IsDeleted = false, DeletedOn = null },
                new CustomControl () { Name = "DropDown", CreatedBy = null, CreatedOn = DateTime.UtcNow, IsDeleted = false, DeletedOn = null },
                new CustomControl () { Name = "Radio", CreatedBy = null, CreatedOn = DateTime.UtcNow, IsDeleted = false, DeletedOn = null },
                new CustomControl () { Name = "Checkbox", CreatedBy = null, CreatedOn = DateTime.UtcNow, IsDeleted = false, DeletedOn = null },
                new CustomControl () { Name = "Date", CreatedBy = null, CreatedOn = DateTime.UtcNow, IsDeleted = false, DeletedOn = null },
                new CustomControl () { Name = "Heading", CreatedBy = null, CreatedOn = DateTime.UtcNow, IsDeleted = false, DeletedOn = null }
                };
                customControlList.AddRange(customControlList);
                _context.CustomControl.AddRange(customControlList);
                _context.SaveChanges();
            }
        }

        public static void SeedCustomTable(OneClappContext _context)
        {

            if (_context.CustomTable != null && !_context.CustomTable.Any())
            {
                List<CustomTable> customTableList = new List<CustomTable>() {
                new CustomTable () { Name = "Person", CreatedOn = DateTime.UtcNow, IsDeleted = false, DeletedOn = null },
                // new CustomTable () { Name = "Ticket", CreatedOn = DateTime.UtcNow, IsDeleted = false, DeletedOn = null },
                // new CustomTable () { Name = "TaskStatus", CreatedOn = DateTime.UtcNow, IsDeleted = false, DeletedOn = null },
                new CustomTable () { Name = "User", CreatedOn = DateTime.UtcNow, IsDeleted = false, DeletedOn = null },
                new CustomTable () { Name = "Organization", CreatedOn = DateTime.UtcNow, IsDeleted = false, DeletedOn = null },
                new CustomTable () { Name = "Lead", CreatedOn = DateTime.UtcNow, IsDeleted = false, DeletedOn = null },
                new CustomTable () { Name = "Form", CreatedOn = DateTime.UtcNow, IsDeleted = false, DeletedOn = null },
                new CustomTable () { Name = "Project", CreatedOn = DateTime.UtcNow, IsDeleted = false, DeletedOn = null },
                new CustomTable () { Name = "Task", CreatedOn = DateTime.UtcNow, IsDeleted = false, DeletedOn = null }

                };
                customTableList.AddRange(customTableList);
                _context.CustomTable.AddRange(customTableList);
                _context.SaveChanges();
            }
        }

        public static void SeedCustomModule(OneClappContext _context)
        {

            if (_context.CustomModule != null && !_context.CustomModule.Any())
            {
                List<CustomModule> customModuleList = new List<CustomModule>() {
                new CustomModule () { Name = "Person", MasterTableId = _context.CustomTable.Where (t => t.Name == "Person").FirstOrDefault ().Id, CreatedOn = DateTime.UtcNow, IsDeleted = false, DeletedOn = null },
                // new CustomModule () { Name = "Ticket", MasterTableId = _context.CustomTable.Where (t => t.Name == "Ticket").FirstOrDefault ().Id, CreatedOn = DateTime.UtcNow, IsDeleted = false, DeletedOn = null },
                // new CustomModule () { Name = "TaskStatus", MasterTableId = _context.CustomTable.Where (t => t.Name == "TaskStatus").FirstOrDefault ().Id, CreatedOn = DateTime.UtcNow, IsDeleted = false, DeletedOn = null },
                new CustomModule () { Name = "User", MasterTableId = _context.CustomTable.Where (t => t.Name == "User").FirstOrDefault ().Id, CreatedOn = DateTime.UtcNow, IsDeleted = false, DeletedOn = null },
                new CustomModule () { Name = "Organization", MasterTableId = _context.CustomTable.Where (t => t.Name == "Organization").FirstOrDefault ().Id, CreatedOn = DateTime.UtcNow, IsDeleted = false, DeletedOn = null },
                new CustomModule () { Name = "Lead", MasterTableId = _context.CustomTable.Where (t => t.Name == "Lead").FirstOrDefault ().Id, CreatedOn = DateTime.UtcNow, IsDeleted = false, DeletedOn = null },
                new CustomModule () { Name = "Form", MasterTableId = _context.CustomTable.Where (t => t.Name == "Form").FirstOrDefault ().Id, CreatedOn = DateTime.UtcNow, IsDeleted = false, DeletedOn = null },
                new CustomModule () { Name = "TimeTrack", MasterTableId = _context.CustomTable.Where (t => t.Name == "Project").FirstOrDefault ().Id, CreatedOn = DateTime.UtcNow, IsDeleted = false, DeletedOn = null },
                new CustomModule () { Name = "Task", MasterTableId = _context.CustomTable.Where (t => t.Name == "Task").FirstOrDefault ().Id, CreatedOn = DateTime.UtcNow, IsDeleted = false, DeletedOn = null }
                };
                customModuleList.AddRange(customModuleList);
                _context.CustomModule.AddRange(customModuleList);
                _context.SaveChanges();
            }
        }

        public static void SeedCustomTableColumn(OneClappContext _context)
        {

            if (_context.CustomTableColumn != null && !_context.CustomTableColumn.Any())
            {

                // List<CustomTableColumn> tables = new List<CustomTableColumn>() {
                // new CustomTableColumn () { Name = "Name", ControlId = _context.CustomControl.Where (t => t.Name == "TextBox").FirstOrDefault ().Id, MasterTableId = _context.CustomTable.Where(t => t.Name == "Person").FirstOrDefault().Id, IsDefault = true, TenantId = _context.Tenants.Where(t => t.TenantName == "testit").FirstOrDefault().TenantId, CreatedOn = DateTime.UtcNow, IsDeleted = false, DeletedOn = null },
                // new CustomTableColumn () { Name = "Label", ControlId = _context.CustomControl.Where (t => t.Name == "TextBox").FirstOrDefault ().Id, MasterTableId = _context.CustomTable.Where(t => t.Name == "Person").FirstOrDefault().Id, IsDefault = true, TenantId = _context.Tenants.Where(t => t.TenantName == "testit").FirstOrDefault().TenantId, CreatedOn = DateTime.UtcNow, IsDeleted = false, DeletedOn = null },
                // new CustomTableColumn () { Name = "Organization", ControlId = _context.CustomControl.Where (t => t.Name == "TextBox").FirstOrDefault ().Id, MasterTableId = _context.CustomTable.Where(t => t.Name == "Person").FirstOrDefault().Id, IsDefault = true, TenantId = _context.Tenants.Where(t => t.TenantName == "testit").FirstOrDefault().TenantId, CreatedOn = DateTime.UtcNow, IsDeleted = false, DeletedOn = null },
                // new CustomTableColumn () { Name = "Email", ControlId = _context.CustomControl.Where (t => t.Name == "TextBox").FirstOrDefault ().Id, MasterTableId = _context.CustomTable.Where(t => t.Name == "Person").FirstOrDefault().Id, IsDefault = true, TenantId = _context.Tenants.Where(t => t.TenantName == "testit").FirstOrDefault().TenantId, CreatedOn = DateTime.UtcNow, IsDeleted = false, DeletedOn = null },
                // new CustomTableColumn () { Name = "Phone", ControlId = _context.CustomControl.Where (t => t.Name == "TextBox").FirstOrDefault ().Id, MasterTableId = _context.CustomTable.Where(t => t.Name == "Person").FirstOrDefault().Id, IsDefault = true, TenantId = _context.Tenants.Where(t => t.TenantName == "testit").FirstOrDefault().TenantId, CreatedOn = DateTime.UtcNow, IsDeleted = false, DeletedOn = null },
                // new CustomTableColumn () { Name = "Name", ControlId = _context.CustomControl.Where (t => t.Name == "TextBox").FirstOrDefault ().Id, MasterTableId = _context.CustomTable.Where(t => t.Name == "Organization").FirstOrDefault().Id, IsDefault = true, TenantId = _context.Tenants.Where(t => t.TenantName == "testit").FirstOrDefault().TenantId, CreatedOn = DateTime.UtcNow, IsDeleted = false, DeletedOn = null },
                // new CustomTableColumn () { Name = "Label", ControlId = _context.CustomControl.Where (t => t.Name == "TextBox").FirstOrDefault ().Id, MasterTableId = _context.CustomTable.Where(t => t.Name == "Organization").FirstOrDefault().Id, IsDefault = true, TenantId = _context.Tenants.Where(t => t.TenantName == "testit").FirstOrDefault().TenantId, CreatedOn = DateTime.UtcNow, IsDeleted = false, DeletedOn = null },
                // new CustomTableColumn () { Name = "Address", ControlId = _context.CustomControl.Where (t => t.Name == "TextArea").FirstOrDefault ().Id, MasterTableId = _context.CustomTable.Where(t => t.Name == "Organization").FirstOrDefault().Id, IsDefault = true, TenantId = _context.Tenants.Where(t => t.TenantName == "testit").FirstOrDefault().TenantId, CreatedOn = DateTime.UtcNow, IsDeleted = false, DeletedOn = null },
                // // new CustomTableColumn () { Name = "People", ControlId = _context.CustomControl.Where (t => t.Name == "TextBox").FirstOrDefault ().Id, MasterTableId = _context.CustomTable.Where(t => t.Name == "Organization").FirstOrDefault().Id, IsDefault = true, TenantId = _context.Tenants.Where(t => t.TenantName == "testit").FirstOrDefault().TenantId, CreatedOn = DateTime.UtcNow, IsDeleted = false, DeletedOn = null },
                // new CustomTableColumn () { Name = "Title", ControlId = _context.CustomControl.Where (t => t.Name == "TextBox").FirstOrDefault ().Id, MasterTableId = _context.CustomTable.Where(t => t.Name == "Lead").FirstOrDefault().Id, IsDefault = true, TenantId = _context.Tenants.Where(t => t.TenantName == "testit").FirstOrDefault().TenantId, CreatedOn = DateTime.UtcNow, IsDeleted = false, DeletedOn = null },
                // new CustomTableColumn () { Name = "Label", ControlId = _context.CustomControl.Where (t => t.Name == "TextBox").FirstOrDefault ().Id, MasterTableId = _context.CustomTable.Where(t => t.Name == "Lead").FirstOrDefault().Id, IsDefault = true, TenantId = _context.Tenants.Where(t => t.TenantName == "testit").FirstOrDefault().TenantId, CreatedOn = DateTime.UtcNow, IsDeleted = false, DeletedOn = null },
                // new CustomTableColumn () { Name = "Organization", ControlId = _context.CustomControl.Where (t => t.Name == "TextBox").FirstOrDefault ().Id, MasterTableId = _context.CustomTable.Where(t => t.Name == "Lead").FirstOrDefault().Id, IsDefault = true, TenantId = _context.Tenants.Where(t => t.TenantName == "testit").FirstOrDefault().TenantId, CreatedOn = DateTime.UtcNow, IsDeleted = false, DeletedOn = null },
                // new CustomTableColumn () { Name = "Person", ControlId = _context.CustomControl.Where (t => t.Name == "TextBox").FirstOrDefault ().Id, MasterTableId = _context.CustomTable.Where(t => t.Name == "Lead").FirstOrDefault().Id, IsDefault = true, TenantId = _context.Tenants.Where(t => t.TenantName == "testit").FirstOrDefault().TenantId, CreatedOn = DateTime.UtcNow, IsDeleted = false, DeletedOn = null },
                // };

                List<CustomTableColumn> customTableColumnList = new List<CustomTableColumn>() {
                new CustomTableColumn () { Name = "Name", ControlId = _context.CustomControl.Where (t => t.Name == "TextBox").FirstOrDefault ().Id, MasterTableId = _context.CustomTable.Where(t => t.Name == "Person").FirstOrDefault().Id, IsDefault = true, CreatedOn = DateTime.UtcNow, IsDeleted = false, DeletedOn = null },
                new CustomTableColumn () { Name = "Label", ControlId = _context.CustomControl.Where (t => t.Name == "TextBox").FirstOrDefault ().Id, MasterTableId = _context.CustomTable.Where(t => t.Name == "Person").FirstOrDefault().Id, IsDefault = true, CreatedOn = DateTime.UtcNow, IsDeleted = false, DeletedOn = null },
                new CustomTableColumn () { Name = "Organization", ControlId = _context.CustomControl.Where (t => t.Name == "TextBox").FirstOrDefault ().Id, MasterTableId = _context.CustomTable.Where(t => t.Name == "Person").FirstOrDefault().Id, IsDefault = true, CreatedOn = DateTime.UtcNow, IsDeleted = false, DeletedOn = null },
                new CustomTableColumn () { Name = "Email", ControlId = _context.CustomControl.Where (t => t.Name == "TextBox").FirstOrDefault ().Id, MasterTableId = _context.CustomTable.Where(t => t.Name == "Person").FirstOrDefault().Id, IsDefault = true, CreatedOn = DateTime.UtcNow, IsDeleted = false, DeletedOn = null },
                new CustomTableColumn () { Name = "Phone", ControlId = _context.CustomControl.Where (t => t.Name == "TextBox").FirstOrDefault ().Id, MasterTableId = _context.CustomTable.Where(t => t.Name == "Person").FirstOrDefault().Id, IsDefault = true, CreatedOn = DateTime.UtcNow, IsDeleted = false, DeletedOn = null },
                new CustomTableColumn () { Name = "FirstName", ControlId = _context.CustomControl.Where (t => t.Name == "TextBox").FirstOrDefault ().Id, MasterTableId = _context.CustomTable.Where(t => t.Name == "Person").FirstOrDefault().Id, IsDefault = true, CreatedOn = DateTime.UtcNow, IsDeleted = false, DeletedOn = null },
                new CustomTableColumn () { Name = "LastName", ControlId = _context.CustomControl.Where (t => t.Name == "TextBox").FirstOrDefault ().Id, MasterTableId = _context.CustomTable.Where(t => t.Name == "Person").FirstOrDefault().Id, IsDefault = true, CreatedOn = DateTime.UtcNow, IsDeleted = false, DeletedOn = null },
                new CustomTableColumn () { Name = "Salutation", ControlId = _context.CustomControl.Where (t => t.Name == "DropDown").FirstOrDefault ().Id, MasterTableId = _context.CustomTable.Where(t => t.Name == "Person").FirstOrDefault().Id, IsDefault = true, CreatedOn = DateTime.UtcNow, IsDeleted = false, DeletedOn = null },
                new CustomTableColumn () { Name = "Name", ControlId = _context.CustomControl.Where (t => t.Name == "TextBox").FirstOrDefault ().Id, MasterTableId = _context.CustomTable.Where(t => t.Name == "Organization").FirstOrDefault().Id, IsDefault = true, CreatedOn = DateTime.UtcNow, IsDeleted = false, DeletedOn = null },
                new CustomTableColumn () { Name = "Label", ControlId = _context.CustomControl.Where (t => t.Name == "TextBox").FirstOrDefault ().Id, MasterTableId = _context.CustomTable.Where(t => t.Name == "Organization").FirstOrDefault().Id, IsDefault = true, CreatedOn = DateTime.UtcNow, IsDeleted = false, DeletedOn = null },
                new CustomTableColumn () { Name = "Address", ControlId = _context.CustomControl.Where (t => t.Name == "TextArea").FirstOrDefault ().Id, MasterTableId = _context.CustomTable.Where(t => t.Name == "Organization").FirstOrDefault().Id, IsDefault = true, CreatedOn = DateTime.UtcNow, IsDeleted = false, DeletedOn = null },
                // new CustomTableColumn () { Name = "People", ControlId = _context.CustomControl.Where (t => t.Name == "TextBox").FirstOrDefault ().Id, MasterTableId = _context.CustomTable.Where(t => t.Name == "Organization").FirstOrDefault().Id, IsDefault = true, CreatedOn = DateTime.UtcNow, IsDeleted = false, DeletedOn = null },
                new CustomTableColumn () { Name = "Title", ControlId = _context.CustomControl.Where (t => t.Name == "TextBox").FirstOrDefault ().Id, MasterTableId = _context.CustomTable.Where(t => t.Name == "Lead").FirstOrDefault().Id, IsDefault = true, CreatedOn = DateTime.UtcNow, IsDeleted = false, DeletedOn = null },
                new CustomTableColumn () { Name = "Label", ControlId = _context.CustomControl.Where (t => t.Name == "TextBox").FirstOrDefault ().Id, MasterTableId = _context.CustomTable.Where(t => t.Name == "Lead").FirstOrDefault().Id, IsDefault = true, CreatedOn = DateTime.UtcNow, IsDeleted = false, DeletedOn = null },
                new CustomTableColumn () { Name = "Organization", ControlId = _context.CustomControl.Where (t => t.Name == "TextBox").FirstOrDefault ().Id, MasterTableId = _context.CustomTable.Where(t => t.Name == "Lead").FirstOrDefault().Id, IsDefault = true, CreatedOn = DateTime.UtcNow, IsDeleted = false, DeletedOn = null },
                new CustomTableColumn () { Name = "Person", ControlId = _context.CustomControl.Where (t => t.Name == "TextBox").FirstOrDefault ().Id, MasterTableId = _context.CustomTable.Where(t => t.Name == "Lead").FirstOrDefault().Id, IsDefault = true, CreatedOn = DateTime.UtcNow, IsDeleted = false, DeletedOn = null },


                new CustomTableColumn () { Name = "Name", ControlId = _context.CustomControl.Where (t => t.Name == "TextBox").FirstOrDefault ().Id, MasterTableId = _context.CustomTable.Where(t => t.Name == "Form").FirstOrDefault().Id, IsDefault = true, CreatedOn = DateTime.UtcNow, IsDeleted = false, DeletedOn = null },
                new CustomTableColumn () { Name = "SubmissionCount", ControlId = _context.CustomControl.Where (t => t.Name == "TextBox").FirstOrDefault ().Id, MasterTableId = _context.CustomTable.Where(t => t.Name == "Form").FirstOrDefault().Id, IsDefault = true, CreatedOn = DateTime.UtcNow, IsDeleted = false, DeletedOn = null },
                new CustomTableColumn () { Name = "CreatedOn", ControlId = _context.CustomControl.Where (t => t.Name == "TextBox").FirstOrDefault ().Id, MasterTableId = _context.CustomTable.Where(t => t.Name == "Form").FirstOrDefault().Id, IsDefault = true, CreatedOn = DateTime.UtcNow, IsDeleted = false, DeletedOn = null },
                new CustomTableColumn () { Name = "Status", ControlId = _context.CustomControl.Where (t => t.Name == "DropDown").FirstOrDefault ().Id, MasterTableId = _context.CustomTable.Where(t => t.Name == "Form").FirstOrDefault().Id, IsDefault = true, CreatedOn = DateTime.UtcNow, IsDeleted = false, DeletedOn = null },


                new CustomTableColumn () { Name = "Name", ControlId = _context.CustomControl.Where (t => t.Name == "TextBox").FirstOrDefault ().Id, MasterTableId = _context.CustomTable.Where(t => t.Name == "Project").FirstOrDefault().Id, IsDefault = true, CreatedOn = DateTime.UtcNow, IsDeleted = false, DeletedOn = null },
                new CustomTableColumn () { Name = "Description", ControlId = _context.CustomControl.Where (t => t.Name == "TextArea").FirstOrDefault ().Id, MasterTableId = _context.CustomTable.Where(t => t.Name == "Project").FirstOrDefault().Id, IsDefault = true, CreatedOn = DateTime.UtcNow, IsDeleted = false, DeletedOn = null },
                new CustomTableColumn () { Name = "Due Date", ControlId = _context.CustomControl.Where (t => t.Name == "Date").FirstOrDefault ().Id, MasterTableId = _context.CustomTable.Where(t => t.Name == "Project").FirstOrDefault().Id, IsDefault = true, CreatedOn = DateTime.UtcNow, IsDeleted = false, DeletedOn = null },
                new CustomTableColumn () { Name = "Status", ControlId = _context.CustomControl.Where (t => t.Name == "DropDown").FirstOrDefault ().Id, MasterTableId = _context.CustomTable.Where(t => t.Name == "Project").FirstOrDefault().Id, IsDefault = true, CreatedOn = DateTime.UtcNow, IsDeleted = false, DeletedOn = null },
                new CustomTableColumn () { Name = "Logo", ControlId = _context.CustomControl.Where (t => t.Name == "TextBox").FirstOrDefault ().Id, MasterTableId = _context.CustomTable.Where(t => t.Name == "Project").FirstOrDefault().Id, IsDefault = true, CreatedOn = DateTime.UtcNow, IsDeleted = false, DeletedOn = null },
                new CustomTableColumn () { Name = "EstimatedTime", ControlId = _context.CustomControl.Where (t => t.Name == "TextBox").FirstOrDefault ().Id, MasterTableId = _context.CustomTable.Where(t => t.Name == "Project").FirstOrDefault().Id, IsDefault = true, CreatedOn = DateTime.UtcNow, IsDeleted = false, DeletedOn = null }
                };
                customTableColumnList.AddRange(customTableColumnList);
                _context.CustomTableColumn.AddRange(customTableColumnList);
                _context.SaveChanges();
            }
        }

        public static void SeedCustomerType(OneClappContext _context)
        {

            if (_context.CustomerType != null && !_context.CustomerType.Any())
            {
                List<CustomerType> customerTypeList = new List<CustomerType>() {
                new CustomerType () { Name = "Company", CreatedBy = null, CreatedOn = DateTime.UtcNow, UpdatedBy = null, UpdatedOn = null, IsDeleted = false, DeletedOn = null },
                new CustomerType () { Name = "Person", CreatedBy = null, CreatedOn = DateTime.UtcNow, UpdatedBy = null, UpdatedOn = null, IsDeleted = false, DeletedOn = null }
                };
                customerTypeList.AddRange(customerTypeList);
                _context.CustomerType.AddRange(customerTypeList);
                _context.SaveChanges();
            }
        }

        public static void SeedActivityType(OneClappContext _context)
        {

            if (_context.ActivityType != null && !_context.ActivityType.Any())
            {
                List<ActivityType> activityTypeList = new List<ActivityType>() {
                new ActivityType () { Name = "Call", CreatedBy = null, CreatedOn = DateTime.UtcNow, UpdatedBy = null, UpdatedOn = null, IsDeleted = false, DeletedOn = null },
                new ActivityType () { Name = "Meeting", CreatedBy = null, CreatedOn = DateTime.UtcNow, UpdatedBy = null, UpdatedOn = null, IsDeleted = false, DeletedOn = null },
                new ActivityType () { Name = "Task", CreatedBy = null, CreatedOn = DateTime.UtcNow, UpdatedBy = null, UpdatedOn = null, IsDeleted = false, DeletedOn = null },
                new ActivityType () { Name = "DeadLine", CreatedBy = null, CreatedOn = DateTime.UtcNow, UpdatedBy = null, UpdatedOn = null, IsDeleted = false, DeletedOn = null },
                new ActivityType () { Name = "Email", CreatedBy = null, CreatedOn = DateTime.UtcNow, UpdatedBy = null, UpdatedOn = null, IsDeleted = false, DeletedOn = null },
                new ActivityType () { Name = "Lunch", CreatedBy = null, CreatedOn = DateTime.UtcNow, UpdatedBy = null, UpdatedOn = null, IsDeleted = false, DeletedOn = null }
                };
                activityTypeList.AddRange(activityTypeList);
                _context.ActivityType.AddRange(activityTypeList);
                _context.SaveChanges();
            }
        }

        // public static void SeedOrganizationLabel (OneClappContext _context) {

        //     if (_context.OrganizationLabel != null && !_context.OrganizationLabel.Any ()) {
        //         List<OrganizationLabel> tables = new List<OrganizationLabel> () {
        //         new OrganizationLabel () { Name = "CUSTOMER", Color = "green", TenantId = null, CreatedBy = null, CreatedOn = DateTime.UtcNow, UpdatedBy = null, UpdatedOn = null, IsDeleted = false, DeletedOn = null },
        //         new OrganizationLabel () { Name = "HOT LEAD", Color = "red", TenantId = null, CreatedBy = null, CreatedOn = DateTime.UtcNow, UpdatedBy = null, UpdatedOn = null, IsDeleted = false, DeletedOn = null },
        //         new OrganizationLabel () { Name = "WARM LEAD", Color = "#f8cf07", TenantId = null, CreatedBy = null, CreatedOn = DateTime.UtcNow, UpdatedBy = null, UpdatedOn = null, IsDeleted = false, DeletedOn = null },
        //         new OrganizationLabel () { Name = "COLD LEAD", Color = "#13b4ff", TenantId = null, CreatedBy = null, CreatedOn = DateTime.UtcNow, UpdatedBy = null, UpdatedOn = null, IsDeleted = false, DeletedOn = null },
        //         new OrganizationLabel () { Name = "SUPPLIER", Color = "#ab3fdd", TenantId = null, CreatedBy = null, CreatedOn = DateTime.UtcNow, UpdatedBy = null, UpdatedOn = null, IsDeleted = false, DeletedOn = null }
        //         };
        //         tables.AddRange (tables);
        //         _context.OrganizationLabel.AddRange (tables);
        //     }
        // }

        // public static void SeedCustomerLabel (OneClappContext _context) {

        //     if (_context.OrganizationLabel != null && !_context.OrganizationLabel.Any ()) {
        //         List<CustomerLabel> tables = new List<CustomerLabel> () {
        //         new CustomerLabel () { Name = "CUSTOMER", Color = "green", TenantId = null, CreatedBy = null, CreatedOn = DateTime.UtcNow, UpdatedBy = null, UpdatedOn = null, IsDeleted = false, DeletedOn = null },
        //         new CustomerLabel () { Name = "HOT LEAD", Color = "red", TenantId = null, CreatedBy = null, CreatedOn = DateTime.UtcNow, UpdatedBy = null, UpdatedOn = null, IsDeleted = false, DeletedOn = null },
        //         new CustomerLabel () { Name = "WARM LEAD", Color = "#f8cf07", TenantId = null, CreatedBy = null, CreatedOn = DateTime.UtcNow, UpdatedBy = null, UpdatedOn = null, IsDeleted = false, DeletedOn = null },
        //         new CustomerLabel () { Name = "COLD LEAD", Color = "#13b4ff", TenantId = null, CreatedBy = null, CreatedOn = DateTime.UtcNow, UpdatedBy = null, UpdatedOn = null, IsDeleted = false, DeletedOn = null },
        //         new CustomerLabel () { Name = "SUPPLIER", Color = "#ab3fdd", TenantId = null, CreatedBy = null, CreatedOn = DateTime.UtcNow, UpdatedBy = null, UpdatedOn = null, IsDeleted = false, DeletedOn = null }
        //         };
        //         tables.AddRange (tables);
        //         _context.CustomerLabel.AddRange (tables);
        //     }
        // }

        public static void SeedEmailPhoneNoType(OneClappContext _context)
        {

            if (_context.EmailPhoneNoType != null && !_context.EmailPhoneNoType.Any())
            {
                List<EmailPhoneNoType> emailPhoneNoTypeList = new List<EmailPhoneNoType>() {
                new EmailPhoneNoType () { Name = "Work", CreatedOn = DateTime.UtcNow, IsDeleted = false, DeletedOn = null },
                new EmailPhoneNoType () { Name = "Home", CreatedOn = DateTime.UtcNow, IsDeleted = false, DeletedOn = null },
                new EmailPhoneNoType () { Name = "Mobile", CreatedOn = DateTime.UtcNow, IsDeleted = false, DeletedOn = null },
                new EmailPhoneNoType () { Name = "Other", CreatedOn = DateTime.UtcNow, IsDeleted = false, DeletedOn = null }
                };
                emailPhoneNoTypeList.AddRange(emailPhoneNoTypeList);
                _context.EmailPhoneNoType.AddRange(emailPhoneNoTypeList);
                _context.SaveChanges();
            }
        }

        // public static void SeedLeadLabel (OneClappContext _context) {

        //     if (_context.LeadLabel != null && !_context.LeadLabel.Any ()) {
        //         List<LeadLabel> tables = new List<LeadLabel> () {
        //         new LeadLabel () { Name = "Hot", CreatedOn = DateTime.UtcNow, IsDeleted = false, DeletedOn = null },
        //         new LeadLabel () { Name = "Warm", CreatedOn = DateTime.UtcNow, IsDeleted = false, DeletedOn = null },
        //         new LeadLabel () { Name = "Cold", CreatedOn = DateTime.UtcNow, IsDeleted = false, DeletedOn = null }
        //         };
        //         tables.AddRange (tables);
        //         _context.LeadLabel.AddRange (tables);
        //     }
        // }

        public static void SeedLabelCateGory(OneClappContext _context)
        {

            if (_context.LabelCategory != null && !_context.LabelCategory.Any())
            {
                List<LabelCategory> labelCategoryList = new List<LabelCategory>() {
                new LabelCategory () { Name = "Person", CreatedOn = DateTime.UtcNow, IsDeleted = false, DeletedOn = null },
                new LabelCategory () { Name = "Organization", CreatedOn = DateTime.UtcNow, IsDeleted = false, DeletedOn = null },
                new LabelCategory () { Name = "Lead", CreatedOn = DateTime.UtcNow, IsDeleted = false, DeletedOn = null }
                };
                labelCategoryList.AddRange(labelCategoryList);
                _context.LabelCategory.AddRange(labelCategoryList);
                _context.SaveChanges();
            }
        }

        public static void SeedLabel(OneClappContext _context)
        {

            if (_context.Label != null && !_context.Label.Any())
            {
                List<Label> labelList = new List<Label>() {
                new Label () { Name = "CUSTOMER", Color = "green", LabelCategoryId = _context.LabelCategory.Where (t => t.Name == "Person").FirstOrDefault ().Id, TenantId = null, CreatedBy = null, CreatedOn = DateTime.UtcNow, UpdatedBy = null, UpdatedOn = null, IsDeleted = false, DeletedOn = null },
                new Label () { Name = "HOT LEAD", Color = "red", LabelCategoryId = _context.LabelCategory.Where (t => t.Name == "Person").FirstOrDefault ().Id, TenantId = null, CreatedBy = null, CreatedOn = DateTime.UtcNow, UpdatedBy = null, UpdatedOn = null, IsDeleted = false, DeletedOn = null },
                new Label () { Name = "WARM LEAD", Color = "#f8cf07", LabelCategoryId = _context.LabelCategory.Where (t => t.Name == "Person").FirstOrDefault ().Id, TenantId = null, CreatedBy = null, CreatedOn = DateTime.UtcNow, UpdatedBy = null, UpdatedOn = null, IsDeleted = false, DeletedOn = null },
                new Label () { Name = "COLD LEAD", Color = "#13b4ff", LabelCategoryId = _context.LabelCategory.Where (t => t.Name == "Person").FirstOrDefault ().Id, TenantId = null, CreatedBy = null, CreatedOn = DateTime.UtcNow, UpdatedBy = null, UpdatedOn = null, IsDeleted = false, DeletedOn = null },
                new Label () { Name = "SUPPLIER", Color = "#ab3fdd", LabelCategoryId = _context.LabelCategory.Where (t => t.Name == "Person").FirstOrDefault ().Id, TenantId = null, CreatedBy = null, CreatedOn = DateTime.UtcNow, UpdatedBy = null, UpdatedOn = null, IsDeleted = false, DeletedOn = null },

                new Label () { Name = "CUSTOMER", Color = "green", LabelCategoryId = _context.LabelCategory.Where (t => t.Name == "Organization").FirstOrDefault ().Id, TenantId = null, CreatedBy = null, CreatedOn = DateTime.UtcNow, UpdatedBy = null, UpdatedOn = null, IsDeleted = false, DeletedOn = null },
                new Label () { Name = "HOT LEAD", Color = "red", LabelCategoryId = _context.LabelCategory.Where (t => t.Name == "Organization").FirstOrDefault ().Id, TenantId = null, CreatedBy = null, CreatedOn = DateTime.UtcNow, UpdatedBy = null, UpdatedOn = null, IsDeleted = false, DeletedOn = null },
                new Label () { Name = "WARM LEAD", Color = "#f8cf07", LabelCategoryId = _context.LabelCategory.Where (t => t.Name == "Organization").FirstOrDefault ().Id, TenantId = null, CreatedBy = null, CreatedOn = DateTime.UtcNow, UpdatedBy = null, UpdatedOn = null, IsDeleted = false, DeletedOn = null },
                new Label () { Name = "COLD LEAD", Color = "#13b4ff", LabelCategoryId = _context.LabelCategory.Where (t => t.Name == "Organization").FirstOrDefault ().Id, TenantId = null, CreatedBy = null, CreatedOn = DateTime.UtcNow, UpdatedBy = null, UpdatedOn = null, IsDeleted = false, DeletedOn = null },
                new Label () { Name = "SUPPLIER", Color = "#ab3fdd", LabelCategoryId = _context.LabelCategory.Where (t => t.Name == "Organization").FirstOrDefault ().Id, TenantId = null, CreatedBy = null, CreatedOn = DateTime.UtcNow, UpdatedBy = null, UpdatedOn = null, IsDeleted = false, DeletedOn = null },

                new Label () { Name = "Hot", Color = "red", LabelCategoryId = _context.LabelCategory.Where (t => t.Name == "Lead").FirstOrDefault ().Id, TenantId = null, CreatedBy = null, CreatedOn = DateTime.UtcNow, UpdatedBy = null, UpdatedOn = null, IsDeleted = false, DeletedOn = null },
                new Label () { Name = "Warm", Color = "#f8cf07", LabelCategoryId = _context.LabelCategory.Where (t => t.Name == "Lead").FirstOrDefault ().Id, TenantId = null, CreatedBy = null, CreatedOn = DateTime.UtcNow, UpdatedBy = null, UpdatedOn = null, IsDeleted = false, DeletedOn = null },
                new Label () { Name = "Cold", Color = "#13b4ff", LabelCategoryId = _context.LabelCategory.Where (t => t.Name == "Lead").FirstOrDefault ().Id, TenantId = null, CreatedBy = null, CreatedOn = DateTime.UtcNow, UpdatedBy = null, UpdatedOn = null, IsDeleted = false, DeletedOn = null },

                };
                labelList.AddRange(labelList);
                _context.Label.AddRange(labelList);
                _context.SaveChanges();
            }
        }

        public static void SeedActivityAvailability(OneClappContext _context)
        {
            if (_context.ActivityAvailability != null && !_context.ActivityAvailability.Any())
            {
                List<ActivityAvailability> activityAvailabilityList = new List<ActivityAvailability>() {
                new ActivityAvailability () { Name = "Busy" },
                new ActivityAvailability () { Name = "Free" }
                };
                activityAvailabilityList.AddRange(activityAvailabilityList);
                _context.ActivityAvailability.AddRange(activityAvailabilityList);
                _context.SaveChanges();
            }
        }

        public static void SeedCalendarRepeatTypes(OneClappContext _context)
        {
            if (_context.CalendarRepeatType != null && !_context.CalendarRepeatType.Any())
            {
                List<CalendarRepeatType> calendarRepeatTypeList = new List<CalendarRepeatType>() {
                new CalendarRepeatType () { Name = "day" },
                new CalendarRepeatType () { Name = "week" },
                new CalendarRepeatType () { Name = "month" },
                new CalendarRepeatType () { Name = "year" }
                };
                calendarRepeatTypeList.AddRange(calendarRepeatTypeList);
                _context.CalendarRepeatType.AddRange(calendarRepeatTypeList);
                _context.SaveChanges();
            }
        }

        public static void SeedCalendarList(OneClappContext _context)
        {
            if (_context.CalendarList != null && !_context.CalendarList.Any())
            {
                List<CalendarList> calendarList = new List<CalendarList>() {
                new CalendarList () { Name = "My Tasks" }
                };
                calendarList.AddRange(calendarList);
                _context.CalendarList.AddRange(calendarList);
                _context.SaveChanges();
            }
        }

        public static void SeedOneClappLatestThemeLayout(OneClappContext _context)
        {
            if (_context.OneClappLatestThemeLayout != null && !_context.OneClappLatestThemeLayout.Any())
            {
                List<OneClappLatestThemeLayout> oneClappLatestThemeLayoutList = new List<OneClappLatestThemeLayout>() {
                // new OneClappLatestThemeLayout () { Name = "empty", TemplateHtml= "<div class='flex flex-col cursor-pointer' (click)='setLayout('empty')'><div class='flex flex-col h-20 rounded-md overflow-hidden border-2 hover:opacity-80' [class.border-primary]='config.layout === 'empty''><div class='flex flex-col flex-auto bg-gray-50 dark:bg - gray - 900'></div></ div >< div class='mt-2 text-md font-medium text-center text-secondary' [class.text-primary]='config.layout === 'empty''> Empty </div></div>" },
                // new OneClappLatestThemeLayout() { Name = "centered", TemplateHtml="<div class='flex flex-col cursor-pointer'><div class='flex h-20 rounded-md overflow-hidden border-2 hover:opacity-80'><div class='flex flex-col flex-auto my-1 mx-2 border rounded-md overflow-hidden'><div class='flex items-center h-3 bg-gray-100 dark:bg-gray-800'><div class='flex ml-1.5'><div class='w-1 h-1 rounded-full bg-gray-300 dark:bg-gray-700'></div><div class='w-3 h-1 ml-1 rounded-full bg-gray-300 dark:bg-gray-700'></div><div class='w-3 h-1 ml-1 rounded-full bg-gray-300 dark:bg-gray-700'></div><div class='w-3 h-1 ml-1 rounded-full bg-gray-300 dark:bg-gray-700'></div></div><div class='flex items-center justify-end ml-auto mr-1.5'><div class='w-1 h-1 ml-1 rounded-full bg-gray-300 dark:bg-gray-700'></div><div class='w-1 h-1 ml-1 rounded-full bg-gray-300 dark:bg-gray-700'></div></div></div><div class='flex flex-auto border-t bg-gray-50 dark:bg-gray-900'></div></div></div> </div>" },
                // new OneClappLatestThemeLayout() { Name = "enterprise" , TemplateHtml="<div class='flex flex-col cursor-pointer'><div class='flex flex-col h-20 rounded-md overflow-hidden border-2 hover:opacity-80'><div class='flex items-center h-3 px-2 bg-gray-100 dark:bg-gray-800'><div class='w-2 h-2 rounded-full bg-gray-300 dark:bg-gray-700'></div><div class='flex items-center justify-end ml-auto space-x-1'><div class='w-1 h-1 rounded-full bg-gray-300 dark:bg-gray-700'></div><div class='w-1 h-1 rounded-full bg-gray-300 dark:bg-gray-700'></div><div class='w-1 h-1 rounded-full bg-gray-300 dark:bg-gray-700'></div></div></div><div class='flex items-center h-3 px-2 border-t border-b space-x-1 bg-gray-100 dark:bg-gray-800'><div class='w-3 h-1 rounded-full bg-gray-300 dark:bg-gray-700'></div><div class='w-3 h-1 rounded-full bg-gray-300 dark:bg-gray-700'></div><div class='w-3 h-1 rounded-full bg-gray-300 dark:bg-gray-700'></div><div class='w-3 h-1 rounded-full bg-gray-300 dark:bg-gray-700'></div></div><div class='flex flex-col flex-auto my-1 mx-2 border rounded overflow-hidden'><div class='flex flex-auto bg-gray-50 dark:bg-gray-900'></div></div></div></div>" },
                // new OneClappLatestThemeLayout() { Name = "material", TemplateHtml="<div class='flex flex-col cursor-pointer'><div class='flex flex-col h-20 rounded-md overflow-hidden border-2 hover:opacity-80'><div class='flex flex-col flex-auto my-1 mx-2 border rounded overflow-hidden'><div class='flex items-center h-4 px-2 bg-gray-100 dark:bg-gray-800'><div class='w-2 h-2 rounded-full bg-gray-300 dark:bg-gray-700'></div><div class='flex items-center justify-end ml-auto space-x-1'><div class='w-1 h-1 rounded-full bg-gray-300 dark:bg-gray-700'></div><div class='w-1 h-1 rounded-full bg-gray-300 dark:bg-gray-700'></div><div class='w-1 h-1 rounded-full bg-gray-300 dark:bg-gray-700'></div></div></div><div class='flex items-center h-2 px-2 space-x-1 bg-gray-100 dark:bg-gray-800'><div class='w-3 h-1 rounded-full bg-gray-300 dark:bg-gray-700'></div><div class='w-3 h-1 rounded-full bg-gray-300 dark:bg-gray-700'></div><div class='w-3 h-1 rounded-full bg-gray-300 dark:bg-gray-700'></div><div class='w-3 h-1 rounded-full bg-gray-300 dark:bg-gray-700'></div></div><div class='flex flex-auto border-t bg-gray-50 dark:bg-gray-900'></div></div></div> </div>" },
                // new OneClappLatestThemeLayout() { Name = "modern", TemplateHtml="<div class='flex flex-col cursor-pointer'><div class='flex flex-col h-20 rounded-md overflow-hidden border-2 hover:opacity-80'><div class='flex items-center h-4 px-2 border-b bg-gray-100 dark:bg-gray-800'><div class='w-2 h-2 rounded-full bg-gray-300 dark:bg-gray-700'></div><div class='flex items-center h-3 ml-2 space-x-1'><div class='w-3 h-1 rounded-full bg-gray-300 dark:bg-gray-700'></div><div class='w-3 h-1 rounded-full bg-gray-300 dark:bg-gray-700'></div><div class='w-3 h-1 rounded-full bg-gray-300 dark:bg-gray-700'></div></div><div class='flex items-center justify-end ml-auto space-x-1'><div class='w-1 h-1 rounded-full bg-gray-300 dark:bg-gray-700'></div><div class='w-1 h-1 rounded-full bg-gray-300 dark:bg-gray-700'></div></div></div><div class='flex flex-col flex-auto'><div class='flex flex-auto bg-gray-50 dark:bg-gray-900'></div></div></div> </div>" },
                // new OneClappLatestThemeLayout() { Name = "classic", TemplateHtml="<div class='flex flex-col cursor-pointer'> <div class='flex h-20 rounded-md overflow-hidden border-2 hover:opacity-80'><div class='w-8 bg-gray-100 dark:bg-gray-800'><div class='mt-3 mx-1.5 space-y-1'><div class='h-1 rounded-sm bg-gray-300 dark:bg-gray-700'></div><div class='h-1 rounded-sm bg-gray-300 dark:bg-gray-700'></div><div class='h-1 rounded-sm bg-gray-300 dark:bg-gray-700'></div><div class='h-1 rounded-sm bg-gray-300 dark:bg-gray-700'></div><div class='h-1 rounded-sm bg-gray-300 dark:bg-gray-700'></div></div></div><div class='flex flex-col flex-auto border-l'><div class='h-3 bg-gray-100 dark:bg-gray-800'><div class='flex items-center justify-end h-full mr-1.5'><div class='w-1 h-1 ml-1 rounded-full bg-gray-300 dark:bg-gray-700'></div><div class='w-1 h-1 ml-1 rounded-full bg-gray-300 dark:bg-gray-700'></div><div class='w-1 h-1 ml-1 rounded-full bg-gray-300 dark:bg-gray-700'></div></div></div><div class='flex flex-auto border-t bg-gray-50 dark:bg-gray-900'></div></div></div><div class='mt-2 text-md font-medium text-center text-secondary'></div></div>" },
                // new OneClappLatestThemeLayout() { Name = "classy", TemplateHtml="<div class='flex flex-col cursor-pointer' > <div class='flex h-20 rounded-md overflow-hidden border-2 hover:opacity-80'><div class='w-8 bg-gray-100 dark:bg-gray-800'><div class='flex items-center mt-1 mx-1'><div class='w-1 h-1 rounded-full bg-gray-300 dark:bg-gray-700'></div><div class='w-1 h-1 ml-auto rounded-full bg-gray-300 dark:bg-gray-700'></div><div class='w-1 h-1 ml-0.5 rounded-full bg-gray-300 dark:bg-gray-700'></div></div><div class='w-4 h-4 mt-2.5 mx-auto rounded-full bg-gray-300 dark:bg-gray-700'></div><div class='mt-2 mx-1 space-y-1'><div class='h-1 rounded-sm bg-gray-300 dark:bg-gray-700'></div><div class='h-1 rounded-sm bg-gray-300 dark:bg-gray-700'></div><div class='h-1 rounded-sm bg-gray-300 dark:confibg-gray-700'></div><div class='h-1 rounded-sm bg-gray-300 dark:bg-gray-700'></div></div></div><div class='flex flex-col flex-auto border-l'><div class='h-3 bg-gray-100 dark:bg-gray-800'><div class='flex items-center justify-end h-full mr-2'><div class='w-1 h-1 rounded-full bg-gray-300 dark:bg-gray-700'></div></div></div><div class='flex flex-auto border-t bg-gray-50 dark:bg-gray-900'></div></div></div></div>" },
                // new OneClappLatestThemeLayout() { Name = "compact", TemplateHtml="<div class='flex flex-col cursor-pointer'><div class='flex h-20 rounded-md overflow-hidden border-2 hover:opacity-80'><div class='w-5 bg-gray-100 dark:bg-gray-800'><div class='w-3 h-3 mt-2 mx-auto rounded-sm bg-gray-300 dark:bg-gray-700'></div><div class='flex flex-col items-center w-full mt-2 space-y-1'><div class='w-3 h-2.5 rounded-sm bg-gray-300 dark:bg-gray-700'></div><div class='w-3 h-2.5 rounded-sm bg-gray-300 dark:bg-gray-700'></div><div class='w-3 h-2.5 rounded-sm bg-gray-300 dark:bg-gray-700'></div></div></div><div class='flex flex-col flex-auto border-l'><div class='h-3 bg-gray-100 dark:bg-gray-800'><div class='flex items-center justify-end h-full mr-1.5'><div class='w-1 h-1 ml-1 rounded-full bg-gray-300 dark:bg-gray-700'></div><div class='w-1 h-1 ml-1 rounded-full bg-gray-300 dark:bg-gray-700'></div><div class='w-1 h-1 ml-1 rounded-full bg-gray-300 dark:bg-gray-700'></div></div></div><div class='flex flex-auto border-t bg-gray-50 dark:bg-gray-900'></div></div></div></div>" },
                // new OneClappLatestThemeLayout() { Name = "dense", TemplateHtml="<div class='flex flex-col cursor-pointer'><div class='flex h-20 rounded-md overflow-hidden border-2 hover:opacity-80'><div class='w-4 bg-gray-100 dark:bg-gray-800'><div class='w-2 h-2 mt-2 mx-auto rounded-sm bg-gray-300 dark:bg-gray-700'></div><div class='flex flex-col items-center w-full mt-2 space-y-1'><div class='w-2 h-2 rounded-sm bg-gray-300 dark:bg-gray-700'></div><div class='w-2 h-2 rounded-sm bg-gray-300 dark:bg-gray-700'></div><div class='w-2 h-2 rounded-sm bg-gray-300 dark:bg-gray-700'></div></div></div><div class='flex flex-col flex-auto border-l'><div class='h-3 bg-gray-100 dark:bg-gray-800'><div class='flex items-center justify-end h-full mr-1.5'><div class='w-1 h-1 ml-1 rounded-full bg-gray-300 dark:bg-gray-700'></div><div class='w-1 h-1 ml-1 rounded-full bg-gray-300 dark:bg-gray-700'></div><div class='w-1 h-1 ml-1 rounded-full bg-gray-300 dark:bg-gray-700'></div></div></div><div class='flex flex-auto border-t bg-gray-50 dark:bg-gray-900'></div></div></div></div>" },
                // new OneClappLatestThemeLayout() { Name = "futuristic", TemplateHtml="<div class='flex flex-col cursor-pointer'><div class='flex h-20 rounded-md overflow-hidden border-2 hover:opacity-80'><div class='w-8 bg-gray-100 dark:bg-gray-800'><div class='flex flex-col flex-auto h-full py-3 px-1.5 space-y-1'><div class='h-1 rounded-sm bg-gray-300 dark:bg-gray-700'></div><div class='h-1 rounded-sm bg-gray-300 dark:bg-gray-700'></div><div class='h-1 rounded-sm bg-gray-300 dark:bg-gray-700'></div><div class='flex-auto'></div><div class='h-1 rounded-sm bg-gray-300 dark:bg-gray-700'></div></div></div><div class='flex flex-col flex-auto border-l'><div class='h-3 bg-gray-100 dark:bg-gray-800'><div class='flex items-center justify-end h-full mr-1.5'><div class='w-1 h-1 ml-1 rounded-full bg-gray-300 dark:bg-gray-700'></div><div class='w-1 h-1 ml-1 rounded-full bg-gray-300 dark:bg-gray-700'></div><div class='w-1 h-1 ml-1 rounded-full bg-gray-300 dark:bg-gray-700'></div></div></div><div class='flex flex-auto border-t bg-gray-50 dark:bg-gray-900'></div></div></div></div>" },
                // new OneClappLatestThemeLayout() { Name = "thin", TemplateHtml="<div lass='flex flex-col cursor-pointer'><div class='flex h-20 rounded-md overflow-hidden border-2 hover:opacity-80'><div class='w-3 bg-gray-100 dark:bg-gray-800'><div class='w-1.5 h-1.5 mt-2 mx-auto rounded-sm bg-gray-300 dark:bg-gray-700'></div><div class='flex flex-col items-center w-full mt-2 space-y-1'><div class='w-1.5 h-1.5 rounded-full bg-gray-300 dark:bg-gray-700'></div><div class='w-1.5 h-1.5 rounded-full bg-gray-300 dark:bg-gray-700'></div><div class='w-1.5 h-1.5 rounded-full bg-gray-300 dark:bg-gray-700'></div>div class='w-1.5 h-1.5 rounded-full bg-gray-300 dark:bg-gray-700'></div><div class='w-1.5 h-1.5 rounded-full bg-gray-300 dark:bg-gray-700'></div></div></div><div class='flex flex-col flex-auto border-l'><div class='h-3 bg-gray-100 dark:bg-gray-800'><div class='flex items-center justify-end h-full mr-1.5'><div class='w-1 h-1 ml-1 rounded-full bg-gray-300 dark:bg-gray-700'></div><div class='w-1 h-1 ml-1 rounded-full bg-gray-300 dark:bg-gray-700'></div><div class='w-1 h-1 ml-1 rounded-full bg-gray-300 dark:bg-gray-700'></div></div></div><div class='flex flex-auto border-t bg-gray-50 dark:bg-gray-900'></div></div></div></div>" }


                // new OneClappLatestThemeLayout() { Name = "modern", CreatedOn = DateTime.UtcNow ,TemplateHtml="<div class='flex flex-col cursor-pointer'><div class='flex flex-col h-20 rounded-md overflow-hidden border-2 hover:opacity-80'><div class='flex items-center h-4 px-2 border-b bg-gray-100 dark:bg-gray-800'><div class='w-2 h-2 rounded-full bg-gray-300 dark:bg-gray-700'></div><div class='flex items-center h-3 ml-2 space-x-1'><div class='w-3 h-1 rounded-full bg-gray-300 dark:bg-gray-700'></div><div class='w-3 h-1 rounded-full bg-gray-300 dark:bg-gray-700'></div><div class='w-3 h-1 rounded-full bg-gray-300 dark:bg-gray-700'></div></div><div class='flex items-center justify-end ml-auto space-x-1'><div class='w-1 h-1 rounded-full bg-gray-300 dark:bg-gray-700'></div><div class='w-1 h-1 rounded-full bg-gray-300 dark:bg-gray-700'></div></div></div><div class='flex flex-col flex-auto'><div class='flex flex-auto bg-gray-50 dark:bg-gray-900'></div></div></div> </div>" },
                // new OneClappLatestThemeLayout() { Name = "classic", CreatedOn = DateTime.UtcNow,TemplateHtml="<div class='flex flex-col cursor-pointer'> <div class='flex h-20 rounded-md overflow-hidden border-2 hover:opacity-80'><div class='w-8 bg-gray-100 dark:bg-gray-800'><div class='mt-3 mx-1.5 space-y-1'><div class='h-1 rounded-sm bg-gray-300 dark:bg-gray-700'></div><div class='h-1 rounded-sm bg-gray-300 dark:bg-gray-700'></div><div class='h-1 rounded-sm bg-gray-300 dark:bg-gray-700'></div><div class='h-1 rounded-sm bg-gray-300 dark:bg-gray-700'></div><div class='h-1 rounded-sm bg-gray-300 dark:bg-gray-700'></div></div></div><div class='flex flex-col flex-auto border-l'><div class='h-3 bg-gray-100 dark:bg-gray-800'><div class='flex items-center justify-end h-full mr-1.5'><div class='w-1 h-1 ml-1 rounded-full bg-gray-300 dark:bg-gray-700'></div><div class='w-1 h-1 ml-1 rounded-full bg-gray-300 dark:bg-gray-700'></div><div class='w-1 h-1 ml-1 rounded-full bg-gray-300 dark:bg-gray-700'></div></div></div><div class='flex flex-auto border-t bg-gray-50 dark:bg-gray-900'></div></div></div><div class='mt-2 text-md font-medium text-center text-secondary'></div></div>" },
                // new OneClappLatestThemeLayout() { Name = "classy",CreatedOn = DateTime.UtcNow, TemplateHtml="<div class='flex flex-col cursor-pointer' > <div class='flex h-20 rounded-md overflow-hidden border-2 hover:opacity-80'><div class='w-8 bg-gray-100 dark:bg-gray-800'><div class='flex items-center mt-1 mx-1'><div class='w-1 h-1 rounded-full bg-gray-300 dark:bg-gray-700'></div><div class='w-1 h-1 ml-auto rounded-full bg-gray-300 dark:bg-gray-700'></div><div class='w-1 h-1 ml-0.5 rounded-full bg-gray-300 dark:bg-gray-700'></div></div><div class='w-4 h-4 mt-2.5 mx-auto rounded-full bg-gray-300 dark:bg-gray-700'></div><div class='mt-2 mx-1 space-y-1'><div class='h-1 rounded-sm bg-gray-300 dark:bg-gray-700'></div><div class='h-1 rounded-sm bg-gray-300 dark:bg-gray-700'></div><div class='h-1 rounded-sm bg-gray-300 dark:confibg-gray-700'></div><div class='h-1 rounded-sm bg-gray-300 dark:bg-gray-700'></div></div></div><div class='flex flex-col flex-auto border-l'><div class='h-3 bg-gray-100 dark:bg-gray-800'><div class='flex items-center justify-end h-full mr-2'><div class='w-1 h-1 rounded-full bg-gray-300 dark:bg-gray-700'></div></div></div><div class='flex flex-auto border-t bg-gray-50 dark:bg-gray-900'></div></div></div></div>" },
                // new OneClappLatestThemeLayout() { Name = "compact",CreatedOn = DateTime.UtcNow, TemplateHtml="<div class='flex flex-col cursor-pointer'><div class='flex h-20 rounded-md overflow-hidden border-2 hover:opacity-80'><div class='w-5 bg-gray-100 dark:bg-gray-800'><div class='w-3 h-3 mt-2 mx-auto rounded-sm bg-gray-300 dark:bg-gray-700'></div><div class='flex flex-col items-center w-full mt-2 space-y-1'><div class='w-3 h-2.5 rounded-sm bg-gray-300 dark:bg-gray-700'></div><div class='w-3 h-2.5 rounded-sm bg-gray-300 dark:bg-gray-700'></div><div class='w-3 h-2.5 rounded-sm bg-gray-300 dark:bg-gray-700'></div></div></div><div class='flex flex-col flex-auto border-l'><div class='h-3 bg-gray-100 dark:bg-gray-800'><div class='flex items-center justify-end h-full mr-1.5'><div class='w-1 h-1 ml-1 rounded-full bg-gray-300 dark:bg-gray-700'></div><div class='w-1 h-1 ml-1 rounded-full bg-gray-300 dark:bg-gray-700'></div><div class='w-1 h-1 ml-1 rounded-full bg-gray-300 dark:bg-gray-700'></div></div></div><div class='flex flex-auto border-t bg-gray-50 dark:bg-gray-900'></div></div></div></div>" },
                // new OneClappLatestThemeLayout() { Name = "dense",CreatedOn = DateTime.UtcNow, TemplateHtml="<div class='flex flex-col cursor-pointer'><div class='flex h-20 rounded-md overflow-hidden border-2 hover:opacity-80'><div class='w-4 bg-gray-100 dark:bg-gray-800'><div class='w-2 h-2 mt-2 mx-auto rounded-sm bg-gray-300 dark:bg-gray-700'></div><div class='flex flex-col items-center w-full mt-2 space-y-1'><div class='w-2 h-2 rounded-sm bg-gray-300 dark:bg-gray-700'></div><div class='w-2 h-2 rounded-sm bg-gray-300 dark:bg-gray-700'></div><div class='w-2 h-2 rounded-sm bg-gray-300 dark:bg-gray-700'></div></div></div><div class='flex flex-col flex-auto border-l'><div class='h-3 bg-gray-100 dark:bg-gray-800'><div class='flex items-center justify-end h-full mr-1.5'><div class='w-1 h-1 ml-1 rounded-full bg-gray-300 dark:bg-gray-700'></div><div class='w-1 h-1 ml-1 rounded-full bg-gray-300 dark:bg-gray-700'></div><div class='w-1 h-1 ml-1 rounded-full bg-gray-300 dark:bg-gray-700'></div></div></div><div class='flex flex-auto border-t bg-gray-50 dark:bg-gray-900'></div></div></div></div>" },
                // new OneClappLatestThemeLayout() { Name = "futuristic",CreatedOn = DateTime.UtcNow, TemplateHtml="<div class='flex flex-col cursor-pointer'><div class='flex h-20 rounded-md overflow-hidden border-2 hover:opacity-80'><div class='w-8 bg-gray-100 dark:bg-gray-800'><div class='flex flex-col flex-auto h-full py-3 px-1.5 space-y-1'><div class='h-1 rounded-sm bg-gray-300 dark:bg-gray-700'></div><div class='h-1 rounded-sm bg-gray-300 dark:bg-gray-700'></div><div class='h-1 rounded-sm bg-gray-300 dark:bg-gray-700'></div><div class='flex-auto'></div><div class='h-1 rounded-sm bg-gray-300 dark:bg-gray-700'></div></div></div><div class='flex flex-col flex-auto border-l'><div class='h-3 bg-gray-100 dark:bg-gray-800'><div class='flex items-center justify-end h-full mr-1.5'><div class='w-1 h-1 ml-1 rounded-full bg-gray-300 dark:bg-gray-700'></div><div class='w-1 h-1 ml-1 rounded-full bg-gray-300 dark:bg-gray-700'></div><div class='w-1 h-1 ml-1 rounded-full bg-gray-300 dark:bg-gray-700'></div></div></div><div class='flex flex-auto border-t bg-gray-50 dark:bg-gray-900'></div></div></div></div>" },
                // new OneClappLatestThemeLayout() { Name = "thin",CreatedOn = DateTime.UtcNow, TemplateHtml="<div class='flex flex-col cursor-pointer'><div class='flex h-20 rounded-md overflow-hidden border-2 hover:opacity-80'><div class='w-3 bg-gray-100 dark:bg-gray-800'><div class='w-1.5 h-1.5 mt-2 mx-auto rounded-sm bg-gray-300 dark:bg-gray-700'></div><div class='flex flex-col items-center w-full mt-2 space-y-1'><div class='w-1.5 h-1.5 rounded-full bg-gray-300 dark:bg-gray-700'></div><div class='w-1.5 h-1.5 rounded-full bg-gray-300 dark:bg-gray-700'></div><div class='w-1.5 h-1.5 rounded-full bg-gray-300 dark:bg-gray-700'></div><div class='w-1.5 h-1.5 rounded-full bg-gray-300 dark:bg-gray-700'></div><div class='w-1.5 h-1.5 rounded-full bg-gray-300 dark:bg-gray-700'></div></div></div><div class='flex flex-col flex-auto border-l'><div class='h-3 bg-gray-100 dark:bg-gray-800'><div class='flex items-center justify-end h-full mr-1.5'><div class='w-1 h-1 ml-1 rounded-full bg-gray-300 dark:bg-gray-700'></div><div class='w-1 h-1 ml-1 rounded-full bg-gray-300 dark:bg-gray-700'></div><div class='w-1 h-1 ml-1 rounded-full bg-gray-300 dark:bg-gray-700'></div></div></div><div class='flex flex-auto border-t bg-gray-50 dark:bg-gray-900'></div></div></div></div>" },
                // new OneClappLatestThemeLayout () { Name = "empty",CreatedOn = DateTime.UtcNow, TemplateHtml= "<div class='flex flex-col cursor-pointer' (click)='setLayout('empty')'><div class='flex flex-col h-20 rounded-md overflow-hidden border-2 hover:opacity-80' [class.border-primary]='config.layout === 'empty''><div class='flex flex-col flex-auto bg-gray-50 dark:bg - gray - 900'></div></ div >< div class='mt-2 text-md font-medium text-center text-secondary' [class.text-primary]='config.layout === 'empty''> Empty </div></div>" ,IsDeleted = true},
                // new OneClappLatestThemeLayout() { Name = "centered",CreatedOn = DateTime.UtcNow, TemplateHtml="<div class='flex flex-col cursor-pointer'><div class='flex h-20 rounded-md overflow-hidden border-2 hover:opacity-80'><div class='flex flex-col flex-auto my-1 mx-2 border rounded-md overflow-hidden'><div class='flex items-center h-3 bg-gray-100 dark:bg-gray-800'><div class='flex ml-1.5'><div class='w-1 h-1 rounded-full bg-gray-300 dark:bg-gray-700'></div><div class='w-3 h-1 ml-1 rounded-full bg-gray-300 dark:bg-gray-700'></div><div class='w-3 h-1 ml-1 rounded-full bg-gray-300 dark:bg-gray-700'></div><div class='w-3 h-1 ml-1 rounded-full bg-gray-300 dark:bg-gray-700'></div></div><div class='flex items-center justify-end ml-auto mr-1.5'><div class='w-1 h-1 ml-1 rounded-full bg-gray-300 dark:bg-gray-700'></div><div class='w-1 h-1 ml-1 rounded-full bg-gray-300 dark:bg-gray-700'></div></div></div><div class='flex flex-auto border-t bg-gray-50 dark:bg-gray-900'></div></div></div> </div>" ,IsDeleted = true},
                // new OneClappLatestThemeLayout() { Name = "enterprise" ,CreatedOn = DateTime.UtcNow, TemplateHtml="<div class='flex flex-col cursor-pointer'><div class='flex flex-col h-20 rounded-md overflow-hidden border-2 hover:opacity-80'><div class='flex items-center h-3 px-2 bg-gray-100 dark:bg-gray-800'><div class='w-2 h-2 rounded-full bg-gray-300 dark:bg-gray-700'></div><div class='flex items-center justify-end ml-auto space-x-1'><div class='w-1 h-1 rounded-full bg-gray-300 dark:bg-gray-700'></div><div class='w-1 h-1 rounded-full bg-gray-300 dark:bg-gray-700'></div><div class='w-1 h-1 rounded-full bg-gray-300 dark:bg-gray-700'></div></div></div><div class='flex items-center h-3 px-2 border-t border-b space-x-1 bg-gray-100 dark:bg-gray-800'><div class='w-3 h-1 rounded-full bg-gray-300 dark:bg-gray-700'></div><div class='w-3 h-1 rounded-full bg-gray-300 dark:bg-gray-700'></div><div class='w-3 h-1 rounded-full bg-gray-300 dark:bg-gray-700'></div><div class='w-3 h-1 rounded-full bg-gray-300 dark:bg-gray-700'></div></div><div class='flex flex-col flex-auto my-1 mx-2 border rounded overflow-hidden'><div class='flex flex-auto bg-gray-50 dark:bg-gray-900'></div></div></div></div>" ,IsDeleted = true },
                // new OneClappLatestThemeLayout() { Name = "material",CreatedOn = DateTime.UtcNow, TemplateHtml="<div class='flex flex-col cursor-pointer'><div class='flex flex-col h-20 rounded-md overflow-hidden border-2 hover:opacity-80'><div class='flex flex-col flex-auto my-1 mx-2 border rounded overflow-hidden'><div class='flex items-center h-4 px-2 bg-gray-100 dark:bg-gray-800'><div class='w-2 h-2 rounded-full bg-gray-300 dark:bg-gray-700'></div><div class='flex items-center justify-end ml-auto space-x-1'><div class='w-1 h-1 rounded-full bg-gray-300 dark:bg-gray-700'></div><div class='w-1 h-1 rounded-full bg-gray-300 dark:bg-gray-700'></div><div class='w-1 h-1 rounded-full bg-gray-300 dark:bg-gray-700'></div></div></div><div class='flex items-center h-2 px-2 space-x-1 bg-gray-100 dark:bg-gray-800'><div class='w-3 h-1 rounded-full bg-gray-300 dark:bg-gray-700'></div><div class='w-3 h-1 rounded-full bg-gray-300 dark:bg-gray-700'></div><div class='w-3 h-1 rounded-full bg-gray-300 dark:bg-gray-700'></div><div class='w-3 h-1 rounded-full bg-gray-300 dark:bg-gray-700'></div></div><div class='flex flex-auto border-t bg-gray-50 dark:bg-gray-900'></div></div></div> </div>" ,IsDeleted = true},

                // update on date:29-09-2021
                // new OneClappLatestThemeLayout() { Name = "modern", CreatedOn = DateTime.UtcNow ,TemplateHtml="<div class='flex flex-col cursor-pointer'><div class='flex flex-col h-20 rounded-md overflow-hidden  hover:opacity-80'><div class='flex items-center h-4 px-2 border-b bg-gray-100 dark:bg-gray-800'><div class='w-2 h-2 rounded-full bg-gray-300 dark:bg-gray-700'></div><div class='flex items-center h-3 ml-2 space-x-1'><div class='w-3 h-1 rounded-full bg-gray-300 dark:bg-gray-700'></div><div class='w-3 h-1 rounded-full bg-gray-300 dark:bg-gray-700'></div><div class='w-3 h-1 rounded-full bg-gray-300 dark:bg-gray-700'></div></div><div class='flex items-center justify-end ml-auto space-x-1'><div class='w-1 h-1 rounded-full bg-gray-300 dark:bg-gray-700'></div><div class='w-1 h-1 rounded-full bg-gray-300 dark:bg-gray-700'></div></div></div><div class='flex flex-col flex-auto'><div class='flex flex-auto bg-gray-50 dark:bg-gray-900'></div></div></div> </div>" },
                // new OneClappLatestThemeLayout() { Name = "classic", CreatedOn = DateTime.UtcNow,TemplateHtml=" <div class='flex flex-col cursor-pointer'><div class='flex h-20 rounded-md overflow-hidden   hover:opacity-80'><div class='w-8 bg-gray-100 dark:bg-gray-800'><div class='mt-3 mx-1.5 space-y-1'><div class='h-1 rounded-sm bg-gray-300 dark:bg-gray-700'></div><div class='h-1 rounded-sm bg-gray-300 dark:bg-gray-700'></div><div class='h-1 rounded-sm bg-gray-300 dark:bg-gray-700'></div><div class='h-1 rounded-sm bg-gray-300 dark:bg-gray-700'></div><div class='h-1 rounded-sm bg-gray-300 dark:bg-gray-700'></div></div></div><div class='flex flex-col flex-auto border-l'><div class='h-3 bg-gray-100 dark:bg-gray-800'><div class='flex items-center justify-end h-full mr-1.5'><div class='w-1 h-1 ml-1 rounded-full bg-gray-300 dark:bg-gray-700'></div><div class='w-1 h-1 ml-1 rounded-full bg-gray-300 dark:bg-gray-700'></div><div class='w-1 h-1 ml-1 rounded-full bg-gray-300 dark:bg-gray-700'></div></div></div><div class='flex flex-auto border-t bg-gray-50 dark:bg-gray-900'></div></div></div> </div>" },
                // new OneClappLatestThemeLayout() { Name = "classy",CreatedOn = DateTime.UtcNow, TemplateHtml="<div class='flex flex-col cursor-pointer' > <div class='flex h-20 rounded-md overflow-hidden  hover:opacity-80'><div class='w-8 bg-gray-100 dark:bg-gray-800'><div class='flex items-center mt-1 mx-1'><div class='w-1 h-1 rounded-full bg-gray-300 dark:bg-gray-700'></div><div class='w-1 h-1 ml-auto rounded-full bg-gray-300 dark:bg-gray-700'></div><div class='w-1 h-1 ml-0.5 rounded-full bg-gray-300 dark:bg-gray-700'></div></div><div class='w-4 h-4 mt-2.5 mx-auto rounded-full bg-gray-300 dark:bg-gray-700'></div><div class='mt-2 mx-1 space-y-1'><div class='h-1 rounded-sm bg-gray-300 dark:bg-gray-700'></div><div class='h-1 rounded-sm bg-gray-300 dark:bg-gray-700'></div><div class='h-1 rounded-sm bg-gray-300 dark:confibg-gray-700'></div><div class='h-1 rounded-sm bg-gray-300 dark:bg-gray-700'></div></div></div><div class='flex flex-col flex-auto border-l'><div class='h-3 bg-gray-100 dark:bg-gray-800'><div class='flex items-center justify-end h-full mr-2'><div class='w-1 h-1 rounded-full bg-gray-300 dark:bg-gray-700'></div></div></div><div class='flex flex-auto border-t bg-gray-50 dark:bg-gray-900'></div></div></div></div>" },
                // new OneClappLatestThemeLayout() { Name = "compact",CreatedOn = DateTime.UtcNow, TemplateHtml="<div class='flex flex-col cursor-pointer'><div class='flex h-20 rounded-md overflow-hidden  hover:opacity-80'><div class='w-5 bg-gray-100 dark:bg-gray-800'><div class='w-3 h-3 mt-2 mx-auto rounded-sm bg-gray-300 dark:bg-gray-700'></div><div class='flex flex-col items-center w-full mt-2 space-y-1'><div class='w-3 h-2.5 rounded-sm bg-gray-300 dark:bg-gray-700'></div><div class='w-3 h-2.5 rounded-sm bg-gray-300 dark:bg-gray-700'></div><div class='w-3 h-2.5 rounded-sm bg-gray-300 dark:bg-gray-700'></div></div></div><div class='flex flex-col flex-auto border-l'><div class='h-3 bg-gray-100 dark:bg-gray-800'><div class='flex items-center justify-end h-full mr-1.5'><div class='w-1 h-1 ml-1 rounded-full bg-gray-300 dark:bg-gray-700'></div><div class='w-1 h-1 ml-1 rounded-full bg-gray-300 dark:bg-gray-700'></div><div class='w-1 h-1 ml-1 rounded-full bg-gray-300 dark:bg-gray-700'></div></div></div><div class='flex flex-auto border-t bg-gray-50 dark:bg-gray-900'></div></div></div></div>" },
                // new OneClappLatestThemeLayout() { Name = "dense",CreatedOn = DateTime.UtcNow, TemplateHtml="<div class='flex flex-col cursor-pointer'><div class='flex h-20 rounded-md overflow-hidden  hover:opacity-80'><div class='w-4 bg-gray-100 dark:bg-gray-800'><div class='w-2 h-2 mt-2 mx-auto rounded-sm bg-gray-300 dark:bg-gray-700'></div><div class='flex flex-col items-center w-full mt-2 space-y-1'><div class='w-2 h-2 rounded-sm bg-gray-300 dark:bg-gray-700'></div><div class='w-2 h-2 rounded-sm bg-gray-300 dark:bg-gray-700'></div><div class='w-2 h-2 rounded-sm bg-gray-300 dark:bg-gray-700'></div></div></div><div class='flex flex-col flex-auto border-l'><div class='h-3 bg-gray-100 dark:bg-gray-800'><div class='flex items-center justify-end h-full mr-1.5'><div class='w-1 h-1 ml-1 rounded-full bg-gray-300 dark:bg-gray-700'></div><div class='w-1 h-1 ml-1 rounded-full bg-gray-300 dark:bg-gray-700'></div><div class='w-1 h-1 ml-1 rounded-full bg-gray-300 dark:bg-gray-700'></div></div></div><div class='flex flex-auto border-t bg-gray-50 dark:bg-gray-900'></div></div></div></div>" },
                // new OneClappLatestThemeLayout() { Name = "futuristic",CreatedOn = DateTime.UtcNow, TemplateHtml="<div class='flex flex-col cursor-pointer'><div class='flex h-20 rounded-md overflow-hidden  hover:opacity-80'><div class='w-8 bg-gray-100 dark:bg-gray-800'><div class='flex flex-col flex-auto h-full py-3 px-1.5 space-y-1'><div class='h-1 rounded-sm bg-gray-300 dark:bg-gray-700'></div><div class='h-1 rounded-sm bg-gray-300 dark:bg-gray-700'></div><div class='h-1 rounded-sm bg-gray-300 dark:bg-gray-700'></div><div class='flex-auto'></div><div class='h-1 rounded-sm bg-gray-300 dark:bg-gray-700'></div></div></div><div class='flex flex-col flex-auto border-l'><div class='h-3 bg-gray-100 dark:bg-gray-800'><div class='flex items-center justify-end h-full mr-1.5'><div class='w-1 h-1 ml-1 rounded-full bg-gray-300 dark:bg-gray-700'></div><div class='w-1 h-1 ml-1 rounded-full bg-gray-300 dark:bg-gray-700'></div><div class='w-1 h-1 ml-1 rounded-full bg-gray-300 dark:bg-gray-700'></div></div></div><div class='flex flex-auto border-t bg-gray-50 dark:bg-gray-900'></div></div></div></div>" },
                // new OneClappLatestThemeLayout() { Name = "thin",CreatedOn = DateTime.UtcNow, TemplateHtml="<div class='flex flex-col cursor-pointer'><div class='flex h-20 rounded-md overflow-hidden  hover:opacity-80'><div class='w-3 bg-gray-100 dark:bg-gray-800'><div class='w-1.5 h-1.5 mt-2 mx-auto rounded-sm bg-gray-300 dark:bg-gray-700'></div><div class='flex flex-col items-center w-full mt-2 space-y-1'><div class='w-1.5 h-1.5 rounded-full bg-gray-300 dark:bg-gray-700'></div><div class='w-1.5 h-1.5 rounded-full bg-gray-300 dark:bg-gray-700'></div><div class='w-1.5 h-1.5 rounded-full bg-gray-300 dark:bg-gray-700'></div><div class='w-1.5 h-1.5 rounded-full bg-gray-300 dark:bg-gray-700'></div><div class='w-1.5 h-1.5 rounded-full bg-gray-300 dark:bg-gray-700'></div></div></div><div class='flex flex-col flex-auto border-l'><div class='h-3 bg-gray-100 dark:bg-gray-800'><div class='flex items-center justify-end h-full mr-1.5'><div class='w-1 h-1 ml-1 rounded-full bg-gray-300 dark:bg-gray-700'></div><div class='w-1 h-1 ml-1 rounded-full bg-gray-300 dark:bg-gray-700'></div><div class='w-1 h-1 ml-1 rounded-full bg-gray-300 dark:bg-gray-700'></div></div></div><div class='flex flex-auto border-t bg-gray-50 dark:bg-gray-900'></div></div></div></div>" },
                // new OneClappLatestThemeLayout () { Name = "empty",CreatedOn = DateTime.UtcNow, TemplateHtml= "<div class='flex flex-col cursor-pointer' (click)='setLayout('empty')'><div class='flex flex-col h-20 rounded-md overflow-hidden  hover:opacity-80' [class.border-primary]='config.layout === 'empty''><div class='flex flex-col flex-auto bg-gray-50 dark:bg - gray - 900'></div></ div >< div class='mt-2 text-md font-medium text-center text-secondary' [class.text-primary]='config.layout === 'empty''> Empty </div></div>" ,IsDeleted = true},
                // new OneClappLatestThemeLayout() { Name = "centered",CreatedOn = DateTime.UtcNow, TemplateHtml="<div class='flex flex-col cursor-pointer'><div class='flex h-20 rounded-md overflow-hidden  hover:opacity-80'><div class='flex flex-col flex-auto my-1 mx-2 border rounded-md overflow-hidden'><div class='flex items-center h-3 bg-gray-100 dark:bg-gray-800'><div class='flex ml-1.5'><div class='w-1 h-1 rounded-full bg-gray-300 dark:bg-gray-700'></div><div class='w-3 h-1 ml-1 rounded-full bg-gray-300 dark:bg-gray-700'></div><div class='w-3 h-1 ml-1 rounded-full bg-gray-300 dark:bg-gray-700'></div><div class='w-3 h-1 ml-1 rounded-full bg-gray-300 dark:bg-gray-700'></div></div><div class='flex items-center justify-end ml-auto mr-1.5'><div class='w-1 h-1 ml-1 rounded-full bg-gray-300 dark:bg-gray-700'></div><div class='w-1 h-1 ml-1 rounded-full bg-gray-300 dark:bg-gray-700'></div></div></div><div class='flex flex-auto border-t bg-gray-50 dark:bg-gray-900'></div></div></div> </div>" ,IsDeleted = true},
                // new OneClappLatestThemeLayout() { Name = "enterprise" ,CreatedOn = DateTime.UtcNow, TemplateHtml="<div class='flex flex-col cursor-pointer'><div class='flex flex-col h-20 rounded-md overflow-hidden  hover:opacity-80'><div class='flex items-center h-3 px-2 bg-gray-100 dark:bg-gray-800'><div class='w-2 h-2 rounded-full bg-gray-300 dark:bg-gray-700'></div><div class='flex items-center justify-end ml-auto space-x-1'><div class='w-1 h-1 rounded-full bg-gray-300 dark:bg-gray-700'></div><div class='w-1 h-1 rounded-full bg-gray-300 dark:bg-gray-700'></div><div class='w-1 h-1 rounded-full bg-gray-300 dark:bg-gray-700'></div></div></div><div class='flex items-center h-3 px-2 border-t border-b space-x-1 bg-gray-100 dark:bg-gray-800'><div class='w-3 h-1 rounded-full bg-gray-300 dark:bg-gray-700'></div><div class='w-3 h-1 rounded-full bg-gray-300 dark:bg-gray-700'></div><div class='w-3 h-1 rounded-full bg-gray-300 dark:bg-gray-700'></div><div class='w-3 h-1 rounded-full bg-gray-300 dark:bg-gray-700'></div></div><div class='flex flex-col flex-auto my-1 mx-2 border rounded overflow-hidden'><div class='flex flex-auto bg-gray-50 dark:bg-gray-900'></div></div></div></div>" ,IsDeleted = true },
                // new OneClappLatestThemeLayout() { Name = "material",CreatedOn = DateTime.UtcNow, TemplateHtml="<div class='flex flex-col cursor-pointer'><div class='flex flex-col h-20 rounded-md overflow-hidden  hover:opacity-80'><div class='flex flex-col flex-auto my-1 mx-2 border rounded overflow-hidden'><div class='flex items-center h-4 px-2 bg-gray-100 dark:bg-gray-800'><div class='w-2 h-2 rounded-full bg-gray-300 dark:bg-gray-700'></div><div class='flex items-center justify-end ml-auto space-x-1'><div class='w-1 h-1 rounded-full bg-gray-300 dark:bg-gray-700'></div><div class='w-1 h-1 rounded-full bg-gray-300 dark:bg-gray-700'></div><div class='w-1 h-1 rounded-full bg-gray-300 dark:bg-gray-700'></div></div></div><div class='flex items-center h-2 px-2 space-x-1 bg-gray-100 dark:bg-gray-800'><div class='w-3 h-1 rounded-full bg-gray-300 dark:bg-gray-700'></div><div class='w-3 h-1 rounded-full bg-gray-300 dark:bg-gray-700'></div><div class='w-3 h-1 rounded-full bg-gray-300 dark:bg-gray-700'></div><div class='w-3 h-1 rounded-full bg-gray-300 dark:bg-gray-700'></div></div><div class='flex flex-auto border-t bg-gray-50 dark:bg-gray-900'></div></div></div> </div>" ,IsDeleted = true},

                // update on date:30-09-2021
                new OneClappLatestThemeLayout() { Name = "modern", CreatedOn = DateTime.UtcNow ,TemplateHtml="<div class='flex flex-col cursor-pointer'><div class='flex flex-col h-20 rounded-md overflow-hidden  hover:opacity-80'><div class='flex items-center h-4 px-2 border-b bg-gray-100 dark:bg-gray-800'><div class='w-2 h-2 rounded-full bg-gray-300 dark:bg-gray-700'></div><div class='flex items-center h-3 ml-2 space-x-1'><div class='w-3 h-1 rounded-full bg-gray-300 dark:bg-gray-700'></div><div class='w-3 h-1 rounded-full bg-gray-300 dark:bg-gray-700'></div><div class='w-3 h-1 rounded-full bg-gray-300 dark:bg-gray-700'></div></div><div class='flex items-center justify-end ml-auto space-x-1'><div class='w-1 h-1 rounded-full bg-gray-300 dark:bg-gray-700'></div><div class='w-1 h-1 rounded-full bg-gray-300 dark:bg-gray-700'></div></div></div><div class='flex flex-col flex-auto'><div class='flex flex-auto bg-gray-50 dark:bg-gray-900'></div></div></div> </div>" },
                // new OneClappLatestThemeLayout() { Name = "classic", CreatedOn = DateTime.UtcNow,TemplateHtml=" <div class='flex flex-col cursor-pointer'><div class='flex h-20 rounded-md overflow-hidden   hover:opacity-80'><div class='w-8 bg-gray-100 dark:bg-gray-800'><div class='mt-3 mx-1.5 space-y-1'><div class='h-1 rounded-sm bg-gray-300 dark:bg-gray-700'></div><div class='h-1 rounded-sm bg-gray-300 dark:bg-gray-700'></div><div class='h-1 rounded-sm bg-gray-300 dark:bg-gray-700'></div><div class='h-1 rounded-sm bg-gray-300 dark:bg-gray-700'></div><div class='h-1 rounded-sm bg-gray-300 dark:bg-gray-700'></div></div></div><div class='flex flex-col flex-auto border-l'><div class='h-3 bg-gray-100 dark:bg-gray-800'><div class='flex items-center justify-end h-full mr-1.5'><div class='w-1 h-1 ml-1 rounded-full bg-gray-300 dark:bg-gray-700'></div><div class='w-1 h-1 ml-1 rounded-full bg-gray-300 dark:bg-gray-700'></div><div class='w-1 h-1 ml-1 rounded-full bg-gray-300 dark:bg-gray-700'></div></div></div><div class='flex flex-auto border-t bg-gray-50 dark:bg-gray-900'></div></div></div> </div>" },
                // new OneClappLatestThemeLayout() { Name = "classy",CreatedOn = DateTime.UtcNow, TemplateHtml="<div class='flex flex-col cursor-pointer' > <div class='flex h-20 rounded-md overflow-hidden  hover:opacity-80'><div class='w-8 bg-gray-100 dark:bg-gray-800'><div class='flex items-center mt-1 mx-1'><div class='w-1 h-1 rounded-full bg-gray-300 dark:bg-gray-700'></div><div class='w-1 h-1 ml-auto rounded-full bg-gray-300 dark:bg-gray-700'></div><div class='w-1 h-1 ml-0.5 rounded-full bg-gray-300 dark:bg-gray-700'></div></div><div class='w-4 h-4 mt-2.5 mx-auto rounded-full bg-gray-300 dark:bg-gray-700'></div><div class='mt-2 mx-1 space-y-1'><div class='h-1 rounded-sm bg-gray-300 dark:bg-gray-700'></div><div class='h-1 rounded-sm bg-gray-300 dark:bg-gray-700'></div><div class='h-1 rounded-sm bg-gray-300 dark:bg-gray-700'></div><div class='h-1 rounded-sm bg-gray-300 dark:bg-gray-700'></div></div></div><div class='flex flex-col flex-auto border-l'><div class='h-3 bg-gray-100 dark:bg-gray-800'><div class='flex items-center justify-end h-full mr-2'><div class='w-1 h-1 rounded-full bg-gray-300 dark:bg-gray-700'></div></div></div><div class='flex flex-auto border-t bg-gray-50 dark:bg-gray-900'></div></div></div></div>" },
                new OneClappLatestThemeLayout() { Name = "compact",CreatedOn = DateTime.UtcNow, TemplateHtml="<div class='flex flex-col cursor-pointer'><div class='flex h-20 rounded-md overflow-hidden  hover:opacity-80'><div class='w-5 bg-gray-100 dark:bg-gray-800'><div class='w-3 h-3 mt-2 mx-auto rounded-sm bg-gray-300 dark:bg-gray-700'></div><div class='flex flex-col items-center w-full mt-2 space-y-1'><div class='w-3 h-2.5 rounded-sm bg-gray-300 dark:bg-gray-700'></div><div class='w-3 h-2.5 rounded-sm bg-gray-300 dark:bg-gray-700'></div><div class='w-3 h-2.5 rounded-sm bg-gray-300 dark:bg-gray-700'></div></div></div><div class='flex flex-col flex-auto border-l'><div class='h-3 bg-gray-100 dark:bg-gray-800'><div class='flex items-center justify-end h-full mr-1.5'><div class='w-1 h-1 ml-1 rounded-full bg-gray-300 dark:bg-gray-700'></div><div class='w-1 h-1 ml-1 rounded-full bg-gray-300 dark:bg-gray-700'></div><div class='w-1 h-1 ml-1 rounded-full bg-gray-300 dark:bg-gray-700'></div></div></div><div class='flex flex-auto border-t bg-gray-50 dark:bg-gray-900'></div></div></div></div>" },
                new OneClappLatestThemeLayout() { Name = "dense",CreatedOn = DateTime.UtcNow, TemplateHtml="<div class='flex flex-col cursor-pointer'><div class='flex h-20 rounded-md overflow-hidden  hover:opacity-80'><div class='w-4 bg-gray-100 dark:bg-gray-800'><div class='w-2 h-2 mt-2 mx-auto rounded-sm bg-gray-300 dark:bg-gray-700'></div><div class='flex flex-col items-center w-full mt-2 space-y-1'><div class='w-2 h-2 rounded-sm bg-gray-300 dark:bg-gray-700'></div><div class='w-2 h-2 rounded-sm bg-gray-300 dark:bg-gray-700'></div><div class='w-2 h-2 rounded-sm bg-gray-300 dark:bg-gray-700'></div></div></div><div class='flex flex-col flex-auto border-l'><div class='h-3 bg-gray-100 dark:bg-gray-800'><div class='flex items-center justify-end h-full mr-1.5'><div class='w-1 h-1 ml-1 rounded-full bg-gray-300 dark:bg-gray-700'></div><div class='w-1 h-1 ml-1 rounded-full bg-gray-300 dark:bg-gray-700'></div><div class='w-1 h-1 ml-1 rounded-full bg-gray-300 dark:bg-gray-700'></div></div></div><div class='flex flex-auto border-t bg-gray-50 dark:bg-gray-900'></div></div></div></div>" },
                // new OneClappLatestThemeLayout() { Name = "futuristic",CreatedOn = DateTime.UtcNow, TemplateHtml="<div class='flex flex-col cursor-pointer'><div class='flex h-20 rounded-md overflow-hidden  hover:opacity-80'><div class='w-8 bg-gray-100 dark:bg-gray-800'><div class='flex flex-col flex-auto h-full py-3 px-1.5 space-y-1'><div class='h-1 rounded-sm bg-gray-300 dark:bg-gray-700'></div><div class='h-1 rounded-sm bg-gray-300 dark:bg-gray-700'></div><div class='h-1 rounded-sm bg-gray-300 dark:bg-gray-700'></div><div class='flex-auto'></div><div class='h-1 rounded-sm bg-gray-300 dark:bg-gray-700'></div></div></div><div class='flex flex-col flex-auto border-l'><div class='h-3 bg-gray-100 dark:bg-gray-800'><div class='flex items-center justify-end h-full mr-1.5'><div class='w-1 h-1 ml-1 rounded-full bg-gray-300 dark:bg-gray-700'></div><div class='w-1 h-1 ml-1 rounded-full bg-gray-300 dark:bg-gray-700'></div><div class='w-1 h-1 ml-1 rounded-full bg-gray-300 dark:bg-gray-700'></div></div></div><div class='flex flex-auto border-t bg-gray-50 dark:bg-gray-900'></div></div></div></div>" },
                new OneClappLatestThemeLayout() { Name = "thin",CreatedOn = DateTime.UtcNow, TemplateHtml="<div class='flex flex-col cursor-pointer'><div class='flex h-20 rounded-md overflow-hidden  hover:opacity-80'><div class='w-3 bg-gray-100 dark:bg-gray-800'><div class='w-1.5 h-1.5 mt-2 mx-auto rounded-sm bg-gray-300 dark:bg-gray-700'></div><div class='flex flex-col items-center w-full mt-2 space-y-1'><div class='w-1.5 h-1.5 rounded-full bg-gray-300 dark:bg-gray-700'></div><div class='w-1.5 h-1.5 rounded-full bg-gray-300 dark:bg-gray-700'></div><div class='w-1.5 h-1.5 rounded-full bg-gray-300 dark:bg-gray-700'></div><div class='w-1.5 h-1.5 rounded-full bg-gray-300 dark:bg-gray-700'></div><div class='w-1.5 h-1.5 rounded-full bg-gray-300 dark:bg-gray-700'></div></div></div><div class='flex flex-col flex-auto border-l'><div class='h-3 bg-gray-100 dark:bg-gray-800'><div class='flex items-center justify-end h-full mr-1.5'><div class='w-1 h-1 ml-1 rounded-full bg-gray-300 dark:bg-gray-700'></div><div class='w-1 h-1 ml-1 rounded-full bg-gray-300 dark:bg-gray-700'></div><div class='w-1 h-1 ml-1 rounded-full bg-gray-300 dark:bg-gray-700'></div></div></div><div class='flex flex-auto border-t bg-gray-50 dark:bg-gray-900'></div></div></div></div>" },
                // new OneClappLatestThemeLayout () { Name = "empty",CreatedOn = DateTime.UtcNow, TemplateHtml= "<div class='flex flex-col cursor-pointer' (click)='setLayout('empty')'><div class='flex flex-col h-20 rounded-md overflow-hidden  hover:opacity-80' [class.border-primary]='config.layout === 'empty''><div class='flex flex-col flex-auto bg-gray-50 dark:bg - gray - 900'></div></ div >< div class='mt-2 text-md font-medium text-center text-secondary' [class.text-primary]='config.layout === 'empty''> Empty </div></div>" ,IsDeleted = true},
                // new OneClappLatestThemeLayout() { Name = "centered",CreatedOn = DateTime.UtcNow, TemplateHtml="<div class='flex flex-col cursor-pointer'><div class='flex h-20 rounded-md overflow-hidden  hover:opacity-80'><div class='flex flex-col flex-auto my-1 mx-2 border rounded-md overflow-hidden'><div class='flex items-center h-3 bg-gray-100 dark:bg-gray-800'><div class='flex ml-1.5'><div class='w-1 h-1 rounded-full bg-gray-300 dark:bg-gray-700'></div><div class='w-3 h-1 ml-1 rounded-full bg-gray-300 dark:bg-gray-700'></div><div class='w-3 h-1 ml-1 rounded-full bg-gray-300 dark:bg-gray-700'></div><div class='w-3 h-1 ml-1 rounded-full bg-gray-300 dark:bg-gray-700'></div></div><div class='flex items-center justify-end ml-auto mr-1.5'><div class='w-1 h-1 ml-1 rounded-full bg-gray-300 dark:bg-gray-700'></div><div class='w-1 h-1 ml-1 rounded-full bg-gray-300 dark:bg-gray-700'></div></div></div><div class='flex flex-auto border-t bg-gray-50 dark:bg-gray-900'></div></div></div> </div>" ,IsDeleted = true},
                // new OneClappLatestThemeLayout() { Name = "enterprise" ,CreatedOn = DateTime.UtcNow, TemplateHtml="<div class='flex flex-col cursor-pointer'><div class='flex flex-col h-20 rounded-md overflow-hidden  hover:opacity-80'><div class='flex items-center h-3 px-2 bg-gray-100 dark:bg-gray-800'><div class='w-2 h-2 rounded-full bg-gray-300 dark:bg-gray-700'></div><div class='flex items-center justify-end ml-auto space-x-1'><div class='w-1 h-1 rounded-full bg-gray-300 dark:bg-gray-700'></div><div class='w-1 h-1 rounded-full bg-gray-300 dark:bg-gray-700'></div><div class='w-1 h-1 rounded-full bg-gray-300 dark:bg-gray-700'></div></div></div><div class='flex items-center h-3 px-2 border-t border-b space-x-1 bg-gray-100 dark:bg-gray-800'><div class='w-3 h-1 rounded-full bg-gray-300 dark:bg-gray-700'></div><div class='w-3 h-1 rounded-full bg-gray-300 dark:bg-gray-700'></div><div class='w-3 h-1 rounded-full bg-gray-300 dark:bg-gray-700'></div><div class='w-3 h-1 rounded-full bg-gray-300 dark:bg-gray-700'></div></div><div class='flex flex-col flex-auto my-1 mx-2 border rounded overflow-hidden'><div class='flex flex-auto bg-gray-50 dark:bg-gray-900'></div></div></div></div>" ,IsDeleted = true },
                // new OneClappLatestThemeLayout() { Name = "material",CreatedOn = DateTime.UtcNow, TemplateHtml="<div class='flex flex-col cursor-pointer'><div class='flex flex-col h-20 rounded-md overflow-hidden  hover:opacity-80'><div class='flex flex-col flex-auto my-1 mx-2 border rounded overflow-hidden'><div class='flex items-center h-4 px-2 bg-gray-100 dark:bg-gray-800'><div class='w-2 h-2 rounded-full bg-gray-300 dark:bg-gray-700'></div><div class='flex items-center justify-end ml-auto space-x-1'><div class='w-1 h-1 rounded-full bg-gray-300 dark:bg-gray-700'></div><div class='w-1 h-1 rounded-full bg-gray-300 dark:bg-gray-700'></div><div class='w-1 h-1 rounded-full bg-gray-300 dark:bg-gray-700'></div></div></div><div class='flex items-center h-2 px-2 space-x-1 bg-gray-100 dark:bg-gray-800'><div class='w-3 h-1 rounded-full bg-gray-300 dark:bg-gray-700'></div><div class='w-3 h-1 rounded-full bg-gray-300 dark:bg-gray-700'></div><div class='w-3 h-1 rounded-full bg-gray-300 dark:bg-gray-700'></div><div class='w-3 h-1 rounded-full bg-gray-300 dark:bg-gray-700'></div></div><div class='flex flex-auto border-t bg-gray-50 dark:bg-gray-900'></div></div></div> </div>" ,IsDeleted = true},
            };
                oneClappLatestThemeLayoutList.AddRange(oneClappLatestThemeLayoutList);
                _context.OneClappLatestThemeLayout.AddRange(oneClappLatestThemeLayoutList);
                _context.SaveChanges();
            }
        }

        public static void SeedOneClappLatestThemeScheme(OneClappContext _context)
        {
            if (_context.OneClappLatestThemeScheme != null && !_context.OneClappLatestThemeScheme.Any())
            {
                List<OneClappLatestThemeScheme> oneClappLatestThemeSchemeList = new List<OneClappLatestThemeScheme>() {
                // new OneClappLatestThemeScheme () { Name = "auto" , TemplateHtml="<div class='flex items-center py-3 pl-5 pr-6 rounded-full cursor-pointer ring-inset ring-primary bg-hover' [class.ring-2]='config.scheme === 'auto'' [matTooltip]=''Automatically sets the scheme based on user\'s operating system\'s color scheme preference using \'prefer-color-scheme\' media query.'' (click)='setScheme('auto')'> <div class='flex items-center rounded-full overflow-hidden'> <mat-icon class='icon-size-5' [svgIcon]=''heroicons_solid:lightning-bolt''></mat-icon> </div> <div class='flex items-center ml-2 font-medium leading-5' [class.text-secondary]='config.scheme !== 'auto''> Auto </div></div>"},
                // new OneClappLatestThemeScheme () { Name = "dark" , TemplateHtml="<div class='flex items-center py-3 pl-5 pr-6 rounded-full cursor-pointer ring-inset ring-primary bg-hover' [class.ring-2]='config.scheme === 'dark'' (click)='setScheme('dark')'> <div class='flex items-center rounded-full overflow-hidden'> <mat-icon class='icon-size-5' [svgIcon]=''heroicons_solid:moon''></mat-icon> </div> <div class='flex items-center ml-2 font-medium leading-5' [class.text-secondary]='config.scheme !== 'dark''> Dark </div> </div>"},
                // new OneClappLatestThemeScheme () { Name = "light", TemplateHtml="<div class='flex items-center py-3 pl-5 pr-6 rounded-full cursor-pointer ring-inset ring-primary bg-hover'[class.ring-2]='config.scheme === 'light'' (click)='setScheme('light')'><div class='flex items-center rounded-full overflow-hidden'><mat-icon class='icon-size-5' [svgIcon]=''heroicons_solid:sun''></mat-icon></div><div class='flex items-center ml-2 font-medium leading-5' [class.text-secondary]='config.scheme !== 'light''>Light</div></div>" }

                new OneClappLatestThemeScheme () { Name = "auto" , TemplateHtml="<div class='flex items-center py-3 pl-5 pr-6 rounded-full cursor-pointer ring-inset ring-primary bg-hover' [matTooltip]='Automatically sets the scheme based on user's operating system's color scheme preference using prefer-color-scheme media query.' ><div class='flex items-center rounded-full overflow-hidden'><mat-icon class='icon-size-5' [svgIcon]='heroicons_solid:lightning-bolt'></mat-icon></div> </div>"},
                new OneClappLatestThemeScheme () { Name = "dark" , TemplateHtml="<div class='flex items-center py-3 pl-5 pr-6 rounded-full cursor-pointer ring-inset ring-primary bg-hover' > <div class='flex items-center rounded-full overflow-hidden'><mat-icon class='icon-size-5' [svgIcon]='heroicons_solid:moon'></mat-icon> </div> </div>"},
                new OneClappLatestThemeScheme () { Name = "light", TemplateHtml="<div class='flex items-center py-3 pl-5 pr-6 rounded-full cursor-pointer ring-inset ring-primary bg-hover'><div class='flex items-center rounded-full overflow-hidden'><mat-icon class='icon-size-5' [svgIcon]='heroicons_solid:sun'></mat-icon></div></div>" },
                new OneClappLatestThemeScheme () { Name = "purple", TemplateHtml="<div class='flex items-center py-3 pl-5 pr-6 rounded-full cursor-pointer ring-inset ring-primary bg-hover'><div class='flex items-center rounded-full overflow-hidden'><mat-icon class='icon-size-5' [svgIcon]='heroicons_solid:fire'></mat-icon></div></div>" }
                };
                oneClappLatestThemeSchemeList.AddRange(oneClappLatestThemeSchemeList);
                _context.OneClappLatestThemeScheme.AddRange(oneClappLatestThemeSchemeList);
                _context.SaveChanges();
            }
        }

        public static void SeedOneClappLatestTheme(OneClappContext _context)
        {
            if (_context.OneClappLatestTheme != null && !_context.OneClappLatestTheme.Any())
            {
                List<OneClappLatestTheme> oneClappLatestThemeList = new List<OneClappLatestTheme>() {
                new OneClappLatestTheme () { Name = "default", Primary = "#4f46e5", Accent = "#1e293b", Warn = "#dc2626" },
                new OneClappLatestTheme () { Name = "brand", Primary = "#2196f3", Accent = "#2196f3", Warn = "#dc2626" },
                new OneClappLatestTheme () { Name = "indigo", Primary = "#0d9488", Accent = "#1e293b", Warn = "#dc2626" },
                new OneClappLatestTheme () { Name = "rose", Primary = "#f43f5e", Accent = "#1e293b", Warn = "#dc2626" },
                new OneClappLatestTheme () { Name = "purple", Primary = "#9333ea", Accent = "#1e293b", Warn = "#dc2626" },
                new OneClappLatestTheme () { Name = "amber", Primary = "#f59e0b", Accent = "#1e293b", Warn = "#dc2626" }
                };
                oneClappLatestThemeList.AddRange(oneClappLatestThemeList);
                _context.OneClappLatestTheme.AddRange(oneClappLatestThemeList);
                _context.SaveChanges();
            }
        }

        public static void SeedIntProviders(OneClappContext _context)
        {
            if (_context.IntProvider != null && !_context.IntProvider.Any())
            {
                List<IntProvider> intProviderList = new List<IntProvider>() {
                new IntProvider () { Name = "Google", CreatedOn = DateTime.UtcNow },
                new IntProvider () { Name = "Microsoft", CreatedOn = DateTime.UtcNow }
                };
                intProviderList.AddRange(intProviderList);
                _context.IntProvider.AddRange(intProviderList);
                _context.SaveChanges();
            }
        }

        public static void SeedIntProviderApps(OneClappContext _context)
        {
            var data = _context.IntProvider.Where(t => t.IsDeleted == false).ToList();
            if (_context.IntProviderApp != null && !_context.IntProviderApp.Any())
            {
                List<IntProviderApp> intProviderAppList = new List<IntProviderApp>() {
                new IntProviderApp () { Name = "Calendar",IntProviderId=_context.IntProvider.Where(t => t.Name == "Google").FirstOrDefault().Id, CreatedOn = DateTime.UtcNow },
                new IntProviderApp () { Name = "Calendar",IntProviderId=_context.IntProvider.Where(t => t.Name == "Microsoft").FirstOrDefault().Id, CreatedOn = DateTime.UtcNow },
                new IntProviderApp () { Name = "Gmail",IntProviderId=_context.IntProvider.Where(t => t.Name == "Google").FirstOrDefault().Id, CreatedOn = DateTime.UtcNow },
                new IntProviderApp () { Name = "Outlook",IntProviderId=_context.IntProvider.Where(t => t.Name == "Microsoft").FirstOrDefault().Id, CreatedOn = DateTime.UtcNow },
                };
                intProviderAppList.AddRange(intProviderAppList);
                _context.IntProviderApp.AddRange(intProviderAppList);
                _context.SaveChanges();
            }
        }

        public static void SeedOneClappModules(OneClappContext _context)
        {
            if (_context.OneClappModule != null && !_context.OneClappModule.Any())
            {
                List<OneClappModule> oneClappModuleList = new List<OneClappModule>() {
                new OneClappModule () { Name = "CRM",CreatedBy=_context.Users.Where (t => t.Email == "super.admin@oneclapp.com").FirstOrDefault ().Id, CreatedOn = DateTime.UtcNow },
                new OneClappModule () { Name = "FormBuilder",CreatedBy=_context.Users.Where (t => t.Email == "super.admin@oneclapp.com").FirstOrDefault ().Id, CreatedOn = DateTime.UtcNow }
                };
                oneClappModuleList.AddRange(oneClappModuleList);
                _context.OneClappModule.AddRange(oneClappModuleList);
                _context.SaveChanges();
            }
        }

        public static void SeedOneClappFormTypes(OneClappContext _context)
        {
            if (_context.OneClappFormType != null && !_context.OneClappFormType.Any())
            {
                List<OneClappFormType> oneClappFormTypeList = new List<OneClappFormType>() {
                new OneClappFormType () { Name = "Simple Form", CreatedOn = DateTime.UtcNow },
                new OneClappFormType () { Name = "Custom API Source", CreatedOn = DateTime.UtcNow }
                };
                oneClappFormTypeList.AddRange(oneClappFormTypeList);
                _context.OneClappFormType.AddRange(oneClappFormTypeList);
                _context.SaveChanges();
            }
        }

        public static void SeedOneClappFormStatus(OneClappContext _context)
        {
            if (_context.OneClappFormStatus != null && !_context.OneClappFormStatus.Any())
            {
                List<OneClappFormStatus> oneClappFormStatusList = new List<OneClappFormStatus>() {
                new OneClappFormStatus () { Name = "Verify", CreatedOn = DateTime.UtcNow },
                new OneClappFormStatus () { Name = "Skip", CreatedOn = DateTime.UtcNow }
                };
                oneClappFormStatusList.AddRange(oneClappFormStatusList);
                _context.OneClappFormStatus.AddRange(oneClappFormStatusList);
                _context.SaveChanges();
            }
        }

        public static void SeedOneClappFormAction(OneClappContext _context)
        {
            if (_context.OneClappFormAction != null && !_context.OneClappFormAction.Any())
            {
                List<OneClappFormAction> oneClappFormActionList = new List<OneClappFormAction>() {
                new OneClappFormAction () { Name = "Person", CreatedOn = DateTime.UtcNow },
                new OneClappFormAction () { Name = "Organization", CreatedOn = DateTime.UtcNow },
                new OneClappFormAction () { Name = "Lead", CreatedOn = DateTime.UtcNow }
                };
                oneClappFormActionList.AddRange(oneClappFormActionList);
                _context.OneClappFormAction.AddRange(oneClappFormActionList);
                _context.SaveChanges();
            }
        }

        public static void SeedSubscriptionPlan(OneClappContext _context)
        {
            if (_context.SubscriptionPlan != null && !_context.SubscriptionPlan.Any())
            {
                List<SubscriptionPlan> subscriptionPlanList = new List<SubscriptionPlan>() {
                new SubscriptionPlan () { Name = "Basic", MonthlyPrice = 9, YearlyPrice=7, TrialPeriod=30, CreatedBy=_context.Users.Where(t => t.Email == "super.admin@oneclapp.com").FirstOrDefault().Id, CreatedOn = DateTime.UtcNow },
                new SubscriptionPlan () { Name = "Business",  TrialPeriod=30, CreatedBy=_context.Users.Where(t => t.Email == "super.admin@oneclapp.com").FirstOrDefault().Id, CreatedOn = DateTime.UtcNow },
                new SubscriptionPlan () { Name = "Enterprise",  TrialPeriod=30, CreatedBy=_context.Users.Where(t => t.Email == "super.admin@oneclapp.com").FirstOrDefault().Id, CreatedOn = DateTime.UtcNow }
                };
                subscriptionPlanList.AddRange(subscriptionPlanList);
                _context.SubscriptionPlan.AddRange(subscriptionPlanList);
                _context.SaveChanges();
            }
        }

        public static void SeedSubscriptionPlanDetail(OneClappContext _context)
        {
            if (_context.SubscriptionPlanDetail != null && !_context.SubscriptionPlanDetail.Any())
            {
                List<SubscriptionPlanDetail> subscriptionPlanDetailList = new List<SubscriptionPlanDetail>() {
                new SubscriptionPlanDetail () { SubScriptionPlanId = _context.SubscriptionPlan.Where(t => t.Name == "Basic").FirstOrDefault().Id, FeatureName = "50GB storage", Description="50GB storage", CreatedBy=_context.Users.Where(t => t.Email == "super.admin@oneclapp.com").FirstOrDefault().Id, CreatedOn = DateTime.UtcNow },
                new SubscriptionPlanDetail () { SubScriptionPlanId = _context.SubscriptionPlan.Where(t => t.Name == "Basic").FirstOrDefault().Id, FeatureName = "Calendar integration", Description="Calendar integration", CreatedBy=_context.Users.Where(t => t.Email == "super.admin@oneclapp.com").FirstOrDefault().Id, CreatedOn = DateTime.UtcNow }
                // new SubscriptionPlanDetail () { SubScriptionPlanId = _context.SubscriptionPlan.Where(t => t.Name == "Business").FirstOrDefault().Id, FeatureName = "100GB storage", Description="100GB storage", CreatedBy=_context.Users.Where(t => t.Email == "super.admin@oneclapp.com").FirstOrDefault().Id, CreatedOn = DateTime.UtcNow }
                };
                subscriptionPlanDetailList.AddRange(subscriptionPlanDetailList);
                _context.SubscriptionPlanDetail.AddRange(subscriptionPlanDetailList);
                _context.SaveChanges();
            }
        }

        public static void SeedSubscriptionType(OneClappContext _context)
        {
            if (_context.SubscriptionType != null && !_context.SubscriptionType.Any())
            {
                List<SubscriptionType> subscriptionTypeList = new List<SubscriptionType>() {
                new SubscriptionType () { Name = "Yearly", CreatedBy=_context.Users.Where(t => t.Email == "super.admin@oneclapp.com").FirstOrDefault().Id, CreatedOn = DateTime.UtcNow },
                new SubscriptionType () { Name = "Monthly", CreatedBy=_context.Users.Where(t => t.Email == "super.admin@oneclapp.com").FirstOrDefault().Id, CreatedOn = DateTime.UtcNow }
                };
                subscriptionTypeList.AddRange(subscriptionTypeList);
                _context.SubscriptionType.AddRange(subscriptionTypeList);
                _context.SaveChanges();
            }
        }

        public static void SeedSalutation(OneClappContext _context)
        {
            if (_context.Salutation != null && !_context.Salutation.Any())
            {
                List<Salutation> salutationList = new List<Salutation>() {
                new Salutation () { Name = "Mr.", CreatedBy=_context.Users.Where(t => t.Email == "super.admin@oneclapp.com").FirstOrDefault().Id, CreatedOn = DateTime.UtcNow },
                new Salutation () { Name = "Mrs.", CreatedBy=_context.Users.Where(t => t.Email == "super.admin@oneclapp.com").FirstOrDefault().Id, CreatedOn = DateTime.UtcNow },
                new Salutation () { Name = "Ms.", CreatedBy=_context.Users.Where(t => t.Email == "super.admin@oneclapp.com").FirstOrDefault().Id, CreatedOn = DateTime.UtcNow },
                new Salutation () { Name = "Miss", CreatedBy=_context.Users.Where(t => t.Email == "super.admin@oneclapp.com").FirstOrDefault().Id, CreatedOn = DateTime.UtcNow },
                new Salutation () { Name = "Dr.", CreatedBy=_context.Users.Where(t => t.Email == "super.admin@oneclapp.com").FirstOrDefault().Id, CreatedOn = DateTime.UtcNow },
                new Salutation () { Name = "Professor", CreatedBy=_context.Users.Where(t => t.Email == "super.admin@oneclapp.com").FirstOrDefault().Id, CreatedOn = DateTime.UtcNow }
                };
                salutationList.AddRange(salutationList);
                _context.Salutation.AddRange(salutationList);
                _context.SaveChanges();
            }
        }

        public static void SeedBorderStyle(OneClappContext _context)
        {
            if (_context.BorderStyle != null && !_context.BorderStyle.Any())
            {
                List<BorderStyle> borderStyleList = new List<BorderStyle>() {
                new BorderStyle () { Name = "dashed",  CreatedOn = DateTime.UtcNow },
                new BorderStyle () { Name = "dotted", CreatedOn = DateTime.UtcNow },
                new BorderStyle () { Name = "solid",CreatedOn = DateTime.UtcNow },
                new BorderStyle () { Name = "none", CreatedOn = DateTime.UtcNow }
                };
                borderStyleList.AddRange(borderStyleList);
                _context.BorderStyle.AddRange(borderStyleList);
                _context.SaveChanges();
            }
        }
    }
}