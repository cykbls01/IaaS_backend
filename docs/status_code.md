# 涉及到的状态/类型代码

## 状态/类型码

---

### 角色代码 (user表中的role)

role|含义
-|-
1|学生
2|教师
3|部门管理员
4|管理员

### 申请记录type (apply_record表中的operate_type)

type|含义
-|-
1|创建
2|批量创建
3|修改
-1|删除
-2|批量删除

### 申请记录status (apply_record表中的status)

status|含义
-|-
-1|已撤销
0|未处理
1|已拒绝
2|已通过

### 实验虚拟机status (experiment表中的vm_status)

status|含义
-|-
-2|创建失败
-1|已删除
0|无
1|创建中
2|已创建

### 互评申诉status (peer_assessment表中的appeal_status)

status|含义
-|-
0|无
1|待处理
2|已处理

### 标准作业status（assignment表中的is_standard）

 status|含义
 -|-
 0|不是标准作业
 1|已抽中待挑选的作业
 2 标准作业
 
 ### 虚拟机模板状态 （virtual_machine表中的image）
 status|含义
 -|-
 0|非模板或VMware模板
 1|aCloud镜像(image)
 2|aCloud模板(虚拟机，但标记为模板，可用来创建新虚拟机)
