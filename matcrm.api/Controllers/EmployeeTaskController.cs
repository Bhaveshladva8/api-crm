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
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;
using Microsoft.Net.Http.Headers;
using matcrm.api.SignalR;
using matcrm.data.Models.Dto;
using matcrm.data.Models.Request;
using matcrm.data.Models.Response;
using matcrm.data.Models.Tables;
using matcrm.data.Models.ViewModels;
using matcrm.service.BusinessLogic;
using matcrm.service.Common;
using matcrm.service.Services;
using matcrm.data.Context;
using matcrm.service.Utility;

namespace matcrm.api.Controllers
{
    [Authorize]
    [Route("[controller]/[action]")]
    public class EmployeeTaskController : Controller
    {
        private readonly IEmployeeTaskService _employeeTaskService;
        private readonly IEmployeeTaskUserSerivce _employeeTaskUserService;
        private readonly IEmployeeTaskTimeRecordService _employeeTaskTimeRecordService;
        private readonly IEmployeeSubTaskService _employeeSubTaskService;
        private readonly IEmployeeChildTaskService _employeeChildTaskService;
        private readonly IEmployeeTaskUserSerivce _employeeTaskUserSerivce;
        private readonly IEmployeeSubTaskUserService _employeeSubTaskUserService;
        private readonly IEmployeeChildTaskUserService _employeeChildTaskUserService;
        private readonly IEmployeeSubTaskTimeRecordService _employeeSubTaskTimeRecordService;
        private readonly IEmployeeChildTaskTimeRecordService _employeeChildTaskTimeRecordService;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IEmployeeTaskAttachmentService _employeeTaskAttachmentService;
        private readonly IEmployeeTaskActivityService _employeeTaskActivityService;
        private readonly IEmployeeTaskCommentService _employeeTaskCommentService;
        private readonly IUserService _userService;
        private readonly ICustomerService _customerService;
        private readonly IEmployeeTaskStatusService _employeeTaskStatusService;
        private readonly IConfiguration _config;
        private readonly IEmailTemplateService _emailTemplateService;
        private readonly IEmailLogService _emailLogService;
        private readonly IEmailConfigService _emailConfigService;
        private readonly IEmailProviderService _emailProvider;
        private readonly IEmployeeSubTaskAttachmentService _employeeSubTaskAttachmentService;
        private readonly IEmployeeChildTaskAttachmentService _employeeChildTaskAttachmentService;
        private readonly IEmployeeSubTaskActivityService _employeeSubTaskActivityService;
        private readonly IEmployeeChildTaskActivityService _employeeChildTaskActivityService;
        private readonly IEmployeeSubTaskCommentService _employeeSubTaskCommentService;
        private readonly IEmployeeChildTaskCommentService _employeeChildTaskCommentService;
        private readonly IStatusService _statusService;
        private readonly IHubContext<BroadcastHub, IHubClient> _hubContext;
        private readonly OneClappContext _context;
        private IMapper _mapper;
        private int UserId = 0;
        private int TenantId = 0;

        private SendEmail sendEmail;

        public EmployeeTaskController(IEmployeeTaskService employeeTaskService,
            IEmployeeTaskUserSerivce employeeTaskUserService,
            IEmployeeTaskTimeRecordService employeeTaskTimeRecordService,
            IHostingEnvironment hostingEnvironment,
            IEmployeeTaskAttachmentService employeeTaskAttachmentService,
            IEmployeeTaskActivityService employeeTaskActivityService,
            IUserService userService,
            ICustomerService customerService,
            IEmployeeTaskCommentService employeeTaskCommentService,
            IEmployeeTaskStatusService employeeTaskStatusService,
            IConfiguration config,
            // IPdfTemplateService pdfTemplateService,
            ITenantConfigService tenantConfig,
            IEmailTemplateService emailTemplateService,
            IEmailLogService emailLogService,
            IEmailConfigService emailConfigService,
            IEmailProviderService emailProvider,
            IEmployeeSubTaskService employeeSubTaskService,
            IEmployeeChildTaskService employeeChildTaskService,
            IEmployeeTaskUserSerivce employeeTaskUserSerivce,
            IEmployeeSubTaskUserService employeeSubTaskUserService,
            IEmployeeChildTaskUserService employeeChildTaskUserService,
            IEmployeeSubTaskTimeRecordService employeeSubTaskTimeRecordService,
            IEmployeeChildTaskTimeRecordService employeeChildTaskTimeRecordService,
            IWeClappService weClappService,
            IEmployeeSubTaskAttachmentService employeeSubTaskAttachmentService,
            IEmployeeChildTaskAttachmentService employeeChildTaskAttachmentService,
            IEmployeeSubTaskActivityService employeeSubTaskActivityService,
            IEmployeeChildTaskActivityService employeeChildTaskActivityService,
            IEmployeeSubTaskCommentService employeeSubTaskCommentService,
            IEmployeeChildTaskCommentService employeeChildTaskCommentService,
            IHubContext<BroadcastHub, IHubClient> hubContext,
            OneClappContext context,
            IMapper mapper,
            IStatusService statusService)
        {
            _employeeTaskService = employeeTaskService;
            _employeeTaskUserService = employeeTaskUserService;
            _employeeTaskTimeRecordService = employeeTaskTimeRecordService;
            _hostingEnvironment = hostingEnvironment;
            _employeeTaskAttachmentService = employeeTaskAttachmentService;
            _employeeTaskActivityService = employeeTaskActivityService;
            _userService = userService;
            _customerService = customerService;
            _employeeTaskCommentService = employeeTaskCommentService;
            _employeeTaskStatusService = employeeTaskStatusService;
            _emailTemplateService = emailTemplateService;
            _emailLogService = emailLogService;
            _emailProvider = emailProvider;
            _employeeTaskService = employeeTaskService;
            _employeeSubTaskService = employeeSubTaskService;
            _employeeChildTaskService = employeeChildTaskService;
            _employeeTaskUserSerivce = employeeTaskUserSerivce;
            _employeeSubTaskUserService = employeeSubTaskUserService;
            _employeeChildTaskUserService = employeeChildTaskUserService;
            _employeeTaskTimeRecordService = employeeTaskTimeRecordService;
            _employeeSubTaskTimeRecordService = employeeSubTaskTimeRecordService;
            _employeeChildTaskTimeRecordService = employeeChildTaskTimeRecordService;
            _hostingEnvironment = hostingEnvironment;
            _employeeSubTaskAttachmentService = employeeSubTaskAttachmentService;
            _employeeChildTaskAttachmentService = employeeChildTaskAttachmentService;
            _employeeSubTaskActivityService = employeeSubTaskActivityService;
            _employeeChildTaskActivityService = employeeChildTaskActivityService;
            _employeeSubTaskCommentService = employeeSubTaskCommentService;
            _employeeChildTaskCommentService = employeeChildTaskCommentService;
            _employeeTaskStatusService = employeeTaskStatusService;
            _hubContext = hubContext;
            _context = context;
            _config = config;
            _mapper = mapper;
            _statusService = statusService;
            sendEmail = new SendEmail(emailTemplateService, emailLogService, emailConfigService, emailProvider, mapper);
        }

        // Save Time Record [Task]
        [Authorize(Roles = "Admin, TenantAdmin, TenantManager, TenantUser, ExternalUser")]
        [HttpPost]
        public async Task<OperationResult<EmployeeTaskAddUpdateResponse>> Add([FromBody] AddUpdateEmployeeTaskRequest employeeTask)
        {
            ClaimsPrincipal user = this.User as ClaimsPrincipal;
            UserId = Convert.ToInt32(user.FindFirst(JwtRegisteredClaimNames.Sid).Value);
            TenantId = Convert.ToInt32(user.FindFirst("TenantId").Value);
            var employeeTaskDto = _mapper.Map<EmployeeTaskDto>(employeeTask);
            employeeTaskDto.IsActive = true;
            employeeTaskDto.TenantId = TenantId;

            if (employeeTask.Id == null)
            {
                employeeTaskDto.CreatedBy = UserId;
            }
            else
            {
                employeeTaskDto.Id = employeeTask.Id;
                employeeTaskDto.UpdatedBy = UserId;
            }

            // For all subtask with completed status then main task automatic completed status
            // if (employeeTask.Id != null) {
            //     var taskId = employeeTask.Id.Value;
            //     var subTasks = _subTaskService.GetAllSubTaskByTask (taskId);
            //     if (employeeTask.UserId != null && subTasks.Count () > 0) {
            //         var userId = employeeTask.UserId.Value;
            //         var employeeTaskStatusList = _employeeTaskService.GetTaskByUser (userId);
            //         var finalStatus = employeeTaskStatusList.Where (t => t.IsFinalize == true).FirstOrDefault ();
            //         if (finalStatus != null) {
            //             var completedSubTaskCount = subTasks.Where (t => t.StatusId == finalStatus.Id).Count ();
            //             if (subTasks.Count () == completedSubTaskCount) {
            //                 taskObj.StatusId = finalStatus.Id;
            //             }
            //         }
            //     }
            // }

            var taskResult = await _employeeTaskService.CheckInsertOrUpdate(employeeTaskDto);
            if (taskResult != null)
            {
                employeeTaskDto.Id = taskResult.Id;
            }

            EmployeeTaskActivity employeeTaskActivityObj = new EmployeeTaskActivity();
            employeeTaskActivityObj.UserId = UserId;
            if (employeeTask.Id == null)
            {
                employeeTaskActivityObj.ProjectId = taskResult.ProjectId;
                employeeTaskActivityObj.EmployeeTaskId = taskResult.Id;
                employeeTaskActivityObj.Activity = Enums.EmployeeTaskActivityEnum.Task_Created.ToString().Replace("_", " ");
            }
            else
            {
                employeeTaskActivityObj.ProjectId = taskResult.ProjectId;
                employeeTaskActivityObj.EmployeeTaskId = taskResult.Id;
                // employeeTaskActivityObj.Activity = "Updated Task";
                employeeTaskActivityObj.Activity = Enums.EmployeeTaskActivityEnum.Task_Updated.ToString().Replace("_", " ");
            }
            var AddUpdate = await _employeeTaskActivityService.CheckInsertOrUpdate(employeeTaskActivityObj);

            if (employeeTask.AssignedUsers != null && employeeTask.AssignedUsers.Count() > 0)
            {
                foreach (var userObj in employeeTask.AssignedUsers)
                {
                    EmployeeTaskUserDto employeeTaskUserDto = new EmployeeTaskUserDto();
                    employeeTaskUserDto.EmployeeTaskId = taskResult.Id;
                    employeeTaskUserDto.UserId = userObj.UserId;
                    employeeTaskUserDto.CreatedBy = UserId;
                    var isExist = _employeeTaskUserService.IsExistOrNot(employeeTaskUserDto);
                    var employeeTaskUserObj = _employeeTaskUserService.CheckInsertOrUpdate(employeeTaskUserDto);
                    if (employeeTaskUserObj != null)
                    {
                        userObj.Id = employeeTaskUserObj.Id;
                    }
                    if (!isExist)
                    {
                        if (employeeTaskUserDto.UserId != null)
                        {
                            var userAssignDetails = _userService.GetUserById(employeeTaskUserDto.UserId.Value);
                            if (userAssignDetails != null)
                                await sendEmail.SendEmailEmployeeTaskUserAssignNotification(userAssignDetails.Email, userAssignDetails.FirstName + ' ' + userAssignDetails.LastName, employeeTask.Description, TenantId);
                            EmployeeTaskActivity employeeTaskActivityObj1 = new EmployeeTaskActivity();
                            employeeTaskActivityObj1.EmployeeTaskId = taskResult.Id;
                            employeeTaskActivityObj1.UserId = UserId;
                            employeeTaskActivityObj1.ProjectId = taskResult.ProjectId;
                            // employeeTaskActivityObj1.Activity = "Assigned the user";
                            employeeTaskActivityObj1.Activity = Enums.EmployeeTaskActivityEnum.Task_assigned_to_user.ToString().Replace("_", " ");
                            var AddUpdate1 = await _employeeTaskActivityService.CheckInsertOrUpdate(employeeTaskActivityObj1);
                        }
                    }
                }
                employeeTaskDto.AssignedUsers = new List<EmployeeTaskUser>();
                // employeeTaskDto.AssignedUsers = employeeTask.AssignedUsers;
            }
            var employeeTaskAddUpdateResponse = _mapper.Map<EmployeeTaskAddUpdateResponse>(employeeTaskDto);
            employeeTaskAddUpdateResponse.AssignedUsers = employeeTask.AssignedUsers;
            return new OperationResult<EmployeeTaskAddUpdateResponse>(true, System.Net.HttpStatusCode.OK, "Task saved successfully.", employeeTaskAddUpdateResponse);
        }

        // Save Time Record [Task]
        [Authorize(Roles = "Admin, TenantAdmin, TenantManager, TenantUser, ExternalUser")]
        [HttpPut]
        public async Task<OperationResult<EmployeeTaskAddUpdateResponse>> Update([FromBody] AddUpdateEmployeeTaskRequest employeeTask)
        {
            ClaimsPrincipal user = this.User as ClaimsPrincipal;
            UserId = Convert.ToInt32(user.FindFirst(JwtRegisteredClaimNames.Sid).Value);
            TenantId = Convert.ToInt32(user.FindFirst("TenantId").Value);
            EmployeeTaskAddUpdateResponse employeeTaskAddUpdateResponse = new EmployeeTaskAddUpdateResponse();
            var employeeTaskDto = _mapper.Map<EmployeeTaskDto>(employeeTask);
            employeeTaskDto.IsActive = true;
            employeeTaskDto.TenantId = TenantId;

            if (employeeTask.Id == null)
            {
                employeeTaskDto.CreatedBy = UserId;
            }
            else
            {
                employeeTaskDto.Id = employeeTask.Id;
                employeeTaskDto.UpdatedBy = UserId;
            }

            // For all subtask with completed status then main task automatic completed status
            // if (employeeTask.Id != null) {
            //     var taskId = employeeTask.Id.Value;
            //     var subTasks = _subTaskService.GetAllSubTaskByTask (taskId);
            //     if (employeeTask.UserId != null && subTasks.Count () > 0) {
            //         var userId = employeeTask.UserId.Value;
            //         var employeeTaskStatusList = _employeeTaskService.GetTaskByUser (userId);
            //         var finalStatus = employeeTaskStatusList.Where (t => t.IsFinalize == true).FirstOrDefault ();
            //         if (finalStatus != null) {
            //             var completedSubTaskCount = subTasks.Where (t => t.StatusId == finalStatus.Id).Count ();
            //             if (subTasks.Count () == completedSubTaskCount) {
            //                 taskObj.StatusId = finalStatus.Id;
            //             }
            //         }
            //     }
            // }

            var taskResult = await _employeeTaskService.CheckInsertOrUpdate(employeeTaskDto);
            if (taskResult != null)
            {
                employeeTaskDto.Id = taskResult.Id;
            }

            EmployeeTaskActivity employeeTaskActivityObj = new EmployeeTaskActivity();
            employeeTaskActivityObj.UserId = UserId;
            if (employeeTask.Id == null)
            {
                employeeTaskActivityObj.ProjectId = taskResult.ProjectId;
                employeeTaskActivityObj.EmployeeTaskId = taskResult.Id;
                employeeTaskActivityObj.Activity = Enums.EmployeeTaskActivityEnum.Task_Created.ToString().Replace("_", " ");
            }
            else
            {
                employeeTaskActivityObj.ProjectId = taskResult.ProjectId;
                employeeTaskActivityObj.EmployeeTaskId = taskResult.Id;
                // employeeTaskActivityObj.Activity = "Updated Task";
                employeeTaskActivityObj.Activity = Enums.EmployeeTaskActivityEnum.Task_Updated.ToString().Replace("_", " ");
            }
            var AddUpdate = await _employeeTaskActivityService.CheckInsertOrUpdate(employeeTaskActivityObj);

            if (employeeTask.AssignedUsers != null && employeeTask.AssignedUsers.Count() > 0)
            {
                foreach (var userObj in employeeTask.AssignedUsers)
                {
                    EmployeeTaskUserDto employeeTaskUserDto = new EmployeeTaskUserDto();
                    employeeTaskUserDto.EmployeeTaskId = taskResult.Id;
                    employeeTaskUserDto.UserId = userObj.UserId;
                    employeeTaskUserDto.CreatedBy = UserId;
                    var isExist = _employeeTaskUserService.IsExistOrNot(employeeTaskUserDto);
                    var employeeTaskUserObj = _employeeTaskUserService.CheckInsertOrUpdate(employeeTaskUserDto);
                    if (employeeTaskUserObj != null)
                    {
                        userObj.Id = employeeTaskUserObj.Id;
                    }
                    if (!isExist)
                    {
                        if (employeeTaskUserDto.UserId != null)
                        {
                            var userAssignDetails = _userService.GetUserById(employeeTaskUserDto.UserId.Value);
                            if (userAssignDetails != null)
                                await sendEmail.SendEmailEmployeeTaskUserAssignNotification(userAssignDetails.Email, userAssignDetails.FirstName + ' ' + userAssignDetails.LastName, employeeTask.Description, TenantId);
                            EmployeeTaskActivity employeeTaskActivityObj1 = new EmployeeTaskActivity();
                            employeeTaskActivityObj1.EmployeeTaskId = taskResult.Id;
                            employeeTaskActivityObj1.UserId = UserId;
                            employeeTaskActivityObj1.ProjectId = taskResult.ProjectId;
                            // employeeTaskActivityObj1.Activity = "Assigned the user";
                            employeeTaskActivityObj1.Activity = Enums.EmployeeTaskActivityEnum.Task_assigned_to_user.ToString().Replace("_", " ");
                            var AddUpdate1 = await _employeeTaskActivityService.CheckInsertOrUpdate(employeeTaskActivityObj1);
                        }
                    }
                }
                employeeTaskDto.AssignedUsers = new List<EmployeeTaskUser>();
            }
            employeeTaskAddUpdateResponse = _mapper.Map<EmployeeTaskAddUpdateResponse>(employeeTaskDto);
            employeeTaskAddUpdateResponse.AssignedUsers = employeeTask.AssignedUsers;
            return new OperationResult<EmployeeTaskAddUpdateResponse>(true, System.Net.HttpStatusCode.OK, "Task updated successfully.", employeeTaskAddUpdateResponse);
        }

        [Authorize(Roles = "Admin, TenantAdmin, TenantManager, TenantUser, ExternalUser")]
        [HttpPost]
        public async Task<OperationResult<List<EmployeeTaskListResponse>>> List([FromBody] EmployeeTaskListRequest model)
        {
            ClaimsPrincipal user = this.User as ClaimsPrincipal;
            UserId = Convert.ToInt32(user.FindFirst(JwtRegisteredClaimNames.Sid).Value);
            TenantId = Convert.ToInt32(user.FindFirst("TenantId").Value);

            List<EmployeeTask> employeeTaskList = new List<EmployeeTask>();

            employeeTaskList = _employeeTaskService.GetAllActiveTaskByTenant(TenantId, model);
            var AllStatus = _statusService.GetByTenant(TenantId);
            var AllUsers = _userService.GetAll();

            var employeeTaskListResponse = _mapper.Map<List<EmployeeTaskListResponse>>(employeeTaskList);

            if (employeeTaskListResponse != null && employeeTaskListResponse.Count() > 0)
            {
                foreach (var item in employeeTaskListResponse)
                {
                    var statusObj = AllStatus.Where(t => t.Id == item.StatusId).FirstOrDefault();
                    if (statusObj != null)
                    {
                        item.Status = statusObj.Name;
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
            return new OperationResult<List<EmployeeTaskListResponse>>(true, System.Net.HttpStatusCode.OK, "", employeeTaskListResponse);
        }

        // Get All Tasks
        // [Authorize(Roles = "Admin, TenantAdmin, TenantManager, TenantUser, ExternalUser")]
        // [HttpGet("{SearchString}")]
        // public async Task<OperationResult<EmployeeTaskListVM>> List(string SearchString)
        // {
        //     ClaimsPrincipal user = this.User as ClaimsPrincipal;
        //     UserId = Convert.ToInt32(user.FindFirst(JwtRegisteredClaimNames.Sid).Value);
        //     TenantId = Convert.ToInt32(user.FindFirst("TenantId").Value);

        //     EmployeeTaskListVM employeeTaskListVMObj = new EmployeeTaskListVM();
        //     EmployeeTaskDto employeeTaskDto = new EmployeeTaskDto();

        //     //var tasks = _employeeTaskService.GetAllActiveByTenant(TenantId);
        //     var tasks = _employeeTaskService.GetAllActiveTaskByTenant(TenantId, SearchString, employeeTaskDto);
        //     if (tasks.Count() == 0)
        //     {
        //         return new OperationResult<EmployeeTaskListVM>(true, System.Net.HttpStatusCode.OK, "", employeeTaskListVMObj);
        //     }

        //     var taskIdList = tasks.Select(t => t.Id).ToList();
        //     employeeTaskListVMObj.Tasks = new List<EmployeeTaskVM>();

        //     if (tasks != null && tasks.Count() > 0)
        //     {
        //         foreach (var taskObj in tasks)
        //         {
        //             var employeeTaskVMObj = _mapper.Map<EmployeeTaskVM>(taskObj);
        //             if (employeeTaskVMObj.Id != null)
        //             {
        //                 var taskTotalDuration = _employeeTaskTimeRecordService.GetTotalEmployeeTaskTimeRecord(employeeTaskVMObj.Id.Value);
        //                 employeeTaskVMObj.Duration = taskTotalDuration;

        //                 var y = 60 * 60 * 1000;
        //                 var h = taskTotalDuration / y;
        //                 var m = (taskTotalDuration - (h * y)) / (y / 60);
        //                 var s = (taskTotalDuration - (h * y) - (m * (y / 60))) / 1000;
        //                 var mi = taskTotalDuration - (h * y) - (m * (y / 60)) - (s * 1000);

        //                 employeeTaskVMObj.Seconds = s;
        //                 employeeTaskVMObj.Minutes = m;
        //                 employeeTaskVMObj.Hours = h;
        //             }
        //             var assignTaskUsers = _employeeTaskUserService.GetAssignUsersByEmployeeTask(taskObj.Id);
        //             if (assignTaskUsers.Count > 0)
        //             {
        //                 employeeTaskVMObj.AssignedUsers = new List<EmployeeTaskUserDto>();
        //                 var assignUsersVMList = _mapper.Map<List<EmployeeTaskUserDto>>(assignTaskUsers);
        //                 employeeTaskVMObj.AssignedUsers = assignUsersVMList;
        //             }
        //             employeeTaskListVMObj.Tasks.Add(employeeTaskVMObj);
        //         }
        //     }

        //     return new OperationResult<EmployeeTaskListVM>(true, System.Net.HttpStatusCode.OK, "", employeeTaskListVMObj);
        // }

        // Assign task to users
        [Authorize(Roles = "Manager,TenantAdmin")]
        [HttpPost]
        public async Task<OperationResult<EmployeeTaskUser>> AssignToCustomer([FromBody] EmployeeTaskUserDto employeeTaskUser)
        {
            var assignEmployeeTaskUserObj = await _employeeTaskUserService.CheckInsertOrUpdate(employeeTaskUser);
            return new OperationResult<EmployeeTaskUser>(true, System.Net.HttpStatusCode.OK, "", assignEmployeeTaskUserObj);
        }

        [Authorize(Roles = "Admin, TenantAdmin, TenantManager, TenantUser, ExternalUser")]
        [HttpPost]
        public async Task<OperationResult<EmployeeTaskTimeRecordResponse>> TimeRecord([FromBody] AddUpdateEmployeeTaskTimeRecordRequest Model)
        {
            ClaimsPrincipal user = this.User as ClaimsPrincipal;
            UserId = Convert.ToInt32(user.FindFirst(JwtRegisteredClaimNames.Sid).Value);
            EmployeeTaskTimeRecord employeeTaskTimeRecordObj = new EmployeeTaskTimeRecord();
            EmployeeTaskTimeRecordResponse employeeTaskTimeRecordResponse = new EmployeeTaskTimeRecordResponse();
            if (Model.Duration != null && Model.EmployeeTaskId != null)
            {
                var taskTotalDuration = _employeeTaskTimeRecordService.GetTotalEmployeeTaskTimeRecord(Model.EmployeeTaskId.Value);
                if (taskTotalDuration >= 0)
                {
                    Model.Duration = Model.Duration - taskTotalDuration;
                }
                var employeeTaskTimeRecordDto = _mapper.Map<EmployeeTaskTimeRecordDto>(Model);
                employeeTaskTimeRecordDto.UserId = UserId;
                employeeTaskTimeRecordObj = await _employeeTaskTimeRecordService.CheckInsertOrUpdate(employeeTaskTimeRecordDto);
                EmployeeTaskActivity employeeTaskActivityObj = new EmployeeTaskActivity();
                employeeTaskActivityObj.EmployeeTaskId = employeeTaskTimeRecordObj.EmployeeTaskId;
                employeeTaskActivityObj.UserId = UserId;
                employeeTaskActivityObj.Activity = "Created time record";
                employeeTaskActivityObj.Activity = Enums.EmployeeTaskActivityEnum.Time_record_added.ToString().Replace("_", " ");
                var AddUpdate1 = await _employeeTaskActivityService.CheckInsertOrUpdate(employeeTaskActivityObj);

                employeeTaskTimeRecordResponse = _mapper.Map<EmployeeTaskTimeRecordResponse>(employeeTaskTimeRecordObj);
                return new OperationResult<EmployeeTaskTimeRecordResponse>(true, System.Net.HttpStatusCode.OK, "", employeeTaskTimeRecordResponse);
            }
            else
            {
                var message = "EmployeeTaskId can not be null";
                if (Model.Duration == null)
                {
                    message = "Duration can not be null";
                }
                return new OperationResult<EmployeeTaskTimeRecordResponse>(false, System.Net.HttpStatusCode.OK, message, employeeTaskTimeRecordResponse);
            }

        }

        [Authorize(Roles = "Admin, TenantAdmin, TenantManager, TenantUser, ExternalUser")]
        // [AllowAnonymous]
        [HttpPost]
        public async Task<OperationResult<List<EmployeeTaskAttachment>>> UploadFiles([FromForm] EmployeeTaskAttachmentRequest model)
        {
            ClaimsPrincipal user = this.User as ClaimsPrincipal;
            UserId = Convert.ToInt32(user.FindFirst(JwtRegisteredClaimNames.Sid).Value);
            List<EmployeeTaskAttachment> employeeTaskAttachmentList = new List<EmployeeTaskAttachment>();

            if (model.FileList == null) throw new Exception("File is null");
            if (model.FileList.Length == 0) throw new Exception("File is empty");

            EmployeeTaskActivity employeeTaskActivityObj = new EmployeeTaskActivity();
            employeeTaskActivityObj.EmployeeTaskId = model.EmployeeTaskId;
            employeeTaskActivityObj.UserId = UserId;
            employeeTaskActivityObj.Activity = "Uploaded document.";

            foreach (IFormFile file in model.FileList)
            {
                // full path to file in temp location
                //var dirPath = _hostingEnvironment.WebRootPath + "\\EmployeeTaskUpload";
                var dirPath = _hostingEnvironment.WebRootPath + OneClappContext.EmployeeTaskUploadDirPath;

                if (!Directory.Exists(dirPath))
                {
                    Directory.CreateDirectory(dirPath);
                }

                var fileName = string.Concat(
                    Path.GetFileNameWithoutExtension(file.FileName + "_" + model.EmployeeTaskId),
                    DateTime.Now.ToString("yyyyMMdd_HHmmss"),
                    Path.GetExtension(file.FileName)
                );
                var filePath = dirPath + "\\" + fileName;

                if (OneClappContext.ClamAVServerIsLive)
                {
                    ScanDocument scanDocumentObj = new ScanDocument();
                    bool fileStatus = await scanDocumentObj.ScanDocumentWithClam(file);
                    if (fileStatus)
                    {
                        return new OperationResult<List<EmployeeTaskAttachment>>(false, System.Net.HttpStatusCode.OK, "Virus Found!");
                    }
                }

                using (var oStream = new FileStream(filePath, FileMode.Create, FileAccess.ReadWrite))
                {
                    await file.CopyToAsync(oStream);
                }

                EmployeeTaskAttachmentDto employeeTaskAttachmentDto = new EmployeeTaskAttachmentDto();
                employeeTaskAttachmentDto.Name = fileName;
                employeeTaskAttachmentDto.EmployeeTaskId = model.EmployeeTaskId;
                employeeTaskAttachmentDto.UserId = UserId;
                var addedItem = await _employeeTaskAttachmentService.CheckInsertOrUpdate(employeeTaskAttachmentDto);
                employeeTaskAttachmentList.Add(addedItem);
            }
            var AddUpdate1 = await _employeeTaskActivityService.CheckInsertOrUpdate(employeeTaskActivityObj);

            await _hubContext.Clients.All.OnUploadEmployeeTaskDocumentEventEmit(model.EmployeeTaskId);

            return new OperationResult<List<EmployeeTaskAttachment>>(true, System.Net.HttpStatusCode.OK, "", employeeTaskAttachmentList);
        }

        [Authorize(Roles = "Admin, TenantAdmin, TenantManager, TenantUser, ExternalUser")]
        // [AllowAnonymous]
        [HttpGet("{EmployeeTaskId}")]
        public async Task<OperationResult<List<EmployeeTaskAttachment>>> Documents(long EmployeeTaskId)
        {

            List<EmployeeTaskAttachment> employeeTaskAttachmentList = new List<EmployeeTaskAttachment>();
            employeeTaskAttachmentList = _employeeTaskAttachmentService.GetAllByEmployeeTaskId(EmployeeTaskId);

            return new OperationResult<List<EmployeeTaskAttachment>>(true, System.Net.HttpStatusCode.OK, "", employeeTaskAttachmentList);
        }

        [AllowAnonymous]
        [HttpGet("{TaskAttachmentId}")]
        public async Task<OperationResult<string>> Document(long TaskAttachmentId)
        {
            var taskAttachmentObj = _employeeTaskAttachmentService.GetEmployeeTaskAttachmentById(TaskAttachmentId);

            //var dirPath = _hostingEnvironment.WebRootPath + "\\EmployeeTaskUpload";
            var dirPath = _hostingEnvironment.WebRootPath + OneClappContext.EmployeeTaskUploadDirPath;
            var filePath = dirPath + "\\" + taskAttachmentObj.Name;
            Byte[] newBytes = System.IO.File.ReadAllBytes(filePath);
            String file = Convert.ToBase64String(newBytes);
            if (file != "")
            {
                return new OperationResult<string>(true, System.Net.HttpStatusCode.OK, "File received successfully", file);
            }
            else
            {
                return new OperationResult<string>(false, System.Net.HttpStatusCode.OK, "Issue in downloading file.");
            }

        }

        // Get Task Detail by Id
        [Authorize(Roles = "Admin, TenantAdmin, TenantManager, TenantUser, ExternalUser")]
        [HttpGet("{EmployeeTaskId}")]
        public async Task<OperationResult<EmployeeTaskVM>> Detail(long EmployeeTaskId)
        {
            EmployeeTaskVM employeeTaskVMObj = new EmployeeTaskVM();

            var AllCustomers = _customerService.GetAllCustomer();
            var AllUsers = _userService.GetAll();
            var taskObj = _employeeTaskService.GetTaskById(EmployeeTaskId);
            employeeTaskVMObj = _mapper.Map<EmployeeTaskVM>(taskObj);

            //var taskTotalDuration = _employeeTaskTimeRecordService.GetTotalEmployeeTaskTimeRecord(EmployeeTaskId);
            //employeeTaskVMObj.Duration = taskTotalDuration;

            // For task assign users
            var assignUsers = _employeeTaskUserService.GetAssignUsersByEmployeeTask(EmployeeTaskId);
            if (assignUsers != null && assignUsers.Count > 0)
            {
                var assignTaskUserVMList = _mapper.Map<List<EmployeeTaskUserDto>>(assignUsers);
                if (employeeTaskVMObj.AssignedUsers == null)
                {
                    employeeTaskVMObj.AssignedUsers = new List<EmployeeTaskUserDto>();
                }
                if (assignTaskUserVMList != null && assignTaskUserVMList.Count() > 0)
                {
                    foreach (var assignUser in assignTaskUserVMList)
                    {
                        if (AllCustomers != null)
                        {
                            var customerObj = AllCustomers.Where(t => t.Id == assignUser.UserId).FirstOrDefault();
                            if (customerObj != null)
                            {
                                assignUser.Name = customerObj.Name;
                                // assignUser.LastName = userObj.LastName;
                            }
                        }
                    }
                }
                employeeTaskVMObj.AssignedUsers = assignTaskUserVMList;

            }

            // For task documents
            var employeeTaskDocuments = _employeeTaskAttachmentService.GetAllByEmployeeTaskId(EmployeeTaskId);
            if (employeeTaskDocuments != null && employeeTaskDocuments.Count > 0)
            {
                if (employeeTaskVMObj.Documents == null)
                {
                    employeeTaskVMObj.Documents = new List<EmployeeTaskAttachment>();
                }
                employeeTaskVMObj.Documents = employeeTaskDocuments;
            }

            // For task commnets
            var employeeTaskComments = _employeeTaskCommentService.GetAllByEmployeeTaskId(EmployeeTaskId);
            if (employeeTaskComments != null && employeeTaskComments.Count > 0)
            {
                var employeeTaskCommentsVMList = _mapper.Map<List<EmployeeTaskCommentDto>>(employeeTaskComments);
                foreach (var employeeTaskCommentObj in employeeTaskCommentsVMList)
                {
                    if (AllUsers != null)
                    {
                        var userObjCom = AllUsers.Where(t => t.Id == employeeTaskCommentObj.UserId).FirstOrDefault();
                        if (userObjCom != null)
                        {
                            employeeTaskCommentObj.FirstName = userObjCom.FirstName;
                            employeeTaskCommentObj.LastName = userObjCom.LastName;
                        }
                    }
                }
                if (employeeTaskVMObj.Comments == null)
                {
                    employeeTaskVMObj.Comments = new List<EmployeeTaskCommentDto>();
                }
                employeeTaskVMObj.Comments = employeeTaskCommentsVMList;
            }

            // For task activities
            var employeeTaskActivities = _employeeTaskActivityService.GetAllByEmployeeTaskId(EmployeeTaskId);
            if (employeeTaskActivities != null && employeeTaskActivities.Count > 0)
            {
                var employeeTaskActivitiesVMList = _mapper.Map<List<EmployeeTaskActivityDto>>(employeeTaskActivities);
                if (employeeTaskActivitiesVMList != null && employeeTaskActivitiesVMList.Count() > 0)
                {
                    foreach (var employeeTaskActivityObj in employeeTaskActivitiesVMList)
                    {
                        if (AllUsers != null)
                        {
                            var userObjAct = AllUsers.Where(t => t.Id == employeeTaskActivityObj.UserId).FirstOrDefault();
                            if (userObjAct != null)
                            {
                                employeeTaskActivityObj.FirstName = userObjAct.FirstName;
                                employeeTaskActivityObj.LastName = userObjAct.LastName;
                            }
                        }
                    }
                }
                if (employeeTaskVMObj.Activities == null)
                {
                    employeeTaskVMObj.Activities = new List<EmployeeTaskActivityDto>();
                }
                employeeTaskVMObj.Activities = employeeTaskActivitiesVMList;
            }

            return new OperationResult<EmployeeTaskVM>(true, System.Net.HttpStatusCode.OK, "", employeeTaskVMObj);
        }

        [Authorize(Roles = "Admin, TenantAdmin, TenantManager, TenantUser, ExternalUser")]
        [HttpDelete("{EmployeeTaskId}")]
        public async Task<OperationResult<EmployeeTaskDeleteResponse>> Remove(long EmployeeTaskId)
        {
            ClaimsPrincipal user = this.User as ClaimsPrincipal;
            UserId = Convert.ToInt32(user.FindFirst(JwtRegisteredClaimNames.Sid).Value);
            TenantId = Convert.ToInt32(user.FindFirst("TenantId").Value);
            EmployeeTaskDto model = new EmployeeTaskDto();

            var employeeTaskId = EmployeeTaskId;

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

            var comments = await _employeeTaskCommentService.DeleteCommentByEmployeeTaskId(employeeTaskId);

            var timeRecords = await _employeeTaskTimeRecordService.DeleteTimeRecordByTaskId(employeeTaskId);

            var taskUsers = await _employeeTaskUserService.DeleteByEmployeeTaskId(employeeTaskId);

            EmployeeTaskActivity employeeTaskActivityObj = new EmployeeTaskActivity();
            employeeTaskActivityObj.EmployeeTaskId = employeeTaskId;
            employeeTaskActivityObj.UserId = UserId;
            employeeTaskActivityObj.Activity = "Removed the task";
            var AddUpdate = await _employeeTaskActivityService.CheckInsertOrUpdate(employeeTaskActivityObj);

            var taskActivities = await _employeeTaskActivityService.DeleteByEmployeeTaskId(employeeTaskId);

            var taskToDelete = await _employeeTaskService.Delete(employeeTaskId);

            var responsemodel = _mapper.Map<EmployeeTaskDeleteResponse>(model);
            return new OperationResult<EmployeeTaskDeleteResponse>(true, System.Net.HttpStatusCode.OK, "Task deleted successfully", responsemodel);

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
        [HttpGet("{EmployeeTaskId}")]
        public async Task<OperationResult<List<EmployeeTaskActivityDto>>> History(long EmployeeTaskId)
        {
            List<EmployeeTaskActivityDto> employeeTaskActivityDtoList = new List<EmployeeTaskActivityDto>();
            var AllUsers = _userService.GetAll();
            var activities = _employeeTaskActivityService.GetAllByEmployeeTaskId(EmployeeTaskId);
            employeeTaskActivityDtoList = _mapper.Map<List<EmployeeTaskActivityDto>>(activities);
            if (employeeTaskActivityDtoList != null && employeeTaskActivityDtoList.Count() > 0)
            {
                foreach (var item in employeeTaskActivityDtoList)
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
                    }
                }
            }
            return new OperationResult<List<EmployeeTaskActivityDto>>(true, System.Net.HttpStatusCode.OK, "", employeeTaskActivityDtoList);
        }

        // Document delete method - Shakti
        [Authorize(Roles = "Admin, TenantAdmin, TenantManager, TenantUser, ExternalUser")]
        [HttpDelete("{Id}")]
        public async Task<OperationResult<EmployeeTaskAttachmentDto>> RemoveDocument(long Id)
        {
            ClaimsPrincipal user = this.User as ClaimsPrincipal;
            UserId = Convert.ToInt32(user.FindFirst(JwtRegisteredClaimNames.Sid).Value);

            var document = _employeeTaskAttachmentService.DeleteEmployeeTaskAttachmentById(Id);

            await _hubContext.Clients.All.OnUploadEmployeeTaskDocumentEventEmit(document.EmployeeTaskId);

            // Remove task documents from folder
            if (document != null)
            {
                //var dirPath = _hostingEnvironment.WebRootPath + "\\EmployeeTaskUpload";
                var dirPath = _hostingEnvironment.WebRootPath + OneClappContext.EmployeeTaskUploadDirPath;
                var filePath = dirPath + "\\" + document.Name;

                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(Path.Combine(filePath));
                }
                EmployeeTaskActivity employeeTaskActivityObj = new EmployeeTaskActivity();
                employeeTaskActivityObj.EmployeeTaskId = document.EmployeeTaskId;
                employeeTaskActivityObj.UserId = UserId;
                employeeTaskActivityObj.Activity = "Removed an attachment";
                var AddUpdate = await _employeeTaskActivityService.CheckInsertOrUpdate(employeeTaskActivityObj);

                return new OperationResult<EmployeeTaskAttachmentDto>(true, System.Net.HttpStatusCode.OK, "Doccument removed successfully");
            }
            else
            {
                return new OperationResult<EmployeeTaskAttachmentDto>(false, System.Net.HttpStatusCode.OK, "Doccument not found");
            }

        }

        // Assigned task user delete method - Shakti
        [Authorize(Roles = "Admin, TenantAdmin, TenantManager, TenantUser, ExternalUser")]
        [HttpDelete("{TaskAssignUserId}")]
        public async Task<OperationResult<EmployeeTaskUserDto>> AssignUser(long TaskAssignUserId)
        {
            ClaimsPrincipal user = this.User as ClaimsPrincipal;
            UserId = Convert.ToInt32(user.FindFirst(JwtRegisteredClaimNames.Sid).Value);
            TenantId = Convert.ToInt32(user.FindFirst("TenantId").Value);

            var assignUserRemove = await _employeeTaskUserService.DeleteAssignedEmployeeTaskUser(TaskAssignUserId);

            if (assignUserRemove != null)
            {

                EmployeeTaskActivity employeeTaskActivityObj = new EmployeeTaskActivity();
                employeeTaskActivityObj.EmployeeTaskId = assignUserRemove.EmployeeTaskId;
                employeeTaskActivityObj.UserId = UserId;
                employeeTaskActivityObj.Activity = "Removed assign user";
                var AddUpdate = await _employeeTaskActivityService.CheckInsertOrUpdate(employeeTaskActivityObj);
                if (assignUserRemove.UserId != null)
                {
                    var removeTaskUserDetails = _userService.GetUserById(assignUserRemove.UserId.Value);
                    if (assignUserRemove.EmployeeTaskId != null)
                    {
                        var taskUserdetails = _employeeTaskService.GetTaskById(assignUserRemove.EmployeeTaskId.Value);
                        if (removeTaskUserDetails != null && taskUserdetails != null)
                        {
                            await sendEmail.SendEmailRemoveEmployeeTaskUserAssignNotification(removeTaskUserDetails.Email, removeTaskUserDetails.FirstName + ' ' + removeTaskUserDetails.LastName, taskUserdetails.Description, TenantId);
                            return new OperationResult<EmployeeTaskUserDto>(true, System.Net.HttpStatusCode.OK, "User removed successfully from task.");
                        }
                        else
                        {
                            return new OperationResult<EmployeeTaskUserDto>(false, System.Net.HttpStatusCode.OK, "Something went wrong");
                        }
                    }
                }
            }

            return new OperationResult<EmployeeTaskUserDto>(false, System.Net.HttpStatusCode.OK, "Remove user not found");
        }

        [Authorize(Roles = "Admin, TenantAdmin, TenantManager, TenantUser, ExternalUser")]
        [HttpPut]
        public async Task<OperationResult<UpdateEmployeeTaskPriorityRequest>> Priority([FromBody] UpdateEmployeeTaskPriorityRequest model)
        {
            ClaimsPrincipal user = this.User as ClaimsPrincipal;
            UserId = Convert.ToInt32(user.FindFirst(JwtRegisteredClaimNames.Sid).Value);
            TenantId = Convert.ToInt32(user.FindFirst("TenantId").Value);
            EmployeeTaskDto employeeTaskDto = new EmployeeTaskDto();
            EmployeeTaskActivity employeeTaskActivityObj = new EmployeeTaskActivity();
            long? ProjectId = null;
            if (model.Id != null)
            {
                // start logic for Update Current task with priority
                var taskObj = _employeeTaskService.GetTaskById(model.Id.Value);
                ProjectId = taskObj.ProjectId;
                taskObj.Priority = model.CurrentPriority;
                taskObj.UpdatedBy = UserId;
                if (model.CurrentSectionId != model.PreviousSectionId)
                {
                    taskObj.SectionId = model.CurrentSectionId;
                    employeeTaskActivityObj.Activity = "Project changed and priority set to this task.";
                }
                else
                {
                    employeeTaskActivityObj.Activity = "Priority changed for this task. ";
                }
                var taskAddUpdate = await _employeeTaskService.UpdateTask(taskObj, taskObj.Id);

                employeeTaskActivityObj.EmployeeTaskId = model.Id;
                employeeTaskActivityObj.UserId = UserId;

                var AddUpdate = await _employeeTaskActivityService.CheckInsertOrUpdate(employeeTaskActivityObj);
                // End Logic

                // Start logic for without section task move to section Or task with section move in to with out section tasks
                if (model.IsSectionChange == true)
                {
                    var CurrentSectionId = model.CurrentSectionId;
                    var PreviousSectionId = model.PreviousSectionId;

                    if (PreviousSectionId > 0 && model.PreviousPriority >= 0)
                    {
                        List<EmployeeTask> taskList = new List<EmployeeTask>();
                        if (ProjectId != null)
                        {
                            taskList = _employeeTaskService.GetAllTaskByProjectId(ProjectId.Value);
                        }

                        if (taskList != null)
                        {
                            var tasks = taskList.Where(t => t.Priority > model.PreviousPriority && t.Id != model.Id).ToList();
                            if (tasks != null && tasks.Count() > 0)
                            {
                                foreach (var item in tasks)
                                {
                                    item.Priority = item.Priority - 1;
                                    await _employeeTaskService.UpdateTask(item, item.Id);
                                }
                            }
                        }
                    }

                    // logic for move task in without sections 
                    if (PreviousSectionId == null && CurrentSectionId != null)
                    {
                        List<EmployeeTask> TaskList = new List<EmployeeTask>();
                        if (ProjectId != null)
                        {
                            TaskList = _employeeTaskService.GetAllTaskByTenantWithOutSection(TenantId, ProjectId.Value);
                        }
                        if (TaskList != null)
                        {
                            var tasks = TaskList.Where(t => t.Priority > model.PreviousPriority && t.Id != model.Id).ToList();
                            if (tasks != null)
                            {
                                foreach (var item in tasks)
                                {
                                    item.Priority = item.Priority - 1;
                                    await _employeeTaskService.UpdateTask(item, item.Id);
                                }
                            }
                        }
                    }
                    // end

                    // start logic for one section task move to other section
                    if (CurrentSectionId == null && PreviousSectionId != null)
                    {
                        var taskList = _employeeTaskService.GetAllTaskByTenantWithOutSection(TenantId, ProjectId.Value);
                        if (taskList != null)
                        {
                            var tasks = taskList.Where(t => t.Priority >= model.CurrentPriority && t.Id != model.Id).ToList();
                            if (tasks != null && tasks.Count() > 0)
                            {
                                foreach (var item in tasks)
                                {
                                    item.Priority = item.Priority + 1;
                                    await _employeeTaskService.UpdateTask(item, item.Id);
                                }
                            }
                        }
                    }

                    if (CurrentSectionId > 0 && model.CurrentPriority >= 0)
                    {
                        List<EmployeeTask> taskList = new List<EmployeeTask>();
                        if (ProjectId != null)
                        {
                            taskList = _employeeTaskService.GetAllTaskByProjectId(ProjectId.Value);
                        }
                        if (taskList != null)
                        {
                            var tasks = taskList.Where(t => t.Priority >= model.CurrentPriority && t.Id != model.Id).ToList();
                            if (tasks != null && tasks.Count() > 0)
                            {
                                foreach (var item in tasks)
                                {
                                    item.Priority = item.Priority + 1;
                                    await _employeeTaskService.UpdateTask(item, item.Id);
                                }
                            }
                        }
                    }
                    // end
                }
                else if (model.PreviousSectionId != null && model.CurrentSectionId != null)
                {
                    // start logic for task move in one section
                    if (model.CurrentSectionId != null && (model.CurrentSectionId == model.PreviousSectionId))
                    {
                        var taskList = _employeeTaskService.GetAllTaskBySection(model.CurrentSectionId.Value);

                        if (model.CurrentPriority < taskList.Count())
                        {
                            if (model.CurrentPriority != model.PreviousPriority)
                            {
                                if (model.PreviousPriority < model.CurrentPriority)
                                {
                                    if (taskList != null)
                                    {
                                        var tasks = taskList.Where(t => t.Priority > model.PreviousPriority && t.Priority <= model.CurrentPriority && t.Id != model.Id).ToList();
                                        if (tasks != null && tasks.Count() > 0)
                                        {
                                            foreach (var item in tasks)
                                            {
                                                item.Priority = item.Priority - 1;
                                                await _employeeTaskService.UpdateTask(item, item.Id);
                                            }
                                        }
                                    }
                                }
                                else if (model.PreviousPriority > model.CurrentPriority)
                                {
                                    if (taskList != null)
                                    {
                                        var tasks = taskList.Where(t => t.Priority < model.PreviousPriority && t.Priority >= model.CurrentPriority && t.Id != model.Id).ToList();
                                        if (tasks != null && tasks.Count() > 0)
                                        {
                                            foreach (var item in tasks)
                                            {
                                                item.Priority = item.Priority + 1;
                                                await _employeeTaskService.UpdateTask(item, item.Id);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    // start logic for task move in with out section list
                    List<EmployeeTask> taskList = new List<EmployeeTask>();
                    if (ProjectId != null)
                    {
                        taskList = _employeeTaskService.GetAllTaskByTenantWithOutSection(TenantId, ProjectId.Value);
                    }


                    if (model.CurrentPriority < taskList.Count())
                    {
                        if (model.CurrentPriority != model.PreviousPriority)
                        {
                            if (model.PreviousPriority < model.CurrentPriority)
                            {
                                if (taskList != null)
                                {
                                    var tasks = taskList.Where(t => t.Priority > model.PreviousPriority && t.Priority <= model.CurrentPriority && t.Id != model.Id).ToList();
                                    if (tasks != null && tasks.Count() > 0)
                                    {
                                        foreach (var item in tasks)
                                        {
                                            item.Priority = item.Priority - 1;
                                            await _employeeTaskService.UpdateTask(item, item.Id);
                                        }
                                    }
                                }
                            }
                            else if (model.PreviousPriority > model.CurrentPriority)
                            {
                                if (taskList != null)
                                {
                                    var tasks = taskList.Where(t => t.Priority < model.PreviousPriority && t.Priority >= model.CurrentPriority && t.Id != model.Id).ToList();
                                    if (tasks != null && tasks.Count() > 0)
                                    {
                                        foreach (var item in tasks)
                                        {
                                            item.Priority = item.Priority + 1;
                                            await _employeeTaskService.UpdateTask(item, item.Id);
                                        }
                                    }
                                }
                            }
                        }
                    }
                    // end
                }
            }
            return new OperationResult<UpdateEmployeeTaskPriorityRequest>(true, System.Net.HttpStatusCode.OK, "", model);
        }

    }
}