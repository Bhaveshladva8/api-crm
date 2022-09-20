using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using matcrm.data.Models.Dto;
using matcrm.data.Models.Tables;
using matcrm.data.Models.ViewModels;
using matcrm.service.Common;
using matcrm.service.Services;
using matcrm.service.BusinessLogic;
using matcrm.data.Models.Request;
using matcrm.data.Models.Response;
using matcrm.data.Context;
using matcrm.service.Utility;

namespace matcrm.api.Controllers
{
    [Authorize]
    [Route("[controller]/[action]")]
    public class ProjectController : Controller
    {
        private readonly IEmployeeProjectService _employeeProjectService;
        private readonly IUserService _userService;
        private readonly IEmployeeProjectStatusService _employeeProjectStatusService;
        private readonly IEmployeeTaskService _employeeTaskService;
        private readonly ISectionService _sectionService;
        private readonly ISectionActivityService _sectionActivityService;
        private readonly IEmployeeTaskUserSerivce _employeeTaskUserService;
        private readonly IEmployeeTaskTimeRecordService _employeeTaskTimeRecordService;
        private readonly IEmployeeTaskAttachmentService _employeeTaskAttachmentService;
        private readonly IEmployeeTaskCommentService _employeeTaskCommentService;
        private readonly IEmployeeTaskActivityService _employeeTaskActivityService;
        private readonly IEmployeeSubTaskService _employeeSubTaskService;
        private readonly IEmployeeChildTaskService _employeeChildTaskService;
        private readonly IEmployeeTaskUserSerivce _employeeTaskUserSerivce;
        private readonly IEmployeeSubTaskUserService _employeeSubTaskUserService;
        private readonly IEmployeeChildTaskUserService _employeeChildTaskUserService;
        private readonly IEmployeeSubTaskTimeRecordService _employeeSubTaskTimeRecordService;
        private readonly IEmployeeChildTaskTimeRecordService _employeeChildTaskTimeRecordService;
        private readonly IWeClappService _weClappService;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IEmployeeSubTaskAttachmentService _employeeSubTaskAttachmentService;
        private readonly IEmployeeChildTaskAttachmentService _employeeChildTaskAttachmentService;
        private readonly IEmployeeSubTaskActivityService _employeeSubTaskActivityService;
        private readonly IEmployeeChildTaskActivityService _employeeChildTaskActivityService;
        private readonly IEmployeeSubTaskCommentService _employeeSubTaskCommentService;
        private readonly IEmployeeChildTaskCommentService _employeeChildTaskCommentService;
        private readonly IEmployeeTaskStatusService _employeeTaskStatusService;
        private readonly ICustomModuleService _customModuleService;
        private CustomFieldLogic customFieldLogic;
        private readonly ICustomControlService _customControlService;
        private readonly ICustomControlOptionService _customControlOptionService;
        private readonly ICustomFieldService _customFieldService;
        private readonly IModuleFieldService _moduleFieldService;
        private readonly ITenantModuleService _tenantModuleService;
        private readonly ICustomTenantFieldService _customTenantFieldService;
        private readonly ICustomTableService _customTableService;
        private readonly ICustomFieldValueService _customFieldValueService;
        private readonly IModuleRecordCustomFieldService _moduleRecordCustomFieldService;
        private readonly ICustomTableColumnService _customTableColumnService;
        private readonly IEmployeeProjectUserService _employeeProjectUserService;
        private readonly IEmployeeProjectActivityService _employeeProjectActivityService;
        private readonly IStatusService _statusService;
        private readonly IEmailTemplateService _emailTemplateService;
        private readonly IEmailLogService _emailLogService;
        private readonly IEmailConfigService _emailConfigService;
        private readonly IEmailProviderService _emailProvider;
        private readonly OneClappContext _context;
        private SendEmail sendEmail;

        private IMapper _mapper;
        private int UserId = 0;
        private int TenantId = 0;

        public ProjectController(
            IEmployeeProjectService employeeProjectService,
            IEmployeeProjectStatusService employeeProjectStatusService,
            IUserService userService,
            IEmployeeTaskService employeeTaskService,
            IEmployeeTaskUserSerivce employeeTaskUserService,
            IEmployeeTaskTimeRecordService employeeTaskTimeRecordService,
            IEmployeeTaskAttachmentService employeeTaskAttachmentService,
            IEmployeeTaskCommentService employeeTaskCommentService,
            IEmployeeTaskActivityService employeeTaskActivityService,
            IEmployeeSubTaskService employeeSubTaskService,
            IEmployeeChildTaskService employeeChildTaskService,
            IEmployeeTaskUserSerivce employeeTaskUserSerivce,
            IEmployeeSubTaskUserService employeeSubTaskUserService,
            IEmployeeChildTaskUserService employeeChildTaskUserService,
            IEmployeeSubTaskTimeRecordService employeeSubTaskTimeRecordService,
            IEmployeeChildTaskTimeRecordService employeeChildTaskTimeRecordService,
            IWeClappService weClappService,
            IHostingEnvironment hostingEnvironment,
            IEmployeeSubTaskAttachmentService employeeSubTaskAttachmentService,
            IEmployeeChildTaskAttachmentService employeeChildTaskAttachmentService,
            IEmployeeSubTaskActivityService employeeSubTaskActivityService,
            IEmployeeChildTaskActivityService employeeChildTaskActivityService,
            IEmployeeSubTaskCommentService employeeSubTaskCommentService,
            IEmployeeChildTaskCommentService employeeChildTaskCommentService,
            IEmployeeTaskStatusService employeeTaskStatusService,
            IMapper mapper,
            ICustomModuleService customModuleService,
            ICustomControlService customControlService,
            ICustomControlOptionService customControlOptionService,
            ICustomFieldService customFieldService,
            IModuleFieldService moduleFieldService,
            ITenantModuleService tenantModuleService,
            ICustomTenantFieldService customTenantFieldService,
            ICustomTableService customTableService,
            ICustomFieldValueService customFieldValueService,
            IModuleRecordCustomFieldService moduleRecordCustomFieldService,
            ICustomTableColumnService customTableColumnService,
            ISectionService sectionService,
            ISectionActivityService sectionActivityService,
            IEmployeeProjectUserService employeeProjectUserService,
            IEmployeeProjectActivityService employeeProjectActivityService,
            IEmailTemplateService emailTemplateService,
            IEmailLogService emailLogService,
            IEmailConfigService emailConfigService,
            IEmailProviderService emailProvider,
            IStatusService statusService,
            OneClappContext context
        )
        {
            _employeeProjectService = employeeProjectService;
            _employeeProjectStatusService = employeeProjectStatusService;
            _userService = userService;
            _employeeTaskService = employeeTaskService;
            _employeeTaskUserService = employeeTaskUserService;
            _employeeTaskTimeRecordService = employeeTaskTimeRecordService;
            _employeeTaskAttachmentService = employeeTaskAttachmentService;
            _employeeTaskCommentService = employeeTaskCommentService;
            _employeeTaskActivityService = employeeTaskActivityService;
            _employeeSubTaskService = employeeSubTaskService;
            _employeeChildTaskService = employeeChildTaskService;
            _employeeTaskUserSerivce = employeeTaskUserSerivce;
            _employeeSubTaskUserService = employeeSubTaskUserService;
            _employeeChildTaskUserService = employeeChildTaskUserService;
            _employeeTaskTimeRecordService = employeeTaskTimeRecordService;
            _employeeSubTaskTimeRecordService = employeeSubTaskTimeRecordService;
            _employeeChildTaskTimeRecordService = employeeChildTaskTimeRecordService;
            _weClappService = weClappService;
            _hostingEnvironment = hostingEnvironment;
            _employeeSubTaskAttachmentService = employeeSubTaskAttachmentService;
            _employeeChildTaskAttachmentService = employeeChildTaskAttachmentService;
            _employeeSubTaskActivityService = employeeSubTaskActivityService;
            _employeeChildTaskActivityService = employeeChildTaskActivityService;
            _userService = userService;
            _employeeSubTaskCommentService = employeeSubTaskCommentService;
            _employeeChildTaskCommentService = employeeChildTaskCommentService;
            _employeeTaskStatusService = employeeTaskStatusService;
            _hostingEnvironment = hostingEnvironment;
            _mapper = mapper;
            _customModuleService = customModuleService;
            _customControlService = customControlService;
            _customControlOptionService = customControlOptionService;
            _customFieldService = customFieldService;
            _customModuleService = customModuleService;
            _moduleFieldService = moduleFieldService;
            _tenantModuleService = tenantModuleService;
            _customTenantFieldService = customTenantFieldService;
            _customTableService = customTableService;
            _customFieldValueService = customFieldValueService;
            _moduleRecordCustomFieldService = moduleRecordCustomFieldService;
            _customTableColumnService = customTableColumnService;
            _sectionActivityService = sectionActivityService;
            _sectionService = sectionService;
            _employeeProjectUserService = employeeProjectUserService;
            _employeeProjectActivityService = employeeProjectActivityService;
            _statusService = statusService;
            _context = context;
            customFieldLogic = new CustomFieldLogic(customControlService, customControlOptionService, customFieldService,
                customModuleService, moduleFieldService, tenantModuleService, customTenantFieldService, customTableService, customFieldValueService, mapper);
            sendEmail = new SendEmail(emailTemplateService, emailLogService, emailConfigService, emailProvider, mapper);
        }

        [Authorize(Roles = "Admin, TenantAdmin, TenantManager, TenantUser, ExternalUser")]
        [HttpPost]
        public async Task<OperationResult<AddUpdateEmployeeProjectResponse>> Add([FromForm] AddUpdateEmployeeProjectRequest requestModel)
        {
            ClaimsPrincipal user = this.User as ClaimsPrincipal;
            UserId = Convert.ToInt32(user.FindFirst(JwtRegisteredClaimNames.Sid).Value);
            TenantId = Convert.ToInt32(user.FindFirst("TenantId").Value);

            var employeeProjectDto = _mapper.Map<EmployeeProjectDto>(requestModel);

            if (!string.IsNullOrEmpty(employeeProjectDto.Duration))
            {
                var data = employeeProjectDto.Duration.Split(":");
                var count = data.Count();
                if (count == 3)
                {
                    var hours = Convert.ToInt16(data[0]);
                    var minutes = Convert.ToInt16(data[1]);
                    var seconds = Convert.ToInt16(data[2]);
                    employeeProjectDto.EstimatedTime = new TimeSpan(hours, minutes, seconds);
                }
            }

            employeeProjectDto.CreatedBy = UserId;
            employeeProjectDto.TenantId = TenantId;
            var filePath = "";

            if (employeeProjectDto.File != null)
            {
                //var dirPath = _hostingEnvironment.WebRootPath + "\\ProjectLogo";
                var dirPath = _hostingEnvironment.WebRootPath + OneClappContext.ProjectLogoDirPath;

                if (!Directory.Exists(dirPath))
                {
                    Directory.CreateDirectory(dirPath);
                }

                var fileName = string.Concat(
                                Path.GetFileNameWithoutExtension(employeeProjectDto.File.FileName),
                                DateTime.Now.ToString("yyyyMMdd_HHmmss"),
                                Path.GetExtension(employeeProjectDto.File.FileName)
                            );
                filePath = dirPath + "\\" + fileName;


                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(Path.Combine(filePath));
                }

                EmployeeProject employeeProjectObj = new EmployeeProject();
                var employeeProject = _mapper.Map<EmployeeProject>(employeeProjectDto);

                employeeProjectDto.Logo = fileName;

                if (OneClappContext.ClamAVServerIsLive)
                {
                    ScanDocument scanDocumentObj = new ScanDocument();
                    bool fileStatus = await scanDocumentObj.ScanDocumentWithClam(employeeProjectDto.File);
                    if (fileStatus)
                    {
                        return new OperationResult<AddUpdateEmployeeProjectResponse>(false, System.Net.HttpStatusCode.OK, "Virus Found!");
                    }
                }

                using (var oStream = new FileStream(filePath, FileMode.Create, FileAccess.ReadWrite))
                {
                    await employeeProjectDto.File.CopyToAsync(oStream);
                }
            }

            var AddUpdate = await _employeeProjectService.CheckInsertOrUpdate(employeeProjectDto);

            EmployeeProjectActivity employeeProjectActivityObj = new EmployeeProjectActivity();
            employeeProjectActivityObj.ProjectId = AddUpdate.Id;
            employeeProjectActivityObj.Activity = Enums.EmployeeProjectActivityEnum.Project_Created.ToString().Replace("_", " ");
            employeeProjectActivityObj.UserId = employeeProjectDto.CreatedBy;
            var projectActivityObj = await _employeeProjectActivityService.CheckInsertOrUpdate(employeeProjectActivityObj);

            //start assign user for project
            if (requestModel.AssignedUsers != null && requestModel.AssignedUsers.Count() > 0)
            {
                foreach (var userObj in requestModel.AssignedUsers)
                {
                    EmployeeProjectUserDto employeeProjectUserDto = new EmployeeProjectUserDto();
                    employeeProjectUserDto.EmployeeProjectId = AddUpdate.Id;
                    employeeProjectUserDto.UserId = userObj.UserId;
                    employeeProjectUserDto.CreatedBy = UserId;
                    var isExist = _employeeProjectUserService.IsExistOrNot(employeeProjectUserDto);
                    if (!isExist)
                    {
                        var employeeProjectUserObj = await _employeeProjectUserService.CheckInsertOrUpdate(employeeProjectUserDto);
                        if (employeeProjectUserObj != null)
                        {
                            userObj.Id = employeeProjectUserObj.Id;
                        }

                        if (employeeProjectUserDto.UserId != null)
                        {
                            var userAssignDetails = _userService.GetUserById(employeeProjectUserDto.UserId.Value);
                            if (userAssignDetails != null)
                                await sendEmail.SendEmailEmployeeProjectUserAssignNotification(userAssignDetails.Email, userAssignDetails.FirstName + ' ' + userAssignDetails.LastName, AddUpdate.Name, TenantId);
                            EmployeeProjectActivity employeeProjectUserActivityObj = new EmployeeProjectActivity();
                            employeeProjectUserActivityObj.ProjectId = AddUpdate.Id;
                            employeeProjectUserActivityObj.UserId = UserId;
                            employeeProjectUserActivityObj.Activity = Enums.EmployeeProjectActivityEnum.Project_assigned_to_user.ToString().Replace("_", " ");
                            var AddUpdate1 = await _employeeProjectActivityService.CheckInsertOrUpdate(employeeProjectUserActivityObj);
                        }
                    }
                }
                employeeProjectDto.AssignedUsers = new List<EmployeeProjectUser>();
            }
            //end assign user for project

            // //start for custom field
            // CustomModule? customModuleObj = null;
            // var employeeProjectTableObj = _customTableService.GetByName("Project");
            // if (employeeProjectTableObj != null)
            // {
            //     customModuleObj = _customModuleService.GetByCustomTable(employeeProjectTableObj.Id);
            // }

            // if (employeeProjectDto.CustomFields != null && employeeProjectDto.CustomFields.Count() > 0)
            // {
            //     foreach (var item in employeeProjectDto.CustomFields)
            //     {
            //         if (item != null)
            //         {
            //             CustomFieldValueDto customFieldValueDto = new CustomFieldValueDto();
            //             customFieldValueDto.FieldId = item.Id;
            //             if (customModuleObj != null)
            //             {
            //                 customFieldValueDto.ModuleId = customModuleObj.Id;
            //             }
            //             customFieldValueDto.RecordId = AddUpdate.Id;
            //             var controlType = "";
            //             if (item.CustomControl != null)
            //             {
            //                 controlType = item.CustomControl.Name;
            //                 customFieldValueDto.ControlType = controlType;
            //             }
            //             customFieldValueDto.Value = item.Value;
            //             customFieldValueDto.CreatedBy = UserId;
            //             customFieldValueDto.TenantId = TenantId;
            //             if (item.CustomControlOptions != null && item.CustomControlOptions.Count() > 0)
            //             {

            //                 var selectedOptionList = item.CustomControlOptions.Where(t => t.IsChecked == true).ToList();
            //                 if (controlType == "Checkbox")
            //                 {
            //                     var deletedList = await _customFieldValueService.DeleteList(customFieldValueDto);
            //                 }
            //                 if (selectedOptionList != null && selectedOptionList.Count() > 0)
            //                 {
            //                     foreach (var option in selectedOptionList)
            //                     {
            //                         customFieldValueDto.OptionId = option.Id;
            //                         var customFieldAddUpdate = await _customFieldValueService.CheckInsertOrUpdate(customFieldValueDto);
            //                     }
            //                 }
            //             }
            //             else
            //             {
            //                 var customFieldAddUpdate = await _customFieldValueService.CheckInsertOrUpdate(customFieldValueDto);
            //             }
            //         }

            //     }
            // }
            // //end for custom field



            var employeeProjectAddUpdateResponse = _mapper.Map<AddUpdateEmployeeProjectResponse>(AddUpdate);
            employeeProjectAddUpdateResponse.LogoURL = filePath;
            if (requestModel.StatusId != null)
            {
                var statusObj = _statusService.GetById(requestModel.StatusId.Value);
                if (statusObj != null)
                {
                    employeeProjectAddUpdateResponse.StatusId = statusObj.Id;
                    employeeProjectAddUpdateResponse.StatusName = statusObj.Name;
                }
            }

            return new OperationResult<AddUpdateEmployeeProjectResponse>(true, System.Net.HttpStatusCode.OK, "Project add successfully", employeeProjectAddUpdateResponse);
        }

        [Authorize(Roles = "Admin, TenantAdmin, TenantManager, TenantUser, ExternalUser")]
        [HttpPut]
        public async Task<OperationResult<AddUpdateEmployeeProjectResponse>> Update([FromForm] AddUpdateEmployeeProjectRequest requestModel)
        {
            ClaimsPrincipal user = this.User as ClaimsPrincipal;
            UserId = Convert.ToInt32(user.FindFirst(JwtRegisteredClaimNames.Sid).Value);
            TenantId = Convert.ToInt32(user.FindFirst("TenantId").Value);
            var employeeProjectDto = _mapper.Map<EmployeeProjectDto>(requestModel);

            employeeProjectDto.UpdatedBy = UserId;
            employeeProjectDto.TenantId = TenantId;

            if (employeeProjectDto.Duration != null)
            {
                var data = employeeProjectDto.Duration.Split(":");
                var count = data.Count();
                if (count == 3)
                {
                    var hours = Convert.ToInt16(data[0]);
                    var minutes = Convert.ToInt16(data[1]);
                    var seconds = Convert.ToInt16(data[2]);
                    employeeProjectDto.EstimatedTime = new TimeSpan(hours, minutes, seconds);
                }
            }
            EmployeeProject employeeProjectObj = new EmployeeProject();
            if (employeeProjectDto.Id != null)
            {
                var filePath = "";
                if (employeeProjectDto.File != null)
                {
                    //var dirPath = _hostingEnvironment.WebRootPath + "\\ProjectLogo";
                    var dirPath = _hostingEnvironment.WebRootPath + OneClappContext.ProjectLogoDirPath;

                    if (!Directory.Exists(dirPath))
                    {
                        Directory.CreateDirectory(dirPath);
                    }

                    var fileName = string.Concat(
                                    Path.GetFileNameWithoutExtension(employeeProjectDto.File.FileName),
                                    DateTime.Now.ToString("yyyyMMdd_HHmmss"),
                                    Path.GetExtension(employeeProjectDto.File.FileName)
                                );
                    filePath = dirPath + "\\" + fileName;


                    if (System.IO.File.Exists(filePath))
                    {
                        System.IO.File.Delete(Path.Combine(filePath));
                    }

                    //EmployeeProject employeeProjectObj = new EmployeeProject();
                    //var employeeProject = _mapper.Map<EmployeeProject>(employeeProjectDto);

                    employeeProjectDto.Logo = fileName;
                    if (OneClappContext.ClamAVServerIsLive)
                    {
                        ScanDocument scanDocumentObj = new ScanDocument();
                        bool fileStatus = await scanDocumentObj.ScanDocumentWithClam(employeeProjectDto.File);
                        if (fileStatus)
                        {
                            return new OperationResult<AddUpdateEmployeeProjectResponse>(false, System.Net.HttpStatusCode.OK, "Virus Found!");
                        }
                    }
                    using (var oStream = new FileStream(filePath, FileMode.Create, FileAccess.ReadWrite))
                    {
                        await employeeProjectDto.File.CopyToAsync(oStream);
                    }
                }

                var existingItem = _employeeProjectService.GetEmployeeProjectById(employeeProjectDto.Id.Value);
                if (existingItem != null)
                {
                    existingItem.Name = employeeProjectDto.Name;
                    existingItem.UpdatedBy = employeeProjectDto.UpdatedBy;
                    var AddUpdate = await _employeeProjectService.CheckInsertOrUpdate(employeeProjectDto);

                    EmployeeProjectActivity employeeProjectActivityObj = new EmployeeProjectActivity();
                    employeeProjectActivityObj.ProjectId = AddUpdate.Id;
                    employeeProjectActivityObj.Activity = Enums.EmployeeProjectActivityEnum.Project_Updated.ToString().Replace("_", " ");
                    employeeProjectActivityObj.UserId = employeeProjectDto.UpdatedBy;
                    var ProjectActivityObj = await _employeeProjectActivityService.CheckInsertOrUpdate(employeeProjectActivityObj);

                    //start assign user for project
                    if (requestModel.AssignedUsers != null && requestModel.AssignedUsers.Count() > 0)
                    {
                        foreach (var userObj in requestModel.AssignedUsers)
                        {
                            EmployeeProjectUserDto employeeProjectUserDto = new EmployeeProjectUserDto();
                            employeeProjectUserDto.EmployeeProjectId = AddUpdate.Id;
                            employeeProjectUserDto.UserId = userObj.UserId;
                            employeeProjectUserDto.CreatedBy = UserId;
                            var isExist = _employeeProjectUserService.IsExistOrNot(employeeProjectUserDto);
                            if (!isExist)
                            {
                                var employeeProjectUserObj = await _employeeProjectUserService.CheckInsertOrUpdate(employeeProjectUserDto);
                                if (employeeProjectUserObj != null)
                                {
                                    userObj.Id = employeeProjectUserObj.Id;
                                }

                                if (employeeProjectUserDto.UserId != null)
                                {
                                    var userAssignDetails = _userService.GetUserById(employeeProjectUserDto.UserId.Value);
                                    if (userAssignDetails != null)
                                        await sendEmail.SendEmailEmployeeProjectUserAssignNotification(userAssignDetails.Email, userAssignDetails.FirstName + ' ' + userAssignDetails.LastName, AddUpdate.Name, TenantId);
                                    EmployeeProjectActivity employeeProjectUserActivityObj = new EmployeeProjectActivity();
                                    employeeProjectUserActivityObj.ProjectId = AddUpdate.Id;
                                    employeeProjectUserActivityObj.UserId = UserId;
                                    employeeProjectUserActivityObj.Activity = Enums.EmployeeProjectActivityEnum.Project_assigned_to_user.ToString().Replace("_", " ");
                                    var AddUpdate1 = await _employeeProjectActivityService.CheckInsertOrUpdate(employeeProjectUserActivityObj);
                                }
                            }
                        }
                        employeeProjectDto.AssignedUsers = new List<EmployeeProjectUser>();
                    }
                    //end assign user for project

                    // //start custom module
                    // CustomModule? customModuleObj = null;
                    // var employeeProjectTableObj = _customTableService.GetByName("Project");
                    // if (employeeProjectTableObj != null)
                    // {
                    //     customModuleObj = _customModuleService.GetByCustomTable(employeeProjectTableObj.Id);
                    // }

                    // if (employeeProjectDto.CustomFields != null && employeeProjectDto.CustomFields.Count() > 0)
                    // {
                    //     foreach (var item in employeeProjectDto.CustomFields)
                    //     {
                    //         if (item != null)
                    //         {
                    //             CustomFieldValueDto customFieldValueDto = new CustomFieldValueDto();
                    //             customFieldValueDto.FieldId = item.Id;
                    //             if (customModuleObj != null)
                    //             {
                    //                 customFieldValueDto.ModuleId = customModuleObj.Id;
                    //             }
                    //             customFieldValueDto.RecordId = AddUpdate.Id;
                    //             var controlType = "";
                    //             if (item.CustomControl != null)
                    //             {
                    //                 controlType = item.CustomControl.Name;
                    //                 customFieldValueDto.ControlType = controlType;
                    //             }
                    //             customFieldValueDto.Value = item.Value;
                    //             customFieldValueDto.CreatedBy = UserId;
                    //             customFieldValueDto.TenantId = TenantId;
                    //             if (item.CustomControlOptions != null && item.CustomControlOptions.Count() > 0)
                    //             {
                    //                 var selectedOptionList = item.CustomControlOptions.Where(t => t.IsChecked == true).ToList();
                    //                 if (controlType == "Checkbox")
                    //                 {
                    //                     var deletedList = await _customFieldValueService.DeleteList(customFieldValueDto);
                    //                 }
                    //                 if (selectedOptionList != null && selectedOptionList.Count() > 0)
                    //                 {
                    //                     foreach (var option in selectedOptionList)
                    //                     {
                    //                         customFieldValueDto.OptionId = option.Id;
                    //                         var customFieldAddUpdate = await _customFieldValueService.CheckInsertOrUpdate(customFieldValueDto);
                    //                     }
                    //                 }
                    //             }
                    //             else
                    //             {
                    //                 var customFieldAddUpdate = await _customFieldValueService.CheckInsertOrUpdate(customFieldValueDto);
                    //             }
                    //         }

                    //     }
                    // }
                    // //end custom module
                    var employeeProjectAddUpdateResponse = _mapper.Map<AddUpdateEmployeeProjectResponse>(AddUpdate);
                    employeeProjectAddUpdateResponse.LogoURL = filePath;

                    if (requestModel.StatusId != null)
                    {
                        var statusObj = _statusService.GetById(requestModel.StatusId.Value);
                        if (statusObj != null)
                        {
                            employeeProjectAddUpdateResponse.StatusId = statusObj.Id;
                            employeeProjectAddUpdateResponse.StatusName = statusObj.Name;
                        }
                    }
                    return new OperationResult<AddUpdateEmployeeProjectResponse>(true, System.Net.HttpStatusCode.OK, "Project Updated successfully", employeeProjectAddUpdateResponse);
                }
            }
            return new OperationResult<AddUpdateEmployeeProjectResponse>(false, System.Net.HttpStatusCode.OK, "Error");
        }

        [Authorize(Roles = "Admin, TenantAdmin, TenantManager, TenantUser, ExternalUser")]
        [HttpDelete("{EmployeeProjectId}")]
        public async Task<OperationResult<EmployeeProjectDeleteResponse>> Remove(long EmployeeProjectId)
        {
            ClaimsPrincipal user = this.User as ClaimsPrincipal;
            UserId = Convert.ToInt32(user.FindFirst(JwtRegisteredClaimNames.Sid).Value);
            TenantId = Convert.ToInt32(user.FindFirst("TenantId").Value);
            EmployeeProjectDto model = new EmployeeProjectDto();
            var employeeProjectId = EmployeeProjectId;

            var tasks = _employeeTaskService.GetAllTaskByProjectId(employeeProjectId);
            var sections = _sectionService.GetByProject(employeeProjectId);

            if (tasks != null && tasks.Count() > 0)
            {
                foreach (var taskObj in tasks)
                {
                    var employeeTaskId = taskObj.Id;

                    var subTasks = _employeeSubTaskService.GetAllSubTaskByTask(employeeTaskId);

                    if (subTasks != null && subTasks.Count() > 0)
                    {
                        foreach (var subTask in subTasks)
                        {
                            var subTaskId = subTask.Id;

                            var childTasks = _employeeChildTaskService.GetAllChildTaskBySubTask(subTaskId);

                            if (childTasks != null && childTasks.Count() > 0)
                            {
                                foreach (var item in childTasks)
                                {
                                    var childTaskId = item.Id;

                                    var childDocuments = await _employeeChildTaskAttachmentService.DeleteAttachmentByChildTaskId(childTaskId);

                                    // Remove child task documents from folder
                                    if (childDocuments != null && childDocuments.Count() > 0)
                                    {
                                        foreach (var childTaskDoc in childDocuments)
                                        {

                                            //var dirPath = _hostingEnvironment.WebRootPath + "\\ChildTaskUpload";
                                            var dirPath = _hostingEnvironment.WebRootPath + OneClappContext.ChildTaskUploadDirPath;
                                            var filePath = dirPath + "\\" + childTaskDoc.Name;

                                            if (System.IO.File.Exists(filePath))
                                            {
                                                System.IO.File.Delete(Path.Combine(filePath));
                                            }
                                        }
                                    }

                                    var childComments = await _employeeChildTaskCommentService.DeleteCommentByChildTaskId(childTaskId);

                                    var childTimeRecords = await _employeeChildTaskTimeRecordService.DeleteTimeRecordByEmployeeChildTaskId(childTaskId);

                                    var childTaskUsers = await _employeeChildTaskUserService.DeleteByChildTaskId(childTaskId);

                                    EmployeeChildTaskActivity employeeChildTaskActivityObj = new EmployeeChildTaskActivity();
                                    employeeChildTaskActivityObj.EmployeeChildTaskId = childTaskId;
                                    employeeChildTaskActivityObj.UserId = UserId;
                                    employeeChildTaskActivityObj.Activity = "Removed the task";
                                    var AddUpdate1 = await _employeeChildTaskActivityService.CheckInsertOrUpdate(employeeChildTaskActivityObj);

                                    var childTaskActivities = await _employeeChildTaskActivityService.DeleteByEmployeeChildTaskId(childTaskId);

                                    var childTaskToDelete = await _employeeChildTaskService.Delete(childTaskId);
                                }
                            }

                            var subDocuments = await _employeeSubTaskAttachmentService.DeleteAttachmentByEmployeeSubTaskId(subTaskId);

                            // Remove sub task documents from folder
                            if (subDocuments != null && subDocuments.Count() > 0)
                            {
                                foreach (var subTaskDoc in subDocuments)
                                {

                                    //var dirPath = _hostingEnvironment.WebRootPath + "\\SubTaskUpload";
                                    var dirPath = _hostingEnvironment.WebRootPath + OneClappContext.SubTaskUploadDirPath;
                                    var filePath = dirPath + "\\" + subTaskDoc.Name;

                                    if (System.IO.File.Exists(filePath))
                                    {
                                        System.IO.File.Delete(Path.Combine(filePath));
                                    }
                                }
                            }

                            var subComments = await _employeeSubTaskCommentService.DeleteCommentByEmployeeSubTaskId(subTaskId);

                            var subTimeRecords = await _employeeSubTaskTimeRecordService.DeleteTimeRecordBySubTaskId(subTaskId);

                            var subTaskUsers = await _employeeSubTaskUserService.DeleteBySubTaskId(subTaskId);

                            EmployeeSubTaskActivity employeeSubTaskActivityObj = new EmployeeSubTaskActivity();
                            employeeSubTaskActivityObj.EmployeeSubTaskId = subTaskId;
                            employeeSubTaskActivityObj.UserId = UserId;
                            employeeSubTaskActivityObj.Activity = "Removed the task";
                            var AddUpdate2 = await _employeeSubTaskActivityService.CheckInsertOrUpdate(employeeSubTaskActivityObj);

                            var subTaskActivities = await _employeeSubTaskActivityService.DeleteByEmployeeSubTaskId(subTaskId);

                            var subTaskToDelete = await _employeeSubTaskService.Delete(subTaskId);
                        }
                    }

                    var documents = await _employeeTaskAttachmentService.DeleteAttachmentByTaskId(employeeTaskId);

                    // Remove task documents from folder
                    if (documents != null && documents.Count() > 0)
                    {
                        foreach (var taskDoc in documents)
                        {

                            //var dirPath = _hostingEnvironment.WebRootPath + "\\EmployeeTaskUpload";
                            var dirPath = _hostingEnvironment.WebRootPath + OneClappContext.EmployeeTaskUploadDirPath;
                            var filePath = dirPath + "\\" + taskDoc.Name;

                            if (System.IO.File.Exists(filePath))
                            {
                                System.IO.File.Delete(Path.Combine(filePath));
                            }
                        }
                    }

                    var comments = await _employeeTaskCommentService.DeleteCommentByEmployeeTaskId(employeeTaskId);

                    var timeRecords = await _employeeTaskTimeRecordService.DeleteTimeRecordByTaskId(employeeTaskId);

                    var taskUsers = await _employeeTaskUserService.DeleteByEmployeeTaskId(employeeTaskId);
                    EmployeeTaskActivity employeeTaskActivityObj = new EmployeeTaskActivity();
                    employeeTaskActivityObj.EmployeeTaskId = employeeTaskId;
                    employeeTaskActivityObj.UserId = UserId;
                    employeeTaskActivityObj.Activity = "Removed this task";
                    var AddUpdateActivity = await _employeeTaskActivityService.CheckInsertOrUpdate(employeeTaskActivityObj);

                    var taskActivities = await _employeeTaskActivityService.DeleteByEmployeeTaskId(employeeTaskId);

                    var taskToDelete = await _employeeTaskService.Delete(employeeTaskId);
                }
            }

            //var DeletedSection = await _sectionService.DeleteSection(employeeProjectId);

            var AddUpdate = await _employeeProjectService.DeleteEmployeeProject(employeeProjectId);

            EmployeeProjectActivity employeeProjectActivityObj = new EmployeeProjectActivity();
            employeeProjectActivityObj.ProjectId = employeeProjectId;
            employeeProjectActivityObj.Activity = Enums.EmployeeProjectActivityEnum.Project_Removed.ToString().Replace("_", " ");
            employeeProjectActivityObj.UserId = UserId;
            var ProjectActivityObj = await _employeeProjectActivityService.CheckInsertOrUpdate(employeeProjectActivityObj);


            var ProjectUsers = await _employeeProjectUserService.DeleteByEmployeeProjectId(employeeProjectId);

            EmployeeProjectActivity employeeProjectUserActivityObj = new EmployeeProjectActivity();
            employeeProjectUserActivityObj.ProjectId = employeeProjectId;
            employeeProjectUserActivityObj.UserId = UserId;
            employeeProjectUserActivityObj.Activity = Enums.EmployeeProjectActivityEnum.Assign_user_removed.ToString().Replace("_", " ");
            var employeeProjectUserObj = await _employeeProjectActivityService.CheckInsertOrUpdate(employeeProjectUserActivityObj);



            // //start for custom module    
            // CustomModule? customModuleObj = null;
            // var employeeProjectTableObj = _customTableService.GetByName("Project");
            // if (employeeProjectTableObj != null)
            // {
            //     customModuleObj = _customModuleService.GetByCustomTable(employeeProjectTableObj.Id);
            // }

            // if (customModuleObj != null)
            // {

            //     var moduleFields = _moduleFieldService.GetAllModuleField(customModuleObj.Id);

            //     var moduleFieldIdList = moduleFields.Select(t => t.Id).ToList();

            //     var moduleRecordFieldList = _moduleRecordCustomFieldService.GetByModuleFieldIdList(moduleFieldIdList);
            //     if (moduleRecordFieldList != null && moduleRecordFieldList.Count() > 0)
            //     {
            //         foreach (var moduleRecordField in moduleRecordFieldList)
            //         {
            //             if (moduleRecordField.RecordId == employeeProjectId)
            //             {
            //                 var DeletedModuleRecordField = await _moduleRecordCustomFieldService.DeleteById(moduleRecordField.Id);

            //                 var moduleFieldId = moduleRecordField.ModuleFieldId;
            //                 long? CustomFieldId1 = null;
            //                 if (moduleRecordField.ModuleField.CustomField != null)
            //                 {
            //                     CustomFieldId1 = moduleRecordField.ModuleField.CustomField.Id;
            //                 }

            //                 if (moduleFieldId != null)
            //                 {
            //                     var DeleteModuleField = await _moduleFieldService.Delete(moduleFieldId.Value);
            //                 }

            //                 if (CustomFieldId1 != null && AddUpdate.TenantId != null)
            //                 {
            //                     var DeleteTenantField = await _customTenantFieldService.DeleteTenantField(CustomFieldId1.Value, AddUpdate.TenantId.Value);
            //                 }

            //                 CustomTableColumnDto customTableColumnDto = new CustomTableColumnDto();
            //                 customTableColumnDto.Name = moduleRecordField.ModuleField.CustomField.Name;
            //                 customTableColumnDto.ControlId = moduleRecordField.ModuleField.CustomField.ControlId;
            //                 customTableColumnDto.IsDefault = false;
            //                 customTableColumnDto.TenantId = AddUpdate.TenantId;
            //                 if (CustomFieldId1 != null)
            //                 {
            //                     customTableColumnDto.CustomFieldId = CustomFieldId1;
            //                 }

            //                 customTableColumnDto.MasterTableId = customModuleObj.Id;

            //                 var deleteTableColumns = await _customTableColumnService.DeleteCustomFields(customTableColumnDto);

            //                 if (CustomFieldId1 != null)
            //                 {
            //                     var deleteTableColumns1 = _customFieldService.DeleteById(CustomFieldId1.Value);
            //                 }

            //             }
            //         }
            //     }
            // }
            // //end for custom module

            var responseDelete = _mapper.Map<EmployeeProjectDeleteResponse>(model);
            return new OperationResult<EmployeeProjectDeleteResponse>(true, System.Net.HttpStatusCode.OK, "Project delete successfully", responseDelete);

        }

        [Authorize(Roles = "Admin, TenantAdmin, TenantManager, TenantUser, ExternalUser")]
        [HttpGet("{EmployeeProjectId}")]
        public async Task<OperationResult<ProjectDetailResponse>> Detail(long EmployeeProjectId)
        {
            //ProjectVM projectVMObj = new ProjectVM();
            ProjectDetailResponse projectDetailResponseObj = new ProjectDetailResponse();
            var projectObj = _employeeProjectService.GetEmployeeProjectById(EmployeeProjectId);
            var AllUsers = _userService.GetAll();

            if (projectObj != null)
            {
                //projectVMObj = _mapper.Map<ProjectVM>(projectObj);

                // var y = 60 * 60 * 1000;
                // if (projectVMObj.Id != null)
                // {
                //     var projectId = projectVMObj.Id.Value;

                var taskList = _employeeTaskService.GetAllTaskByProjectId(EmployeeProjectId);

                //     projectVMObj.TaskCount = taskList.Count();
                //     if (taskList != null && taskList.Count() > 0)
                //     {
                //         projectVMObj.Tasks = new List<EmployeeTaskVM>();
                //         var taskVMList = _mapper.Map<List<EmployeeTaskVM>>(taskList);
                //         foreach (var taskObj in taskVMList)
                //         {
                //             if (taskObj.Id != null)
                //             {
                //                 var assignTaskUsers = _employeeTaskUserService.GetAssignUsersByEmployeeTask(taskObj.Id.Value);
                //                 if (assignTaskUsers.Count > 0)
                //                 {
                //                     taskObj.AssignedUsers = new List<EmployeeTaskUserDto>();
                //                     var assignUsersVMList = _mapper.Map<List<EmployeeTaskUserDto>>(assignTaskUsers);
                //                     taskObj.AssignedUsers = assignUsersVMList;
                //                 }
                //             }
                //         }
                //         projectVMObj.Tasks = taskVMList;
                //     }

                //     // var sectionList = _sectionService.GetByProject(EmployeeProjectId);

                //     // if (sectionList != null && sectionList.Count() > 0)
                //     // {
                //     //     projectVMObj.Sections = new List<EmployeeSectionVM>();
                //     //     var sectionVMList = _mapper.Map<List<EmployeeSectionVM>>(sectionList);
                //     //     //SectionVM sectionVMObj = new SectionVM();
                //     //     foreach (var sectionObj in sectionVMList)
                //     //     {
                //     //         if (sectionObj.Id != null)
                //     //         {
                //     //             var sectionId = sectionObj.Id.Value;

                //     //             var sectiontaskList = _employeeTaskService.GetAllTaskBySection(sectionId);

                //     //             if (sectiontaskList != null && sectiontaskList.Count() > 0)
                //     //             {
                //     //                 sectionObj.Tasks = new List<EmployeeTaskVM>();
                //     //                 var taskVMList = _mapper.Map<List<EmployeeTaskVM>>(taskList);

                //     //                 foreach (var taskObj in taskVMList)
                //     //                 {
                //     //                     if (taskObj.Id != null)
                //     //                     {
                //     //                         var taskTotalDuration = _employeeTaskTimeRecordService.GetTotalEmployeeTaskTimeRecord(taskObj.Id.Value);
                //     //                         taskObj.Duration = taskTotalDuration;

                //     //                         var h = taskTotalDuration / y;
                //     //                         var m = (taskTotalDuration - (h * y)) / (y / 60);
                //     //                         var s = (taskTotalDuration - (h * y) - (m * (y / 60))) / 1000;

                //     //                         taskObj.Seconds = s;
                //     //                         taskObj.Minutes = m;
                //     //                         taskObj.Hours = h;
                //     //                         var assignTaskUsers = _employeeTaskUserService.GetAssignUsersByEmployeeTask(taskObj.Id.Value);
                //     //                         if (assignTaskUsers.Count > 0)
                //     //                         {
                //     //                             taskObj.AssignedUsers = new List<EmployeeTaskUserDto>();
                //     //                             var assignUsersVMList = _mapper.Map<List<EmployeeTaskUserDto>>(assignTaskUsers);
                //     //                             taskObj.AssignedUsers = assignUsersVMList;
                //     //                         }
                //     //                     }
                //     //                 }
                //     //                 sectionObj.Tasks = taskVMList;
                //     //             }
                //     //         }
                //     //     }
                //     //     projectVMObj.Sections = sectionVMList;
                //     // }

                // }
                projectDetailResponseObj = _mapper.Map<ProjectDetailResponse>(projectObj);

                var Timestamp = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds();

                var employeeProjectUsersObj = _employeeProjectUserService.GetAssignUsersByEmployeeProject(EmployeeProjectId);

                if (employeeProjectUsersObj != null && employeeProjectUsersObj.Count > 0)
                {
                    //var assignUserVMList = _mapper.Map<List<EmployeeProjectUserRequestResponse>>(employeeProjectUsersObj);

                    if (projectDetailResponseObj.AssignedUsers == null)
                    {
                        projectDetailResponseObj.AssignedUsers = new List<EmployeeProjectUserRequestResponse>();
                    }

                    foreach (var assignUser in employeeProjectUsersObj)
                    {
                        EmployeeProjectUserRequestResponse employeeProjectUserRequestResponseObj = new EmployeeProjectUserRequestResponse();
                        employeeProjectUserRequestResponseObj.Id = assignUser.Id;
                        employeeProjectUserRequestResponseObj.EmployeeProjectId = EmployeeProjectId;
                        employeeProjectUserRequestResponseObj.UserId = assignUser.User.Id;
                        employeeProjectUserRequestResponseObj.FirstName = assignUser.User.FirstName;
                        employeeProjectUserRequestResponseObj.LastName = assignUser.User.LastName;
                        employeeProjectUserRequestResponseObj.Email = assignUser.User.Email;

                        //var userdirPath = _hostingEnvironment.WebRootPath + "\\ProfileImageUpload\\Original";
                        var userdirPath = _hostingEnvironment.WebRootPath + OneClappContext.OriginalUserProfileDirPath;
                        var userfilePath = "";

                        if (assignUser.User.ProfileImage == null)
                        {
                            userfilePath = "assets/images/default-profile.jpg";
                        }
                        else
                        {
                            userfilePath = OneClappContext.CurrentURL + "User/ProfileImageView/" + assignUser.User.Id + "?" + Timestamp;
                        }

                        employeeProjectUserRequestResponseObj.ProfileImageURL = userfilePath;
                        projectDetailResponseObj.AssignedUsers.Add(employeeProjectUserRequestResponseObj);
                    }

                }


                if (projectObj.Logo == null)
                {
                    projectDetailResponseObj.LogoURL = "assets/images/default-project.png";
                }
                else
                {
                    projectDetailResponseObj.LogoURL = OneClappContext.CurrentURL + "Project/Logo/" + projectDetailResponseObj.Id + "?" + Timestamp;
                }
                if (projectObj.StatusId != null)
                {
                    projectDetailResponseObj.StatusName = projectObj.Status.Name;
                }
                if (projectObj.CreatedBy != null)
                {
                    var userObj = AllUsers.Where(t => t.Id == projectObj.CreatedBy).FirstOrDefault();
                    var UserName = userObj.FirstName + " " + userObj.LastName;
                    if (!String.IsNullOrEmpty(UserName))
                    {
                        projectDetailResponseObj.UserName = userObj.FirstName + " " + userObj.LastName;
                    }
                    else
                    {
                        projectDetailResponseObj.UserName = userObj.Email;
                    }

                    var userfilePath = "";

                    if (userObj.ProfileImage == null)
                    {
                        userfilePath = "assets/images/default-profile.jpg";
                    }
                    else
                    {
                        userfilePath = OneClappContext.CurrentURL + "User/ProfileImageView/" + userObj.Id + "?" + Timestamp;
                    }
                    projectDetailResponseObj.UserProfileURL = userfilePath;
                }
                projectDetailResponseObj.TaskCount = taskList.Count();
                return new OperationResult<ProjectDetailResponse>(true, System.Net.HttpStatusCode.OK, "", projectDetailResponseObj);
            }
            else
            {
                return new OperationResult<ProjectDetailResponse>(false, System.Net.HttpStatusCode.OK, "Project not found", projectDetailResponseObj);
            }
        }

        [Authorize(Roles = "Admin,TenantManager,TenantAdmin, ExternalUser, TenantUser")]
        [HttpPost]
        public async Task<OperationResult<List<EmployeeProjectListResponse>>> List([FromBody] EmployeeProjectListRequest model)
        {
            List<EmployeeProject> employeeProjectList = new List<EmployeeProject>();

            ClaimsPrincipal user = this.User as ClaimsPrincipal;
            TenantId = Convert.ToInt32(user.FindFirst("TenantId").Value);

            employeeProjectList = _employeeProjectService.GetAll(model);
            //var AllStatus = _statusService.GetByTenant(TenantId);

            var employeeProjectListResponse = _mapper.Map<List<EmployeeProjectListResponse>>(employeeProjectList);

            if (employeeProjectListResponse != null)
            {
                if (employeeProjectListResponse != null && employeeProjectListResponse.Count() > 0)
                {
                    //var dirPath = _hostingEnvironment.WebRootPath + "\\ProjectLogo";
                    var dirPath = _hostingEnvironment.WebRootPath + OneClappContext.ProjectLogoDirPath;
                    //filePath = dirPath + "\\" + item.Logo;
                    foreach (var item in employeeProjectListResponse)
                    {
                        // var statusObj = AllStatus.Where(t => t.Id == item.StatusId).FirstOrDefault();
                        // if (statusObj != null)
                        // {
                        //     item.StatusName = statusObj.Name;
                        // }
                        if (item.StatusId != null)
                        {
                            var statusObj = employeeProjectList.Where(t => t.Status.Id == item.StatusId).FirstOrDefault();
                            if (statusObj != null)
                            {
                                item.StatusId = statusObj.Status.Id;
                                item.StatusName = statusObj.Status.Name;
                            }
                        }

                        var Timestamp = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds();
                        if (item.Logo == null)
                        {
                            item.LogoURL = "assets/images/default-project.png";
                        }
                        else
                        {
                            item.LogoURL = OneClappContext.CurrentURL + "Project/Logo/" + item.Id + "?" + Timestamp;
                        }
                    }
                }
            }
            return new OperationResult<List<EmployeeProjectListResponse>>(true, System.Net.HttpStatusCode.OK, "", employeeProjectListResponse);

        }

        [AllowAnonymous]
        [HttpGet("{Id}")]
        public async Task<FileResult> Logo(int Id)
        {
            var projectDetailsObj = _employeeProjectService.GetEmployeeProjectById(Id);
            //var dirPath = _hostingEnvironment.WebRootPath + "\\ProjectLogo";
            var dirPath = _hostingEnvironment.WebRootPath + OneClappContext.ProjectLogoDirPath;
            var filePath = dirPath + "\\" + "default.png";
            if (projectDetailsObj != null && !string.IsNullOrEmpty(projectDetailsObj.Logo))
            {
                filePath = dirPath + "\\" + projectDetailsObj.Logo;
                if (System.IO.File.Exists(filePath))
                {
                    var bytes = await System.IO.File.ReadAllBytesAsync(filePath);

                    return File(bytes, GetMimeTypes(projectDetailsObj.Logo), projectDetailsObj.Logo);
                }
            }
            return null;
        }

        private string GetMimeTypes(string fileName)
        {
            string mimeType = "application/unknown";
            string ext = System.IO.Path.GetExtension(fileName).ToLower();
            Microsoft.Win32.RegistryKey regKey = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(ext);
            if (regKey != null && regKey.GetValue("Content Type") != null)
                mimeType = regKey.GetValue("Content Type").ToString();
            return mimeType;
        }

        [Authorize(Roles = "Admin, TenantAdmin, TenantManager, TenantUser, ExternalUser")]
        [HttpGet("{EmployeeProjectId}")]
        public async Task<OperationResult<List<EmployeeProjectActivityDto>>> History(long EmployeeProjectId)
        {
            List<EmployeeProjectActivityDto> employeeProjectActivityDtoList = new List<EmployeeProjectActivityDto>();
            var AllUsers = _userService.GetAll();
            var activities = _employeeProjectActivityService.GetAllByProjectId(EmployeeProjectId);
            employeeProjectActivityDtoList = _mapper.Map<List<EmployeeProjectActivityDto>>(activities);
            if (employeeProjectActivityDtoList != null && employeeProjectActivityDtoList.Count() > 0)
            {
                foreach (var item in employeeProjectActivityDtoList)
                {
                    var userObj = AllUsers.Where(t => t.Id == item.UserId).FirstOrDefault();
                    if (userObj != null)
                    {
                        item.FirstName = userObj.FirstName;
                        item.LastName = userObj.LastName;
                        item.Email = userObj.Email;
                        if (item.FirstName != null)
                        {
                            item.ShortName = item.FirstName.Substring(0, 1);
                        }
                        if (item.LastName != null)
                        {
                            item.ShortName = item.ShortName + item.LastName.Substring(0, 1);
                        }
                        var Timestamp = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds();
                        if (userObj.ProfileImage == null)
                        {
                            item.ProfileUrl = "assets/images/default-profile.jpg";
                        }
                        else
                        {
                            item.ProfileUrl = OneClappContext.CurrentURL + "User/ProfileImageView/" + userObj.Id + "?" + Timestamp;
                        }
                    }
                }
            }
            return new OperationResult<List<EmployeeProjectActivityDto>>(true, System.Net.HttpStatusCode.OK, "", employeeProjectActivityDtoList);
        }

        // [Authorize(Roles = "Admin,TenantManager,TenantAdmin, ExternalUser, TenantUser")]
        // [HttpGet("{SearchString}")]
        // public async Task<OperationResult<List<EmployeeProjectListResponse>>> All(string SearchString)
        // {
        //     List<EmployeeProjectDto> employeeProjectDtoList = new List<EmployeeProjectDto>();

        //     EmployeeProjectDto employeeProjectObj = new EmployeeProjectDto();

        //     ClaimsPrincipal user = this.User as ClaimsPrincipal;
        //     TenantId = Convert.ToInt32(user.FindFirst("TenantId").Value);

        //     var employeeProjectList = _employeeProjectService.GetAll(SearchString, employeeProjectObj);
        //     employeeProjectDtoList = _mapper.Map<List<EmployeeProjectDto>>(employeeProjectList);
        //     if (employeeProjectDtoList != null && employeeProjectDtoList.Count() > 0)
        //     {
        //         foreach (var item in employeeProjectDtoList)
        //         {
        //             if (item.Id != null)
        //             {
        //                 var projectStatus = _employeeProjectStatusService.GetEmployeeProjectStatusById(item.Id.Value);
        //                 if (projectStatus != null)
        //                 {
        //                     item.StatusId = projectStatus.Id;
        //                 }
        //             }
        //         }
        //     }
        //     CustomModule? customModuleObj = null;
        //     var employeeProjectTableObj = _customTableService.GetByName("Project");
        //     if (employeeProjectTableObj != null)
        //     {
        //         customModuleObj = _customModuleService.GetByCustomTable(employeeProjectTableObj.Id);
        //     }
        //     // var employeeProjectModule = _customModuleService.GetByName("Project");
        //     if (customModuleObj != null)
        //     {
        //         CustomModuleDto customModuleDto = new CustomModuleDto();
        //         customModuleDto.TenantId = TenantId;
        //         customModuleDto.MasterTableId = customModuleObj.MasterTableId;
        //         customModuleDto.Id = customModuleObj.Id;
        //         customModuleDto.Name = customModuleObj.Name;
        //         if (employeeProjectDtoList != null && employeeProjectDtoList.Count() > 0)
        //         {
        //             foreach (var item in employeeProjectDtoList)
        //             {
        //                 customModuleDto.RecordId = item.Id;
        //                 item.CustomFields = await customFieldLogic.GetCustomField(customModuleDto);
        //             }
        //         }
        //     }

        //     var employeeProjectListResponse = _mapper.Map<List<EmployeeProjectListResponse>>(employeeProjectDtoList);

        //     return new OperationResult<List<EmployeeProjectListResponse>>(true, System.Net.HttpStatusCode.OK, "", employeeProjectListResponse);

        // }

        [Authorize(Roles = "Admin,TenantManager,TenantAdmin, ExternalUser, TenantUser")]
        [HttpPost]
        public async Task<OperationResult<List<EmployeeProjectUserRequestResponse>>> Assign([FromBody] AssignUserEmployeeProjectRequest requestModel)
        {
            ClaimsPrincipal user = this.User as ClaimsPrincipal;
            UserId = Convert.ToInt32(user.FindFirst(JwtRegisteredClaimNames.Sid).Value);
            TenantId = Convert.ToInt32(user.FindFirst("TenantId").Value);

            List<EmployeeProjectUserRequestResponse> assignUserEmployeeProjectResponseObj = new List<EmployeeProjectUserRequestResponse>();

            //start assign user for project
            if (requestModel.AssignedUsers != null && requestModel.AssignedUsers.Count() > 0)
            {
                var existingItem = _employeeProjectService.GetEmployeeProjectById(requestModel.EmployeeProjectId);

                foreach (var userObj in requestModel.AssignedUsers)
                {
                    EmployeeProjectUserDto employeeProjectUserDto = new EmployeeProjectUserDto();
                    employeeProjectUserDto.EmployeeProjectId = requestModel.EmployeeProjectId;
                    employeeProjectUserDto.UserId = userObj;
                    employeeProjectUserDto.CreatedBy = UserId;
                    var isExist = _employeeProjectUserService.IsExistOrNot(employeeProjectUserDto);
                    var employeeProjectUserObj = await _employeeProjectUserService.CheckInsertOrUpdate(employeeProjectUserDto);

                    if (!isExist)
                    {
                        if (employeeProjectUserDto.UserId != null)
                        {
                            var userAssignDetails = _userService.GetUserById(employeeProjectUserDto.UserId.Value);
                            if (userAssignDetails != null)
                                await sendEmail.SendEmailEmployeeProjectUserAssignNotification(userAssignDetails.Email, userAssignDetails.FirstName + ' ' + userAssignDetails.LastName, existingItem.Name, TenantId);
                            EmployeeProjectActivity employeeProjectActivityObj = new EmployeeProjectActivity();
                            employeeProjectActivityObj.ProjectId = requestModel.EmployeeProjectId;
                            employeeProjectActivityObj.UserId = UserId;
                            employeeProjectActivityObj.Activity = Enums.EmployeeProjectActivityEnum.Project_assigned_to_user.ToString().Replace("_", " ");
                            var AddUpdateEmployeeProjectActivity = await _employeeProjectActivityService.CheckInsertOrUpdate(employeeProjectActivityObj);
                        }
                    }
                }

                var assignUsers = _employeeProjectUserService.GetAssignUsersByEmployeeProject(requestModel.EmployeeProjectId);
                var AllUsers = _userService.GetAll();

                if (assignUsers != null && assignUsers.Count > 0)
                {
                    var assignProjectUserVMList = _mapper.Map<List<EmployeeProjectUserRequestResponse>>(assignUsers);

                    if (assignProjectUserVMList != null && assignProjectUserVMList.Count() > 0)
                    {
                        foreach (var assignUser in assignProjectUserVMList)
                        {
                            if (AllUsers != null)
                            {
                                var userObj = AllUsers.Where(t => t.Id == assignUser.UserId).FirstOrDefault();
                                if (userObj != null)
                                {
                                    assignUser.EmployeeProjectId = requestModel.EmployeeProjectId;
                                    assignUser.UserId = userObj.Id;
                                    assignUser.FirstName = userObj.FirstName;
                                    assignUser.LastName = userObj.LastName;
                                    assignUser.Email = userObj.Email;

                                    var Timestamp = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds();

                                    if (userObj.ProfileImage == null)
                                    {
                                        assignUser.ProfileImageURL = "assets/images/default-profile.jpg";
                                    }
                                    else
                                    {
                                        assignUser.ProfileImageURL = OneClappContext.CurrentURL + "User/ProfileImageView/" + userObj.Id + "?" + Timestamp;
                                    }
                                }
                            }
                        }
                    }
                    assignUserEmployeeProjectResponseObj = assignProjectUserVMList;
                }
            }
            //end assign user for project
            return new OperationResult<List<EmployeeProjectUserRequestResponse>>(true, System.Net.HttpStatusCode.OK, "User assigned successfully", assignUserEmployeeProjectResponseObj);
        }

        [Authorize(Roles = "Admin,TenantManager,TenantAdmin, ExternalUser, TenantUser")]
        [HttpDelete("{Id}")]
        public async Task<OperationResult> UnAssign(long Id)
        {
            ClaimsPrincipal user = this.User as ClaimsPrincipal;
            UserId = Convert.ToInt32(user.FindFirst(JwtRegisteredClaimNames.Sid).Value);
            TenantId = Convert.ToInt32(user.FindFirst("TenantId").Value);

            if (Id != null)
            {
                var employeeProjectUserObj = await _employeeProjectUserService.UnAssign(Id);
                if (employeeProjectUserObj != null)
                {
                    if (employeeProjectUserObj.EmployeeProjectId != null && employeeProjectUserObj.UserId != null)
                    {
                        var userAssignDetails = _userService.GetUserById(employeeProjectUserObj.UserId.Value);
                        var existingItem = _employeeProjectService.GetEmployeeProjectById(employeeProjectUserObj.EmployeeProjectId.Value);
                        if (userAssignDetails != null)
                            await sendEmail.SendEmailRemoveEmployeeProjectUserAssignNotification(userAssignDetails.Email, userAssignDetails.FirstName + ' ' + userAssignDetails.LastName, existingItem.Name, TenantId);

                        EmployeeProjectActivity employeeProjectActivityObj = new EmployeeProjectActivity();
                        employeeProjectActivityObj.ProjectId = employeeProjectUserObj.EmployeeProjectId.Value;
                        employeeProjectActivityObj.UserId = UserId;
                        employeeProjectActivityObj.Activity = Enums.EmployeeProjectActivityEnum.Assign_user_removed.ToString().Replace("_", " ");
                        var AddUpdateEmployeeProjectActivity = await _employeeProjectActivityService.CheckInsertOrUpdate(employeeProjectActivityObj);
                    }
                }
                return new OperationResult(true, System.Net.HttpStatusCode.OK, "User unassigned", Id);
            }
            else
            {
                return new OperationResult(false, System.Net.HttpStatusCode.OK, "Please provide id", Id);
            }
        }

        [Authorize(Roles = "Admin,TenantManager,TenantAdmin, ExternalUser, TenantUser")]
        [HttpPost]
        public async Task<OperationResult<List<EmployeeProjectTaskListResponse>>> TaskList([FromBody] EmployeeProjectTaskListRequest requestModel)
        {
            ClaimsPrincipal user = this.User as ClaimsPrincipal;
            UserId = Convert.ToInt32(user.FindFirst(JwtRegisteredClaimNames.Sid).Value);
            TenantId = Convert.ToInt32(user.FindFirst("TenantId").Value);

            int totalCount = 0;

            var taskList = _employeeTaskService.GetAllTaskListByProjectId(requestModel);
            totalCount = taskList.Count();

            var pagedTaskList = taskList.Skip((requestModel.PageNumber - 1) * requestModel.PageSize).Take(requestModel.PageSize).ToList();

            var orderTaskList = ShortTaskByColumn(requestModel.ShortColumnName, requestModel.SortType, pagedTaskList);


            //var AllStatus = _statusService.GetByTenant(TenantId);            

            var AllUsers = _userService.GetAll();

            var employeeProjectTaskListResponse = _mapper.Map<List<EmployeeProjectTaskListResponse>>(orderTaskList);

            if (employeeProjectTaskListResponse != null && employeeProjectTaskListResponse.Count() > 0)
            {
                foreach (var item in employeeProjectTaskListResponse)
                {
                    // var statusObj = AllStatus.Where(t => t.Id == item.StatusId).FirstOrDefault();
                    // if (statusObj != null)
                    // {
                    //     item.Status = statusObj.Name;
                    // }
                    if (item.StatusId != null)
                    {
                        var statusObj = taskList.Where(t => t.Status.Id == item.StatusId).FirstOrDefault();
                        if (statusObj != null)
                        {
                            item.Status = statusObj.Status.Name;
                        }
                    }

                    var assignTaskUsers = _employeeTaskUserService.GetAssignUsersByEmployeeTask(item.Id);
                    if (assignTaskUsers != null && assignTaskUsers.Count > 0)
                    {
                        var assignTaskUserVMList = _mapper.Map<List<EmployeeTaskUserRequestResponse>>(assignTaskUsers);
                        if (item.AssignedUsers == null)
                        {
                            item.AssignedUsers = new List<EmployeeTaskUserRequestResponse>();
                        }
                        foreach (var assignTaskUser in assignTaskUserVMList)
                        {
                            if (AllUsers != null)
                            {
                                var userObj2 = AllUsers.Where(t => t.Id == assignTaskUser.UserId).FirstOrDefault();
                                if (userObj2 != null)
                                {
                                    assignTaskUser.AssignUserFirstName = userObj2.FirstName;
                                    assignTaskUser.AssignUserLastName = userObj2.LastName;
                                }
                            }
                        }
                        item.AssignedUsers = assignTaskUserVMList;
                    }
                }
            }
            return new OperationResult<List<EmployeeProjectTaskListResponse>>(true, System.Net.HttpStatusCode.OK, "", employeeProjectTaskListResponse, totalCount);
        }

        private List<EmployeeTask> ShortTaskByColumn(string ShortColumn, string ShortOrder, List<EmployeeTask> taskList)
        {
            List<EmployeeTask> employeeTaskVMList = new List<EmployeeTask>();
            if (ShortColumn != "" && ShortColumn != null)
            {
                if (ShortColumn.ToLower() == "description")
                {
                    if (ShortOrder.ToLower() == "asc")
                    {
                        employeeTaskVMList = taskList.OrderBy(t => t.Description).ToList();
                    }
                    else
                    {
                        employeeTaskVMList = taskList.OrderByDescending(t => t.Description).ToList();
                    }
                }
                else if (ShortColumn.ToLower() == "createdon")
                {
                    if (ShortOrder.ToLower() == "asc")
                    {
                        employeeTaskVMList = taskList.OrderBy(t => t.CreatedOn).ToList();
                    }
                    else
                    {
                        employeeTaskVMList = taskList.OrderByDescending(t => t.CreatedOn).ToList();
                    }
                }
                else if (ShortColumn.ToLower() == "status")
                {
                    if (ShortOrder.ToLower() == "asc")
                    {
                        employeeTaskVMList = taskList.OrderBy(t => t.Status.Name).ToList();
                    }
                    else
                    {
                        employeeTaskVMList = taskList.OrderByDescending(t => t.Status.Name).ToList();
                    }
                }
                else
                {
                    employeeTaskVMList = taskList.OrderByDescending(t => t.CreatedOn).ToList();
                }
            }

            return employeeTaskVMList;
        }


        // [Authorize(Roles = "Admin, TenantAdmin, TenantManager, TenantUser, ExternalUser")]
        // [HttpPut]
        // public async Task<OperationResult<EmployeeProjectDto>> Priority([FromBody] EmployeeProjectPriorityRequest requestModel)
        // {
        //     ClaimsPrincipal user = this.User as ClaimsPrincipal;
        //     UserId = Convert.ToInt32(user.FindFirst(JwtRegisteredClaimNames.Sid).Value);
        //     TenantId = Convert.ToInt32(user.FindFirst("TenantId").Value);
        //     EmployeeProjectDto employeeProjectDtoObj = new EmployeeProjectDto();
        //     EmployeeProjectActivity employeeProjectActivityObj = new EmployeeProjectActivity();
        //     if (requestModel.Id != null)
        //     {
        //         // start logic for Update Current task with priority
        //         var projectObj = _employeeProjectService.GetEmployeeProjectById(requestModel.Id.Value);
        //         projectObj.Priority = requestModel.CurrentPriority;
        //         projectObj.UpdatedBy = UserId;

        //         // employeeProjectActivityObj.Activity = "Priority changed for this project. ";
        //         employeeProjectActivityObj.Activity = Enums.ProjectActivityEnum.Priority_changed_for_this_project.ToString().Replace("_", " ");

        //         var projectAddUpdate = await _employeeProjectService.UpdateEmployeeProject(projectObj, projectObj.Id);

        //         employeeProjectActivityObj.ProjectId = requestModel.Id;
        //         employeeProjectActivityObj.UserId = UserId;

        //         var AddUpdate = await _projectActivityService.CheckInsertOrUpdate(employeeProjectActivityObj);
        //         // End Logic


        //         // start logic for task move in with out section list
        //         var employeeProjectLsit = _employeeProjectService.GetAllByTenant(TenantId);

        //         if (requestModel.CurrentPriority < employeeProjectLsit.Count())
        //         {
        //             if (requestModel.CurrentPriority != requestModel.PreviousPriority)
        //             {
        //                 if (requestModel.PreviousPriority < requestModel.CurrentPriority)
        //                 {
        //                     var projects = employeeProjectLsit.Where(t => t.Priority > requestModel.PreviousPriority && t.Priority <= requestModel.CurrentPriority && t.Id != requestModel.Id).ToList();
        //                     if (projects != null && projects.Count() > 0)
        //                     {
        //                         foreach (var item in projects)
        //                         {
        //                             item.Priority = item.Priority - 1;
        //                             await _employeeProjectService.UpdateEmployeeProject(item, item.Id);
        //                         }
        //                     }
        //                 }
        //                 else if (requestModel.PreviousPriority > requestModel.CurrentPriority)
        //                 {
        //                     var projects = employeeProjectLsit.Where(t => t.Priority < requestModel.PreviousPriority && t.Priority >= requestModel.CurrentPriority && t.Id != requestModel.Id).ToList();
        //                     if (projects != null && projects.Count() > 0)
        //                     {
        //                         foreach (var item in projects)
        //                         {
        //                             item.Priority = item.Priority + 1;
        //                             await _employeeProjectService.UpdateEmployeeProject(item, item.Id);
        //                         }
        //                     }
        //                 }
        //             }

        //             // end
        //         }
        //     }
        //     return new OperationResult<EmployeeProjectDto>(true, System.Net.HttpStatusCode.OK, "", employeeProjectDtoObj);
        // }

    }

}