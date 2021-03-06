using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Quartz;
using VMCloud.Models;
using VMCloud.Models.DAO;

namespace VMCloud.Utils
{
    public class CronUtil : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            CheckVirtualMachineDue();
            //CheckAssignmentDue();
        }

        public void CheckVirtualMachineDue()
        {
            try
            {
                List<VirtualMachine> vms = VMDao.GetVMNearDue();
                List<string> owned = new List<string>();
                Dictionary<string, List<string>> ownedVM = new Dictionary<string, List<string>>();
                foreach (var vm in vms)
                {
                    if (!ownedVM.ContainsKey(vm.owner_id))
                    {
                        ownedVM.Add(vm.owner_id, new List<string>());
                    }
                    ownedVM[vm.owner_id].Add(vm.vm_name);
                    VMDao.AddWarnTimes(vm.vm_name);
                }

                if (ownedVM.Count > 0)
                {
                    List<System_log> logs = new List<System_log>();
                    foreach (var kv in ownedVM)
                    {
                        string ownedVMs = string.Join(",", kv.Value.ToArray());
                        EmailUtil.SendEmail("您的虚拟机已到期，请及时处理",
                            "您的虚拟机" + ownedVMs + "等已达到计划使用期限，请及时登录云平台进行删除。",
                            kv.Key);
                        var log = new System_log
                        {
                            time = DateTime.Now.ToString(),
                            content = "发送虚拟机到期提醒",
                            operate_target_id = kv.Key,
                            operate_target_type = "虚拟机到期提醒",
                            operator_id = "system",
                            complete_time = DateTime.Now.ToString(),
                        };
                        logs.Add(log);
                    }

                    using (var logger = new Logger())
                    {
                        logger.Logs.AddRange(logs);
                    }

                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.StackTrace);
            }
        }
        /*
        public void CheckAssignmentDue()
        {
            List<int> courseId = ExperimentDao.GetExperimentNearDueTime()
                .Where(e => e.course_id != null)
                .Select(e => (int)e.course_id).ToList();
            List<string> userId = new List<string>();
            foreach(int course in courseId)
            {
                List<string> stu = CourseDao.GetStudentsById(course).Select(u => u.id).ToList();
                userId.AddRange(stu);
                userId = userId.Distinct().ToList() ;
            }

            System.Diagnostics.Debug.WriteLine(courseId.Count);
            foreach (string id in userId)
            {
                System.Diagnostics.Debug.WriteLine(id);
                   //EmailUtil.SendEmail("有作业即将到期", "有作业即将到期，请登录云平台查看（若已经提交，请忽略本通知）。", id);
            }
        }
        */
    }
}